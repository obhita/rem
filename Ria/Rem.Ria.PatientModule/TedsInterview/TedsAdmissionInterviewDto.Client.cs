using System.Collections.Generic;
using Pillar.Common.Utility;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// This class defines a data transfer object for TedsAdmissionInterview.
    /// </summary>
    public partial class TedsAdmissionInterviewDto
    {
        /// <summary>
        /// Gets the name of the non response lookup well known names dictionary keyed by property chain.
        /// </summary>
        /// <value>
        /// The name of the non response lookup well known names dictionary keyed by property chain.
        /// </value>
        public Dictionary<string, IEnumerable<string>> NonResponseLookupWellKnownNamesDictionaryKeyedByPropertyName
        {
            get
            {
                var wellKnownNamesForMostProperties = new List<string> { WellKnownNames.TedsModule.TedsNonResponse.NotApplicable, WellKnownNames.TedsModule.TedsNonResponse.Unknown };

                return new Dictionary<string, IEnumerable<string>>
                {
                    {
                        PropertyUtil.ExtractPropertyName ( () => this.TedsGenderInformationPregnantIndicator ),
                        wellKnownNamesForMostProperties
                        },
                    {
                        PropertyUtil.ExtractPropertyName ( () => this.TedsEmploymentStatusInformationDetailedNotInLaborForce ),
                        wellKnownNamesForMostProperties
                        },

                    {
                        PropertyUtil.ExtractPropertyName ( () => this.PrimaryUsualAdministrationRouteType ),
                        wellKnownNamesForMostProperties
                        },
                    {
                        PropertyUtil.ExtractPropertyName ( () => this.PrimaryFirstUseAge ),
                        wellKnownNamesForMostProperties
                        },
                    {
                        PropertyUtil.ExtractPropertyName ( () => this.PrimaryDetailedDrugCode ),
                        wellKnownNamesForMostProperties
                        },
                    {
                        PropertyUtil.ExtractPropertyName ( () => this.PrimaryUseFrequencyType ),
                        wellKnownNamesForMostProperties
                        },

                    {
                        PropertyUtil.ExtractPropertyName ( () => this.SecondaryUsualAdministrationRouteType ),
                        wellKnownNamesForMostProperties
                        },
                    {
                        PropertyUtil.ExtractPropertyName ( () => this.SecondaryFirstUseAge ),
                        wellKnownNamesForMostProperties
                        },
                    {
                        PropertyUtil.ExtractPropertyName ( () => this.SecondaryDetailedDrugCode ),
                        wellKnownNamesForMostProperties
                        },
                    {
                        PropertyUtil.ExtractPropertyName ( () => this.SecondaryUseFrequencyType ),
                        wellKnownNamesForMostProperties
                        },

                    {
                        PropertyUtil.ExtractPropertyName ( () => this.TertiaryUsualAdministrationRouteType ),
                        wellKnownNamesForMostProperties
                        },
                    {
                        PropertyUtil.ExtractPropertyName ( () => this.TertiaryFirstUseAge ),
                        wellKnownNamesForMostProperties
                        },
                    {
                        PropertyUtil.ExtractPropertyName ( () => this.TertiaryDetailedDrugCode ),
                        wellKnownNamesForMostProperties
                        },
                    {
                        PropertyUtil.ExtractPropertyName ( () => this.TertiaryUseFrequencyType ),
                        wellKnownNamesForMostProperties
                        }
                };
            }
        }
    }
}
