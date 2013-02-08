using AutoMapper;
using Rem.Domain.Clinical.TedsModule;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// AutoMapper ITypeConverter implementation to covert DsmDiagnosisResponse to a string.
    /// </summary>
    public class DsmDiagnosisResponseToStringTypeConverter : ITypeConverter<DsmDiagnosisResponse, string>
    {
        /// <summary>
        /// Converts the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A string.</returns>
        public string Convert(ResolutionContext context)
        {
            if (context.IsSourceValueNull)
            {
                return null;
            }

            return (context.SourceValue as DsmDiagnosisResponse).Code;
        }
    }
}