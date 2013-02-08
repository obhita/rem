using System;
using System.Transactions;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// This basically defines a durable Resource Manager which doen NOT support Lightweight Transaction Manager (LTM).
    /// By design, RMs that support promotion must implement the interface “IPromotableSinglePhaseNotification” so that when promotion is required, 
    /// the .NET framework can call the method Promote() of the IPromotableSinglePhaseNotification interface. 
    /// MSMQ and SQL Server 2000 do not implement IPromotableSinglePhaseNotification.
    /// When enlist this RM to the current transaction, it basically tells the System.Transactions infrastructure that we are using DTC to manage the transaction.
    /// While our Resource Manager participates in the 2-phase-commit process, it doesn’t actually do anything.  
    /// It’s sole purpose is to force the trandaction to use DTC.
    /// </summary>
    internal class DummyEnlistmentNotification : IEnlistmentNotification
    {
        #region Constants and Fields

        public static readonly Guid Id = Guid.NewGuid();

        #endregion

        #region Public Methods

        public void Commit ( Enlistment enlistment )
        {
            enlistment.Done ();
        }

        public void InDoubt ( Enlistment enlistment )
        {
            enlistment.Done ();
        }

        public void Prepare ( PreparingEnlistment preparingEnlistment )
        {
            preparingEnlistment.Prepared ();
        }

        public void Rollback ( Enlistment enlistment )
        {
            enlistment.Done ();
        }

        #endregion
    }
}
