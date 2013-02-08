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

using System.Linq;
using Rem.Domain.Core.CommonModule;
using Rem.Domain.Clinical.LabModule;
using Rem.Infrastructure.Service;
using Rem.Ria.Infrastructure.Web.Extension;
using Rem.Ria.Infrastructure.Web.Mapping;

namespace Rem.Ria.PatientModule.Web.PatientDashboard
{
    /// <summary>
    /// Class for handling save lab request.
    /// </summary>
    public class SaveLabRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<LabSpecimenDto>, DtoResponse<LabSpecimenDto>, LabSpecimenDto, LabSpecimen>
    {
        #region Constants and Fields

        private readonly IDtoToDomainMappingHelper _mappingHelper;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveLabRequestHandler"/> class.
        /// </summary>
        /// <param name="mappingHelper">The mapping helper.</param>
        public SaveLabRequestHandler ( IDtoToDomainMappingHelper mappingHelper )
        {
            _mappingHelper = mappingHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( LabSpecimenDto dto, LabSpecimen entity )
        {
            _mappingResult &= MapProperties ( dto, entity );
            return _mappingResult;
        }

        private bool MapProperties ( LabSpecimenDto dto, LabSpecimen entity )
        {
            var labSpecimenType = _mappingHelper.MapLookupField<LabSpecimenType> ( dto.LabSpecimenType );

            entity.ReviseLabSpecimenType ( labSpecimenType );
            entity.ReviseLabReceivedDate ( dto.LabReceivedDate );
            entity.ReviseCollectedHereIndicator ( dto.CollectedHereIndicator );

            // TODO: This needs to be rethought when the domain for Lab is redone.
            var labTest = entity.LabTests.FirstOrDefault ( lt => lt.LabTestInfo.LabTestName.WellKnownName == dto.LabTestName.WellKnownName );
            if ( labTest == null && entity.LabTests.Count > 0 )
            {
                //right now there is only every one lab test per lab specimen?
                entity.RemoveLabTest ( entity.LabTests.ElementAt ( 0 ) );
            }
            var labTestInfo = new LabTestInfoBuilder ()
                .WithLabTestName ( _mappingHelper.MapLookupField<LabTestName> ( dto.LabTestName ) )
                .WithTestReportDate ( dto.LabTestDate )
                .WithLabTestNote ( dto.LabTestNote );
            if ( labTest == null )
            {
                labTest = entity.AddLabTest ( labTestInfo );
            }
            else
            {
                labTest.ReviseLabTestInfo ( labTestInfo );
            }

            var result = new AggregateNodeCollectionMapper<LabResultDto, LabTest, LabResult> ( dto.LabResults, labTest, labTest.LabResults )
                .MapAddedItem (
                    ( lrdto, lt ) =>
                        {
                            CodedConcept labTestResultNameCodedConcept = null;
                            if ( lrdto.LabTestResultNameCodedConcept != null )
                            {
                                labTestResultNameCodedConcept = new CodedConceptBuilder ().WithCodedConceptDto ( lrdto.LabTestResultNameCodedConcept );
                            }

                            lt.AddLabResult (
                                new LabResultBuilder ()
                                    .WithLabTestResultNameCodedConcept ( labTestResultNameCodedConcept )
                                    .WithValue(lrdto.Value)
                                    .WithUnitOfMeasureCode ( lrdto.UnitOfMeasureCode ) );
                        }
                )
                .MapChangedItem (
                    ( lrdto, lt, lr ) =>
                        {
                            lt.RemoveLabResult ( lr );
                            CodedConcept labTestResultNameCodedConcept = null;
                            if ( lrdto.LabTestResultNameCodedConcept != null )
                            {
                                labTestResultNameCodedConcept = new CodedConceptBuilder ().WithCodedConceptDto ( lrdto.LabTestResultNameCodedConcept );
                            }
                            lt.AddLabResult (
                                new LabResultBuilder ()
                                    .WithLabTestResultNameCodedConcept ( labTestResultNameCodedConcept )
                                    .WithValue ( lrdto.Value )
                                    .WithUnitOfMeasureCode ( lrdto.UnitOfMeasureCode ) );
                        } )
                .MapRemovedItem ( ( lrdto, lt, lr ) => lt.RemoveLabResult ( lr ) )
                .Map ();

            return result;
        }

        #endregion
    }
}
