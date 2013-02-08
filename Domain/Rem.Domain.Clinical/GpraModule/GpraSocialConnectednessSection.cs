#region License

// Open Behavioral Health Information Technology Architecture (OBHITA.org)
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

using Pillar.Domain;

namespace Rem.Domain.Clinical.GpraModule
{
    /// <summary>
    /// The GpraSocialConnectednessSection contains patient social correctness information from the Gpra interview.
    /// </summary>
    [Component]
    public class GpraSocialConnectednessSection : GpraInterviewSectionBase
    {
        private readonly GpraNonResponseType<int?> _attendOtherGroupsCount;
        private readonly GpraNonResponseType<bool?> _attendOtherGroupsIndicator;
        private readonly GpraNonResponseType<int?> _attendReligiousGroupsCount;
        private readonly GpraNonResponseType<bool?> _attendReligiousGroupsIndicator;
        private readonly GpraNonResponseType<int?> _attendVoluntaryGroupsCount;
        private readonly GpraNonResponseType<bool?> _attendVoluntaryGroupsIndicator;
        private readonly GpraNonResponseType<GpraTroubleContact> _gpraTroubleContact;
        private readonly string _gpraTroubleContactSpecificationNote;
        private readonly GpraNonResponseType<bool?> _interactFamilyFriendsIndicator;

        private GpraSocialConnectednessSection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GpraSocialConnectednessSection"/> class.
        /// </summary>
        /// <param name="attendOtherGroupsCount">The attend other groups count.</param>
        /// <param name="attendOtherGroupsIndicator">The attend other groups indicator.</param>
        /// <param name="attendReligiousGroupsCount">The attend religious groups count.</param>
        /// <param name="attendReligiousGroupsIndicator">The attend religious groups indicator.</param>
        /// <param name="attendVoluntaryGroupsCount">The attend voluntary groups count.</param>
        /// <param name="attendVoluntaryGroupsIndicator">The attend voluntary groups indicator.</param>
        /// <param name="gpraTroubleContact">The Gpra trouble contact.</param>
        /// <param name="gpraTroubleContactSpecificationNote">The Gpra trouble contact specification note.</param>
        /// <param name="interactFamilyFriendsIndicator">The interact family friends indicator.</param>
        public GpraSocialConnectednessSection(GpraNonResponseType<int?> attendOtherGroupsCount,
                                              GpraNonResponseType<bool?> attendOtherGroupsIndicator,
                                              GpraNonResponseType<int?> attendReligiousGroupsCount,
                                              GpraNonResponseType<bool?> attendReligiousGroupsIndicator,
                                              GpraNonResponseType<int?> attendVoluntaryGroupsCount,
                                              GpraNonResponseType<bool?> attendVoluntaryGroupsIndicator,
                                              GpraNonResponseType<GpraTroubleContact> gpraTroubleContact,
                                              string gpraTroubleContactSpecificationNote,
                                              GpraNonResponseType<bool?> interactFamilyFriendsIndicator
            )
        {
            _attendOtherGroupsCount = attendOtherGroupsCount;
            _attendOtherGroupsIndicator = attendOtherGroupsIndicator;
            _attendReligiousGroupsCount = attendReligiousGroupsCount;
            _attendReligiousGroupsIndicator = attendReligiousGroupsIndicator;
            _attendVoluntaryGroupsCount = attendVoluntaryGroupsCount;
            _attendVoluntaryGroupsIndicator = attendVoluntaryGroupsIndicator;
            _gpraTroubleContact = gpraTroubleContact;
            _gpraTroubleContactSpecificationNote = gpraTroubleContactSpecificationNote;
            _interactFamilyFriendsIndicator = interactFamilyFriendsIndicator;
        }

        /// <summary>
        /// Gets a boolean value indicating attendance to voluntary groups.
        /// </summary>
        public virtual GpraNonResponseType<bool?> AttendVoluntaryGroupsIndicator
        {
            get { return _attendVoluntaryGroupsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the attend voluntary groups count.
        /// </summary>
        public virtual GpraNonResponseType<int?> AttendVoluntaryGroupsCount
        {
            get { return _attendVoluntaryGroupsCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating attendance to religious groups.
        /// </summary>
        public virtual GpraNonResponseType<bool?> AttendReligiousGroupsIndicator
        {
            get { return _attendReligiousGroupsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the attend religious groups count.
        /// </summary>
        public virtual GpraNonResponseType<int?> AttendReligiousGroupsCount
        {
            get { return _attendReligiousGroupsCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating attendance to other groups.
        /// </summary>
        public virtual GpraNonResponseType<bool?> AttendOtherGroupsIndicator
        {
            get { return _attendOtherGroupsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the attend other groups count.
        /// </summary>
        public virtual GpraNonResponseType<int?> AttendOtherGroupsCount
        {
            get { return _attendOtherGroupsCount; }
            private set { }
        }

        /// <summary>
        /// Gets a boolean value indicating interaction with family and friends.
        /// </summary>
        public virtual GpraNonResponseType<bool?> InteractFamilyFriendsIndicator
        {
            get { return _interactFamilyFriendsIndicator; }
            private set { }
        }

        /// <summary>
        /// Gets the <see
        /// cref="T:Rem.Domain.Clinical.GpraModule.GpraTroubleContact">GpraTroubleContact</see>
        /// denoting patient trouble contacts.
        /// </summary>
        public virtual GpraNonResponseType<GpraTroubleContact> GpraTroubleContact
        {
            get { return _gpraTroubleContact; }
            private set { }
        }

        /// <summary>
        /// Gets the Gpra trouble contact specification note.
        /// </summary>
        public virtual string GpraTroubleContactSpecificationNote
        {
            get { return _gpraTroubleContactSpecificationNote; }
            private set { }
        }
    }
}