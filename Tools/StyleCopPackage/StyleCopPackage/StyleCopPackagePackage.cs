using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace FEI.StyleCopPackage
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidStyleCopPackagePkgString)]
    public sealed class StyleCopPackagePackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public StyleCopPackagePackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", ToString()));
        }


        /////////////////////////////////////////////////////////////////////////////
        // Overriden Package Implementation

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            var mcs = GetService(typeof (IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                var ignoreCommandId = new CommandID(GuidList.guidStyleCopPackageCmdSet,
                                                  (int) PkgCmdIDList.cmdidIgnStyleCop);
                var ignoreItem = new MenuCommand(IgnoreItemCallback, ignoreCommandId);
                mcs.AddCommand(ignoreItem);

                var unIgnoreCommandId = new CommandID(GuidList.guidStyleCopPackageCmdSet,
                                                  (int)PkgCmdIDList.cmdidUnIgnStyleCop);
                var unIgnoreItem = new MenuCommand(UnIgnoreItemCallback, unIgnoreCommandId);
                mcs.AddCommand(unIgnoreItem);

                var ignoreProjectCommandId = new CommandID(GuidList.guidStyleCopPackageProjectCmdSet,
                                                   (int) PkgCmdIDList.cmdidIgnStyleCop2);
                var ignoreProjectItem = new MenuCommand(IgnoreItemCallback, ignoreProjectCommandId);
                mcs.AddCommand(ignoreProjectItem);

                var unIgnoreProjectCommandId = new CommandID(GuidList.guidStyleCopPackageProjectCmdSet,
                                                   (int)PkgCmdIDList.cmdidUnIgnStyleCop2);
                var unIgnoreProjectItem = new MenuCommand(UnIgnoreItemCallback, unIgnoreProjectCommandId);
                mcs.AddCommand(unIgnoreProjectItem);
            }
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void IgnoreItemCallback(object sender, EventArgs e)
        {
            var selectionMonitor = (IVsMonitorSelection) GetService(typeof (SVsShellMonitorSelection));

            IntPtr ppHier;
            UInt32 itemid;
            IVsMultiItemSelect multiItemSelect;
            IntPtr ppSC;

            if (
                ErrorHandler.Succeeded(selectionMonitor.GetCurrentSelection(out ppHier, out itemid, out multiItemSelect,
                                                                            out ppSC)))
            {
                if (itemid == VSConstants.VSITEMID_SELECTION)
                {
                    // Multiple selection, do something with all the selected items
                }
                else
                {
                    var hierarchy = Marshal.GetObjectForIUnknown(ppHier) as IVsHierarchy;
                    var projectItem = GetProjectItem(hierarchy, itemid);
                    if (projectItem != null)
                    {
                        var projectFilePath = projectItem.ContainingProject.FullName;
                        var xmlDoc = new XmlDocument();
                        xmlDoc.Load(projectFilePath);

                        var mgr = new XmlNamespaceManager(xmlDoc.NameTable);
                        mgr.AddNamespace("x", xmlDoc.DocumentElement.NamespaceURI);
                        var compileNodes = xmlDoc.SelectNodes("/x:Project/x:ItemGroup/x:Compile", mgr);
                        foreach (XmlNode compileNode in compileNodes)
                        {
                            var include = compileNode.SelectSingleNode("@Include");
                            if (include != null && include.Value.EndsWith(Path.GetFileName(projectItem.FileNames[0])))
                            {
                                var exludeStyleCopNode = compileNode.SelectSingleNode("x:ExcludeFromStyleCop", mgr);
                                if (exludeStyleCopNode != null)
                                {
                                    exludeStyleCopNode.InnerText = "true";
                                }
                                else
                                {
                                    var newNode = xmlDoc.CreateElement("ExcludeFromStyleCop",
                                                                              xmlDoc.DocumentElement.NamespaceURI);
                                    newNode.InnerText = "true";
                                    compileNode.AppendChild(newNode);
                                }
                                xmlDoc.Save(projectFilePath);
                                break;
                            }
                        }
                    }
                    else
                    {
                        var project = GetProject(hierarchy);
                        if (project != null)
                        {
                            var projectFilePath = project.FullName;
                            var xmlDoc = new XmlDocument();
                            xmlDoc.Load(projectFilePath);

                            var mgr = new XmlNamespaceManager(xmlDoc.NameTable);
                            mgr.AddNamespace("x", xmlDoc.DocumentElement.NamespaceURI);
                            var compileNodes = xmlDoc.SelectNodes("/x:Project/x:ItemGroup/x:Compile", mgr);
                            foreach (XmlNode compileNode in compileNodes)
                            {
                                var exludeStyleCopNode = compileNode.SelectSingleNode("x:ExcludeFromStyleCop", mgr);
                                if (exludeStyleCopNode != null)
                                {
                                    exludeStyleCopNode.InnerText = "true";
                                }
                                else
                                {
                                    var newNode = xmlDoc.CreateElement("ExcludeFromStyleCop",
                                                                              xmlDoc.DocumentElement.NamespaceURI);
                                    newNode.InnerText = "true";
                                    compileNode.AppendChild(newNode);
                                }
                            }
                            xmlDoc.Save(projectFilePath);
                        }
                    }
                }
            }
        }

        private void UnIgnoreItemCallback(object sender, EventArgs e)
        {
            var selectionMonitor = (IVsMonitorSelection)GetService(typeof(SVsShellMonitorSelection));

            IntPtr ppHier;
            UInt32 itemid;
            IVsMultiItemSelect multiItemSelect;
            IntPtr ppSC;

            if (
                ErrorHandler.Succeeded(selectionMonitor.GetCurrentSelection(out ppHier, out itemid, out multiItemSelect,
                                                                            out ppSC)))
            {
                if (itemid == VSConstants.VSITEMID_SELECTION)
                {
                    // Multiple selection, do something with all the selected items
                }
                else
                {
                    var hierarchy = Marshal.GetObjectForIUnknown(ppHier) as IVsHierarchy;
                    var projectItem = GetProjectItem(hierarchy, itemid);
                    if (projectItem != null)
                    {
                        var projectFilePath = projectItem.ContainingProject.FullName;
                        var xmlDoc = new XmlDocument();
                        xmlDoc.Load(projectFilePath);

                        var mgr = new XmlNamespaceManager(xmlDoc.NameTable);
                        mgr.AddNamespace("x", xmlDoc.DocumentElement.NamespaceURI);
                        var compileNodes = xmlDoc.SelectNodes("/x:Project/x:ItemGroup/x:Compile", mgr);
                        foreach (XmlNode compileNode in compileNodes)
                        {
                            var include = compileNode.SelectSingleNode("@Include");
                            if (include != null && include.Value.EndsWith(Path.GetFileName(projectItem.FileNames[0])))
                            {
                                var exludeStyleCopNode = compileNode.SelectSingleNode("x:ExcludeFromStyleCop", mgr);
                                if (exludeStyleCopNode != null)
                                {
                                    compileNode.RemoveChild(exludeStyleCopNode);
                                    if(compileNode is XmlElement)
                                    {
                                        (compileNode as XmlElement).IsEmpty = !compileNode.HasChildNodes;
                                    }
                                }
                                xmlDoc.Save(projectFilePath);
                                break;
                            }
                        }
                    }
                    else
                    {
                        var project = GetProject(hierarchy);
                        if (project != null)
                        {
                            var projectFilePath = project.FullName;
                            var xmlDoc = new XmlDocument();
                            xmlDoc.Load(projectFilePath);

                            var mgr = new XmlNamespaceManager(xmlDoc.NameTable);
                            mgr.AddNamespace("x", xmlDoc.DocumentElement.NamespaceURI);
                            var compileNodes = xmlDoc.SelectNodes("/x:Project/x:ItemGroup/x:Compile", mgr);
                            foreach (XmlNode compileNode in compileNodes)
                            {
                                var exludeStyleCopNode = compileNode.SelectSingleNode("x:ExcludeFromStyleCop", mgr);
                                if (exludeStyleCopNode != null)
                                {
                                    compileNode.RemoveChild(exludeStyleCopNode);
                                    if (compileNode is XmlElement)
                                    {
                                        (compileNode as XmlElement).IsEmpty = !compileNode.HasChildNodes;
                                    }
                                }
                            }
                            xmlDoc.Save(projectFilePath);
                        }
                    }
                }
            }
        }

        public ProjectItem GetProjectItem(IVsHierarchy hierarchy, UInt32 prjItemId)
        {
            object prjItemObject = null;
            if (
                ErrorHandler.Succeeded(hierarchy.GetProperty(prjItemId, (int) __VSHPROPID.VSHPROPID_ExtObject,
                                                             out prjItemObject)))
            {
                return prjItemObject as ProjectItem;
            }
            return null;
        }

        public Project GetProject(IVsHierarchy hierarchy)
        {
            object project;

            if (ErrorHandler.Succeeded
                (hierarchy.GetProperty(
                    VSConstants.VSITEMID_ROOT,
                    (int) __VSHPROPID.VSHPROPID_ExtObject,
                    out project)))
            {
                return (project as Project);
            }
            return null;
        }
    }
}