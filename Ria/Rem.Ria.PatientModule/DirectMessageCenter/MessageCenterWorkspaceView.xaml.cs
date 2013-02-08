using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Rem.Ria.PatientModule.DirectMessageCenter
{
    /// <summary>
    /// Message Center Workspace View 
    /// </summary>
    public partial class MessageCenterWorkspaceView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageCenterWorkspaceView"/> class.
        /// </summary>
        public MessageCenterWorkspaceView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageCenterWorkspaceView"/> class.
        /// </summary>
        /// <param name="messageCenterWorkspaceViewModel">The message center workspace view model.</param>
        [InjectionConstructor]
        public MessageCenterWorkspaceView(MessageCenterWorkspaceViewModel messageCenterWorkspaceViewModel)
            : this ()
        {
            DataContext = messageCenterWorkspaceViewModel;
        }
    }
}
