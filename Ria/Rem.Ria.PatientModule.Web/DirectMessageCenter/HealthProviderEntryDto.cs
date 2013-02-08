using Rem.Infrastructure.Service.DataTransferObject;

namespace Rem.Ria.PatientModule.Web.DirectMessageCenter
{
    /// <summary>
    /// Healthcare Provider Entry DataTransferObject
    /// </summary>
    public partial class HealthProviderEntryDto : AbstractDataTransferObject
    {
        #region Constants and Fields

        private string _address1;

        private string _city;

        private string _state;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>The address1.</value>
        public virtual string Address1
        {
            get { return _address1; }
            set { ApplyPropertyChange ( ref _address1, () => Address1, value ); }
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public virtual string City
        {
            get { return _city; }
            set { ApplyPropertyChange ( ref _city, () => City, value ); }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is HC professional.
        /// </summary>
        /// <value><c>true</c> if this instance is HC professional; otherwise, <c>false</c>.</value>
        public bool IsHcProfessional { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the mail.
        /// </summary>
        /// <value>The mail.</value>
        public string Mail { get; set; }

        /// <summary>
        /// Gets or sets the name of the organization.
        /// </summary>
        /// <value>The name of the organization.</value>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Gets or sets the specialization.
        /// </summary>
        /// <value>The specialization.</value>
        public string Specialization { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public virtual string State
        {
            get { return _state; }
            set { ApplyPropertyChange ( ref _state, () => State, value ); }
        }

        /// <summary>
        /// Gets or sets the telephone number.
        /// </summary>
        /// <value>The telephone number.</value>
        public string TelephoneNumber { get; set; }

        #endregion
    }
}
