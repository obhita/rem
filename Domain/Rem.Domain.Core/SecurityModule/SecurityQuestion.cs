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
using Pillar.Domain;

namespace Rem.Domain.Core.SecurityModule
{
    /// <summary>
    /// SecurityQuestion defines a secret question and answer used to test authentication of a user.
    /// </summary>
    public class SecurityQuestion : SystemAccountAggregateNodeBase, IAggregateNodeValueObject
    {
        private string _questionNote;
        private string _answerNote;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityQuestion"/> class.
        /// </summary>
        protected internal SecurityQuestion()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityQuestion"/> class.
        /// </summary>
        /// <param name="question">The question.</param>
        /// <param name="answer">The answer.</param>
        public SecurityQuestion ( 
            string question,
            string answer)
        {
            Check.IsNotNull ( question, "Question is required." );
            Check.IsNotNullOrWhitespace(answer, "Answer is required.");

            _questionNote = question;
            _answerNote = answer;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the question note.
        /// </summary>
        [NotNull]
        public virtual string QuestionNote
        {
            get { return _questionNote; }
            private set { ApplyPropertyChange ( ref _questionNote, () => QuestionNote, value ); }
        }

        /// <summary>
        /// Gets the answer note.
        /// </summary>
        [NotNull]
        public virtual string AnswerNote
        {
            get { return _answerNote; }
            private set { ApplyPropertyChange ( ref _answerNote, () => AnswerNote, value ); }
        }

        #endregion

        /// <summary>
        /// Determines if the values are equal.
        /// </summary>
        /// <param name="securityQuestion">
        /// The other object.
        /// </param>
        /// <returns>
        /// A boolean denoting equality of the values.
        /// </returns>           
        public virtual bool ValuesEqual(SecurityQuestion securityQuestion)
        {
            if (securityQuestion == null)
            {
                return false;
            }

            var valuesEqual =
                Equals(_questionNote, securityQuestion._questionNote) &&
                Equals(_answerNote, securityQuestion._answerNote);

            return valuesEqual;
        }
    }
}