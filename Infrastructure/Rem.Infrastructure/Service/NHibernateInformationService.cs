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
using System.Reflection;
using NHibernate;
using NHibernate.Persister.Entity;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Infrastructure.Service.DataTransferObject;
using StructureMap;
using uNhAddIns.SessionEasier;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// Service that gets NHibernate database information.
    /// </summary>
    public class NHibernateInformationService : INHibernateInformationService
    {
        #region Constants and Fields

        private readonly ISessionFactory _sessionFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateInformationService"/> class.
        /// </summary>
        /// <param name="sessionFactoryProvider">
        /// The session factory provider. 
        /// </param>
        [DefaultConstructor]
        public NHibernateInformationService ( ISessionFactoryProvider sessionFactoryProvider )
        {
            _sessionFactory = sessionFactoryProvider.GetFactory ( null );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NHibernateInformationService"/> class.
        /// </summary>
        /// <param name="sessionFactory">
        /// The session factory. 
        /// </param>
        public NHibernateInformationService ( ISessionFactory sessionFactory )
        {
            _sessionFactory = sessionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the property chain database information.
        /// </summary>
        /// <param name="entityType">
        /// Type of the entity. 
        /// </param>
        /// <param name="memberChain">
        /// The member chain. 
        /// </param>
        /// <returns>
        /// A <see cref="DbInfoDto"/> 
        /// </returns>
        public DbInfoDto GetPropertyChainDatabaseInformation ( Type entityType, IEnumerable<MemberInfo> memberChain )
        {
            Check.IsNotNull ( entityType, "entityType is required." );

            var dbInfo = new DbInfoDto ();

            var reversedMemberChainList = memberChain.ToList ();
            reversedMemberChainList.Reverse ();

            string propertyPath = null;

            foreach ( var memberInfo in reversedMemberChainList )
            {
                if ( propertyPath == null )
                {
                    propertyPath = memberInfo.Name;
                }
                else
                {
                    propertyPath = memberInfo.Name + "." + propertyPath;
                }

                var parentType = memberInfo.DeclaringType;

                if ( typeof( IAggregateRoot ).IsAssignableFrom ( parentType ) || typeof( IAggregateNode ).IsAssignableFrom ( parentType ) )
                {
                    dbInfo = GetPropertyDatabaseInformation ( parentType, propertyPath );
                    break;
                }

                if ( !( parentType.GetCustomAttributes ( typeof( ComponentAttribute ), false ).Count () > 0 ) )
                {
                    throw new ArgumentException ( "Member Chain for mapping contains an invalid member." );
                }
            }

            return dbInfo;
        }

        /// <summary>
        /// Gets the property database information.
        /// </summary>
        /// <param name="entityType">
        /// Type of the entity. 
        /// </param>
        /// <param name="propertyPath">
        /// The property path. 
        /// </param>
        /// <returns>
        /// A <see cref="DbInfoDto"/> 
        /// </returns>
        public DbInfoDto GetPropertyDatabaseInformation ( Type entityType, string propertyPath )
        {
            Check.IsNotNull ( entityType, "entityType is required." );
            Check.IsNotNullOrWhitespace ( propertyPath, "propertyPath cannot be empty or whitespace." );

            var classMapping = _sessionFactory.GetClassMetadata ( entityType );
            var persister = classMapping as AbstractEntityPersister;
            var dbInfo = new DbInfoDto ();

            if ( persister != null )
            {
                dbInfo.Table = persister.TableName;
                dbInfo.Column = string.Join ( ",", persister.GetPropertyColumnNames ( propertyPath ) );
                dbInfo.DataType = persister.GetPropertyType ( propertyPath ).Name;
            }

            return dbInfo;
        }

        #endregion
    }
}
