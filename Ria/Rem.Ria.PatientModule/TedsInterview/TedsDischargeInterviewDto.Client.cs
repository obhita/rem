using System.Collections.Generic;
using System.Runtime.Serialization;
using Pillar.Common.Utility;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// Data transfer object for TedsDischargeInterview class.
    /// </summary>
    public partial class TedsDischargeInterviewDto
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
                var wellKnownNamesGroupA = new List<string>{ WellKnownNames.TedsModule.TedsNonResponse.NotApplicable, WellKnownNames.TedsModule.TedsNonResponse.Unknown };

                return new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            PropertyUtil.ExtractPropertyName ( () => this.PrimaryUseFrequencyType ),
                            wellKnownNamesGroupA
                            },

                        {
                            PropertyUtil.ExtractPropertyName ( () => this.SecondaryUseFrequencyType ),
                            wellKnownNamesGroupA
                            },

                        {
                            PropertyUtil.ExtractPropertyName ( () => this.TertiaryUseFrequencyType ),
                            wellKnownNamesGroupA
                            },

                        {
                            PropertyUtil.ExtractPropertyName ( () => this.DetailedNotInLaborForce ),
                            wellKnownNamesGroupA
                            }
                    };
            }
        }
    }
}
