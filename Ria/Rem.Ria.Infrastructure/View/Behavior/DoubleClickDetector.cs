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
using System.Windows.Input;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// Class for detecting double click.
    /// </summary>
    public class DoubleClickDetector
    {
        #region Constants and Fields

        private bool _firstClickDone;
        private Point _firstClickPosition;
        private DateTime _firstClickTime;

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines whether [is double click] [the specified element].
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        /// <param name="clickTime">The click time.</param>
        /// <returns><c>true</c> if [is double click] [the specified element]; otherwise, <c>false</c>.</returns>
        public bool IsDoubleClick ( UIElement element, MouseButtonEventArgs e, DateTime clickTime )
        {
            var isDoubleClick = false;

            var span = clickTime - _firstClickTime;

            if ( span.TotalMilliseconds > 300 || _firstClickDone == false )
            {
                _firstClickPosition = e.GetPosition ( element );
                _firstClickDone = true;
                _firstClickTime = DateTime.Now;
            }
            else
            {
                var position = e.GetPosition ( element );
                if ( Math.Abs ( _firstClickPosition.X - position.X ) < 4 &&
                     Math.Abs ( _firstClickPosition.Y - position.Y ) < 4 )
                {
                    isDoubleClick = true;
                }
                _firstClickDone = false;
            }

            return isDoubleClick;
        }

        #endregion
    }
}
