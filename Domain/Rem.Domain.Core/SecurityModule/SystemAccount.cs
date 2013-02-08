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

using System.Collections.Generic;
using System.Linq;
using Pillar.Common.InversionOfControl;
using Pillar.Common.Utility;
using Pillar.Domain;
using Pillar.Domain.Primitives;
using Rem.Domain.Core.AgencyModule;

namespace Rem.Domain.Core.SecurityModule
{
    /// <summary>
    /// SystemAccount defines a system security level account.
    /// </summary>
    [Cache]
    public class SystemAccount : AbstractAggregateRoot
    {
        private readonly IList<SystemAccountRole> _systemAccountRoles;
        private readonly IList<Staff> _staffMembers;
        private readonly IList<SecurityQuestion> _securityQuestions;
        private string _identifier;
        private string _identityProviderUriIdentifier;
        private string _identityProviderName;
        private string _displayName;
        private EmailAddress _emailAddress;
        private bool _enabledIndicator;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAccount"/> class.
        /// </summary>
        protected SystemAccount ()
        {
            _systemAccountRoles = new List<SystemAccountRole>();
            _staffMembers = new List<Staff> ();
            _securityQuestions = new List<SecurityQuestion> ();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemAccount"/> class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="identityProviderName">Name of the identity provider.</param>
        /// <param name="identityProviderUri">The identity provider URI.</param>
        protected internal SystemAccount(string identifier, string displayName, EmailAddress emailAddress, string identityProviderName, string identityProviderUri)
            : this()
        {
            Check.IsNotNullOrWhitespace(identifier, "Identifier is required.");
            Check.IsNotNullOrWhitespace(displayName, "Display name is required.");
            Check.IsNotNull(emailAddress, "Email address is required.");
            
            _identityProviderName = Check.IsNotNullOrWhitespaceAndAssign(identityProviderName, "Identity Provider Name is required.");
            _identityProviderUriIdentifier = Check.IsNotNullOrWhitespaceAndAssign(identityProviderUri, "Identity Provider Uri is required.");
            
            _identifier = identifier;
            _displayName = displayName;
            _emailAddress = emailAddress;
            _enabledIndicator = true;
        }
        #endregion

        #region Properties

        /// <summary>
        /// All accounts must have a unique identifier.  When using a federated authentication 
        /// mechanism the identifier will usually be an email address.
        /// </summary>
        [NotNull]
        [ColumnLength ( 100 )]
        [Unique]
        public virtual string Identifier
        {
            get { return _identifier; }
            private set { ApplyPropertyChange ( ref _identifier, () => Identifier, value ); }
        }


        /// <summary>
        /// Gets the display name.
        /// </summary>
        [NotNull]
        public virtual string DisplayName
        {
            get { return _displayName; }
            private set { ApplyPropertyChange(ref _displayName, () => DisplayName, value); }
        }


        /// <summary>
        /// The Uri that identifies the Identity Provider Store used by the External Identity Provider (i.e: Yahoo!, Google, Uri:WindowsLiveID, etc).
        /// </summary>
        [NotNull]
        [ColumnLength(100)]
        public virtual string IdentityProviderUriIdentifier
        {
            get { return _identityProviderUriIdentifier; }
            private set { ApplyPropertyChange(ref _identityProviderUriIdentifier, () => this.IdentityProviderUriIdentifier, value); }
        }


        /// <summary>
        /// The friendly name of the Identity Provider Store used by the External Identity Provider (i.e: Yahoo!, Google, Windows Live ID, etc).
        /// </summary>
        [NotNull]
        public virtual string IdentityProviderName
        {
            get { return _identityProviderName; }
            private set { ApplyPropertyChange(ref _identityProviderName, () => IdentityProviderName, value); }
        }

        /// <summary>
        ///     The EnabledIndicator can be used to deactivate a user account.
        /// </summary>
        public virtual bool EnabledIndicator
        {
            get { return _enabledIndicator; }
            private set { ApplyPropertyChange(ref _enabledIndicator, () => EnabledIndicator, value); }
        }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public virtual EmailAddress EmailAddress
        {
            get { return _emailAddress; }
            private set { ApplyPropertyChange(ref _emailAddress, () => EmailAddress, value); }
        }

        /// <summary>
        ///     A list of the roles that are granted to this account.
        /// </summary>
        public virtual IEnumerable<SystemAccountRole> SystemAccountRoles
        {
            get { return _systemAccountRoles.ToList().AsReadOnly(); }
            private set { }
        }

        /// <summary>
        /// Gets the staff members.
        /// </summary>
        public virtual IEnumerable<Staff> StaffMembers
        {
            get { return _staffMembers.ToList().AsReadOnly (); }
            private set { }
        }

        /// <summary>
        /// Gets the security questions.
        /// </summary>
        public virtual IEnumerable<SecurityQuestion> SecurityQuestions
        {
            get { return _securityQuestions.ToList().AsReadOnly(); }
            private set { }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Enables the account.  The SystemAccount will only authenticate if it is enabled.
        /// </summary>
        public virtual void Enable ()
        {
            EnabledIndicator = true;
        }

        /// <summary>
        ///     Disables the account.  The SystemAccount will not authenticate if it is disabled.
        /// </summary>
        public virtual void Disable ()
        {
            EnabledIndicator = false;
        }

        /// <summary>
        ///     Grants the given role to the system account.
        /// </summary>
        /// <param name = "systemRole">The role to be granted</param>
        public virtual void GrantSystemRole ( SystemRole systemRole )
        {
            Check.IsNotNull(systemRole, "System role is required.");

            DomainRuleEngine.CreateRuleEngine<SystemAccount, SystemRole>(this, () => GrantSystemRole)
                .WithContext(systemRole)
                .Execute(
                    () =>
                    {
                        var systemAccountRole = new SystemAccountRole(this, systemRole);
                        _systemAccountRoles.Add(systemAccountRole);
                        NotifyItemAdded ( () => SystemAccountRoles, systemAccountRole );
                    }
                );
        }

        /// <summary>
        /// Revokes the system role.
        /// </summary>
        /// <param name="systemRole">The system role.</param>
        public virtual void RevokeSystemRole(SystemRole systemRole)
        {
            Check.IsNotNull ( systemRole, "System role is required." );

            var existingSystemRole = _systemAccountRoles.FirstOrDefault ( sar => sar.SystemRole.Key == systemRole.Key );

            if ( existingSystemRole != null )
            {
                _systemAccountRoles.Remove ( existingSystemRole );
                NotifyItemRemoved ( () => SystemAccountRoles, existingSystemRole );
            }
        }

        /// <summary>
        /// Revises the email address.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        public virtual void ReviseEmailAddress ( EmailAddress emailAddress )
        {
            Check.IsNotNull(emailAddress, "Email address is required.");
            EmailAddress = emailAddress;
        }

        /// <summary>
        /// Adds the security question.
        /// </summary>
        /// <param name="securityQuestion">The security question.</param>
        public virtual void AddSecurityQuestion(SecurityQuestion securityQuestion)
        {
            Check.IsNotNull(securityQuestion, "Security question is required.");

            DomainRuleEngine.CreateRuleEngine<SystemAccount, SecurityQuestion> ( this, () => AddSecurityQuestion )
                .WithContext ( securityQuestion )
                .Execute (
                    () =>
                        {
                            securityQuestion.SystemAccount = this;
                            _securityQuestions.Add ( securityQuestion );
                            NotifyItemAdded ( () => _securityQuestions, securityQuestion );
                        } );
        }

        /// <summary>
        /// Removes the security question.
        /// </summary>
        /// <param name="securityQuestion">The security question.</param>
        public virtual void RemoveSecurityQuestion(SecurityQuestion securityQuestion)
        {
            Check.IsNotNull(securityQuestion, "Security question is required.");
            _securityQuestions.Remove(securityQuestion);
            NotifyItemRemoved(() => SecurityQuestions, securityQuestion);
        }

        /// <summary>
        /// Finds all permissions that have been granted to the account.  
        /// <para>
        /// Permissions are granted to roles and roles are granted accounts.  This method 
        /// returns the sum of all of those permissions.
        /// </para>
        /// </summary>
        /// <returns>A IEnumerable&lt;SystemPermission&gt;.</returns>
        public virtual IEnumerable<SystemPermission> FindGrantedPermissions ()
        {
            var systemPermissionService = IoC.CurrentContainer.Resolve<ISystemPermissionService> ();
            var systemRoles = ( from systemAccountRole in SystemAccountRoles
                                where systemAccountRole.SystemAccount.Key == Key
                                select systemAccountRole.SystemRole).ToList ();

            return systemPermissionService.FindGrantedSystemPermissions ( systemRoles );
        }

        #endregion
    }
}
