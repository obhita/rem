using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;

namespace FEI.GhostDocPackage
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
    [Guid(GuidList.guidGhostDocPackagePkgString)]
    public sealed class GhostDocPackagePackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public GhostDocPackagePackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }



        /////////////////////////////////////////////////////////////////////////////
        // Overriden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                // Create the command for the menu item.
                CommandID itemMenuID = new CommandID(GuidList.guidGhostDocPackageCmdSet, (int)PkgCmdIDList.cmdidDocumentThis);
                MenuCommand item = new MenuCommand(MenuItemCallback, itemMenuID );
                mcs.AddCommand( item );

                // Create the command for the menu item.
                CommandID projectCommandID = new CommandID(GuidList.guidGhostDocPackageProjectCmdSet, (int)PkgCmdIDList.cmdidProjectDocumentThis);
                MenuCommand projectItem = new MenuCommand(MenuItemCallback, projectCommandID);
                mcs.AddCommand(projectItem);

                // Create the command for the menu item.
                CommandID folderCommandID = new CommandID(GuidList.guidGhostDocPackageFolderCmdSet, (int)PkgCmdIDList.cmdidFolderDocumentThis);
                MenuCommand folderItem = new MenuCommand(MenuItemCallback, folderCommandID);
                mcs.AddCommand(folderItem);
            }
        }
        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
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
                        if (projectItem.Name.EndsWith(".cs") || projectItem.Name.EndsWith(".vb"))
                        {
                            DocumentFile(projectItem);
                        }
                        else
                        {
                            RecurseItems(projectItem.ProjectItems);
                        }
                    }
                    else
                    {
                        var project = GetProject(hierarchy);
                        if (project != null)
                        {
                            RecurseItems(project.ProjectItems);
                        }
                    }
                }
            }
        }

        private void RecurseItems(ProjectItems projectItems)
        {
            foreach (ProjectItem item in projectItems)
            {
                if (item.Name.EndsWith(".cs") || item.Name.EndsWith(".vb"))
                {
                    DocumentFile(item);
                }
                else
                {
                    RecurseItems(item.ProjectItems);
                }
            }
        }

        private void DocumentFile(ProjectItem projectItem)
        {
            projectItem.Open();
            if (projectItem.Document != null)
            {
                projectItem.Document.Activate();
            }
            projectItem.DTE.ExecuteCommand("Tools.SubMain.GhostDoc.DocumentFile");
        }

        public ProjectItem GetProjectItem(IVsHierarchy hierarchy, UInt32 prjItemId)
        {
            object prjItemObject = null;
            if (
                ErrorHandler.Succeeded(hierarchy.GetProperty(prjItemId, (int)__VSHPROPID.VSHPROPID_ExtObject,
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
                    (int)__VSHPROPID.VSHPROPID_ExtObject,
                    out project)))
            {
                return (project as Project);
            }
            return null;
        }
    }
}
