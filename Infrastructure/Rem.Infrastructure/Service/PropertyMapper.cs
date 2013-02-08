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
using System.Linq.Expressions;
using System.Reflection;
using Pillar.Common.Utility;
using Pillar.Domain;
using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// The <see cref="PropertyMapper&lt;TEntity&gt;"/> maps the properties of an entity to their corresponding values.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class PropertyMapper<TEntity>
        where TEntity : IEntity
    {
        #region Constants and Fields

        private readonly TEntity _entity;
        private readonly IList<Tuple<PropertyInfo, object>> _propertyValueList;
        private readonly IValidatedObject _validatedObject;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMapper&lt;TEntity&gt;"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="validatedObject">The validated object.</param>
        public PropertyMapper (
            TEntity entity, 
            IValidatedObject validatedObject )
        {
            Check.IsNotNull ( entity, "entity is required" );

            _entity = entity;
            _validatedObject = validatedObject;
            _propertyValueList = new List<Tuple<PropertyInfo, object>> ();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Maps the properties of an entity to their corresponding values.
        /// </summary>
        /// <returns>Returns 'true', if the mapping is successful, else returns 'false'.</returns>
        public bool Map ()
        {
            var result = true;

            foreach ( var tuple in _propertyValueList )
            {
                var propertyInfo = tuple.Item1;
                var propertyValue = tuple.Item2;

                if ( propertyInfo.CanWrite && propertyInfo.GetSetMethod ( false ) != null )
                {
                    result &= TryMapProperty ( propertyInfo, propertyValue );
                }
                else
                {
                    throw new ArgumentException (
                        string.Format (
                            "Property {0}.{1} is read only", 
                            typeof( TEntity ).Name, 
                            propertyInfo.Name ) );
                }
            }

            return result;
        }

        /// <summary>
        /// Maps the property with a value.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression, that represents the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <returns>Returns an instance of <see cref="PropertyMapper&lt;TEntity&gt;"/> to support fluent style method chaining.</returns>
        public PropertyMapper<TEntity> MapProperty<TProperty> (
            Expression<Func<TEntity, TProperty>> propertyExpression, 
            TProperty propertyValue )
        {
            PropertyInfo propertyInfo = PropertyUtil.ExtractProperty ( propertyExpression );
            _propertyValueList.Add ( new Tuple<PropertyInfo, object> ( propertyInfo, propertyValue ) );

            return this;
        }

        #endregion


        private bool TryMapProperty<TProperty> ( PropertyInfo propertyInfo, TProperty propertyValue )
        {
            var result = true;

            try
            {
                MethodInfo setter = propertyInfo.GetSetMethod ();
                setter.Invoke ( _entity, new object[] { propertyValue } );
            }
            catch ( TargetInvocationException e )
            {
                result = false;
                _validatedObject.AddDataErrorInfo ( new DataErrorInfo ( e.InnerException.Message, ErrorLevel.Error, propertyInfo.Name ) );
            }

            return result;
        }
    }
}
