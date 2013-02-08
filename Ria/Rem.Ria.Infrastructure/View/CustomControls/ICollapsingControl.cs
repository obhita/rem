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

using System.Windows.Input;

namespace Rem.Ria.Infrastructure.View.CustomControls
{
    /// <summary>
    /// Collapsing states
    /// </summary>
    public enum CollapsingState
    {
        /// <summary>
        /// Small state
        /// </summary>
        Small,

        /// <summary>
        /// Normal state
        /// </summary>
        Normal,

        /// <summary>
        /// Large state
        /// </summary>
        Large
    }

    /// <summary>
    /// ICollapsingControl interface.
    /// </summary>
    public interface ICollapsingControl
    {
        #region Public Properties

        /// <summary>
        /// Gets the get larger command.
        /// </summary>
        ICommand GetLargerCommand { get; }

        /// <summary>
        /// Gets the get smaller command.
        /// </summary>
        ICommand GetSmallerCommand { get; }

        /// <summary>
        /// Gets the go to large command.
        /// </summary>
        ICommand GoToLargeCommand { get; }

        /// <summary>
        /// Gets the go to normal command.
        /// </summary>
        ICommand GoToNormalCommand { get; }

        /// <summary>
        /// Gets the go to small command.
        /// </summary>
        ICommand GoToSmallCommand { get; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>The priority.</value>
        int Priority { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        CollapsingState State { get; set; }

        #endregion
    }
}
