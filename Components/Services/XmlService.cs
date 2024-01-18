using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Components.Services
{
    /// <summary>
    /// The XML service
    /// </summary>
    public static class XmlService
    {

        #region Public Methods

        /// <summary>
        /// Serializes the object into string format
        /// </summary>
        /// <typeparam name="Type">The data type of the object</typeparam>
        /// <param name="dataObj">the object instance</param>
        /// <param name="withXmlNameSpace">flag indicating if the serialized string should show xml name space</param>
        /// <param name="withXmlEncoding">flag indicating if the serialized string should show xml eoncoding attribute</param>
        /// <returns></returns>
        public static string Serialize<Type>(Type dataObj, bool withXmlNameSpace, bool withXmlEncoding)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Type));

            StringBuilder builder = new StringBuilder();

            XmlWriterSettings settings = null;
            XmlWriter xmlWriter = null;
            TextWriter writer = new StringWriter(builder);

            if (!withXmlEncoding)
            {
                settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.Indent = true;
                xmlWriter = XmlWriter.Create(writer, settings);
            }

            if (!withXmlNameSpace)
            {
                XmlSerializerNamespaces nameSpace = new XmlSerializerNamespaces();
                nameSpace.Add("", "");

                if (xmlWriter != null)
                    xmlSerializer.Serialize(xmlWriter, dataObj, nameSpace);
                else
                    xmlSerializer.Serialize(writer, dataObj, nameSpace);
            }
            else
            {
                if (xmlWriter != null)
                    xmlSerializer.Serialize(xmlWriter, dataObj);
                else
                    xmlSerializer.Serialize(writer, dataObj);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Deserialized an xml string into its counter part object
        /// </summary>
        /// <typeparam name="Type">The data type of the object to deserialize into</typeparam>
        /// <param name="xmlString">the xml string</param>
        /// <returns>A new instance that is type of Type representing the data within the xmk string</returns>
        public static Type Deserialize<Type>(string xmlString) where Type : new()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Type));

            TextReader reader = new StringReader(xmlString);
            object dataObj = xmlSerializer.Deserialize(reader);

            return (Type)dataObj;
        }

        #endregion
    }
}
