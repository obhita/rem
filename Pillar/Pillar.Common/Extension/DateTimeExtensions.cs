using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
#if !SILVERLIGHT

#endif

namespace Pillar.Common.Extension
{
    /// <summary>
    /// Extension methods for the <see cref="DateTime"/> type.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gets the age based on Birthday.
        /// </summary>
        /// <param name="birthday">The birth date.</param>
        /// <returns>A integer for age.</returns>
        public static int GetAge ( this DateTime birthday )
        {
            var today = DateTime.Today;

            var age = today.Year - birthday.Year;
            if (today.Month < birthday.Month)
            {
                age = age - 1;
            }
            else if (today.Month == birthday.Month)
            {
                if (today.Day < birthday.Day)
                {
                    age = age - 1;
                }
            }

            return age;
        }
    }
}
