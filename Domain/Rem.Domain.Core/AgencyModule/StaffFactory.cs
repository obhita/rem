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

using Pillar.Common.Extension;
using Pillar.Common.Utility;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The StaffFactory implements lifetime management of the <see cref="T:Rem.Domain.Core.AgencyModule.Agency">Agency</see>.
    /// </summary>
    public class StaffFactory : IStaffFactory
    {
        private readonly ILookupValueRepository _lookupValueRepository;
        private readonly IStaffRepository _staffRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffFactory"/> class.
        /// </summary>
        /// <param name="staffRepository">The staff repository.</param>
        /// <param name="lookupValueRepository">The lookup value repository.</param>
        public StaffFactory ( IStaffRepository staffRepository, ILookupValueRepository lookupValueRepository )
        {
            _staffRepository = staffRepository;
            _lookupValueRepository = lookupValueRepository;
        }

        #region IStaffFactory Members

        /// <summary>
        /// Creates the staff.
        /// </summary>
        /// <param name="agency">The agency.</param>
        /// <param name="staffProfile">The staff profile.</param>
        /// <returns>
        /// A Staff
        /// </returns>
        public Staff CreateStaff ( Agency agency, StaffProfile staffProfile)
        {
            var staff = new Staff(agency, staffProfile);

            AddStaffChecklistItemsAndStaffEvents(staff);

            _staffRepository.MakePersistent ( staff );

            return staff;
        }

        /// <summary>
        /// Destroys the staff.
        /// </summary>
        /// <param name="staff">The staff.</param>
        public void DestroyStaff ( Staff staff )
        {
            Check.IsNotNull ( staff, "Staff is required." );
            _staffRepository.MakeTransient ( staff );
        }

        #endregion

        private void AddStaffChecklistItemsAndStaffEvents(Staff staff)
        {
            var staffChecklistItemTypes = _lookupValueRepository.GetAll ( typeof ( StaffChecklistItemType ) );
            staffChecklistItemTypes.ForEach(staffChecklistItem => staff.AddChecklistItem(new StaffChecklistItem((StaffChecklistItemType)staffChecklistItem)));

            var staffEventTypes = _lookupValueRepository.GetAll(typeof(StaffEventType));
            staffEventTypes.ForEach(staffEventType => staff.AddEvent(new StaffEvent((StaffEventType)staffEventType)));
        }
    }
}