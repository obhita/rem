using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// Interface of Popup.
    /// </summary>
    public interface IPopup
    {
        /// <summary>
        /// Shows this instance.
        /// </summary>
        void Show();

        /// <summary>
        /// Closes this instance.
        /// </summary>
        void Close();
    }
}
