using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.Script
{   //道具类，用于存储单个道具的信息
    public class Item
    {
        public string id;
        public string name;
        public string type;
        public int power;
        public string sprite;
        public string explain;
        public int num;

        public void SetData(XmlElement xml)
        {//通过读取xml表来给道具赋值
            id = xml.GetAttribute("id");
            name = xml.GetAttribute("name");
            type = xml.GetAttribute("type");
            power = XmlTools.GetIntAttribute(xml, "power");
            sprite = xml.GetAttribute("spriteName");
            explain = xml.GetAttribute("explain");
        }
    }
    
}
