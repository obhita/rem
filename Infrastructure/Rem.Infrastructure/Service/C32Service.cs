using System.ServiceModel;
using System.ServiceModel.Activation;
using C32Gen;

namespace Rem.Infrastructure.Service
{
    /// <summary>
    /// WCF Service to get C32 string based on Patient Key.
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class C32Service : IC32Service
    {
        private readonly IC32Builder _c32Builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="C32Service"/> class.
        /// </summary>
        /// <param name="c32Builder">The C32 builder.</param>
        public C32Service (IC32Builder c32Builder)
        {
            _c32Builder = c32Builder;
        }

        /// <summary>
        /// Gets the C32.
        /// </summary>
        /// <param name="patientKey">The patient key.</param>
        /// <returns>
        /// A C32 string.
        /// </returns>
        public string GetC32(long patientKey)
        {
            var c32Xml = _c32Builder.BuildC32Xml(patientKey, false);
            return c32Xml;
        }
    }
}