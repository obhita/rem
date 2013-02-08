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

using Rem.Domain.Clinical.DensAsiModule;
using Rem.Domain.Clinical.GainShortScreenerModule;
using Rem.Domain.Clinical.GpraModule;
using Rem.Domain.Clinical.ImmunizationModule;
using Rem.Domain.Clinical.LabModule;
using Rem.Domain.Clinical.RadiologyModule;
using Rem.Domain.Clinical.SbirtModule;
using Rem.Domain.Clinical.TedsModule;
using Rem.Domain.Clinical.VisitModule;
using StructureMap;

namespace Rem.Infrastructure.Domain
{
    /// <summary>
    /// The <see cref="T:Rem.Infrastructure.Domain.ServiceRegistry"> ServiceRegistry</see> registers factory services. 
    /// </summary>
    public class ServiceRegistry : IServiceRegistry
    {
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceRegistry"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ServiceRegistry(IContainer container)
        {
            _container = container;
        }

        #region Implementation of IServiceRegistry

        /// <summary>
        /// Registers the services.
        /// </summary>
        public void RegisterServices ()
        {
            //Activity scheduler service - initialization.
            var activitySchedulerService = _container.GetInstance<IActivitySchedulerService> ();
            _container.Configure(c => c.For<IActivitySchedulerService>().Singleton().Use(activitySchedulerService));

            //Activity scheduler service - related registrations.
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IVitalSignFactory ), WellKnownNames.VisitModule.ActivityType.VitalSign );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( ILabSpecimenFactory ), WellKnownNames.VisitModule.ActivityType.LabSpecimen );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IRadiologyOrderFactory ), WellKnownNames.VisitModule.ActivityType.RadiologyOrder );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IImmunizationFactory ), WellKnownNames.VisitModule.ActivityType.Immunization );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( ISocialHistoryFactory ), WellKnownNames.VisitModule.ActivityType.SocialHistory );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IBriefInterventionFactory ), WellKnownNames.VisitModule.ActivityType.BriefIntervention );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IAuditCFactory ), WellKnownNames.VisitModule.ActivityType.AuditC );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IPhq9Factory ), WellKnownNames.VisitModule.ActivityType.Phq9 );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IIndividualCounselingFactory ), WellKnownNames.VisitModule.ActivityType.IndividualCounseling );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IDast10Factory ), WellKnownNames.VisitModule.ActivityType.Dast10 );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IGpraInterviewFactory ), WellKnownNames.VisitModule.ActivityType.GpraInterview );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IDensAsiInterviewFactory ), WellKnownNames.VisitModule.ActivityType.DensAsiInterview );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IGainShortScreenerFactory ), WellKnownNames.VisitModule.ActivityType.GainShortScreener );
            activitySchedulerService.RegisterFactoryForActivityType ( typeof ( IAuditFactory ), WellKnownNames.VisitModule.ActivityType.Audit );
            activitySchedulerService.RegisterFactoryForActivityType(typeof(INidaDrugQuestionnaireFactory), WellKnownNames.VisitModule.ActivityType.NidaDrugQuestionnaire);
            activitySchedulerService.RegisterFactoryForActivityType (
                typeof( ITedsAdmissionInterviewFactory ), WellKnownNames.VisitModule.ActivityType.TedsAdmissionInterview );
            activitySchedulerService.RegisterFactoryForActivityType (
                typeof( ITedsDischargeInterviewFactory ), WellKnownNames.VisitModule.ActivityType.TedsDischargeInterview );
        }

        #endregion
    }
}