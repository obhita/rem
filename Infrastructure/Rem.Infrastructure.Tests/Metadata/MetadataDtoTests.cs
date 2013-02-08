using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pillar.Common.Metadata.Dtos;
using Rem.Infrastructure.Metadata;

namespace Rem.Infrastructure.Tests.Metadata
{
    [TestClass]
    public class MetadataDtoTests
    {
        [TestMethod]
        public void MetadataDto_UsingDataContractSerializerToSerialize_Succeed()
        {
            var metadataDto = CreateMetadataDtoWithChildrenAndItems ();
            var knownTypes = new List<Type>
                                {
                                    typeof(HiddenMetadataItemDto),
                                    typeof(ReadonlyMetadataItemDto),
                                    typeof(RequiredMetadataItemDto),
                                    typeof(DisplayNameMetadataItemDto)
                                };

            var text = ContractObjectToXml ( metadataDto, knownTypes );
        }

        [TestMethod]
        public void MetadataDto_UsingDataContractSerializerToDeserialize_Succeed()
        {
            var metadataDto = CreateMetadataDtoWithChildrenAndItems();
            var knownTypes = new List<Type>
                                {
                                    typeof(HiddenMetadataItemDto),
                                    typeof(ReadonlyMetadataItemDto),
                                    typeof(RequiredMetadataItemDto),
                                    typeof(DisplayNameMetadataItemDto)
                                };
            var text = ContractObjectToXml(metadataDto, knownTypes);

            MetadataDto resultMetadataDto = XmlToContractObject<MetadataDto> ( text, knownTypes );
            
            Assert.IsNotNull ( resultMetadataDto );
        }

        [TestMethod]
        public void MetadataDto_UsingDataContractSerializerToDeserialize_ChildrenHasValues()
        {
            var metadataDto = CreateMetadataDtoWithChildrenAndItems();
            var knownTypes = new List<Type>
                                {
                                    typeof(HiddenMetadataItemDto),
                                    typeof(ReadonlyMetadataItemDto),
                                    typeof(RequiredMetadataItemDto),
                                    typeof(DisplayNameMetadataItemDto)
                                };
            var text = ContractObjectToXml(metadataDto, knownTypes);

            MetadataDto resultMetadataDto = XmlToContractObject<MetadataDto>(text, knownTypes);

            Assert.AreEqual ( 1, resultMetadataDto.Children.Count );
        }

        [TestMethod]
        public void MetadataDto_UsingDataContractSerializerToDeserialize_MetadataItemDtosHasValues()
        {
            var metadataDto = CreateMetadataDtoWithChildrenAndItems();
            var knownTypes = new List<Type>
                                {
                                    typeof(HiddenMetadataItemDto),
                                    typeof(ReadonlyMetadataItemDto),
                                    typeof(RequiredMetadataItemDto),
                                    typeof(DisplayNameMetadataItemDto)
                                };
            var text = ContractObjectToXml(metadataDto, knownTypes);

            MetadataDto resultMetadataDto = XmlToContractObject<MetadataDto>(text, knownTypes);

            Assert.AreEqual ( 2, resultMetadataDto.MetadataItemDtos.Count );
        }

        public static string ContractObjectToXml<T>(T obj, IEnumerable<Type> knownTypes)
        {
            var dataContractSerializer = new DataContractSerializer(obj.GetType(), knownTypes);

            String text;
            using (var memoryStream = new MemoryStream())
            {
                dataContractSerializer.WriteObject(memoryStream, obj);
                byte[] data = new byte[memoryStream.Length];
                Array.Copy(memoryStream.GetBuffer(), data, data.Length);

                text = Encoding.UTF8.GetString(data);
            }
            return text;
        }

        public static T XmlToContractObject<T>(string data, IEnumerable<Type> knownTypes)
        {
            var dataContractSerializer = new DataContractSerializer(typeof(T), knownTypes);
            
            byte[] byteArray = Encoding.UTF8.GetBytes( data );
            MemoryStream stream = new MemoryStream ( byteArray );

            var obj = dataContractSerializer.ReadObject ( stream );

            return (T)obj;
        }

        private MetadataDto CreateMetadataDtoWithChildrenAndItems()
        {
            var metadataDto = new MetadataDto("Default");
            metadataDto.AddMetadataItem(new ReadonlyMetadataItemDto { IsReadonly = true });
            metadataDto.AddMetadataItem(new DisplayNameMetadataItemDto { Name = "Test" });
            var child = metadataDto.AddChildMetadata("Child");
            child.AddMetadataItem(new HiddenMetadataItemDto { IsHidden = true });
            child.AddMetadataItem ( new RequiredMetadataItemDto { IsRequired = true } );

            return metadataDto;
        }
    }
}
