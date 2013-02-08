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

using Pillar.Common.Utility;
using Pillar.Domain;

namespace Rem.Domain.Core.AgencyModule
{
    /// <summary>
    /// The LocationFrequentlyAskedQuestion contains a frequently asked question associated to a location.
    /// </summary>
    public class LocationFrequentlyAskedQuestion : LocationAggregateNodeBase, IAggregateNodeValueObject
    {
        private string _answerNote;
        private string _questionNote;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationFrequentlyAskedQuestion"/> class.
        /// </summary>
        protected internal LocationFrequentlyAskedQuestion ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationFrequentlyAskedQuestion"/> class.
        /// </summary>
        /// <param name="questionNote">The question note.</param>
        /// <param name="answerNote">The answer note.</param>
        protected internal LocationFrequentlyAskedQuestion(string questionNote, string answerNote)
        {
            Check.IsNotNullOrWhitespace(questionNote, () => QuestionNote);
            Check.IsNotNullOrWhitespace(answerNote, () => AnswerNote);

            _questionNote = questionNote;
            _answerNote = answerNote;
        }

        /// <summary>
        /// Gets QuestionNote.
        /// </summary>
        [NotNull]
        public virtual string QuestionNote
        {
            get { return _questionNote; }
            private set { ApplyPropertyChange ( ref _questionNote, () => QuestionNote, value ); }
        }

        /// <summary>
        /// Gets AnswerNote.
        /// </summary>
        [NotNull]
        public virtual string AnswerNote
        {
            get { return _answerNote; }
            private set { ApplyPropertyChange ( ref _answerNote, () => AnswerNote, value ); }
        }

            
        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="other">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>             
        public virtual bool ValuesEqual(LocationFrequentlyAskedQuestion other)
        {
            if (other == null)
            {
                return false;
            }

            var valuesEqual = Equals(_questionNote, other.QuestionNote) && Equals(_answerNote, other.AnswerNote);

            return valuesEqual;
        }
    }
}