using AutoMapper;
using Rem.Domain.Clinical.DensAsiModule;
using Rem.Domain.Clinical.VisitModule;
using Rem.Infrastructure.Service.DataTransferObject.Mapping;
using Rem.Ria.PatientModule.Web.DensAsiInterview;

namespace Rem.Ria.PatientModule.Web.Common
{
    /// <summary>
    /// ActivityAutoMapperSetup Class.
    /// </summary>
    public class ActivityAutoMapperSetup
    {
        #region Public Methods

        /// <summary>
        /// Creates the map to activity dto.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <returns>A <see cref="AutoMapper.IMappingExpression&lt;TSource,TDestination&gt;"/></returns>
        public static IMappingExpression<TSource, TDestination> CreateMapToActivityDto<TSource, TDestination>()
            where TSource : Activity
            where TDestination : ActivityDto
        {
            return CreateMapToActivityDtoWithoutResults<TSource, TDestination> ()
                .ForMember ( opt => opt.Results, dest => dest.Ignore () );
        }

        /// <summary>
        /// Creates the map to activity dto without results.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <returns>A <see cref="AutoMapper.IMappingExpression&lt;TSource,TDestination&gt;"/></returns>
        public static IMappingExpression<TSource, TDestination> CreateMapToActivityDtoWithoutResults<TSource, TDestination>()
            where TSource : Activity
            where TDestination : ActivityDto
        {
            return AutoMapperSetup.CreateMapToEditableDto<TSource, TDestination> ()
                .ForMember ( dest => dest.VisitKey, opt => opt.MapFrom ( vs => vs.Visit.Key ) )
                .ForMember ( dest => dest.ClinicianKey, opt => opt.MapFrom ( vs => vs.Visit.Staff.Key ) )
                .ForMember ( dest => dest.PatientKey, opt => opt.MapFrom ( vs => vs.ClinicalCase.Patient.Key ) )
                .ForMember ( dest => dest.ActivityStartDateTime, opt => opt.MapFrom ( vs => vs.ActivityDateTimeRange.StartDateTime ) )
                .ForMember ( dest => dest.ActivityEndDateTime, opt => opt.MapFrom ( vs => vs.ActivityDateTimeRange.EndDateTime ) )
                .ForMember ( dest => dest.AppointmentStartDateTime, opt => opt.MapFrom ( vs => vs.Visit.AppointmentDateTimeRange.StartDateTime ) )
                .ForMember ( dest => dest.VisitTemplateName, opt => opt.MapFrom ( src => src.Visit.Name ) )
                .ForMember ( dest => dest.VisitStatusWellKnownName, opt => opt.MapFrom ( src => src.Visit.VisitStatus.WellKnownName ) )
                .ForMember ( dest => dest.ProvenanceKey, opt => opt.MapFrom ( src => src.Provenance.Key ) )
                .ForMember ( dest => dest.ClinicalCaseKey, opt => opt.MapFrom ( src => src.ClinicalCase.Key ) )
                .ForMember (
                    dest => dest.ProvenanceAssigningAuthorityName,
                    opt => opt.MapFrom ( src => src.Provenance.TaggedDataElement.AssigningAuthorityName ) );
        }

        #endregion
    }
}
