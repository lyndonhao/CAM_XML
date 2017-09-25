using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlTools
{
    public class XmlTool
    {
        static XmlTextWriter xmlWriter;

        /// <summary>
        /// 创建XML文件及声明
        /// </summary>
        /// <param name="strPath">表名</param>
        public static bool createXmlFile(string xmlPath)
        {
            //创建一个xml文档
            xmlWriter = new XmlTextWriter(xmlPath, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();
            return true;
        }

       
        /// <summary>
        /// 添加元素，带属性，一个
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        public static void createTable(string fileName, string attributeName, string attributeValue)
        {
            xmlWriter.WriteStartElement(fileName);
            xmlWriter.WriteAttributeString(attributeName, attributeValue);          
        }


        /// <summary>
        /// 添加元素，带属性，多个
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        public static void createTable(string fileName, string[] attributeName, string[] attributeValue)
        {
            xmlWriter.WriteStartElement(fileName);

            for (int i = 0; i < attributeName.Length; i++)
            {
                xmlWriter.WriteAttributeString(attributeName[i], attributeValue[i]);
            } 
        }


        /// <summary>
        /// 添加元素，带属性，两个
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <param name="attributeName1">属性名1</param>
        /// <param name="attributeValue1">属性值1</param>
        public static void createTable(string fileName, string attributeName, string attributeValue, string attributeName1, string attributeValue1)
        {
            xmlWriter.WriteStartElement(fileName);
            xmlWriter.WriteAttributeString(attributeName, attributeValue);
            xmlWriter.WriteAttributeString(attributeName1, attributeValue1);
        }


        /// <summary>
        /// 添加空白（default）
        /// </summary>
        public static void createWhiteSpace()
        {
            xmlWriter.WriteWhitespace("\n        ");
        }


        /// <summary>
        /// 表结束
        /// </summary>
        public static void endTable()
        {
            xmlWriter.WriteEndElement();
        }


        /// <summary>
        /// 关闭
        /// </summary>
        public static void closeTable()
        {
            xmlWriter.Close();
        }


        /// <summary>
        /// 添加元素，无属性
        /// </summary>
        /// <param name="fileName">表名</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        public static void createTable(string fileName)
        {
            xmlWriter.WriteStartElement(fileName);
            //xmlWriter.WriteAttributeString("", "");
        }


        //*********************未用
        #region
        public void createTableHeade(string fileName)
        {
            //Database表
            //xmlWriter.WriteStartElement(fileName);
            //xmlWriter.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            //xmlWriter.WriteAttributeString("xmlns", "http://www.staubli.com/robotics/VAL3/Data/2");

            xmlWriter.WriteElementString("ss", "we", "df", "123");
        }

        public void createTableElement(string fileName, string attributeName, string attributeValue)
        {
            xmlWriter.WriteStartElement(fileName);
            xmlWriter.WriteElementString(attributeName, attributeValue);

        }

        public void createTableAttribute(string attributeName, string attributeValue)
        {

            xmlWriter.WriteElementString(attributeName, attributeValue);
            xmlWriter.WriteWhitespace("\n   ");
        }
 
        public void createElement(string fileName, string[] attributeName, string[] attributeValue)
        {
            //xmlWriter.WriteElementString();

            xmlWriter.WriteRaw("/n  <item>/n" +

                    "    <title>Unreal Tournament 2003</title>/n" +

                    "    <format>CD</format>/n" +

                    "  </item>/n");
        }

        public void endAttribute()
        {
            xmlWriter.WriteEndAttribute();
        }
        #endregion
        //**********************
    }


}
