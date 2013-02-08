using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Pillar.Common.Utility;
using Pillar.FluentRuleEngine.RuleSelectors;

namespace Pillar.FluentRuleEngine
{
    /// <summary>
    /// Class that monitors a logical tree of a subject and runs the rules effected by a property change or collection modification.
    /// </summary>
    /// <typeparam name="TSubject">The type of the subject.</typeparam>
    /// <typeparam name="TRecurse">The type of object to recurse.</typeparam>
    public class NotifyPropertyChangedRuleExecutor<TSubject, TRecurse> : IDisposable
        where TSubject : class
        where TRecurse : class
    {
        private readonly List<KeyValuePair<WeakReference, string>> _collectionPropertyNames = new List<KeyValuePair<WeakReference, string>> ();
        private readonly IRuleEngine<TSubject> _ruleEngine;
        private readonly List<string> _runAllRulesProperties = new List<string>();
        private readonly List<string> _ignoreProperties = new List<string>();
        private TSubject _subject;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyPropertyChangedRuleExecutor{TSubject,TRecurse}"/> class.
        /// </summary>
        public NotifyPropertyChangedRuleExecutor ()
        {
            _ruleEngine = new RuleEngineFactory ( new RuleCollectionFactory () ).CreateRuleEngine<TSubject> ();
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose ()
        {
            Dispose ( true );
        }

        #endregion

        /// <summary>
        /// Watches all objects(that implement the <see cref="INotifyPropertyChanged"/> or <see cref="INotifyCollectionChanged"/> interfaces) in the logical tree of the subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        public void WatchSubject(TSubject subject)
        {
            Check.IsNotNull ( subject, "subject is required." );

            CleanUpCollectionPropertyNames();

            if (_subject != null)
            {
                StopWatchingSubject ( _subject );
            }

            WatchObject(subject);

            _subject = subject;
        }

        /// <summary>
        /// Stops Watching all objects in the logical tree of the subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        public void StopWatchingSubject(TSubject subject)
        {
            CleanUpCollectionPropertyNames();

            StopWatchingObject(subject);

            _subject = null;
        }

        /// <summary>
        /// Adds a property to run all rules when changed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        public void AddRunAllRulesProperty<TProperty>(Expression<Func<TSubject, TProperty>> propertyExpression)
        {
            lock ( _ruleEngine )
            {
                _runAllRulesProperties.Add ( PropertyUtil.ExtractPropertyName ( propertyExpression ) );
            }
        }

        /// <summary>
        /// Ignores the property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        public void IgnoreProperty<TProperty>(Expression<Func<TSubject, TProperty>> propertyExpression)
        {
            lock (_ruleEngine)
            {
                _ignoreProperties.Add(PropertyUtil.ExtractPropertyName(propertyExpression));
            }
        }

        private void StopWatchingObject(object objToStopWatching)
        {
            LogicalTreeWalker.Walk<TRecurse>(
                objToStopWatching,
                item =>
                    {
                        if ( item is INotifyPropertyChanged )
                        {
                            ( item as INotifyPropertyChanged ).PropertyChanged -= PropertyChanged;
                        }
                        else if ( item is INotifyCollectionChanged )
                        {
                            ( item as INotifyCollectionChanged ).CollectionChanged -= CollectionChanged;

                            lock ( _collectionPropertyNames )
                            {
                                var itemToRemove =
                                    _collectionPropertyNames.FirstOrDefault ( kvp => kvp.Key.IsAlive && kvp.Key.Target == item );
                                _collectionPropertyNames.Remove ( itemToRemove );
                            }
                        }
                    } );
        }

        private void CheckListen(object item, object parent, PropertyInfo info)
        {
            if (item is INotifyPropertyChanged)
            {
                (item as INotifyPropertyChanged).PropertyChanged += PropertyChanged;
            }
            else if (item is INotifyCollectionChanged && info != null)
            {
                (item as INotifyCollectionChanged).CollectionChanged += CollectionChanged;

                lock (_collectionPropertyNames)
                {
                    _collectionPropertyNames.Add(new KeyValuePair<WeakReference, string>(new WeakReference(item), info.Name));
                }
            }
        }

        private void WatchObject ( object objToWatch )
        {
            CheckListen ( objToWatch, null, null );
            var logicalTreeWalker = new LogicalTreeWalker ();
            logicalTreeWalker.RegisterAction<TRecurse>( CheckListen );
            logicalTreeWalker.Walk<TRecurse>(objToWatch);
        }

        private void CleanUpCollectionPropertyNames ()
        {
            lock ( _collectionPropertyNames )
            {
                foreach ( var item in _collectionPropertyNames.Where ( kvp => !kvp.Key.IsAlive ) )
                {
                    _collectionPropertyNames.Remove ( item );
                }
            }
        }

        private void PropertyChanged ( object sender, PropertyChangedEventArgs e )
        {
            lock ( _ruleEngine )
            {
                if (!_ignoreProperties.Contains(e.PropertyName))
                {
                    if ( _runAllRulesProperties.Contains ( e.PropertyName ) )
                    {
                        _ruleEngine.ExecuteAllRules ( _subject );
                    }
                    else
                    {
                        _ruleEngine.ExecuteSelectedRules (
                            _subject, new PropertyChainContainsMemberRuleSelector<object> ( e.PropertyName ) );
                    }

                    var propertyInfo = sender.GetType ().GetProperty ( e.PropertyName );
                    if ( propertyInfo != null )
                    {
                        var propertyValue = propertyInfo.GetValue ( sender, null );
                        if ( propertyValue != null )
                        {
                            WatchObject ( propertyValue );
                        }
                    }
                }
            }
        }

        private void CollectionChanged ( object sender, NotifyCollectionChangedEventArgs e )
        {
            lock ( _ruleEngine )
            {
                CleanUpCollectionPropertyNames ();

                foreach ( var newItem in e.NewItems )
                {
                    var collectionPropertyNameItem = _collectionPropertyNames.FirstOrDefault (
                        kvp => kvp.Key.IsAlive && kvp.Key.Target == sender );
                    if ( collectionPropertyNameItem.Key != null )
                    {
                        _ruleEngine.ExecuteSelectedRules (
                            ( TSubject )sender, new PropertyChainContainsMemberRuleSelector<object> ( collectionPropertyNameItem.Value ) );
                    }

                    WatchObject ( newItem );
                }

                foreach ( var oldItem in e.OldItems )
                {
                    StopWatchingObject ( oldItem );
                }
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose ( bool disposing )
        {
            if (!_disposed )
            {
                if (disposing && _subject != null)
                {
                    StopWatchingSubject ( _subject );
                }

                _disposed = true;
            }
        }
    }
}
