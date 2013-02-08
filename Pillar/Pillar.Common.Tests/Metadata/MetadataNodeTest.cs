using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Metadata;

namespace Pillar.Common.Tests.Metadata
{
    [TestClass]
    public class MetadataNodeTest
    {
        [TestMethod]
        public void ResourceName_Get_Succeed()
        {
            var metadataNode = new MetadataNode("Default");
            Assert.AreEqual("Default", metadataNode.ResourceName);
        }

        [TestMethod]
        public void AddChild_WithValidResourceName_CreatedAMetadataNode()
        {
            var metadataNode = new MetadataNode("Default");
            var child = metadataNode.AddChild("Child.Resource");

            Assert.IsNotNull(child);
            Assert.IsInstanceOfType(child, typeof(MetadataNode));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void AddChild_WithExistingResourceName_ThrowException()
        {
            var metadataNode = new MetadataNode("Default");
            var child = metadataNode.AddChild("Child.Resource");
            metadataNode.AddChild("Child.Resource");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddChild_WithNullResourceName_ThrowException()
        {
            var metadataNode = new MetadataNode("Default");
            metadataNode.AddChild(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddChild_WithEmptyResourceName_ThrowException()
        {
            var metadataNode = new MetadataNode("Default");
            metadataNode.AddChild("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddChild_WithWhiteSpacesResourceName_ThrowException()
        {
            var metadataNode = new MetadataNode("Default");
            metadataNode.AddChild("  ");
        }

        [TestMethod]
        public void AddChild_WithValidParameter_ChildrenPropertyHasOneItem()
        {
            var metadataNode = new MetadataNode("Default");
            metadataNode.AddChild("Child.Resource");

            Assert.AreEqual(1, metadataNode.Children.Count);
        }

        [TestMethod]
        public void AddChild_WithValidParameterTwice_ChildrenPropertyHasTwoItems()
        {
            var metadataNode = new MetadataNode("Default");
            metadataNode.AddChild("Child.Resource1");
            metadataNode.AddChild("Child.Resource2");

            Assert.AreEqual(2, metadataNode.Children.Count);
        }

        [TestMethod]
        public void HasChild_NoChildAdded_ReturnFalse()
        {
            var metadataNode = new MetadataNode("Default");
            Assert.IsFalse(metadataNode.HasChild("Child.Resource1"));
        }

        [TestMethod]
        public void HasChild_NoChildForSpecificResource_ReturnFalse()
        {
            var metadataNode = new MetadataNode("Default");
            metadataNode.AddChild("Child.Resource1");

            Assert.IsFalse(metadataNode.HasChild("Child.Resource2"));
        }

        [TestMethod]
        public void HasChild_SpecificResourceIsThere_ReturnTrue()
        {
            var metadataNode = new MetadataNode("Default");
            metadataNode.AddChild("Child.Resource1");
            metadataNode.AddChild("Child.Resource2");

            Assert.IsTrue(metadataNode.HasChild("Child.Resource2"));
        }

        [TestMethod]
        public void FindChildMetadata_NoChildAdded_ReturnNull()
        {
            var metadataNode = new MetadataNode("Default");
            Assert.IsNull(metadataNode.FindChildMetadata("Child.Resource1"));
        }

        [TestMethod]
        public void FindChildMetadata_NoChildForSpecificResource_ReturnNull()
        {
            var metadataNode = new MetadataNode("Default");
            metadataNode.AddChild("Child.Resource1");

            Assert.IsNull(metadataNode.FindChildMetadata("Child.Resource2"));
        }

        [TestMethod]
        public void FindChildMetadata_SpecificResourceIsThere_ReturnMetadataNode()
        {
            var metadataNode = new MetadataNode("Default");
            metadataNode.AddChild("Child.Resource1");
            metadataNode.AddChild("Child.Resource2");

            var result = metadataNode.FindChildMetadata("Child.Resource2");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MetadataNode));
        }

        [TestMethod]
        public void MetadataItems_Set_Succeed()
        {
            var metadataNode = new MetadataNode("Default");
            metadataNode.MetadataItems = new List<IMetadataItem> { new Mock<IMetadataItem>().Object };

            Assert.AreEqual(1, metadataNode.MetadataItems.Count);
        }

        [TestMethod]
        public void MetadataItems_Add_Succeed()
        {
            var metadataNode = new MetadataNode("Default");
            metadataNode.MetadataItems.Add(new Mock<IMetadataItem>().Object);

            Assert.AreEqual(1, metadataNode.MetadataItems.Count);
        }
    }
}
