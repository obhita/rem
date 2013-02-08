#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion

using System;
using Rem.Ria.NewCropModule.Web.NCScript;
using Rem.WellKnownNames.PatientModule;
using AllergySeverityType = Rem.Ria.NewCropModule.Web.NCScript.AllergySeverityType;

namespace Rem.Ria.NewCropModule.Web
{
    /// <summary>
    /// Class for helping nc script.
    /// </summary>
    internal static class NcScriptHelper
    {
        #region Public Methods

        /// <summary>
        /// Removes the name of the unwanted parts from drug.
        /// </summary>
        /// <param name="drugName">Name of the drug.</param>
        /// <returns>A <see cref="System.String"/></returns>
        public static string RemoveUnwantedPartsFromDrugName ( string drugName )
        {
            var startAt = drugName.IndexOf ( '[' );
            var result = drugName.Substring ( 0, startAt );

            return result;
        }

        /// <summary>
        /// Transforms the type of the rem allergy severity type to new crop allergy severity.
        /// </summary>
        /// <param name="allergySeverityType">Type of the allergy severity.</param>
        /// <returns>A <see cref="Rem.Ria.NewCropModule.Web.NCScript.AllergySeverityType"/></returns>
        public static AllergySeverityType TransformRemAllergySeverityTypeToNewCropAllergySeverityType (
            Domain.Clinical.PatientModule.AllergySeverityType allergySeverityType )
        {
            if ( allergySeverityType != null )
            {
                if ( allergySeverityType.WellKnownName == WellKnownNames.PatientModule.AllergySeverityType.Severe )
                {
                    return AllergySeverityType.Severe;
                }

                return allergySeverityType.WellKnownName == WellKnownNames.PatientModule.AllergySeverityType.Mild
                           ? AllergySeverityType.Mild
                           : AllergySeverityType.Moderate;
            }
            return AllergySeverityType.Unspecified;
        }

        /// <summary>
        /// Transforms the rem medication into new crop prescription archive status.
        /// </summary>
        /// <param name="medicationStatusCode">The medication status code.</param>
        /// <returns>A <see cref="Rem.Ria.NewCropModule.Web.NCScript.PrescriptionArchiveType"/></returns>
        public static PrescriptionArchiveType TransformRemMedicationIntoNewCropPrescriptionArchiveStatus ( string medicationStatusCode )
        {
            var newCropStatus = TransformRemMedicationStatusIntoNewCropPrescriptionStatus ( medicationStatusCode );
            return newCropStatus == PrescriptionStatusType.Pending ? PrescriptionArchiveType.Yes : PrescriptionArchiveType.No;
        }

        /// <summary>
        /// Transforms the rem medication status into new crop prescription status.
        /// </summary>
        /// <param name="medicationStatusCode">The medication status code.</param>
        /// <returns>A <see cref="Rem.Ria.NewCropModule.Web.NCScript.PrescriptionStatusType"/></returns>
        public static PrescriptionStatusType TransformRemMedicationStatusIntoNewCropPrescriptionStatus ( string medicationStatusCode )
        {
            return medicationStatusCode == MedicationStatus.Active
                       ? PrescriptionStatusType.Current
                       : PrescriptionStatusType.Pending;
        }

        /// <summary>
        /// Transforms to prescription archive status.
        /// </summary>
        /// <param name="archiveStatusCode">The archive status code.</param>
        /// <returns>A <see cref="Rem.Ria.NewCropModule.Web.NCScript.PrescriptionArchiveType"/></returns>
        public static PrescriptionArchiveType TransformToPrescriptionArchiveStatus ( string archiveStatusCode )
        {
            if ( archiveStatusCode == "N" )
            {
                return PrescriptionArchiveType.No;
            }
            if ( archiveStatusCode == "Y" )
            {
                return PrescriptionArchiveType.Yes;
            }

            throw new ApplicationException ( "An invalid ArchiveStatusCode has been provided." );
        }

        /// <summary>
        /// Transforms to prescription status.
        /// </summary>
        /// <param name="prescriptionStatusCode">The prescription status code.</param>
        /// <returns>A <see cref="Rem.Ria.NewCropModule.Web.NCScript.PrescriptionStatusType"/></returns>
        public static PrescriptionStatusType TransformToPrescriptionStatus ( string prescriptionStatusCode )
        {
            if ( prescriptionStatusCode == "C" )
            {
                return PrescriptionStatusType.Current;
            }
            if ( prescriptionStatusCode == "P" )
            {
                return PrescriptionStatusType.Pending;
            }

            throw new ApplicationException ( "An invalid PrescriptionStatusCode has been provided." );
        }

        #endregion
    }
}
