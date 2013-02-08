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

using System.Windows;
using System.Windows.Interactivity;

namespace Rem.Ria.Infrastructure.View.Behavior
{
    /// <summary>
    /// StylizedBehaviors class.
    /// </summary>
    public class StylizedBehaviors
    {
        #region Constants and Fields

        /// <summary>
        /// Dependency Property for BehaviorsProperty Property.
        /// </summary>
        public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached (
            @"Behaviors",
            typeof( StylizedBehaviorCollection ),
            typeof( StylizedBehaviors ),
            new PropertyMetadata ( null, OnPropertyChanged ) );

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the behaviors.
        /// </summary>
        /// <param name="uie">The dependency object.</param>
        /// <returns>A <see cref="Rem.Ria.Infrastructure.View.Behavior.StylizedBehaviorCollection"/></returns>
        public static StylizedBehaviorCollection GetBehaviors ( DependencyObject uie )
        {
            return ( StylizedBehaviorCollection )uie.GetValue ( BehaviorsProperty );
        }

        /// <summary>
        /// Sets the behaviors.
        /// </summary>
        /// <param name="uie">The dependency object.</param>
        /// <param name="value">The styled value.</param>
        public static void SetBehaviors ( DependencyObject uie, StylizedBehaviorCollection value )
        {
            uie.SetValue ( BehaviorsProperty, value );
        }

        #endregion

        #region Methods

        private static void OnPropertyChanged ( DependencyObject dpo, DependencyPropertyChangedEventArgs e )
        {
            var uie = dpo as UIElement;

            if ( uie == null )
            {
                return;
            }

            var itemBehaviors = Interaction.GetBehaviors ( uie );

            var newBehaviors = e.NewValue as StylizedBehaviorCollection;

            itemBehaviors.Clear ();

            if ( newBehaviors != null )
            {
                foreach ( var behavior in newBehaviors )
                {
                    itemBehaviors.Add ( behavior );
                }
            }
        }

        #endregion
    }
}
