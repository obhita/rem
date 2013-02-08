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

using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Pillar.Domain.Event;
using Rem.Domain.Clinical.VisitModule.Event;
using Rem.Domain.Core.CommonModule;

namespace Rem.Domain.Clinical.VisitModule
{
    /// <summary>
    /// SocialHistory is an <see cref="Activity">Activity</see> short screener that manages the scheduling of child activities
    /// AuditC, Dast10, PHQ2 along with the patient's smoking information.
    /// </summary>
    public class SocialHistory : Activity
    {
        private SocialHistoryAuditC _socialHistoryAuditC;
        private SocialHistoryDast10 _socialHistoryDast10;
        private SocialHistoryPhq2 _socialHistoryPhq2;
        private SocialHistorySmoking _socialHistorySmoking;
        private IActivitySchedulerService _activitySchedulerService;
        private ILookupValueRepository _lookupValueRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialHistory"/> class.
        /// </summary>
        protected internal SocialHistory ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialHistory"/> class.
        /// </summary>
        /// <param name="visit">The visit.</param>
        /// <param name="activityType">Type of the activity.</param>
        protected internal SocialHistory (
            Visit visit,
            ActivityType activityType )
            : base ( visit, activityType )
        {
        }

        #region Public Properties 

        /// <summary>
        /// Gets the social history smoking.
        /// </summary>
        public virtual SocialHistorySmoking SocialHistorySmoking
        {
            get { return _socialHistorySmoking; }
            private set { ApplyPropertyChange ( ref _socialHistorySmoking, () => SocialHistorySmoking, value ); }
        }

        /// <summary>
        /// Gets the social history PHQ2.
        /// </summary>
        public virtual SocialHistoryPhq2 SocialHistoryPhq2
        {
            get { return _socialHistoryPhq2; }
            private set { ApplyPropertyChange ( ref _socialHistoryPhq2, () => SocialHistoryPhq2, value ); }
        }

        /// <summary>
        /// Gets the social history Dast10.
        /// </summary>
        public virtual SocialHistoryDast10 SocialHistoryDast10
        {
            get { return _socialHistoryDast10; }
            private set { ApplyPropertyChange ( ref _socialHistoryDast10, () => SocialHistoryDast10, value ); }
        }

        /// <summary>
        /// Gets the social history AuditC.
        /// </summary>
        public virtual SocialHistoryAuditC SocialHistoryAuditC
        {
            get { return _socialHistoryAuditC; }
            private set { ApplyPropertyChange ( ref _socialHistoryAuditC, () => SocialHistoryAuditC, value ); }
        }

        /// <summary>
        /// Revises the social history smoking.
        /// </summary>
        /// <param name="socialHistorySmoking">The social history smoking.</param>
        public virtual void ReviseSocialHistorySmoking ( SocialHistorySmoking socialHistorySmoking )
        {
            Check.IsNotNull ( socialHistorySmoking, "socialHistorySmoking is required." );

            _socialHistorySmoking = socialHistorySmoking;

            DomainEvent.Raise ( new SocialHistorySmokingChangedEvent { SocialHistory = this } );
        }

        /// <summary>
        /// Revises the social history PHQ2.
        /// </summary>
        /// <param name="socialHistoryPhq2">The social history PHQ2.</param>
        public virtual void ReviseSocialHistoryPhq2 ( SocialHistoryPhq2 socialHistoryPhq2 )
        {
            Check.IsNotNull ( socialHistoryPhq2, "socialHistoryPhq2 is required." );

            _socialHistoryPhq2 = socialHistoryPhq2;

            if (_socialHistoryPhq2.IsPhq9ThresholdSatisfied)
            {
                ScheduleActivity ( WellKnownNames.VisitModule.ActivityType.Phq9 );
            }
        }

        /// <summary>
        /// Revises the social history dast10.
        /// </summary>
        /// <param name="socialHistoryDast10">The social history dast10.</param>
        public virtual void ReviseSocialHistoryDast10 ( SocialHistoryDast10 socialHistoryDast10 )
        {
            Check.IsNotNull ( socialHistoryDast10, "socialHistoryDast10 is required." );

            _socialHistoryDast10 = socialHistoryDast10;

            if ( _socialHistoryDast10.IsDast10ThresholdSatisfied )
            {
                ScheduleActivity(WellKnownNames.VisitModule.ActivityType.Dast10);
            }
        }

        /// <summary>
        /// Revises the social history AuditC.
        /// </summary>
        /// <param name="socialHistoryAuditC">The social history audit C.</param>
        public virtual void ReviseSocialHistoryAuditC ( SocialHistoryAuditC socialHistoryAuditC )
        {
            Check.IsNotNull ( socialHistoryAuditC, "socialHistoryAuditC is required." );

            _socialHistoryAuditC = socialHistoryAuditC;

            if ( _socialHistoryAuditC.IsAuditCThresholdSatisfied)
            {
                ScheduleActivity(WellKnownNames.VisitModule.ActivityType.AuditC);
            }
        }

        private void ScheduleActivity( string activityWellKnownName )
        {
            if (_activitySchedulerService == null)
            {
                _activitySchedulerService = IoC.CurrentContainer.Resolve<IActivitySchedulerService>();
            }

            if (_lookupValueRepository == null)
            {
                _lookupValueRepository = IoC.CurrentContainer.Resolve<ILookupValueRepository>();
            }

            var activityType = _lookupValueRepository.GetLookupByWellKnownName<ActivityType>(activityWellKnownName);

            _activitySchedulerService.ScheduleActivity ( Visit.Key, activityType );
        }

        #endregion
    }
}