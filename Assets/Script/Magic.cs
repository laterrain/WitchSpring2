using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.Script
{
    public class Magic
    {//单个魔法的信息类
        public string id;
        public string name;
        public float power;
        public int needMp;
        public string type;
        public string explain;

        public void SetData(XmlElement xml)
        {//从xml表中的数据给魔法赋值
            id = xml.GetAttribute("id");
            name = xml.GetAttribute("name");
            power = float.Parse(xml.GetAttribute("power"));
            needMp = XmlTools.GetIntAttribute(xml, "needMp");
            type = xml.GetAttribute("type");
            explain = xml.GetAttribute("explain");
        }
    }
}
