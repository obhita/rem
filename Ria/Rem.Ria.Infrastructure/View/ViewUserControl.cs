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

using System;
using System.Windows;
using System.Windows.Controls;
using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.Infrastructure.View
{
    /// <summary>
    /// ViewUserControl class.
    /// </summary>
    public class ViewUserControl : UserControl, IView
    {
        #region Constants and Fields

        private bool _initialized;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewUserControl"/> class.
        /// </summary>
        public ViewUserControl ()
        {
            Loaded += ViewUserControlLoaded;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The Initialized event will fire after the view has completed its internal
        /// initialization.  This can be used to inform the Screen object when it can
        /// begin its own initialization process.
        /// </summary>
        public event EventHandler Initialized = ( sender, eventHandler ) => { };

        #endregion

        #region Public Properties

        /// <summary>
        /// The base view model supporting the this view.
        /// </summary>
        /// <value>The view model.</value>
        public IViewModel ViewModel
        {
            get { return DataContext as IViewModel; }

            set { DataContext = value; }
        }

        /// <summary>
        /// The views name uniquely identifies the view within a
        /// </summary>
        /// <value>The name of the view.</value>
        public string ViewName { get; set; }

        #endregion

        #region Methods

        private void ViewUserControlLoaded ( object sender, RoutedEventArgs e )
        {
            // Sometimes views will be created inside a tabitem.  We are running into a problem with tab items.
            // turns out that the tabitem will detach the contents of the tab when it is not visible
            // and then reattach when you click the tab.  this causes the loaded event to fire again.
            // we need to make sure we only initialize the very first time.
            if ( !_initialized )
            {
                Initialized ( this, new EventArgs () );
                _initialized = true;
            }
        }

        #endregion
    }
}
