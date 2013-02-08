select 
    a.ActivityKey                  as ActivityKey,
    v.AppointmentKey               as VisitKey,
    v.Name                         as VisitName,
    vs.WellKnownName               as VisitStatusWellKnownName,
    apt.AppointmentStartDateTime   as VisitAppointmentStartDateTime,
    at.Name                        as ActivityTypeName,
    at.WellKnownName               as ActivityTypeWellKnownName,
    cc.PatientKey                  as PatientKey
from   
                     VisitModule.Activity a
    right outer join VisitModule.Visit    v             on a.VisitKey           = v.AppointmentKey
    right outer join AgencyModule.Appointment apt       on v.AppointmentKey     = apt.AppointmentKey
    right outer join ClinicalCaseModule.ClinicalCase cc on v.ClinicalCaseKey    = cc.ClinicalCaseKey
    right outer join VisitModule.VisitStatusLkp vs      on v.VisitStatusLkpKey  = vs.VisitStatusLkpKey
    left  outer join VisitModule.ActivityTypeLkp at     on a.ActivityTypeLkpKey = at.ActivityTypeLkpKey
where
    v.AppointmentKey in 
        ( 
            select
                TOP (@VisitCount)
                apt2.AppointmentKey  as AppointmentKey
            from
                     AgencyModule.Appointment apt2
                join VisitModule.Visit        v2       on v2.AppointmentKey = apt2.AppointmentKey
            where
                v2.ClinicalCaseKey = @ClinicalCaseKey     and
                apt2.AppointmentStartDateTime >= @TargetDate
            order by apt2.AppointmentStartDateTime
        )