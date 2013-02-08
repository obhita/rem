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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using Pillar.Common.Utility;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// Static class for getting property name mapping information from AutoMapper.
    /// </summary>
    public static class PropertyNameMapper
    {
        #region Public Methods

        /// <summary>
        /// Gets the destination property names.
        /// </summary>
        /// <param name="sourceType">
        /// Type of the source. 
        /// </param>
        /// <param name="destinationType">
        /// Type of the destination. 
        /// </param>
        /// <param name="sourcePropertyNames">
        /// The source property names. 
        /// </param>
        /// <returns>
        /// List of destination property names. 
        /// </returns>
        public static string[] GetDestinationPropertyNames ( Type sourceType, Type destinationType, params string[] sourcePropertyNames )
        {
            var destinationPropertyNames = new List<string> ();

            var typeMap = Mapper.FindTypeMapFor ( sourceType, destinationType );

            if ( typeMap != null )
            {
                foreach ( var propertyMap in typeMap.GetPropertyMaps ().Where ( m => m.IsMapped () ) )
                {
                    var useDestinationName = false;
                    if ( propertyMap.CustomExpression != null )
                    {
                        var propertyChainMembers = GetPropertyChainMembers ( propertyMap.CustomExpression ).Skip ( 1 );
                        if ( propertyChainMembers.Any ( sourcePropertyNames.Contains ) )
                        {
                            useDestinationName = true;
                        }
                    }
                    else if ( propertyMap.SourceMember != null )
                    {
                        if ( sourcePropertyNames.Contains ( propertyMap.SourceMember.Name ) )
                        {
                            useDestinationName = true;
                        }
                    }

                    if ( useDestinationName )
                    {
                        destinationPropertyNames.Add ( propertyMap.DestinationProperty.Name );
                    }
                }
            }

            return destinationPropertyNames.ToArray ();
        }

        /// <summary>
        /// Gets the name of the distinct destination property.
        /// </summary>
        /// <param name="sourceType">
        /// Type of the source. 
        /// </param>
        /// <param name="destinationType">
        /// Type of the destination. 
        /// </param>
        /// <param name="sourcePropertyName">
        /// Name of the source property. 
        /// </param>
        /// <returns>
        /// Null if no property found, otherwise destination property name. 
        /// </returns>
        public static string GetDistinctDestinationPropertyName ( Type sourceType, Type destinationType, string sourcePropertyName )
        {
            string destinationPropertyName = null;

            var typeMap = Mapper.FindTypeMapFor ( sourceType, destinationType );

            if ( typeMap != null )
            {
                foreach ( var propertyMap in typeMap.GetPropertyMaps ().Where ( m => m.IsMapped () ) )
                {
                    var useDestinationName = false;
                    if ( propertyMap.CustomExpression != null )
                    {
                        var propertyChainMembers = GetPropertyChainMembers ( propertyMap.CustomExpression ).Skip ( 1 ).ToList ();

                        if ( propertyChainMembers.Count > 0 && propertyChainMembers[propertyChainMembers.Count - 1] == sourcePropertyName )
                        {
                            useDestinationName = true;
                        }
                    }
                    else if ( propertyMap.SourceMember != null )
                    {
                        if ( sourcePropertyName == propertyMap.SourceMember.Name )
                        {
                            useDestinationName = true;
                        }
                    }

                    if ( useDestinationName )
                    {
                        destinationPropertyName = propertyMap.DestinationProperty.Name;
                    }
                }
            }

            return destinationPropertyName;
        }

        /// <summary>
        /// Gets the source property chain.
        /// </summary>
        /// <param name="destinationType">
        /// Type of the destination. 
        /// </param>
        /// <param name="destinationPropertyName">
        /// Name of the destination property. 
        /// </param>
        /// <returns>
        /// List of memberInfos of source. 
        /// </returns>
        public static IEnumerable<MemberInfo> GetSourcePropertyChain ( Type destinationType, string destinationPropertyName )
        {
            var memberInfoList = new List<MemberInfo> ();

            var typeMap = Mapper.GetAllTypeMaps ().FirstOrDefault ( t => t.DestinationType == destinationType );

            if ( typeMap != null )
            {
                var propertyMaps = typeMap.GetPropertyMaps ().Where ( m => m.IsMapped () );
                foreach ( var propertyMap in propertyMaps )
                {
                    if ( propertyMap.DestinationProperty.Name == destinationPropertyName )
                    {
                        if ( propertyMap.CustomExpression != null )
                        {
                            memberInfoList.AddRange ( GetPropertyChainMemberInfos ( propertyMap.CustomExpression ).Reverse () );
                        }
                        else if ( propertyMap.SourceMember != null )
                        {
                            memberInfoList.Add ( propertyMap.SourceMember );
                        }
                    }
                }
            }

            return memberInfoList;
        }

        #endregion

        #region Methods

        private static IEnumerable<MemberInfo> GetPropertyChainMemberInfos ( LambdaExpression expr )
        {
            var me = ExpressionTreeWalker.FindFirst<MemberExpression> ( expr );

            while ( me != null )
            {
                yield return me.Member;

                me = me.Expression as MemberExpression;
            }
        }

        private static IEnumerable<string> GetPropertyChainMembers ( LambdaExpression expr )
        {
            var me = ExpressionTreeWalker.FindFirst<MemberExpression> ( expr );

            while ( me != null )
            {
                string propertyName = me.Member.Name;

                yield return propertyName;

                me = me.Expression as MemberExpression;
            }
        }

        #endregion
    }
}
