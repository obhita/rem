using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pillar.Common.Tests;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Core.Tests.AgencyModule
{
    [TestClass]
    public class AppointmentFactoryTests
    {
        [TestMethod]
        public void CreateAppointment_GivenValidArguments_CreatesAnAppointment ()
        {
            var appointmentRepository = new Mock<IAppointmentRepository> ();
            var appointmentFactory = new AppointmentFactory (
                appointmentRepository.Object );

            var staff = new Mock<Staff> ();
            var start = new DateTime ( 2000, 1, 1, 9, 0, 0 );
            var end = new DateTime ( 2000, 1, 1, 10, 0, 0 );

            Appointment appointment = appointmentFactory.CreateAppointment ( staff.Object, new DateTimeRange ( start, end ) );

            Assert.IsNotNull ( appointment );
        }

        [TestMethod]
        public void CreateAppointment_GivenValidArguments_AppointmentIsEditable ()
        {
            using ( var serviceLocatorFixture = new ServiceLocatorFixture () )
            {
                // Setup
                var appointmentRepository = new Mock<IAppointmentRepository> ();
                var appointmentFactory = new AppointmentFactory (
                    appointmentRepository.Object );

                var staff = new Mock<Staff> ();
                var start = new DateTime ( 2000, 1, 1, 9, 0, 0 );
                var end = new DateTime ( 2000, 1, 1, 10, 0, 0 );

                Appointment appointment = appointmentFactory.CreateAppointment ( staff.Object, new DateTimeRange ( start, end ) );

                appointment.ReviseSubjectDescription ( "Some Appointment" );
            }
        }

        [TestMethod]
        public void CreateAppointment_GivenValidArguments_AppointmentIsPersisted ()
        {
            bool isPersisted = false;

            var appointmentRepository = new Mock<IAppointmentRepository> ();
            appointmentRepository
                .Setup ( a => a.MakePersistent ( It.IsAny<Appointment> () ) )
                .Callback ( () => isPersisted = true );
            var appointmentFactory = new AppointmentFactory (
                appointmentRepository.Object );

            var staff = new Mock<Staff> ();
            var start = new DateTime ( 2000, 1, 1, 9, 0, 0 );
            var end = new DateTime ( 2000, 1, 1, 10, 0, 0 );

            appointmentFactory.CreateAppointment ( staff.Object, new DateTimeRange ( start, end ) );

            Assert.IsTrue ( isPersisted );
        }

        [TestMethod]
        [ExpectedException ( typeof ( ArgumentException ) )]
        public void CreateAppointment_NullStaff_ThrowsArgumentException ()
        {
            var appointmentRepository = new Mock<IAppointmentRepository> ();
            var appointmentFactory = new AppointmentFactory (
                appointmentRepository.Object );

            var start = new DateTime ( 2000, 1, 1, 9, 0, 0 );
            var end = new DateTime ( 2000, 1, 1, 10, 0, 0 );

            appointmentFactory.CreateAppointment ( null, new DateTimeRange ( start, end ) );
        }

        [TestMethod]
        public void DestroyAppointment_GivenAnAppointment_AppointmentIsMadeTransient ()
        {
            bool isTransient = false;

            var appointmentRepository = new Mock<IAppointmentRepository> ();
            appointmentRepository
                .Setup ( a => a.MakeTransient ( It.IsAny<Appointment> () ) )
                .Callback ( () => isTransient = true );
            var appointmentFactory = new AppointmentFactory (
                appointmentRepository.Object );

            var appointment = new Mock<Appointment> ();

            appointmentFactory.DestroyAppointment ( appointment.Object );

            Assert.IsTrue ( isTransient );
        }
    }
}