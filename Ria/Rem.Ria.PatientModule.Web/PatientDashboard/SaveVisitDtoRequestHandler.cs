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

using Pillar.Common.Utility;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;
using Rem.Ria.PatientModule.Web.Common;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling save visit dto request.
    /// </summary>
    public class SaveVisitDtoRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<VisitDto>, DtoResponse<VisitDto>, VisitDto, Visit>
    {
        #region Constants and Fields

        private readonly IStaffRepository _staffRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IVisitRepository _visitRepository;
        private bool saveResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveVisitDtoRequestHandler"/> class.
        /// </summary>
        /// <param name="visitRepository">The visit repository.</param>
        /// <param name="staffRepository">The staff repository.</param>
        /// <param name="locationRepository">The location repository. </param>
        public SaveVisitDtoRequestHandler (
            IVisitRepository visitRepository, IStaffRepository staffRepository, ILocationRepository locationRepository )
        {
            _visitRepository = visitRepository;
            _staffRepository = staffRepository;
            _locationRepository = locationRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( VisitDto dto, Visit entity )
        {
            Visit visit;
            if ( TryGetVisit ( dto, out visit ) )
            {
                visit.ReviseNote ( dto.Note );
                visit.RescheduleAppointment ( new DateTimeRange ( dto.AppointmentStartDateTime, dto.AppointmentEndDateTime ) );
            }

            Staff staff;
            if ( TryGetStaff ( dto, out staff ) )
            {
                visit.ReassignAppointment ( staff );
            }

            Location location;
            if (TryGetLocation(dto, out location))
            {
                visit.ChangeServiceLocation ( location );
            }

            return saveResult;
        }

        private bool TryGetStaff ( VisitDto dto, out Staff staff )
        {
            Check.IsNotNull ( dto.Staff.Key, "Staff Key is required." );
            staff = _staffRepository.GetByKey ( dto.Staff.Key );

            if ( staff == null )
            {
                dto.AddDataErrorInfo (
                    new DataErrorInfo ( "Staff not found.", ErrorLevel.Error, PropertyUtil.ExtractPropertyName ( () => dto.Staff ) ) );
                saveResult = false;
                return false;
            }

            return true;
        }

        private bool TryGetLocation(VisitDto dto, out Location location)
        {
            Check.IsNotNull(dto.Location.Key, "Location Key is required.");
            location = _locationRepository.GetByKey(dto.Location.Key);

            if (location == null)
            {
                dto.AddDataErrorInfo(
                    new DataErrorInfo("Location not found.", ErrorLevel.Error, PropertyUtil.ExtractPropertyName(() => dto.Location)));
                saveResult = false;
                return false;
            }

            return true;
        }

        private bool TryGetVisit ( VisitDto dto, out Visit visit )
        {
            Check.IsNotNull ( dto.Key, "Visit Key is required." );
            visit = _visitRepository.GetByKey ( dto.Key );

            if ( visit == null )
            {
                dto.AddDataErrorInfo ( new DataErrorInfo ( "Visit not found.", ErrorLevel.Error ) );
                saveResult = false;
                return false;
            }

            return true;
        }

        #endregion
    }
}
