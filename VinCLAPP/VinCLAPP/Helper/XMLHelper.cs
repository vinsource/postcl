using System;
using System.Text;
using System.Xml;

namespace VinCLAPP.Helper
{
    public class XMLHelper
    {
        public static XmlNodeList SelectElements(string pathToElement, string pathToFile)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(pathToFile);
                String XPathExpression = "//" + pathToElement;
                XmlNodeList nodelist = doc.SelectNodes(XPathExpression);
                if (nodelist.Count > 0)
                    return nodelist;
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static XmlNode SelectFirstElement(string pathToElement, string pathToFile)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(pathToFile);
                String XPathExpression = "//" + pathToElement;
                XmlNodeList nodelist = doc.SelectNodes(XPathExpression);
                if (nodelist.Count > 0)
                    return nodelist.Item(0);
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static XmlNodeList SelectElements(string pathToElement, string pathToFile, string condition)
        {
            try
            {
                String XPathExpression;
                if (condition.Contains("&") || condition.Contains("||"))
                {
                    var paramlist = new string[2];
                    paramlist[0] = "&";
                    paramlist[1] = "||";

                    string[] splitString;

                    splitString = condition.Split(paramlist, StringSplitOptions.RemoveEmptyEntries);

                    var builder = new StringBuilder();
                    builder.Append("[");
                    foreach (string temp in splitString)
                    {
                        int index = temp.Trim().IndexOf('=');
                        string value = temp.Trim().Substring(index + 1);
                        string atrribute = temp.Trim().Substring(0, index);
                        builder.Append("@" + atrribute.Trim() + "=" + "\"" + value.Trim() + "\"");
                        builder.Append(" and ");
                    }
                    builder.Append("]");
                    int lastEnd = builder.ToString().LastIndexOf(" and ");
                    XPathExpression = pathToElement + builder.ToString().Remove(lastEnd, 5);
                }
                else
                {
                    int index = condition.Trim().IndexOf('=');
                    string value = condition.Trim().Substring(index + 1);
                    string atrribute = condition.Trim().Substring(0, index);

                    XPathExpression = pathToElement + "[translate" + "(@" + atrribute.Trim() +
                                      ",'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz')" + "=" + "\"" +
                                      value.ToLower().Trim() + "\"" + "]";
                }
                return SelectElements(XPathExpression, pathToFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static XmlNode SelectOneElement(string pathToElement, string pathToFile, string condition)
        {
            try
            {
                XmlNodeList nodelist = SelectElements(pathToElement, pathToFile, condition);
                if (nodelist == null)
                    return null;
                else
                {
                    if (nodelist.Count == 1)
                        return nodelist.Item(0);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetAttributeValue(XmlNode node, string attributeName)
        {
            try
            {
                foreach (XmlAttribute att in node.Attributes)
                {
                    if (att.Name.ToLower().Equals(attributeName.ToLower()))
                        return att.Value;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}