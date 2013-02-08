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

namespace Rem.Ria.PatientModule.Web.TedsInterview
{
    /// <summary>
    /// Class to populate default TedsAnswers
    /// </summary>
    public static class TedsAnswerPopulator
    {
        /// <summary>
        /// Popupates the teds answers.
        /// </summary>
        /// <typeparam name="T">The generic type parameter.</typeparam>
        /// <param name="dto">The dto.</param>
        /// <returns>An intance of T.</returns>
        public static T PopupateTedsAnswers<T>(T dto) where T : class 
        {
            if (dto == null)
            {
                return null;
            }

            var propertyInfos = typeof(T).GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                if (!propertyInfo.CanRead || !propertyInfo.CanWrite)
                {
                    continue;
                }

                var type = propertyInfo.PropertyType;
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(TedsAnswerDto<>))
                {
                    var value = propertyInfo.GetValue(dto, null);
                    if (value == null)
                    {
                        value = Activator.CreateInstance(type, true);

                        propertyInfo.SetValue(dto, value, null);
                    }
                }
            }

            return dto;
        }
    }
}
