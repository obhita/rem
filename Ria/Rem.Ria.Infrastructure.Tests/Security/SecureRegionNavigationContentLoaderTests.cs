using System;
using Microsoft.Practices.Prism.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.InversionOfControl;
using Pillar.Security.AccessControl;
using Rem.Ria.Infrastructure.Security;

namespace Rem.Ria.Infrastructure.Tests.Security
{
    [TestClass]
    public class SecureRegionNavigationContentLoaderTests
    {
        [TestMethod]
        public void LoadContent_HasPermission_ViewAddedToRegion()
        {
            var view = new object ();
            var mockServiceLocator = new Mock<IContainer> ();
            mockServiceLocator.Setup ( sl => sl.Resolve<object> ( It.IsAny<string> () ) ).Returns ( view );
            var mockAccessControlManager = new Mock<IAccessControlManager> ();
            mockAccessControlManager.Setup ( acm => acm.CanAccess ( It.IsAny<ResourceRequest> () ) ).Returns ( true );
            var region = new Region ();
            var secureRegionNavigationContentLoader = new SecureRegionNavigationContentLoader ( mockServiceLocator.Object, mockAccessControlManager.Object );
            secureRegionNavigationContentLoader.LoadContent ( region, new NavigationContext ( new Mock<IRegionNavigationService> ().Object, new Uri ( "http://testuri/", UriKind.Absolute ) ) );
            Assert.IsTrue ( region.Views.Contains ( view ) );
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LoadContent_NoPermission_InvalidOperationException()
        {
            var view = new object();
            var mockServiceLocator = new Mock<IContainer>();
            mockServiceLocator.Setup(sl => sl.Resolve<object>(It.IsAny<string>())).Returns(view);
            var mockAccessControlManager = new Mock<IAccessControlManager>();
            mockAccessControlManager.Setup(acm => acm.CanAccess(It.IsAny<ResourceRequest>())).Returns(false);
            var region = new Region();
            var secureRegionNavigationContentLoader = new SecureRegionNavigationContentLoader(mockServiceLocator.Object, mockAccessControlManager.Object);
            secureRegionNavigationContentLoader.LoadContent(region, new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("http://testuri/", UriKind.Absolute)));
        }

        [TestMethod]
        public void LoadContent_NoPermission_IfExceptionhandledViewNotAddedToRegion()
        {
            var view = new object();
            var mockServiceLocator = new Mock<IContainer>();
            mockServiceLocator.Setup(sl => sl.Resolve<object>(It.IsAny<string>())).Returns(view);
            var mockAccessControlManager = new Mock<IAccessControlManager>();
            mockAccessControlManager.Setup(acm => acm.CanAccess(It.IsAny<ResourceRequest>())).Returns(false);
            var region = new Region();
            var secureRegionNavigationContentLoader = new SecureRegionNavigationContentLoader(mockServiceLocator.Object, mockAccessControlManager.Object);
            try
            {

                secureRegionNavigationContentLoader.LoadContent(region, new NavigationContext(new Mock<IRegionNavigationService>().Object, new Uri("http://testuri/", UriKind.Absolute)));
            }
            catch ( Exception )
            {
                
            }
            Assert.IsFalse(region.Views.Contains(view));
        }
    }
}
