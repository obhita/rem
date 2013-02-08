using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Domain;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Core.SecurityModule;
using Rem.Infrastructure.Bootstrapper;

namespace Rem.Domain.GlobalTests
{
    [TestClass]
    public class GlobalDomainTests
    {
        private IEnumerable<Assembly> GetRemDomainAssemblyList ()
        {
            AssemblyLocator assemblyLocator = new AssemblyLocator();
            return assemblyLocator.LocateDomainAssemblies ();
        }

        //  This test ensures that any Entity in our Domain is either
        //  an Aggregate Node or Aggregate Root
        [TestMethod]
        public void AllEntities_AreAggregateNodesOrRoots_Succeeds ()
        {
            var reportBuilder = new StringBuilder ();
            var failedTypeCount = 0;

            foreach (var domainAssembly in GetRemDomainAssemblyList())
            {
                var failedTypes =
                domainAssembly.GetTypes()
                    .Where(p => !p.IsAbstract && typeof(IEntity).IsAssignableFrom(p)
                                  && !(typeof(IAggregateNode).IsAssignableFrom(p) || typeof(IAggregateRoot).IsAssignableFrom(p))).ToList();

                var message = string.Format("The following types are not an IAggregateNode or IAggregateRoot:{0}",
                                              string.Join(Environment.NewLine, failedTypes));

                failedTypeCount = failedTypeCount + failedTypes.Count ();
                reportBuilder.AppendLine ( message );
            }

            Assert.AreEqual(0, failedTypeCount, reportBuilder.ToString().Trim());
        }

        [TestMethod]
        public void Entity_AllInstanceMethodsAndPropertiesMustBeVirtual_Succeeds ()
        {
            var reportBuilder = new StringBuilder();

            foreach (var domainAssembly in GetRemDomainAssemblyList())
            {
                var singleReportBuilder = new StringBuilder();

                var typeList =
                    domainAssembly.GetTypes ().Where ( t => !t.IsAbstract && typeof( Entity ).IsAssignableFrom ( t ) ).OrderBy ( p => p.FullName );

                foreach (var t in typeList)
                {
                    var nonVirtualMethods = t.GetMethods(BindingFlags.Instance).Where(m => !m.IsVirtual && m.DeclaringType.ToString() == t.FullName);

                    foreach (var nonVirtualMethod in nonVirtualMethods)
                    {
                        singleReportBuilder.AppendLine(String.Format("The Entity {0}.{1} Method is not marked as virtual", t.FullName, nonVirtualMethod.Name));
                    }

                    var nonVirtualProperties = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(m => !m.GetGetMethod().IsVirtual && m.DeclaringType.ToString() == t.FullName);

                    foreach (var nonVirtualProperty in nonVirtualProperties)
                    {
                        singleReportBuilder.AppendLine(String.Format("The Entity {0}.{1} property is not marked as virtual", t.FullName, nonVirtualProperty.Name));
                    }
                }

                reportBuilder.AppendLine ( singleReportBuilder.ToString ().Trim() );
            }

            Assert.IsTrue(reportBuilder.ToString().Trim().Length == 0, reportBuilder.ToString().Trim());
        }

        [TestMethod]
        public void Entity_ShouldHaveDefaultConstructor_Succeeds ()
        {
            var reportBuilder = new StringBuilder();

            foreach (var domainAssembly in GetRemDomainAssemblyList())
            {
                var singleReportBuilder = new StringBuilder();

                IEnumerable<Type> typeList = domainAssembly.GetTypes().Where(t => !t.IsAbstract && typeof(Entity).IsAssignableFrom(t)).
                        OrderBy(p => p.FullName);

                foreach (var t in typeList)
                {
                    var defaultConstructor = t.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
                    if (defaultConstructor == null)
                    {
                        singleReportBuilder.AppendLine(String.Format("The Entity {0} should have default constructor", t.FullName));
                    }
                }

                reportBuilder.AppendLine ( singleReportBuilder.ToString () );
            }

            Assert.IsTrue(reportBuilder.ToString().Trim().Length == 0, reportBuilder.ToString().Trim());
        }

