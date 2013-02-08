using System;
using System.Linq.Expressions;
using Pillar.Common.Metadata.Dtos;
using Pillar.Common.Utility;

namespace Pillar.Common.Metadata
{
    /// <summary>
    /// MetadataExtensions class.
    /// </summary>
    public static class MetadataExtensions
    {
        #region Public Methods

        /// <summary>
        /// Filters the LKP.
        /// </summary>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <param name="wellKnownNames">The well known names.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool FilterLkp ( this IMetadataProvider metadataProvider, params string[] wellKnownNames )
        {
            return HandleMetadataItemDto (
                metadataProvider.MetadataDto,
                metadataProvider.GetType ().FullName,
                true,
                new FilterLookupMetadataItemDto { WellKnownNames = wellKnownNames } );
        }

        /// <summary>
        /// Filters the LKP.
        /// </summary>
        /// <typeparam name="TProvider">The type of the provider.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="wellKnownNames">The well known names.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool FilterLkp<TProvider, TProperty> (
            this TProvider metadataProvider, Expression<Func<TProvider, TProperty>> propertyExpression, params string[] wellKnownNames )
            where TProvider : IMetadataProvider
        {
            if (typeof(IMetadataProvider).IsAssignableFrom(typeof(TProperty)))
            {
                var metaDataProvider = propertyExpression.Compile().Invoke(metadataProvider) as IMetadataProvider;
                if (metaDataProvider == null)
                {
                    return false;
                }
                return FilterLkp(metaDataProvider);
            }
            var propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );
            return HandleMetadataItemDto (
                metadataProvider.MetadataDto, propertyName, true, new FilterLookupMetadataItemDto { WellKnownNames = wellKnownNames } );
        }

        /// <summary>
        /// Hides the specified metadata provider.
        /// </summary>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool Hide ( this IMetadataProvider metadataProvider )
        {
            return HandleMetadataItemDto (
                metadataProvider.MetadataDto, metadataProvider.GetType ().FullName, true, new HiddenMetadataItemDto { IsHidden = true } );
        }

        /// <summary>
        /// Hides the specified metadata provider.
        /// </summary>
        /// <typeparam name="TProvider">The type of the provider.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool Hide<TProvider, TProperty> ( this TProvider metadataProvider, Expression<Func<TProvider, TProperty>> propertyExpression )
            where TProvider : IMetadataProvider
        {
            if (typeof(IMetadataProvider).IsAssignableFrom(typeof(TProperty)))
            {
                var metaDataProvider = propertyExpression.Compile ().Invoke ( metadataProvider ) as IMetadataProvider;
                if (metaDataProvider == null)
                {
                    return false;
                }
                return Hide( metaDataProvider);
            }
            var propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );
            return HandleMetadataItemDto ( metadataProvider.MetadataDto, propertyName, true, new HiddenMetadataItemDto { IsHidden = true } );
        }

        /// <summary>
        /// Disables the specified metadata provider.
        /// </summary>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool Disable(this IMetadataProvider metadataProvider)
        {
            return HandleMetadataItemDto (
                metadataProvider.MetadataDto, metadataProvider.GetType().FullName, true, new DisabledMetadataItemDto { IsDisabled = true });
        }

        /// <summary>
        /// Disables the specified metadata provider.
        /// </summary>
        /// <typeparam name="TProvider">The type of the provider.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool Disable<TProvider, TProperty>(this TProvider metadataProvider, Expression<Func<TProvider, TProperty>> propertyExpression)
                    where TProvider : IMetadataProvider
        {
            if (typeof(IMetadataProvider).IsAssignableFrom(typeof(TProperty)))
            {
                var metaDataProvider = propertyExpression.Compile().Invoke(metadataProvider) as IMetadataProvider;
                if (metaDataProvider == null)
                {
                    return false;
                }
                return Disable(metaDataProvider);
            }
            var propertyName = PropertyUtil.ExtractPropertyName(propertyExpression);
            return HandleMetadataItemDto(metadataProvider.MetadataDto, propertyName, true, new DisabledMetadataItemDto { IsDisabled = true });
        }


        /// <summary>
        /// Enables the specified metadata provider.
        /// </summary>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool Enable(this IMetadataProvider metadataProvider)
        {
            return HandleMetadataItemDto<DisabledMetadataItemDto>(metadataProvider.MetadataDto, metadataProvider.GetType().FullName, false, null);
        }

        /// <summary>
        /// Enables the specified metadata provider.
        /// </summary>
        /// <typeparam name="TProvider">The type of the provider.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool Enable<TProvider, TProperty>(this TProvider metadataProvider, Expression<Func<TProvider, TProperty>> propertyExpression)
                    where TProvider : IMetadataProvider
        {
            if (typeof(IMetadataProvider).IsAssignableFrom(typeof(TProperty)))
            {
                var metaDataProvider = propertyExpression.Compile().Invoke(metadataProvider) as IMetadataProvider;
                if (metaDataProvider == null)
                {
                    return false;
                }
                return Enable(metaDataProvider);
            }
            var propertyName = PropertyUtil.ExtractPropertyName(propertyExpression);
            return HandleMetadataItemDto<DisabledMetadataItemDto>(metadataProvider.MetadataDto, propertyName, false, null);
        }

        /// <summary>
        /// Determines whether [is not read only] [the specified metadata provider].
        /// </summary>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <returns><c>true</c> if [is not read only] [the specified metadata provider]; otherwise, <c>false</c>.</returns>
        public static bool IsNotReadOnly ( this IMetadataProvider metadataProvider )
        {
            return HandleMetadataItemDto<ReadonlyMetadataItemDto> ( metadataProvider.MetadataDto, metadataProvider.GetType ().FullName, false, null );
        }

        /// <summary>
        /// Determines whether [is not read only] [the specified metadata provider].
        /// </summary>
        /// <typeparam name="TProvider">The type of the provider.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns><c>true</c> if [is not read only] [the specified metadata provider]; otherwise, <c>false</c>.</returns>
        public static bool IsNotReadOnly<TProvider, TProperty> (
            this TProvider metadataProvider, Expression<Func<TProvider, TProperty>> propertyExpression )
            where TProvider : IMetadataProvider
        {
            if (typeof(IMetadataProvider).IsAssignableFrom(typeof(TProperty)))
            {
                var metaDataProvider = propertyExpression.Compile().Invoke(metadataProvider) as IMetadataProvider;
                if (metaDataProvider == null)
                {
                    return false;
                }
                return IsNotReadOnly(metaDataProvider);
            }
            var propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );
            return HandleMetadataItemDto<ReadonlyMetadataItemDto> ( metadataProvider.MetadataDto, propertyName, false, null );
        }

        /// <summary>
        /// Determines whether [is read-only] [the specified metadata provider].
        /// </summary>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <returns><c>true</c> if [is ready only] [the specified metadata provider]; otherwise, <c>false</c>.</returns>
        public static bool IsReadOnly ( this IMetadataProvider metadataProvider )
        {
            return HandleMetadataItemDto (
                metadataProvider.MetadataDto, metadataProvider.GetType ().FullName, true, new ReadonlyMetadataItemDto { IsReadonly = true } );
        }

        /// <summary>
        /// Determines whether [is read-only] [the specified metadata provider].
        /// </summary>
        /// <typeparam name="TProvider">The type of the provider.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns><c>true</c> if [is ready only] [the specified metadata provider]; otherwise, <c>false</c>.</returns>
        public static bool IsReadOnly<TProvider, TProperty> (
            this TProvider metadataProvider, Expression<Func<TProvider, TProperty>> propertyExpression )
            where TProvider : IMetadataProvider
        {
            if (typeof(IMetadataProvider).IsAssignableFrom(typeof(TProperty)))
            {
                var metaDataProvider = propertyExpression.Compile().Invoke(metadataProvider) as IMetadataProvider;
                if (metaDataProvider == null)
                {
                    return false;
                }
                return IsReadOnly(metaDataProvider);
            }
            var propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );
            return HandleMetadataItemDto ( metadataProvider.MetadataDto, propertyName, true, new ReadonlyMetadataItemDto { IsReadonly = true } );
        }

        /// <summary>
        /// Shows the specified metadata provider.
        /// </summary>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool Show ( this IMetadataProvider metadataProvider )
        {
            return HandleMetadataItemDto<HiddenMetadataItemDto> ( metadataProvider.MetadataDto, metadataProvider.GetType ().FullName, false, null );
        }

        /// <summary>
        /// Shows the specified metadata provider.
        /// </summary>
        /// <typeparam name="TProvider">The type of the provider.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool Show<TProvider, TProperty> ( this TProvider metadataProvider, Expression<Func<TProvider, TProperty>> propertyExpression )
            where TProvider : IMetadataProvider
        {
            if ( typeof(IMetadataProvider).IsAssignableFrom ( typeof(TProperty) ))
            {
                var metaDataProvider = propertyExpression.Compile().Invoke(metadataProvider) as IMetadataProvider;
                if (metaDataProvider == null)
                {
                    return false;
                }
                return Show ( metaDataProvider );
            }
            var propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );
            return HandleMetadataItemDto<HiddenMetadataItemDto> ( metadataProvider.MetadataDto, propertyName, false, null );
        }

        /// <summary>
        /// Uns the filter LKP.
        /// </summary>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool UnFilterLkp ( this IMetadataProvider metadataProvider )
        {
            return HandleMetadataItemDto<FilterLookupMetadataItemDto> (
                metadataProvider.MetadataDto, metadataProvider.GetType ().FullName, false, null );
        }

        /// <summary>
        /// Uns the filter LKP.
        /// </summary>
        /// <typeparam name="TProvider">The type of the provider.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="metadataProvider">The metadata provider.</param>
        /// <param name="propertyExpression">The property expression.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        public static bool UnFilterLkp<TProvider, TProperty> (
            this TProvider metadataProvider, Expression<Func<TProvider, TProperty>> propertyExpression )
            where TProvider : IMetadataProvider
        {
            if (typeof(IMetadataProvider).IsAssignableFrom(typeof(TProperty)))
            {
                var metaDataProvider = propertyExpression.Compile().Invoke(metadataProvider) as IMetadataProvider;
                if (metaDataProvider == null)
                {
                    return false;
                }
                return UnFilterLkp(metaDataProvider);
            }
            var propertyName = PropertyUtil.ExtractPropertyName ( propertyExpression );
            return HandleMetadataItemDto<FilterLookupMetadataItemDto> ( metadataProvider.MetadataDto, propertyName, false, null );
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the metadata item dto.
        /// </summary>
        /// <typeparam name="T">The type of metadataitemdto.</typeparam>
        /// <param name="metadataDto">The metadata dto.</param>
        /// <param name="resourceName">Name of the resource.</param>
        /// <param name="apply">If set to <c>true</c> [apply].</param>
        /// <param name="metaDataItemToApply">The meta data item to apply.</param>
        /// <returns>A <see cref="System.Boolean"/></returns>
        private static bool HandleMetadataItemDto<T> ( this MetadataDto metadataDto, string resourceName, bool apply, T metaDataItemToApply )
            where T : IMetadataItemDto
        {
            var done = false;
            var metaDataToApply = metadataDto;
            if ( metaDataToApply.ResourceName != resourceName )
            {
                metaDataToApply = metadataDto.GetChildMetadata ( resourceName );
            }

            var exists = metaDataToApply.FindMetadataItem<T> () != null;
            if ( !apply && exists )
            {
                metaDataToApply.RemoveMetadataItem<T> ();
                done = true;
            }

            if ( apply && !exists )
            {
                metaDataToApply.AddMetadataItem ( metaDataItemToApply );
                done = true;
            }

            return done;
        }

        #endregion
    }
}
