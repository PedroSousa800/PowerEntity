﻿using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using PowerEntity.Model;

namespace Tools
{
    public class Serializer
    {
        public T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        public string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);

                var outputXML = textWriter.ToString();

                int index = outputXML.IndexOf("\r\n");

                return outputXML.Substring(index + 1);

                //return textWriter.ToString();
            }
        }
    }
}