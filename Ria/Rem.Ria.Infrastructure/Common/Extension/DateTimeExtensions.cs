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
using System.Globalization;

namespace Rem.Ria.Infrastructure.Common.Extension
{
    /// <summary>
    /// DateTimeExtensions class.
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Public Methods

        /// <summary>
        /// Returns the first day of the week that the specified date
        /// is in.
        /// </summary>
        /// <param name="dayInWeek">The day in week.</param>
        /// <param name="cultureInfo">The culture info.</param>
        /// <returns>A <see cref="System.DateTime"/></returns>
        public static DateTime GetFirstDayOfWeek ( this DateTime dayInWeek, CultureInfo cultureInfo )
        {
            var firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            var firstDayInWeek = dayInWeek.Date;
            while ( firstDayInWeek.DayOfWeek != firstDay )
            {
                firstDayInWeek = firstDayInWeek.AddDays ( -1 );
            }

            return firstDayInWeek;
        }

        /// <summary>
        /// Returns the first day of the week that the specified
        /// date is in using the current culture.
        /// </summary>
        /// <param name="dayInWeek">The day in week.</param>
        /// <returns>A <see cref="System.DateTime"/></returns>
        public static DateTime GetFirstDayOfWeek ( this DateTime dayInWeek )
        {
            var cultureInfo = CultureInfo.CurrentCulture;
            var firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            var firstDayInWeek = dayInWeek.Date;
            while ( firstDayInWeek.DayOfWeek != firstDay )
            {
                firstDayInWeek = firstDayInWeek.AddDays ( -1 );
            }

            return firstDayInWeek;
        }

        #endregion
    }
}
