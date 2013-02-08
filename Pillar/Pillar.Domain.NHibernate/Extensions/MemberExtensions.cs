using System.Reflection;
using FluentNHibernate;
using Pillar.Common.Extension;

namespace Pillar.Domain.NHibernate.Extensions
{
    /// <summary>
    /// MemberExtensions class.
    /// </summary>
    public static class MemberExtensions
    {
        #region Public Methods

        /// <summary>
        /// Determines whether the specified member is db nullable.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns><c>true</c> if the specified member is db nullable; otherwise, <c>false</c>.</returns>
        public static bool IsDbNullable ( this Member member )
        {
            if ( !member.PropertyType.IsNullable () || !member.MemberInfo.IsDbNullable () )
            {
                return false;
            }

            return true;
        }

        private static bool IsDbNullable ( this MemberInfo memberInfo )
        {
            var notNullAttributes = memberInfo.GetCustomAttributes ( typeof( NotNullAttribute ), false );
            if ( notNullAttributes.Length > 0 )
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
