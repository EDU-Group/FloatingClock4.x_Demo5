using System.Xml.Serialization;
using System.Xml;

namespace xfsz_xml
{
    public class Xml
    {
        /// <summary>
        /// Xml帮助类
        /// </summary>

        #region Xml反序列化为对象

        /// <summary>
        /// Xml反序列化为指定模型对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xmlContent">Xml内容</param>
        /// <param name="isThrowException">是否抛出异常</param>
        /// <returns></returns>
        public static T XmlConvertToModel<T>(string xmlContent, bool isThrowException = false) where T : class
        {
            StringReader stringReader = null;
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                stringReader = new StringReader(xmlContent);
                return (T)xmlSerializer.Deserialize(stringReader);
            }
            catch (Exception ex)
            {
                if (isThrowException)
                {
                    throw ex;
                }
                return null;
            }
            finally
            {
                stringReader?.Dispose();
            }
        }

        /// <summary>     
        /// 读取Xml文件内容反序列化为指定的对象  
        /// </summary>    
        /// <param name="filePath">Xml文件的位置（绝对路径）</param>  
        /// <returns></returns>    
        public static T DeserializeFromXml<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new ArgumentNullException(filePath + " not Exists");
                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    T ret = (T)xs.Deserialize(reader);
                    return ret;
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        #endregion

        #region 对象序列化为Xml

        /// <summary>
        /// 对象序列化为Xml
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="isThrowException">是否抛出异常</param>
        /// <returns></returns>
        public static string ObjectSerializerXml<T>(T obj, bool isThrowException = false)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    Type t = obj.GetType();
                    //强制指定命名空间，覆盖默认的命名空间  
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    //在Xml序列化时去除默认命名空间xmlns:xsd和xmlns:xsi
                    namespaces.Add(string.Empty, string.Empty);
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    //序列化时增加namespaces
                    serializer.Serialize(sw, obj, namespaces);
                    sw.Close();

                    string replaceStr = sw.ToString().Replace(@"<?xml version=""1.0"" encoding=""utf-16""?>", "");
                    return replaceStr;
                }
            }
            catch (Exception ex)
            {
                if (isThrowException)
                {
                    throw ex;
                }
                return string.Empty;
            }

        }

        #endregion

        #region Xml字符处理

        /// <summary>
        /// 特殊符号转换为转义字符
        /// </summary>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public string XmlSpecialSymbolConvert(string xmlStr)
        {
            return xmlStr.Replace("&", "&").Replace("<", "<").Replace(">", ">").Replace("\'", "&apos;");
        }

        #endregion

        #region 创建Xml文档

        /// <summary>
        /// 创建Xml文档
        /// </summary>
        /// <param name="saveFilePath">文件保存位置</param>
        public void CreateXmlDocument(string saveFilePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //创建类型声明节点  
            XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "");
            xmlDoc.AppendChild(node);
            //创建Xml根节点
            XmlNode root = xmlDoc.CreateElement("books");
            xmlDoc.AppendChild(root);

            XmlNode root1 = xmlDoc.CreateElement("book");
            root.AppendChild(root1);

            //创建子节点
            CreateNode(xmlDoc, root1, "author", "追逐时光者");
            CreateNode(xmlDoc, root1, "title", "XML学习教程");
            CreateNode(xmlDoc, root1, "publisher", "时光出版社");

            //将文件保存到指定位置
            xmlDoc.Save(saveFilePath/*"D://xmlSampleCreateFile.xml"*/);
        }

        /// <summary>    
        /// 创建节点    
        /// </summary>    
        /// <param name="xmlDoc">xml文档</param>    
        /// <param name="parentNode">Xml父节点</param>    
        /// <param name="name">节点名</param>    
        /// <param name="value">节点值</param>    
        ///   
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            //创建对应Xml节点元素
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

        #endregion

        #region Xml数据读取

        /// <summary>
        /// 读取Xml指定节点中的数据
        /// </summary>
        /// <param name="filePath">Xml文档路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">读取数据的属性名</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.XmlReadNodeAttributeValue(path, "/books/book", "author")
         ************************************************/
        public static string XmlReadNodeAttributeValue(string filePath, string node, string attribute)
        {
            string value = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xmlNode = doc.SelectSingleNode(node);
                value = (attribute.Equals("") ? xmlNode.InnerText : xmlNode.Attributes[attribute].Value);
            }
            catch { }
            return value;
        }


        /// <summary>
        /// 获得xml文件中指定节点的节点数据
        /// </summary>
        /// <param name="filePath">Xml文档路径</param>
        /// <param name="nodeName">节点名</param>
        /// <returns></returns>
        public static string GetNodeInfoByNodeName(string filePath, string nodeName)
        {
            string XmlString = string.Empty;
            XmlDocument xml = new XmlDocument();
            xml.Load(filePath);
            XmlElement root = xml.DocumentElement;
            XmlNode node = root.SelectSingleNode("//" + nodeName);
            if (node != null)
            {
                XmlString = node.InnerText;
            }
            return XmlString;
        }

        /// <summary>
        /// 获取某一节点的所有孩子节点的值
        /// </summary>
        /// <param name="node">要查询的节点</param>
        /// <param name="filePath">Xml文档路径</param>
        public string[] ReadAllChildallValue(string node, string filePath)
        {
            int i = 0;
            string[] str = { };
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode(node);
            XmlNodeList nodelist = xn.ChildNodes;  //得到该节点的子节点
            if (nodelist.Count > 0)
            {
                str = new string[nodelist.Count];
                foreach (XmlElement el in nodelist)//读元素值
                {
                    str[i] = el.Value;
                    i++;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取某一节点的所有孩子节点的值
        /// </summary>
        /// <param name="node">要查询的节点</param>
        /// <param name="filePath">Xml文档路径</param>
        public XmlNodeList ReadAllChild(string node, string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode(node);
            XmlNodeList nodelist = xn.ChildNodes;  //得到该节点的子节点
            return nodelist;
        }

        #endregion

        #region Xml插入数据

        /// <summary>
        /// Xml指定节点元素属性插入数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="element">元素名</param>
        /// <param name="attribute">属性名</param>
        /// <param name="value">属性数据</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.XmlInsertValue(path, "/books", "book", "author", "Value")
         ************************************************/
        public static void XmlInsertValue(string path, string node, string element, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xmlNode = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xmlNode;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    //添加新增的节点
                    xmlNode.AppendChild(xe);
                }

                //保存Xml文档
                doc.Save(path);
            }
            catch { }
        }

        #endregion

        #region Xml修改数据

        /// <summary>
        /// Xml指定节点元素属性修改数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名</param>
        /// <param name="value">属性数据</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.XmlUpdateValue(path, "/books", "book","author","Value")
         ************************************************/
        public static void XmlUpdateValue(string path, string node, string attribute, string value)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xmlNode = doc.SelectSingleNode(node);
                XmlElement xmlElement = (XmlElement)xmlNode;
                if (attribute.Equals(""))
                    xmlElement.InnerText = value;
                else
                    xmlElement.SetAttribute(attribute, value);

                //保存Xml文档
                doc.Save(path);
            }
            catch { }
        }


        #endregion

        #region Xml删除数据

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
         * XmlHelper.XmlDelete(path, "/books", "book")
         ************************************************/
        public static void XmlDelete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(path);
            }
            catch { }
        }

        #endregion
    }

}

