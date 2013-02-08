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

using System;
using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The StaffTrainingCourse defines a training course for a staff.
    /// </summary>
    public class StaffTrainingCourse : StaffAggregateNodeBase, IAggregateNodeValueObject
    {
        #region Private members

        private DateTime? _completedDate;
        private string _otherTrainingCourseName;
        private TrainingCourse _trainingCourse;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffTrainingCourse"/> class.
        /// </summary>
        protected internal StaffTrainingCourse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffTrainingCourse"/> class.
        /// </summary>
        /// <param name="trainingCourse">The training course.</param>
        /// <param name="otherTrainingCourseName">Name of the other training course.</param>
        /// <param name="completedDate">The completed date.</param>
        public StaffTrainingCourse(TrainingCourse trainingCourse, string otherTrainingCourseName, DateTime? completedDate)
        {
            Check.IsNotNull(trainingCourse, () => TrainingCourse);
            _trainingCourse = trainingCourse;
            _otherTrainingCourseName = otherTrainingCourseName;
            _completedDate = completedDate;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the training course.
        /// </summary>
        [NotNull]
        public virtual TrainingCourse TrainingCourse
        {
            get { return _trainingCourse; }
            private set { ApplyPropertyChange(ref _trainingCourse, () => TrainingCourse, value); }
        }

        /// <summary>
        /// Gets the name of the other training course.
        /// </summary>
        /// <value>
        /// The name of the other training course.
        /// </value>
        public virtual string OtherTrainingCourseName
        {
            get { return _otherTrainingCourseName; }
            private set { ApplyPropertyChange(ref _otherTrainingCourseName, () => OtherTrainingCourseName, value); }
        }

        /// <summary>
        /// Gets the completed date.
        /// </summary>
        public virtual DateTime? CompletedDate
        {
            get { return _completedDate; }
            private set { ApplyPropertyChange(ref _completedDate, () => CompletedDate, value); }
        }

        #endregion

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="staffTrainingCourse">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>               
        public virtual bool ValuesEqual(StaffTrainingCourse staffTrainingCourse)
        {
            if (staffTrainingCourse == null)
            {
                return false;
            }

            var valuesEqual = Equals(_trainingCourse.Key, staffTrainingCourse._trainingCourse.Key)
                              && Equals(_completedDate, staffTrainingCourse._completedDate)
                              && Equals(_otherTrainingCourseName, staffTrainingCourse._otherTrainingCourseName);
            return valuesEqual;
        }
    }
}