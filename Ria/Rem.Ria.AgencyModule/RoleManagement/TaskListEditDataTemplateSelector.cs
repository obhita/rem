﻿using System.Windows;
using Rem.Ria.AgencyModule.Web.Common;
using Telerik.Windows.Controls;

namespace Rem.Ria.AgencyModule.RoleManagement
{
    /// <summary>
    /// Class for selecting task list edit data template.
    /// </summary>
    public class TaskListEditDataTemplateSelector : DataTemplateSelector
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the permission edit template.
        /// </summary>
        /// <value>The permission edit template.</value>
        public DataTemplate PermissionEditTemplate { get; set; }

        /// <summary>
        /// Gets or sets the task edit template.
        /// </summary>
        /// <value>The task edit template.</value>
        public DataTemplate TaskEditTemplate { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, returns a DataTemplate based on custom logic.
        /// </summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>A <see cref="System.Windows.DataTemplate"/></returns>
        public override DataTemplate SelectTemplate ( object item, DependencyObject container )
        {
            if ( item is SystemRoleDto )
            {
                return TaskEditTemplate;
            }
            else if ( item is SystemPermissionDto )
            {
                return PermissionEditTemplate;
            }

            return null;
        }

        #endregion
    }
}
