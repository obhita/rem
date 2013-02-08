#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Metadata;
using Rem.Ria.Infrastructure.Service;

namespace Rem.Ria.Infrastructure.View.Configuration
{
    /// <summary>
    /// Class for behaviing configuration.
    /// </summary>
    internal class ConfigurationBehavior : Behavior<FrameworkElement>
    {
        #region Constants and Fields

        private static readonly DependencyProperty ChangeTrackingMetadataBindingProperty = DependencyProperty.Register (
            "ChangeTrackingMetadataBinding",
            typeof( object ),
            typeof( ConfigurationBehavior ),
            new PropertyMetadata ( null, ChangeTrackingMetadataBindingChanged ) );

        private static readonly DependencyProperty DataContextProxyProperty = DependencyProperty.Register (
            "DataContextProxy", typeof( object ), typeof( ConfigurationBehavior ), new PropertyMetadata ( DataContextProxyChanged ) );

        private static readonly DependencyProperty MetadataBindingProperty = DependencyProperty.Register (
            "MetadataBinding", typeof( object ), typeof( ConfigurationBehavior ), new PropertyMetadata ( null ) );

        private readonly IDictionary<Type, object> _localStorage = new Dictionary<Type, object> ();
        private readonly object _metadataSync = new object ();

        private IMetadataItemApplicatorService _metadataItemApplicatorService;
        private MetadataProxy _metadataProxy;
        private string _metadataToApply;

        #endregion

        #region Public Properties

        /// <summary>
        /// Value that is used to discover the IMetaDataProvider to be used by the Configuration.
        /// </summary>
        /// <value>The data context proxy.</value>
        public object DataContextProxy
        {
            get { return GetValue ( DataContextProxyProperty ); }
            set { SetValue ( DataContextProxyProperty, value ); }
        }

