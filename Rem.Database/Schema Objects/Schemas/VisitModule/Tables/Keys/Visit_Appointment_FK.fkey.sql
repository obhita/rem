ALTER TABLE [VisitModule].[Visit]
    ADD CONSTRAINT [Visit_Appointment_FK] FOREIGN KEY ([AppointmentKey]) REFERENCES [AgencyModule].[Appointment] ([AppointmentKey]) ON DELETE NO ACTION ON UPDATE NO ACTION;

