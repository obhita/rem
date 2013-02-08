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

using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.ReportsModule
{
    /// <summary>
    ///  Report information along with its SystemsResource name (Uri).
    /// </summary>
    public class Report : AuditableAggregateRootBase
    {
        private string _description;
        private string _name;
        private string _resourceName;

        /// <summary>
        ///   Display name for the report
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            private set { ApplyPropertyChange ( ref _name, () => Name, value ); }
        }

        /// <summary>
        ///   Description for the report
        /// </summary>
        public virtual string Description
        {
            get { return _description; }
            private set { ApplyPropertyChange ( ref _description, () => Description, value ); }
        }

        /// <summary>
        ///  Resource name as in SystemResource. SecurityModule.SystemResource type is not used to minimize dependency. This has the Report Uri.
        /// </summary>
        public virtual string ResourceName
        {
            get { return _resourceName; }
            private set { ApplyPropertyChange ( ref _resourceName, () => ResourceName, value ); }
        }

        /// <summary>
        /// Renames the report.
        /// </summary>
        /// <param name="name">The name.</param>
        public virtual void Rename ( string name )
        {
            _name = name;
        }

        /// <summary>
        /// Revises the report description.
        /// </summary>
        /// <param name="description">The description.</param>
        public virtual void ReviseDescription ( string description )
        {
            _description = description;
        }

        /// <summary>
        /// Revises the report uri resource.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        public virtual void ReviseResourceName ( string resourceName )
        {
            _resourceName = resourceName;
        }
    }
}