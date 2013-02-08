using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using Rem.Ria.AgencyModule.Web.Common;

namespace Rem.Ria.AgencyModule.StaffEditor
{
    /// <summary>
    /// Class for converting staff system role to system role.
    /// </summary>
    public class StaffSystemRoleToSystemRoleConverter : IValueConverter
    {
        #region Public Methods

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>The value to be passed to the target dependency property.</returns>
        public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            //double div;
            //if (value is double && double.TryParse(parameter.ToString(), out div))
            //{
            //    return (double)value * div;
            //}
            return new ObservableCollection<SystemRoleDto> ();
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>The value to be passed to the source object.</returns>
        public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            //double div;
            //if (value is double && double.TryParse(parameter.ToString(), out div))
            //{
            //    return (double)value / div;
            //}
            return new ObservableCollection<StaffSystemRoleDto> ();
        }

        #endregion
    }
}
