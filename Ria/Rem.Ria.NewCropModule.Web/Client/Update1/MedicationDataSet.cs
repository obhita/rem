using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Rem.Ria.NewCropModule.Web.Client.Update1
{
    /// <remarks/>
    [GeneratedCode("System.Xml", "4.0.30319.225")]
    [Serializable]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, TypeName = "NewDataSet")]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "NewDataSet")]
    public class MedicationDataSet
    {
        private List<MedicationTable> itemsField;

        /// <remarks/>
        [XmlElement("Table", Form = XmlSchemaForm.Unqualified)]
        public virtual List<MedicationTable> Items
        {
            get { return itemsField; }
            set { itemsField = value; }
        }
    }
}