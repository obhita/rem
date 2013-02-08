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

using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;
using Rem.Infrastructure.Service;

namespace Rem.Ria.AgencyModule.Web.LocationEditor
{
    /// <summary>
    /// Class for handling save location operation schedules request.
    /// </summary>
    public class SaveLocationOperationSchedulesRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<LocationOperationSchedulesDto>, DtoResponse<LocationOperationSchedulesDto>, LocationOperationSchedulesDto, Location>
    {
        #region Constants and Fields

        private bool _mappingResult = true;

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="location">The location.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( LocationOperationSchedulesDto dto, Location location )
        {
            _mappingResult &=
                new AggregateNodeCollectionMapper<LocationOperationScheduleDto, Location, LocationOperationSchedule> (
                    dto.LocationOperationSchedules, location, location.LocationOperationSchedules ).MapRemovedItem ( RemoveLocationOperationSchedule )
                    .MapAddedItem ( AddLocationOperationSchedule ).MapChangedItem ( ChangeLocationOperationSchedule ).Map ();

            return _mappingResult;
        }

        private static void AddLocationWorkHour ( LocationWorkHourDto dto, LocationOperationSchedule operationSchedule )
        {
            operationSchedule.AddWorkHour ( new LocationWorkHour ( dto.DayOfWeek, new TimeRange ( dto.StartTime, dto.EndTime ) ) );
        }

        private static void ChangeLocationWorkHour ( LocationWorkHourDto dto, LocationOperationSchedule operationSchedule, LocationWorkHour workHour )
        {
            RemoveLocationWorkHour ( dto, operationSchedule, workHour );
            AddLocationWorkHour ( dto, operationSchedule );
        }

        private static void RemoveLocationWorkHour ( LocationWorkHourDto dto, LocationOperationSchedule operationSchedule, LocationWorkHour workHour )
        {
            operationSchedule.RemoveWorkHour ( workHour );
        }

        private void AddLocationOperationSchedule ( LocationOperationScheduleDto dto, Location location )
        {
            var operationSchedule = location.AddOperationSchedule ( dto.Name );

            _mappingResult &= MapLocationOperationScheduleProperties ( operationSchedule, dto );
        }

        private void ChangeLocationOperationSchedule (
            LocationOperationScheduleDto dto, Location location, LocationOperationSchedule locationOperationSchedule )
        {
            _mappingResult &= MapLocationOperationScheduleProperties ( locationOperationSchedule, dto );
        }

        private bool MapLocationOperationScheduleProperties ( LocationOperationSchedule locationOperationSchedule, LocationOperationScheduleDto dto )
        {
            var result = true;

            result &= new PropertyMapper<LocationOperationSchedule> ( locationOperationSchedule, dto ).MapProperty ( x => x.Name, dto.Name ).Map ();

            result &=
                new AggregateNodeCollectionMapper<LocationWorkHourDto, LocationOperationSchedule, LocationWorkHour> (
                    dto.LocationWorkHours, locationOperationSchedule, locationOperationSchedule.LocationWorkHours ).MapAddedItem (
                        AddLocationWorkHour ).MapChangedItem ( ChangeLocationWorkHour ).MapRemovedItem ( RemoveLocationWorkHour ).Map ();

            return result;
        }

        private void RemoveLocationOperationSchedule (
            LocationOperationScheduleDto dto, Location location, LocationOperationSchedule locationOperationSchedule )
        {
            location.RemoveOperationSchedule ( locationOperationSchedule );
        }

        #endregion
    }
}
