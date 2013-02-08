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
using Pillar.Common.Extension;
using Pillar.Common.Utility;

namespace Rem.Domain.Clinical.LabModule
{
    /// <summary>
    /// LabTest accepts information related to a laboratory test or result.
    /// </summary>
    public class LabTest : LabSpecimenAggregateNodeBase
    {
        private readonly IList<LabResult> _labResults;
        private LabTestInfo _labTestInfo;

        internal LabTest()
        {
            _labResults = new List<LabResult>();
        }

        /// <summary>
        /// Gets the lab results.
        /// </summary>
        public virtual IList<LabResult> LabResults
        {
            get { return _labResults; }
            private set { }
        }

        /// <summary>
        /// Gets the lab test info.
        /// </summary>
        public virtual LabTestInfo LabTestInfo
        {
            get { return _labTestInfo; }
            private set { ApplyPropertyChange ( ref _labTestInfo, () => LabTestInfo, value ); }
        }

        /// <summary>
        /// Revises the lab test info.
        /// </summary>
        /// <param name="labTestInfo">The lab test info.</param>
        public virtual void ReviseLabTestInfo(LabTestInfo labTestInfo)
        {
            Check.IsNotNull ( labTestInfo, () => LabTestInfo );

            LabTestInfo = labTestInfo;
        }

        /// <summary>
        /// Adds the lab result.
        /// </summary>
        /// <param name="labResult">The lab result.</param>
        public virtual void AddLabResult( LabResult labResult)
        {
            Check.IsNotNull ( labResult, "Lab Result is required." );

            labResult.LabTest = this;
            _labResults.Add(labResult);

            NotifyItemAdded(() => LabResults, labResult);
        }

        /// <summary>
        /// Removes the lab result.
        /// </summary>
        /// <param name="labResult">The lab result.</param>
        public virtual void RemoveLabResult(LabResult labResult)
        {
            Check.IsNotNull(labResult, "Lab Result is required.");

            _labResults.Delete(labResult);

            NotifyItemRemoved(() => LabResults, labResult);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return
                string.Format("({0}) - {1}", _labTestInfo == null ? string.Empty : _labTestInfo.LabTestName.ToString (), _labResults == null ? string.Empty : _labResults.ToString()).Trim();
        }
    }
}