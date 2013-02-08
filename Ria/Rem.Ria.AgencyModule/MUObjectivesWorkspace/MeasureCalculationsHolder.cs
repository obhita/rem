using Rem.Ria.Infrastructure.ViewModel;

namespace Rem.Ria.AgencyModule.MUObjectivesWorkspace
{
    /// <summary>
    /// Class for holding measure calculations.
    /// </summary>
    public class MeasureCalculationsHolder : CustomNotificationObject
    {
        #region Constants and Fields

        private double _denominator;
        private double _numerator;
        private string _text;
        private string _value;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the denominator.
        /// </summary>
        /// <value>The denominator.</value>
        public double Denominator
        {
            get { return _denominator; }
            set { ApplyPropertyChange ( ref _denominator, () => Denominator, value ); }
        }

        /// <summary>
        /// Gets or sets the numerator.
        /// </summary>
        /// <value>The numerator.</value>
        public double Numerator
        {
            get { return _numerator; }
            set { ApplyPropertyChange ( ref _numerator, () => Numerator, value ); }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text for the holder.</value>
        public string Text
        {
            get { return _text; }
            set { ApplyPropertyChange ( ref _text, () => Text, value ); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return _value; }
            set { ApplyPropertyChange ( ref _value, () => Value, value ); }
        }

        #endregion
    }
}