        /// <summary>
        /// String value indicating the Property of the IMetaDataProver used by the Confiuration.
        /// If null configuration will use the IMetaDataProvider itself to configure the control.
        /// </summary>
        /// <value>The metadata.</value>
        public string Metadata { private get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the change tracking metadata binding.
        /// </summary>
        /// <value>The change tracking metadata binding.</value>
        private object ChangeTrackingMetadataBinding
        {
            get { return GetValue ( ChangeTrackingMetadataBindingProperty ); }
            set { SetValue ( ChangeTrackingMetadataBindingProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the metadata binding.
        /// </summary>
        /// <value>The metadata binding.</value>
        private object MetadataBinding
        {
            get { return GetValue ( MetadataBindingProperty ); }
            set { SetValue ( MetadataBindingProperty, value ); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached ()
        {
            base.OnAttached ();

            var metaDataOveride = Configuration.GetMetaDataOveride ( AssociatedObject );
            if ( !string.IsNullOrWhiteSpace ( metaDataOveride ) )
            {
                Metadata = metaDataOveride;
            }

            if ( ReadLocalValue ( DataContextProxyProperty ) == DependencyProperty.UnsetValue )
            {
                var binding = new Binding ();
                binding.ValidatesOnNotifyDataErrors = false;
                BindingOperations.SetBinding ( this, DataContextProxyProperty, binding );
            }
            else if ( DataContextProxy != null )
            {
                HandleDataContextProxyChanged ();
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching ()
        {
            base.OnDetaching ();
            CleanUp ();
        }

        private static void ChangeTrackingMetadataBindingChanged ( DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs )
        {
            var configurationBehavior = dependencyObject as ConfigurationBehavior;
            if ( configurationBehavior != null )
            {
                if ( eventArgs.NewValue != null )
                {
                    configurationBehavior.HandleChangeTrackingMetaDataChanged ();
                }
            }
        }

        private static void DataContextProxyChanged ( DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs )
        {
            var configurationBehavior = dependencyObject as ConfigurationBehavior;
            if ( configurationBehavior != null )
            {
                if ( eventArgs.OldValue != null )
                {
                    configurationBehavior.CleanUp ();
                }
                if ( eventArgs.NewValue != null )
                {
                    configurationBehavior.HandleDataContextProxyChanged ();
                }
            }
        }

        private void AttachMetaData ( IMetadataProvider metadataProvider, string metadataToApply )
        {
            var metadataService = IoC.CurrentContainer.Resolve<IMetadataService>();

            _metadataProxy = string.IsNullOrWhiteSpace ( metadataToApply )
                                 ? new MetadataProxy ( metadataService, metadataProvider )
                                 : new MetadataProxy ( metadataService, metadataProvider, metadataToApply );

            _metadataProxy.MetadataChanged += MetadataProxyMetadataChanged;
            _metadataProxy.ReportInitialMetadata ();
        }

        private void CleanUp ()
        {
            if ( _metadataProxy != null )
            {
                _metadataProxy.MetadataChanged -= MetadataProxyMetadataChanged;
            }
        }

        private void HandleChangeTrackingMetaDataChanged ()
        {
            lock ( _metadataSync )
            {
                AttachMetaData ( ChangeTrackingMetadataBinding as IMetadataProvider, _metadataToApply );
            }
        }

        private void HandleDataContextProxyChanged ()
        {
            lock ( _metadataSync )
            {
                var workingMetadataChain = Metadata;
                _metadataToApply = string.Empty;
                var currentMetadataBinding = new Binding ();
                currentMetadataBinding.NotifyOnValidationError = false;
                currentMetadataBinding.Source = DataContextProxy;
                ClearValue ( MetadataBindingProperty );
                if ( !string.IsNullOrWhiteSpace ( workingMetadataChain ) )
                {
                    currentMetadataBinding.Path = new PropertyPath ( workingMetadataChain );
                }
                BindingOperations.SetBinding ( this, MetadataBindingProperty, currentMetadataBinding );

                if ( string.IsNullOrWhiteSpace ( workingMetadataChain ) && !( MetadataBinding is IMetadataProvider ) )
                {
                    return;
                }

                while ( !( MetadataBinding is IMetadataProvider ) )
                {
                    var metadataParts = workingMetadataChain.Split ( '.' );
                    _metadataToApply = metadataParts[metadataParts.Length - 1];
                    workingMetadataChain = metadataParts.Length > 1
                                               ? workingMetadataChain.Substring ( 0, workingMetadataChain.Length - ( _metadataToApply.Length + 1 ) )
                                               : workingMetadataChain.Substring ( 0, workingMetadataChain.Length - ( _metadataToApply.Length ) );

                    currentMetadataBinding = new Binding ();
                    currentMetadataBinding.ValidatesOnNotifyDataErrors = false;
                    currentMetadataBinding.Source = DataContextProxy;
                    currentMetadataBinding.Path = new PropertyPath ( workingMetadataChain );
                    BindingOperations.SetBinding ( this, MetadataBindingProperty, currentMetadataBinding );

                    if ( string.IsNullOrWhiteSpace ( workingMetadataChain ) && !( MetadataBinding is IMetadataProvider ) )
                    {
                        return;
                    }
                }

                //This will cause the HandleChangeTrackingMetadataBindingChanged method to be invoked.
                BindingOperations.SetBinding ( this, ChangeTrackingMetadataBindingProperty, currentMetadataBinding );
            }
        }

        private void MetadataProxyMetadataChanged ( object sender, MetadataChangedEventArgs eventArgs )
        {
            if ( _metadataItemApplicatorService == null )
            {
                _metadataItemApplicatorService = IoC.CurrentContainer.Resolve<IMetadataItemApplicatorService> ();
            }

            if ( AssociatedObject != null )
            {
                var applicator = _metadataItemApplicatorService.GetMetadataItemApplicator (
                    AssociatedObject, eventArgs.MetadataItemDto );
                if ( applicator != null )
                {
                    if ( eventArgs.MetadataAction == MetadataAction.Added )
                    {
                        applicator.Apply ( AssociatedObject, eventArgs.MetadataItemDto, _localStorage );
                    }
                    else
                    {
                        applicator.Unapply ( AssociatedObject, eventArgs.MetadataItemDto, _localStorage );
                    }
                }
            }
        }

        #endregion
    }
}
