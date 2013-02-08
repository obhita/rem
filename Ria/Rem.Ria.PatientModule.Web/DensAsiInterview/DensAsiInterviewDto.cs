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

using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.DensAsiInterview
{
    /// <summary>
    /// Data transfer object for DensAsiInterview class.
    /// </summary>
    public class DensAsiInterviewDto : ActivityDto
    {
        #region Constants and Fields

        private DensAsiClosureDto _densAsiClosure;
        private DensAsiDrugAlcoholUseDto _densAsiDrugAlcoholUse;
        private DensAsiDsmIvDto _densAsiDsmIv;
        private DensAsiEmploymentStatusDto _densAsiEmploymentStatus;
        private DensAsiFamilySocialRelationshipsDto _densAsiFamilySocialRelationships;
        private DensAsiLegalStatusDto _densAsiLegalStatus;
        private DensAsiMedicalStatusDto _densAsiMedicalStatus;
        private DensAsiPatientProfileDto _densAsiPatientProfile;
        private DensAsiPsychiatricStatusDto _densAsiPsychiatricStatus;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the dens asi closure.
        /// </summary>
        /// <value>The dens asi closure.</value>
        public DensAsiClosureDto DensAsiClosure
        {
            get { return _densAsiClosure; }
            set { RaisePropertyChanged ( ref _densAsiClosure, () => DensAsiClosure, value ); }
        }

        /// <summary>
        /// Gets or sets the dens asi drug alcohol use.
        /// </summary>
        /// <value>The dens asi drug alcohol use.</value>
        public DensAsiDrugAlcoholUseDto DensAsiDrugAlcoholUse
        {
            get { return _densAsiDrugAlcoholUse; }
            set { RaisePropertyChanged ( ref _densAsiDrugAlcoholUse, () => DensAsiDrugAlcoholUse, value ); }
        }

        /// <summary>
        /// Gets or sets the dens asi DSM iv.
        /// </summary>
        /// <value>The dens asi DSM iv.</value>
        public DensAsiDsmIvDto DensAsiDsmIv
        {
            get { return _densAsiDsmIv; }
            set { RaisePropertyChanged ( ref _densAsiDsmIv, () => DensAsiDsmIv, value ); }
        }

        /// <summary>
        /// Gets or sets the dens asi employment status.
        /// </summary>
        /// <value>The dens asi employment status.</value>
        public DensAsiEmploymentStatusDto DensAsiEmploymentStatus
        {
            get { return _densAsiEmploymentStatus; }
            set { RaisePropertyChanged ( ref _densAsiEmploymentStatus, () => DensAsiEmploymentStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the dens asi family social relationships.
        /// </summary>
        /// <value>The dens asi family social relationships.</value>
        public DensAsiFamilySocialRelationshipsDto DensAsiFamilySocialRelationships
        {
            get { return _densAsiFamilySocialRelationships; }
            set { RaisePropertyChanged ( ref _densAsiFamilySocialRelationships, () => DensAsiFamilySocialRelationships, value ); }
        }

        /// <summary>
        /// Gets or sets the dens asi legal status.
        /// </summary>
        /// <value>The dens asi legal status.</value>
        public DensAsiLegalStatusDto DensAsiLegalStatus
        {
            get { return _densAsiLegalStatus; }
            set { RaisePropertyChanged ( ref _densAsiLegalStatus, () => DensAsiLegalStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the dens asi medical status.
        /// </summary>
        /// <value>The dens asi medical status.</value>
        public DensAsiMedicalStatusDto DensAsiMedicalStatus
        {
            get { return _densAsiMedicalStatus; }
            set { RaisePropertyChanged ( ref _densAsiMedicalStatus, () => DensAsiMedicalStatus, value ); }
        }

        /// <summary>
        /// Gets or sets the dens asi patient profile.
        /// </summary>
        /// <value>The dens asi patient profile.</value>
        public DensAsiPatientProfileDto DensAsiPatientProfile
        {
            get { return _densAsiPatientProfile; }
            set { RaisePropertyChanged ( ref _densAsiPatientProfile, () => DensAsiPatientProfile, value ); }
        }

        /// <summary>
        /// Gets or sets the dens asi psychiatric status.
        /// </summary>
        /// <value>The dens asi psychiatric status.</value>
        public DensAsiPsychiatricStatusDto DensAsiPsychiatricStatus
        {
            get { return _densAsiPsychiatricStatus; }
            set { RaisePropertyChanged ( ref _densAsiPsychiatricStatus, () => DensAsiPsychiatricStatus, value ); }
        }

        #endregion
    }
}
