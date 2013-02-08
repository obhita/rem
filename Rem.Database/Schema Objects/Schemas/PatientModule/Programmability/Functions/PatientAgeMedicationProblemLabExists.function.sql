create function PatientModule.PatientAgeMedicationProblemLabExists
(
    @PatientKey                         bigint,
    @CutOffPatientBirthDate             date,
    @RootMedicationCodeCodedConceptCode nvarchar(10),
    @ProblemCodeCodedConceptCode        nvarchar(10),
    @LabTestTypeCodedConceptCode        nvarchar(10),
    @ValidLabOrderMonthCount            int
)
returns bit as
begin

     --************************************************************************
     --* Declaration
     --************************************************************************

    declare @RecordCount    int

     --************************************************************************
     --* Initialization
     --************************************************************************

    set @RecordCount    = 0

     --************************************************************************
     --* Logic
     --************************************************************************

    
      --Determine if the patient has the particular medication, problem, and
      --lab combination.  If they do, then return 1.  If they do not, return 0.
    

    select
			@RecordCount = count(*)        
    from
                    PatientModule.Patient               pt
             join   PatientModule.Medication            med     on  (pt.PatientKey                      = med.PatientKey)
             join   PatientModule.MedicationStatusLkp   medsl   on  (med.MedicationStatusLkpKey         = medsl.MedicationStatusLkpKey)     
             join   ClinicalCaseModule.ClinicalCase     c       on  (pt.PatientKey                      = c.PatientKey)
             join   ClinicalCaseModule.Problem          pb      on  (c.ClinicalCaseKey                  = pb.ClinicalCaseKey)
             join   ClinicalCaseModule.ProblemStatusLkp pbsl    on  (pb.ProblemStatusLkpKey             = pbsl.ProblemStatusLkpKey)

		left join	(
						select
							v.ClinicalCaseKey,
							cc_lt.LabTestNameLkpKey,
							v.CheckedInDateTime
						from
							    	VisitModule.Visit           v
							 join   VisitModule.Activity        a       on  (v.AppointmentKey       = a.VisitKey)
							 join   LabModule.LabTest           lt      on  (a.ActivityKey          = lt.LabSpecimenKey)
						     join   LabModule.LabTestNameLkp	cc_lt	on  (lt.LabTestNameLkpKey   = cc_lt.LabTestNameLkpKey)      and
         																	(cc_lt.CodedConceptCode = @LabTestTypeCodedConceptCode)		
         				where 
         				    ((  datediff(																									-- The number of months between today
                                            mm,																								-- and the lab test's visit's checked in date
                                            isnull(v.CheckedInDateTime, dateadd(mm, -(@ValidLabOrderMonthCount+1), current_timestamp)),		-- is less than or equal to the ValidLabOrderMonthCount
                                            current_timestamp                                       
                                         ) <= @ValidLabOrderMonthCount)) 						
					)	                                lt      on  (c.ClinicalCaseKey                  = lt.ClinicalCaseKey)
    where
        (pt.PatientKey													=  @PatientKey)				and     -- For a given patient
        (isnull(pt.BirthDate, dateadd(mm, 1, @CutOffPatientBirthDate))	<= @CutOffPatientBirthDate)	and     -- Patient must be older than the cut off date
		(med.RootMedicationCodedConceptCode		= @RootMedicationCodeCodedConceptCode)				and		-- For the given medication
		(isnull(medsl.WellKnownName, '')		= 'Active')												and		-- Medication must be active
		(pb.ProblemCodeCodedConceptCode			= @ProblemCodeCodedConceptCode)						and		-- For the given problem
		(isnull(pbsl.WellKnownName, '')			= 'Active')											and		-- Problem must be active
        (lt.LabTestNameLkpKey					is	 null)													
    return (
            case 
                when (@RecordCount  > 0)    then    1
                else                                0
            end
           )
end