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
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Clinical.ProgramModule;
using Rem.Infrastructure.Service;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.AgencyModule.Web.ProgramOfferingEditor
{
    /// <summary>
    /// Class for handling save program offering profile dto request.
    /// </summary>
    public class SaveProgramOfferingProfileDtoRequestHandler :
        SaveAggregateDtoRequestHandlerBase<SaveDtoRequest<ProgramOfferingProfileDto>, DtoResponse<ProgramOfferingProfileDto>, ProgramOfferingProfileDto, ProgramOffering>
    {
        #region Constants and Fields

        private readonly IProgramOfferingFactory _programOfferingFactory;
        private bool _isNew;
        private bool _mappingResult = true;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveProgramOfferingProfileDtoRequestHandler"/> class.
        /// </summary>
        /// <param name="programOfferingFactory">The program offering factory.</param>
        public SaveProgramOfferingProfileDtoRequestHandler ( IProgramOfferingFactory programOfferingFactory )
        {
            _programOfferingFactory = programOfferingFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the new.
        /// </summary>
        /// <param name="dto">The dto creating new for.</param>
        /// <returns>A <see cref="Rem.Domain.Clinical.ProgramModule.ProgramOffering"/></returns>
        protected override ProgramOffering CreateNew ( ProgramOfferingProfileDto dto )
        {
            Check.IsNotNull ( dto.StartDate, "Start Date is required." );
            Program program;
            Location location;
            TryGetProgram ( dto, out program );
            TryGetLocation ( dto, out location );
            var programOffering = _programOfferingFactory.CreateProgramOffering ( program, location, dto.StartDate.Value );
            _isNew = true;
            return programOffering;
        }

        /// <summary>
        /// Processes the single aggregate.
        /// </summary>
        /// <param name="dto">The dto to process.</param>
        /// <param name="programOffering">The program offering.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        protected override bool ProcessSingleAggregate ( ProgramOfferingProfileDto dto, ProgramOffering programOffering )
        {
            Check.IsNotNull ( dto.StartDate, "Start Date is required." );
            if ( !_isNew )
            {
                Location location;
                if ( TryGetLocation ( dto, out location ) )
                {
                    programOffering.ReviseLocation ( location );
                }
                Program program;
                if ( TryGetProgram ( dto, out program ) )
                {
                    programOffering.ReviseProgram ( program );
                }
                if ( dto.StartDate != null )
                {
                    programOffering.ReviseStartDate ( dto.StartDate.Value );
                }
            }

            programOffering.ReviseEndDate ( dto.EndDate );
            programOffering.ReviseCapacityValue ( dto.CapacityCount );

            return _mappingResult;
        }

        private bool TryGetLocation ( ProgramOfferingProfileDto dto, out Location location )
        {
            Check.IsNotNull ( dto.Location.Key, "Location is required." );
            location = Session.Get<Location> ( dto.Location.Key );
            if ( location == null )
            {
                dto.AddDataErrorInfo (
                    new DataErrorInfo ( "Location not Found.", ErrorLevel.Error, PropertyUtil.ExtractPropertyName ( () => dto.Location ) ) );
                _mappingResult = false;
                return false;
            }
            return true;
        }

        private bool TryGetProgram ( ProgramOfferingProfileDto dto, out Program program )
        {
            Check.IsNotNull ( dto.Program.Key, "Program is required." );
            program = Session.Get<Program> ( dto.Program.Key );
            if ( program == null )
            {
                dto.AddDataErrorInfo (
                    new DataErrorInfo ( "Program not Found.", ErrorLevel.Error, PropertyUtil.ExtractPropertyName ( () => dto.Program ) ) );
                _mappingResult = false;
                return false;
            }
            return true;
        }

        #endregion
    }
}
