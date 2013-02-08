using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Castle.DynamicProxy;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Commands;

namespace Rem.Ria.Infrastructure.Tests.ViewModel
{
    [TestClass]
    public class ViewModelSecureCommandGovernanceTests
    {
        [TestMethod]
        [Ignore]
        //TODO: fix this unit test.
        public void VerifyAllViewModelCommandsAreIntercepted()
        {
            List<Assembly> assemblies = Deployment.Current.Parts.Select(ap =>
                Application.GetResourceStream(new Uri(ap.Source, UriKind.Relative))).Select(sri => new AssemblyPart().Load(sri.Stream)).ToList();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes ();
                foreach ( var type in types )
                {
                    if (type.IsClass && !type.IsAbstract && type.FullName.StartsWith("Rem") && !type.FullName.Contains(".Tests.") && type.Name.EndsWith("ViewModel"))
                    {
                        CheckViewModel ( type );
                    }
                }
            }
        }

        private class TestCommandFactory : ICommandFactory
        {
            public TCommand Build<TCommand, TOwner> ( TOwner owner, Expression<Func<ICommand>> propertyExpression, Action executeMethod, Func<bool> canExecuteMethod = null ) where TCommand : class, ICommand
            {
                var gen = new ProxyGenerator();
                return gen.CreateInterfaceProxyWithTarget(
                    typeof(ICommand), Activator.CreateInstance(typeof(TCommand), executeMethod)) as TCommand;
            }

            public TCommand Build<TCommand, TOwner, TParameter> ( TOwner owner, Expression<Func<ICommand>> propertyExpression, Action<TParameter> executeMethod, Func<TParameter, bool> canExecuteMethod = null ) where TCommand : class, ICommand
            {
                var gen = new ProxyGenerator();
                return gen.CreateInterfaceProxyWithTarget(
                    typeof(ICommand), Activator.CreateInstance(typeof(TCommand), executeMethod)) as TCommand;
            }

            public TCommand Build<TCommand, TOwner> ( IFrameworkCommandInfo frameworkCommandInfo, TCommand commandInstance ) where TCommand : class, ICommand
            {
                var gen = new ProxyGenerator();
                return gen.CreateInterfaceProxyWithTarget(
                    typeof(ICommand), commandInstance) as TCommand;
            }
        }

        private void CheckViewModel ( Type type )
        {
            var propertyInfos = type.GetProperties ();
            var propertyTypes = propertyInfos.Select(p => p.PropertyType).ToList ();
            IEnumerable<Type> constructoryParamTypes = null;
            var constructorMethods = type.GetConstructors ();
            ConstructorInfo constructor = null;
            if(constructorMethods.Count() == 1)
            {
                constructor = constructorMethods[ 0 ];
            }
            else
            {
                foreach ( var constructorMethod in constructorMethods )
                {
                    var attribute = constructorMethod.GetCustomAttributes ( typeof ( InjectionConstructorAttribute ), false );
                    if(attribute.Count() > 0)
                    {
                        constructor = constructorMethod;
                        break;
                    }
                }
                if (constructor == null)
                {
                    constructor = constructorMethods[ 0 ];
                }
            }
            constructoryParamTypes = constructor.GetParameters().Select(p => p.ParameterType);
            var constructorParams = new List<object> ();
            foreach ( var constructoryParamType in constructoryParamTypes )
            {
                if (constructoryParamType == typeof(ICommandFactory))
                {
                    constructorParams.Add(new TestCommandFactory());
                }
                else
                {
                    var mock = Activator.CreateInstance ( typeof( Mock<> ).MakeGenericType ( constructoryParamType ) );
                    constructorParams.Add ( mock.GetType ().GetProperty ( "Object", constructoryParamType ).GetValue ( mock, null ) );
                }
            }
            var viewModel = constructor.Invoke ( constructorParams.ToArray () );
            foreach ( var propertyType in propertyTypes )
            {
                if (propertyType == typeof(ICommand))
                {
                    var propertyInfo = propertyInfos[ propertyTypes.IndexOf ( propertyType ) ];
                    var propertyValue = propertyInfo.GetValue ( viewModel, null );
                    var valueType = propertyValue.GetType ();
                    if(!(valueType is IProxyTargetAccessor))
                    {
                        throw new InvalidOperationException(viewModel.GetType().FullName + ": Any command of a view model must be constructed using the CommandFactory.");
                    }
                }
            }
        }
    }
}
