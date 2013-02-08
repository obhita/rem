using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FindConflictingReferences
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type a bin folder path:");
            var binFolderPath = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(binFolderPath) || !Directory.Exists(binFolderPath))
            {
                Console.WriteLine("Type a Correct bin folder path:");
                binFolderPath = Console.ReadLine();
            }

            var assemblies = GetAllAssemblies(binFolderPath);

            var references = GetReferencesFromAllAssemblies(assemblies);

            var groupsOfConflicts = FindReferencesWithTheSameShortNameButDiffererntFullNames(references);

            Console.Out.WriteLine("******************************************************Begin******************************************************");
            Debug.WriteLine("******************************************************Begin******************************************************");

            foreach (var group in groupsOfConflicts)
            {
                Console.Out.WriteLine("Possible conflicts for {0}:", group.Key);
                Debug.WriteLine(string.Format("Possible conflicts for {0}:", group.Key));
                foreach (var reference in group)
                {
                    Console.Out.WriteLine("{0} references {1}",
                                          reference.Assembly.Name.PadRight(45),
                                          reference.ReferencedAssembly.FullName);
                    Debug.WriteLine("{0} references {1}",
                                          reference.Assembly.Name.PadRight(45),
                                          reference.ReferencedAssembly.FullName);
                }
            }

            Console.Out.WriteLine("******************************************************Begin******************************************************");
            Debug.WriteLine("******************************************************Begin******************************************************");

            Console.WriteLine("Hit any key to close this program.");
            Console.ReadKey ();
        }

        private static IEnumerable<IGrouping<string, Reference>> FindReferencesWithTheSameShortNameButDiffererntFullNames(List<Reference> references)
        {
            return from reference in references
                   group reference by reference.ReferencedAssembly.Name
                       into referenceGroup
                       where referenceGroup.ToList().Select(reference => reference.ReferencedAssembly.FullName).Distinct().Count() > 1
                       select referenceGroup;
        }

        private static List<Reference> GetReferencesFromAllAssemblies(List<Assembly> assemblies)
        {
            var references = new List<Reference>();
            foreach (var assembly in assemblies)
            {
                foreach (var referencedAssembly in assembly.GetReferencedAssemblies())
                {
                    references.Add(new Reference
                    {
                        Assembly = assembly.GetName(),
                        ReferencedAssembly = referencedAssembly
                    });
                }
            }
            return references;
        }

        private static List<Assembly> GetAllAssemblies(string path)
        {
            var files = new List<FileInfo>();
            var directoryToSearch = new DirectoryInfo(path);
            files.AddRange(directoryToSearch.GetFiles("*.dll", SearchOption.AllDirectories));
            files.AddRange(directoryToSearch.GetFiles("*.exe", SearchOption.AllDirectories));
            return files.ConvertAll(file => Assembly.LoadFile(file.FullName));
        }

        private class Reference
        {
            public AssemblyName Assembly { get; set; }
            public AssemblyName ReferencedAssembly { get; set; }
        }
    }
}
