using System.Collections.Generic;
using Pillar.Common.Metadata;

namespace Pillar.Common.Tests.Metadata
{
    internal class MetadataMergerTestFixture
    {
        public static IList<MetadataRoot> SingleMetadataInSameLayer_TestValue ()
        {
            var metadataRootList = new List<MetadataRoot>
                                       {
                                           new MetadataRoot ( "MyResource", 1 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                                           new RequiredMetadataItem { IsRequired = true }
                                                                       }
                                               }
                                       };

            return metadataRootList;
        }

        public static IMetadata SingleMetadataInSameLayer_ExpectedResult ()
        {
            var metadata = new MetadataNode ( "MyResource" )
                               {
                                   MetadataItems = new List<IMetadataItem>
                                                       {
                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                           new RequiredMetadataItem { IsRequired = true }
                                                       }
                               };

            return metadata;
        }

        public static IList<MetadataRoot> MultiMetadataInSameLayer_TestValue ()
        {
            var metadataRootList = new List<MetadataRoot>
                                       {
                                           new MetadataRoot ( "MyResource", 1 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                                           new RequiredMetadataItem { IsRequired = true }
                                                                       }
                                               },
                                           new MetadataRoot ( "MyResource", 1 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = false },
                                                                           new HiddenMetadataItem () { IsHidden = true }
                                                                       }
                                               }
                                       };
            return metadataRootList;
        }

        public static IMetadata MultiMetadataInSameLayer_ExpectedResult ()
        {
            var metadata = new MetadataNode ( "MyResource" )
                               {
                                   MetadataItems = new List<IMetadataItem>
                                                       {
                                                           new ReadonlyMetadataItem { IsReadonly = false },
                                                           new RequiredMetadataItem { IsRequired = true },
                                                           new HiddenMetadataItem () { IsHidden = true }
                                                       }
                               };

            return metadata;
        }

        public static IList<MetadataRoot> MultiMetadataInThreeLayers_TestValue ()
        {
            var metadataRootList = new List<MetadataRoot>
                                       {
                                           new MetadataRoot ( "MyResource", 1 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                                           new RequiredMetadataItem { IsRequired = true }
                                                                       }
                                               },
                                           new MetadataRoot ( "MyResource", 2 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = false },
                                                                           new HiddenMetadataItem () { IsHidden = true }
                                                                       }
                                               },
                                           new MetadataRoot ( "MyResource", 3 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                                           new HiddenMetadataItem () { IsHidden = false }
                                                                       }
                                               },
                                       };

            return metadataRootList;
        }

        public static IMetadata MultiMetadataInThreeLayers_ExpectedResult ()
        {
            var metadata = new MetadataNode ( "MyResource" )
                               {
                                   MetadataItems = new List<IMetadataItem>
                                                       {
                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                           new RequiredMetadataItem { IsRequired = true },
                                                           new HiddenMetadataItem { IsHidden = false },
                                                       }
                               };

            return metadata;
        }

        public static IList<MetadataRoot> MultiMetadataInThreeLayersWithOneLevelChildren_TestValue()
        {
            var metadataRootList = new List<MetadataRoot>
                                       {
                                           new MetadataRoot ( "MyResource", 1 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                                           new RequiredMetadataItem { IsRequired = true }
                                                                       }
                                               },
                                           new MetadataRoot ( "MyResource", 2 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = false },
                                                                           new HiddenMetadataItem { IsHidden = true }
                                                                       }
                                               },
                                           new MetadataRoot ( "MyResource", 3 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                                           new HiddenMetadataItem { IsHidden = false }
                                                                       }
                                               },
                                       };

            var child11 = metadataRootList[ 0 ].AddChild ( "FirstName" );
            child11.MetadataItems = new List<IMetadataItem>
                                        {
                                            new ReadonlyMetadataItem { IsReadonly = true },
                                            new DisplayNameMetadataItem { Name = "Child-Default" }
                                        };
            var child21 = metadataRootList[ 1 ].AddChild ( "FirstName" );
            child21.MetadataItems = new List<IMetadataItem>
                                        {
                                            new DisplayNameMetadataItem { Name = "Child-MD Default" }
                                        };
            var child22 = metadataRootList[ 1 ].AddChild ( "Age" );
            child22.MetadataItems = new List<IMetadataItem>
                                        {
                                            new HiddenMetadataItem { IsHidden = true }
                                        };
            var child31 = metadataRootList[ 2 ].AddChild ( "FirstName" );
            child31.MetadataItems = new List<IMetadataItem>
                                        {
                                            new RequiredMetadataItem { IsRequired = true },
                                            new DisplayNameMetadataItem { Name = "Child-MD Customize" }
                                        };

            return metadataRootList;
        }

        public static IMetadata MultiMetadataInThreeLayersWithOneLevelChildren_ExpectedResult()
        {
            var metadata = new MetadataNode("MyResource")
            {
                MetadataItems = new List<IMetadataItem>
                                                       {
                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                           new RequiredMetadataItem { IsRequired = true },
                                                           new HiddenMetadataItem { IsHidden = false }
                                                       }
            };

            var child1 = metadata.AddChild("FirstName");
            child1.MetadataItems = new List<IMetadataItem>
                                        {
                                            new ReadonlyMetadataItem { IsReadonly = true },
                                            new DisplayNameMetadataItem { Name = "Child-MD Customize" },
                                            new RequiredMetadataItem { IsRequired = true }
                                        };
            var child2 = metadata.AddChild("Age");
            child2.MetadataItems = new List<IMetadataItem>
                                        {
                                            new HiddenMetadataItem { IsHidden = true }
                                        };

            return metadata;
        }

        public static IList<MetadataRoot> MultiMetadataInThreeLayersWithTwoLevelsChildren_TestValue()
        {
            var metadataRootList = new List<MetadataRoot>
                                       {
                                           new MetadataRoot ( "MyResource", 1 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                                           new RequiredMetadataItem { IsRequired = true }
                                                                       }
                                               },
                                           new MetadataRoot ( "MyResource", 2 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = false },
                                                                           new HiddenMetadataItem () { IsHidden = true }
                                                                       }
                                               },
                                           new MetadataRoot ( "MyResource", 3 )
                                               {
                                                   MetadataItems = new List<IMetadataItem>
                                                                       {
                                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                                           new HiddenMetadataItem () { IsHidden = false }
                                                                       }
                                               },
                                       };

            // first level children
            var child11 = metadataRootList[ 0 ].AddChild ( "FirstName" );
            child11.MetadataItems = new List<IMetadataItem>
                                        {
                                            new ReadonlyMetadataItem { IsReadonly = true },
                                            new DisplayNameMetadataItem { Name = "Child-Default" }
                                        };
            var child21 = metadataRootList[ 1 ].AddChild ( "FirstName" );
            child21.MetadataItems = new List<IMetadataItem>
                                        {
                                            new DisplayNameMetadataItem { Name = "Child-MD Default" }
                                        };
            var child22 = metadataRootList[ 1 ].AddChild ( "Age" );
            child22.MetadataItems = new List<IMetadataItem>
                                        {
                                            new HiddenMetadataItem { IsHidden = true }
                                        };
            var child31 = metadataRootList[ 2 ].AddChild ( "FirstName" );
            child31.MetadataItems = new List<IMetadataItem>
                                        {
                                            new RequiredMetadataItem { IsRequired = true },
                                            new DisplayNameMetadataItem { Name = "Child-MD Customize" }
                                        };

            // second level children
            var child11Level2 = child11.AddChild ( "Level2" );
            child11Level2.MetadataItems = new List<IMetadataItem>
                                              {
                                                  new ReadonlyMetadataItem { IsReadonly = true },
                                                  new DisplayNameMetadataItem { Name = "Child-Default Level2" }
                                              };
            var child21Level2 = child21.AddChild ( "Level2" );
            child21Level2.MetadataItems = new List<IMetadataItem>
                                              {
                                                  new DisplayNameMetadataItem { Name = "Child-MD Default Level2" }
                                              };
            var child31Level2 = child31.AddChild ( "Level2" );
            child31Level2.MetadataItems = new List<IMetadataItem>
                                              {
                                                  new RequiredMetadataItem { IsRequired = true },
                                                  new DisplayNameMetadataItem { Name = "Child-MD Customize Level2" }
                                              };

            return metadataRootList;
        }

        public static IMetadata MultiMetadataInThreeLayersWithTwoLevelsChildren_ExpectedResult()
        {
            var metadata = new MetadataNode("MyResource")
            {
                MetadataItems = new List<IMetadataItem>
                                                       {
                                                           new ReadonlyMetadataItem { IsReadonly = true },
                                                           new RequiredMetadataItem { IsRequired = true },
                                                           new HiddenMetadataItem { IsHidden = false }
                                                       }
            };

            var child1 = metadata.AddChild("FirstName");
            child1.MetadataItems = new List<IMetadataItem>
                                        {
                                            new ReadonlyMetadataItem { IsReadonly = true },
                                            new DisplayNameMetadataItem { Name = "Child-MD Customize" },
                                            new RequiredMetadataItem { IsRequired = true }
                                        };
            var child2 = metadata.AddChild("Age");
            child2.MetadataItems = new List<IMetadataItem>
                                        {
                                            new HiddenMetadataItem { IsHidden = true }
                                        };

            var child1Level2 = child1.AddChild ( "Level2" );
            child1Level2.MetadataItems = new List<IMetadataItem>
                                             {
                                                 new ReadonlyMetadataItem { IsReadonly = true },
                                                 new DisplayNameMetadataItem { Name = "Child-MD Customize Level2" },
                                                 new RequiredMetadataItem { IsRequired = true }
                                             };

            return metadata;
        }
    }
}
