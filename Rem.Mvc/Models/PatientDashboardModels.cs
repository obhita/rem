using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rem.Ria.PatientModule.Web.ClinicalCaseEditor;
using Rem.Ria.PatientModule.Web.Common;
using Rem.Ria.PatientModule.Web.PatientDashboard;

namespace Rem.Mvc.Models
{
    public abstract class PatientDashboardModelBase
    {
        public long PatientKey { get; set; }
        public string PatientName { get; set; }
    }

    public class GetAllClinicalCasesWithActivitiesForSelectedClinicalCaseModel : PatientDashboardModelBase
    {
        public IEnumerable<ClinicalCaseSummaryDto> ClinicalCases { get; set; }
        public long SelectedClinicalCaseKey { get; set; }
        public IEnumerable<ActivityDto> Activities { get; set; }
    }

    public class EditSocialHistoryModel : PatientDashboardModelBase
    {
        public SocialHistoryDto SocialHistory { get; set; }
    }
}