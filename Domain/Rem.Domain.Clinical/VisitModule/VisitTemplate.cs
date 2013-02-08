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

using System.Collections.Generic;
using System.Linq;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Domain.Core.AgencyModule;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// VisitTemplate serves as a base pattern for a set of activities.
    /// </summary>
    public class VisitTemplate : AuditableAggregateRootBase
    {
        private readonly IList<VisitTemplateActivityType> _activityTypes;
        private readonly Agency _agency;
        private string _cptCode;
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitTemplate"/> class.
        /// </summary>
        protected internal VisitTemplate ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisitTemplate"/> class.
        /// </summary>
        /// <param name="agency">The agency.</param>
        /// <param name="name">The name.</param>
        /// <param name="cptCode">The CPT code.</param>
        protected internal VisitTemplate ( Agency agency, string name, string cptCode )
        {
            Check.IsNotNull ( agency, "Agency is required." );
            Check.IsNotNullOrWhitespace ( name, "Name is required." );
            Check.IsNotNullOrWhitespace ( cptCode, "CptCode is required." );

            _name = name;
            _cptCode = cptCode;
            _agency = agency;
            _activityTypes = new List<VisitTemplateActivityType> ();
        }

        /// <summary>
        /// Gets the agency.
        /// </summary>
        [NotNull]
        public virtual Agency Agency
        {
            get { return _agency; }
            private set { }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        [NotNull]
        public virtual string Name
        {
            get { return _name; }
            private set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        /// Gets the CPT code.
        /// </summary>
        [NotNull]
        public virtual string CptCode
        {
            get { return _cptCode; }
            private set { ApplyPropertyChange ( ref _cptCode, () => CptCode, value ); }
        }

        /// <summary>
        /// Gets the activity types.
        /// </summary>
        public virtual IEnumerable<VisitTemplateActivityType> ActivityTypes
        {
            get { return _activityTypes.ToList ().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Renames the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void Rename ( string name )
        {
            Check.IsNotNullOrWhitespace ( name, "Name is required." );

            Name = name;
        }

        /// <summary>
        /// Revises the CPT code.
        /// </summary>
        /// <param name="cptCode">The CPT code.</param>
        public virtual void ReviseCptCode ( string cptCode )
        {
            Check.IsNotNullOrWhitespace ( cptCode, "CptCode is required." );

            CptCode = cptCode;
        }

        /// <summary>
        /// Adds the type of the activity.
        /// </summary>
        /// <returns>A VisitTemplateActivityType.</returns>
        public virtual VisitTemplateActivityType AddActivityType ()
        {
            var activityType = new VisitTemplateActivityType ( this );
            _activityTypes.Add ( activityType );

            NotifyItemAdded ( () => ActivityTypes, activityType );
            return activityType;
        }

        #region Overrides

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString ()
        {
            return Name;
        }

        #endregion
    }
}