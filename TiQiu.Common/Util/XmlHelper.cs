using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Configuration;

namespace TiQiu.Common.Util
{
    internal static class XmlHelper
    {
        public static XmlNode GetNode(XmlDocument xmlDoc, string xpath)
        {
            return xmlDoc.SelectSingleNode(xpath);
        }

        public static string GetNodeValue(XmlDocument xmlDoc, string xpath)
        {
            XmlNode node = xmlDoc.SelectSingleNode(xpath);
            if (node == null)
            {
                return null;
                
            }
            return GetNodeValue(node);
        }

        public static string GetNodeValue(XmlNode node)
        {
            return node.InnerText;
        }

        public static string GetNodeAttribute(XmlDocument xmlDoc, string xpath, string attribute)
        {
            XmlNode node = xmlDoc.SelectSingleNode(xpath);
            if (node == null)
            {
                return null;
            }
            return GetNodeAttribute(node, attribute);
        }

        public static string GetNodeAttribute(XmlNode node, string attributeName)
        {
            if (node.Attributes == null
                        || node.Attributes[attributeName] == null
                        || node.Attributes[attributeName].Value == null
                        || node.Attributes[attributeName].Value.Trim() == string.Empty)
            {
                return null; 
            }

            return node.Attributes[attributeName].Value.Trim();
        }

        public static XmlNode[] GetChildrenNodes(XmlNode node, string nodeName)
        {
            return GetChildrenNodes(node, delegate(XmlNode child) {
                return child.Name == nodeName;
            });
        }

        public static XmlNode[] GetChildrenNodes(XmlNode node, Predicate<XmlNode> match)
        {
            if (node == null || node.ChildNodes == null || node.ChildNodes.Count <= 0)
            {
                return new XmlNode[0];
            }
            List<XmlNode> nodeList = new List<XmlNode>(node.ChildNodes.Count);
            foreach(XmlNode child in node.ChildNodes)
            {
                if (match(child))
                {
                    nodeList.Add(child);
                }
            }
            return nodeList.ToArray();
        }
    }
}
