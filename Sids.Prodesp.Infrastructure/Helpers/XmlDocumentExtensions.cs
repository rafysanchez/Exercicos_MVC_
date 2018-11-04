namespace Sids.Prodesp.Infrastructure.Helpers
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;
    using System.Collections.Specialized;

    public static class XmlDocumentExtensions
    {
        public static XmlDocument ToXml(this string text, string source)
        {
            var document = new XmlDocument();

            try
            {
                document.LoadXml(text);
            }
            catch (Exception ex)
            {
                throw new Exception($"{source.ToUpper()} - {text.ToUpper()}");
            }

            return document;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static T ConvertNodeTo<T>(this XmlNodeList node) where T : class
        {
            MemoryStream stream = null;
            T retorno = null;

            try
            {
                stream = new MemoryStream();
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(node[node.Count - 1].OuterXml);
                    writer.Flush();

                    stream.Position = 0;

                    XmlRootAttribute theAttrib = Attribute.GetCustomAttribute(typeof(T), typeof(XmlRootAttribute)) as XmlRootAttribute;


                    //var serializer = new XmlSerializer(typeof(T), node[node.Count - 1].Name);
                    var serializer = new XmlSerializer(typeof(T), theAttrib.ElementName);

                    retorno = serializer.Deserialize(stream) as T;

                    stream = null;
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }

            return retorno;
        }

        public static T Deserialize<T>(this XmlDocument document) where T : class
        {

            XmlRootAttribute theAttrib = Attribute.GetCustomAttribute(typeof(T), typeof(XmlRootAttribute)) as XmlRootAttribute;
            XmlSerializer serializer = new XmlSerializer(typeof(T), theAttrib.ElementName);
            using (XmlReader reader = new XmlNodeReader(document))
            {
                T response = (T)serializer.Deserialize(reader);

                return response;
            }
        }

        public static XmlDocument LoadXmlDocumentFromXmlStream<T>(T document, TextWriter stream) where T : class
        {
            SerializeXmlStream(document, stream);

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(stream.ToString());
            xmlDocument.GetElementsByTagName(typeof(T).Name).Item(0).Attributes.RemoveAll();
            xmlDocument.RemoveChild(xmlDocument.FirstChild);
            return xmlDocument;
        }

        public static XmlDocument GenerateIdForTagName(this XmlDocument document, bool tagId, params string[] tagNames)
        {
            if (tagId) return document;

            foreach (var tagName in tagNames)
                document.GenerateIdForTagName(tagName);

            return document;
        }

        public static XmlDocument RemoveRepeatedTag(this XmlDocument document, string tagName)
        {
            document.InnerXml = document.InnerXml?.Replace($"<{tagName}>", "").Replace($"</{tagName}></{tagName}>", $"</{tagName}>");

            return document;
        }

        public static XmlDocument RemoveRepeatedTagWhitoutId(this XmlDocument document, string tagName)
        {
            document.InnerXml = document.InnerXml?.Replace($"<{tagName}><{tagName}>", $"<{tagName}>").Replace($"</{tagName}></{tagName}>", $"</{tagName}>");

            return document;
        }

        public static XmlDocument RemoveTag(this XmlDocument document, string tagName)
        {
            document.InnerXml = document.InnerXml?.Replace($"<{tagName}>", "").Replace($"</{tagName}>", "");

            return document;
        }

        public static XmlDocument RepairSelfClosingTag(this XmlDocument document, string tagName)
        {
            document.InnerXml = document.InnerXml?.Replace($"<{tagName} />", $"<{tagName}></{tagName}>");

            return document;
        }

        public static XmlDocument RepairRepeatedTag(this XmlDocument document, string tagName)
        {
            document.InnerXml = document.InnerXml.Replace($"<{tagName}><{tagName}>", $"<{tagName}>").Replace($"</{tagName}></{tagName}>", $"</{tagName}>");

            return document;
        }


        static void GenerateIdForTagName(this XmlDocument document, string tagName)
        {
            var node = document.GetElementsByTagName(tagName);

            for (var i = 1; i < node.Count; i++)
            {
                var attr = document.CreateAttribute("Id");
                attr.Value = i.ToString();
                node[i].Attributes.Append(attr);
            }

            document.RemoveRepeatedTag(tagName);
        }

        static void SerializeXmlStream<T>(this T document, TextWriter stream) where T : class
        {
            var serializer = new XmlSerializer(document.GetType());
            serializer.Serialize(stream, document);
        }

        public static XmlDocument RepairRepeatedTagWithNumbers(this XmlDocument document, string tagName)
        {
            return RepairRepeatedTagWithNumbers(document, tagName, null);
        }
        public static XmlDocument RepairRepeatedTagWithNumbers(this XmlDocument document, string tagName, StringDictionary tagsToRename)
        {
            var oldElement = document.GetElementsByTagName(tagName)[0];
            var element = document.GetElementsByTagName(tagName)[0];

            if (element != null && oldElement != null)
            {
                var pattern = @"(?<parteOk>\w+)(?<parteRemover>\d)+";
                Regex regex = new Regex(pattern);

                RenameTags(document, tagsToRename, element, regex);

                var documento = document.GetElementsByTagName("documento");

                ReplaceDocument(tagName, oldElement, element, documento);
            }

            return document;
        }

        private static void RenameTags(XmlDocument document, StringDictionary tagsToRename, XmlNode element, Regex regex)
        {
            foreach (XmlNode item in element.ChildNodes)
            {
                var tags = item.ChildNodes;
                for (int i = 0; i < tags.Count; i++)
                {
                    var tag = tags[i];
                    var match = regex.Match(tag.Name);

                    if (match.Success)
                    {
                        var newName = regex.Replace(tag.Name, match.Groups["parteOk"].Value);

                        if (tagsToRename != null && tagsToRename.Count > 0)
                        {
                            newName = tagsToRename[newName] ?? newName;
                        }

                        XmlNode newNode = document.CreateElement(newName);

                        newNode.InnerText = tag.InnerText;

                        item.InsertBefore(newNode, tag);
                        item.RemoveChild(tag);
                    }
                }
            }
        }

        private static void ReplaceDocument(string tagName, XmlNode oldElement, XmlNode element, XmlNodeList documento)
        {
            foreach (XmlNode item in documento)
            {
                var nodeXml = new XmlDocument();
                nodeXml.LoadXml(item.OuterXml);

                if (nodeXml.GetElementsByTagName(tagName).Count > 0)
                {
                    item.ReplaceChild(element, oldElement);
                }
            }
        }
    }
}