        [TestMethod]
        public void AggregateRoot_MustNotHavePublicConstructors_Succeeds ()
        {
            var reportBuilder = new StringBuilder();

            foreach (var domainAssembly in GetRemDomainAssemblyList())
            {
                var singleReport = new StringBuilder();

                var typeList = domainAssembly.GetTypes()
                    .Where(t => t.BaseType == typeof(AbstractAggregateRoot) && !t.IsAbstract).OrderBy(p => p.FullName);

                foreach (var t in typeList)
                {
                    var publicCtors = t.GetConstructors().Where(c => c.IsPublic);
                    if (publicCtors.Count() > 0)
                    {
                        singleReport.AppendLine(String.Format("The AggregateRoot {0} has public {1} Constructor(s)", t.FullName, publicCtors.Count()));
                    }
                }

                reportBuilder.AppendLine ( singleReport.ToString ());
            }

            Assert.IsTrue(reportBuilder.ToString().Trim().Length == 0, reportBuilder.ToString().Trim());
        }

        [TestMethod]
        public void AggregateNodesThatAreNotValueObjects_MustNotHavePublicConstructors_Succeeds ()
        {
            var reportBuilder = new StringBuilder();

            foreach (var domainAssembly in GetRemDomainAssemblyList())
            {
                var singleReportBuilder = new StringBuilder();

                var typeList = domainAssembly.GetTypes()
                    .Where(t =>
                             typeof(IAggregateNode).IsAssignableFrom(t) &&
                             !typeof(IAggregateNodeValueObject).IsAssignableFrom(t) &&
                             !t.IsAbstract).OrderBy(p => p.FullName);

                foreach (var t in typeList)
                {
                    var publicCtors = t.GetConstructors().Where(c => c.IsPublic).ToList();
                    if (publicCtors.Count() > 0)
                    {
                        singleReportBuilder.AppendLine(String.Format("The AggregateNode {0} has public {1} Constructor(s)", t.FullName, publicCtors.Count()));
                    }
                }

                reportBuilder.AppendLine ( singleReportBuilder.ToString () );
            }

            Assert.IsTrue(reportBuilder.ToString().Trim().Length == 0, reportBuilder.ToString().Trim());
        }

        [TestMethod]
        public void ComponentClass_MustHaveParameterlessConstructorToSatisfyNHibernateAndShouldBePrivateToo()
        {
            var reportBuilder = new StringBuilder();

            foreach (var domainAssembly in GetRemDomainAssemblyList())
            {
                var singleReportBuilder = new StringBuilder();

                var typeList = typeof(DateRange).Assembly.GetTypes()
                    .Where(t => IsComponent(t) && t.IsClass).ToList();

                var domaintypeList = domainAssembly.GetTypes()
                    .Where(t => IsComponent(t) && t.IsClass);

                typeList.AddRange(domaintypeList);

                foreach (var t in typeList)
                {
                    var parameterlessConstructor = t.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null);

                    if (parameterlessConstructor == null)
                    {
                        singleReportBuilder.AppendLine(String.Format("The Component {0} is required to have parameterless constructor.", t.FullName));
                    }
                    else if (!parameterlessConstructor.IsPrivate)
                    {
                        singleReportBuilder.AppendLine(String.Format("The parameterless constructor of Component {0} should be private.", t.FullName));
                    }
                }

                reportBuilder.AppendLine ( singleReportBuilder.ToString () );
            }

            Assert.IsTrue(reportBuilder.ToString().Trim().Length == 0, reportBuilder.ToString().Trim());
        }

        [TestMethod]
        public void Auditable_AllEntitiesExceptSystemAccountMustBeAuditable_Succeeds ()
        {
            var reportBuilder = new StringBuilder();

            foreach (var domainAssembly in GetRemDomainAssemblyList())
            {
                var singleReportBuilder = new StringBuilder();

                var typeList = domainAssembly.GetTypes()
                    .Where(t => typeof(Entity).IsAssignableFrom(t) && !t.IsAbstract);

                foreach (var t in typeList)
                {
                    if (typeof(SystemAccount).IsAssignableFrom(t))
                    {
                        continue;
                    }

                    if (!typeof(IAuditable).IsAssignableFrom(t))
                    {
                        singleReportBuilder.AppendLine(String.Format(
                            "The Entity {0} does not implement IAuditable", t.FullName));
                    }
                }

                reportBuilder.AppendLine ( singleReportBuilder.ToString () );
            }

            Assert.IsTrue(reportBuilder.ToString().Trim().Length == 0, reportBuilder.ToString().Trim());
        }

        private static bool IsComponent(Type type)
        {
            var nhibernateComponentAttributes = type.GetCustomAttributes(typeof(ComponentAttribute), false);
            var result = nhibernateComponentAttributes.Length == 1;
            return result;
        }
    }
}