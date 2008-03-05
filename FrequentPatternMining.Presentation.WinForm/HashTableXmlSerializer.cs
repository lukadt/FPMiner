using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Collections;
using System.IO;
using System.Xml.Schema;

namespace FrequentPatternMining.Presentation.WinForm
{
    public class HashTableXmlSerializer : IXmlSerializable
    {
        private IDictionary dictionary;

        public HashTableXmlSerializer()
        {
            this.dictionary = new Hashtable();
        }

        private HashTableXmlSerializer(IDictionary dictionary)
        {
            this.dictionary = dictionary;
        }

        public static void Serialize(IDictionary dictionary, Stream stream)
        {
            HashTableXmlSerializer ds = new HashTableXmlSerializer(dictionary);
            XmlSerializer xs = new XmlSerializer(typeof(HashTableXmlSerializer));
            xs.Serialize(stream, ds);
        }

        public static IDictionary Deserialize(XmlReader reader)
        {
            XmlSerializer xs = new XmlSerializer(typeof(HashTableXmlSerializer));
            HashTableXmlSerializer ds = (HashTableXmlSerializer)xs.Deserialize(reader);
            return ds.dictionary;
        }

        public static IDictionary Deserialize(TextReader textReader)
        {
            XmlSerializer xs = new XmlSerializer(typeof(HashTableXmlSerializer));
            HashTableXmlSerializer ds = (HashTableXmlSerializer)xs.Deserialize(textReader);
            return ds.dictionary;
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            reader.Read();
            reader.ReadStartElement("Map");
            while (reader.NodeType != XmlNodeType.EndElement)
            {
                reader.ReadStartElement("Item");
                string key = reader.ReadElementString("Key");
                string value = reader.ReadElementString("Value");
                reader.ReadEndElement();
                reader.MoveToContent();
                dictionary.Add(key, value);
            }
            reader.ReadEndElement();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Map");
            foreach (object key in dictionary.Keys)
            {
                object value = dictionary[key];
                writer.WriteStartElement("Item");
                writer.WriteElementString("Key", key.ToString());
                writer.WriteElementString("Value", value.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
