using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Rem.Ria.PatientModule.Web.TedsInterview;

namespace Rem.Ria.PatientModule.TedsInterview
{
    /// <summary>
    /// This conveter coverts TedsAdmissionInterviewDto to the Visibility of Secondary/Tertiary Substance Usage Section
    /// </summary>
    public class SubstanceUsageVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var tedsAdmissionInterview = value as TedsAdmissionInterviewDto;
            if (tedsAdmissionInterview != null)
            {
                if (parameter.ToString() == "Secondary")
                {
                    if (tedsAdmissionInterview.SecondarySubstanceProblemType.IsAnswered() || 
                        tedsAdmissionInterview.SecondaryUseFrequencyType.IsAnswered() || 
                        tedsAdmissionInterview.SecondaryUsualAdministrationRouteType.IsAnswered() || 
                        tedsAdmissionInterview.SecondaryFirstUseAge.IsAnswered() || 
                        tedsAdmissionInterview.SecondaryDetailedDrugCode.IsAnswered())
                    {
                        return Visibility.Visible;
                    }
                }

                if (parameter.ToString() == "Tertiary")
                {
                    if (tedsAdmissionInterview.TertiarySubstanceProblemType.IsAnswered() ||
                        tedsAdmissionInterview.TertiaryUseFrequencyType.IsAnswered() ||
                        tedsAdmissionInterview.TertiaryUsualAdministrationRouteType.IsAnswered() ||
                        tedsAdmissionInterview.TertiaryFirstUseAge.IsAnswered() ||
                        tedsAdmissionInterview.TertiaryDetailedDrugCode.IsAnswered())
                    {
                        return Visibility.Visible;
                    }
                }
            }

            return Visibility.Collapsed;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException ();
        }
    }
}
