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
namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// StaffHrBuilder provides a fluent interface for creating a StaffHr.
    /// </summary>
    public class StaffHrBuilder
    {
        private string _confidentialNote;
        private EmploymentType _employmentType;
        private Staff _supervisorStaff;
        private string _titleName;

        /// <summary>
        /// Performs an implicit conversion from <see cref="StaffHrBuilder"/> to <see cref="StaffHr"/>.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator StaffHr(StaffHrBuilder builder)
        {
            return builder.Build();
        }

        /// <summary>
        /// Assigns the type of the employment.
        /// </summary>
        /// <param name="employmentType">Type of the employment.</param>
        /// <returns>A StaffHrBuilder.</returns>
        public StaffHrBuilder WithEmploymentType(EmploymentType employmentType)
        {
            _employmentType = employmentType;
            return this;
        }

        /// <summary>
        /// Assigns the name of the title.
        /// </summary>
        /// <param name="titleName">Name of the title.</param>
        /// <returns>A StaffHrBuilder.</returns>
        public StaffHrBuilder WithTitleName(string titleName)
        {
            _titleName = titleName;
            return this;
        }

        /// <summary>
        /// Assigns the supervisor staff.
        /// </summary>
        /// <param name="supervisorStaff">The supervisor staff.</param>
        /// <returns>A StaffHrBuilder.</returns>
        public StaffHrBuilder WithSupervisorStaff(Staff supervisorStaff)
        {
            _supervisorStaff = supervisorStaff;
            return this;
        }

        /// <summary>
        /// Assigns the confidential note.
        /// </summary>
        /// <param name="confidentialNote">The confidential note.</param>
        /// <returns>A StaffHrBuilder.</returns>
        public StaffHrBuilder WithConfidentialNote(string confidentialNote)
        {
            _confidentialNote = confidentialNote;
            return this;
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A StaffHr.</returns>
        public StaffHr Build()
        {
            return new StaffHr(_employmentType, _titleName, _supervisorStaff, _confidentialNote);
        }
    }
}