using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Configuration;

namespace Pillar.Common.Tests.Configuration
{
    /// <summary>
    ///This is a test class for AppSettingsConfigurationTest and is intended
    ///to contain all AppSettingsConfigurationTest Unit Tests
    ///</summary>
    [TestClass ()]
    public class AppSettingsConfigurationTest
    {
        /// <summary>
        /// This test assumes that the App.config file exists in this Test Project.
        /// </summary>
        [TestMethod]
        public void GetProperty_GettingAssignedValue_ReturnsExpectedValue ()
        {
            const string testSettingKey = "TestSettingKey";

            IConfigurationPropertiesProvider config = new AppSettingsConfiguration ();
            config.SetProperty(testSettingKey, "TEST");

            Assert.AreEqual("TEST", config.GetProperty(testSettingKey));
        }

        [TestMethod]
        public void GetProperty_GettingUnAssignedValue_ReturnsNull ()
        {
            IConfigurationPropertiesProvider config = new AppSettingsConfiguration ();
            Assert.AreEqual ( null, config.GetProperty ( Guid.NewGuid ().ToString () ) );
        }
    }
}
