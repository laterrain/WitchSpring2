using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.Script
{
    public class PlayerInfo
    {
        public string id;
        public string name;
        public int hp;
        public int maxHp;
        public int sp;
        public int maxSp;
        public int str;
        public int magic;
        public int def;
        public int agile;

        public void SetData(XmlElement xml)
        {
            id = xml.GetAttribute("ID");
            name = xml.GetAttribute("name");
            hp = XmlTools.GetIntAttribute(xml, "hp");
            maxHp = XmlTools.GetIntAttribute(xml, "maxHp");
            sp = XmlTools.GetIntAttribute(xml, "sp");
            maxSp = XmlTools.GetIntAttribute(xml, "maxSp");
            str = XmlTools.GetIntAttribute(xml, "str");
            magic = XmlTools.GetIntAttribute(xml, "magic");
            def = XmlTools.GetIntAttribute(xml, "Def");
            agile = XmlTools.GetIntAttribute(xml, "agile");
        }
    }
}
