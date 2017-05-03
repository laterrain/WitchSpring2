using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.Script
{
    public class MonsterVo
    {
        public string enemyID;
        public string name;
        public int hp;
        public int atk;
        public int sp;
        public int aDef;
        public int mDef;
        public string lostItem;
        public string explain;

        public void SetData(XmlElement xml)
        {
            enemyID = xml.GetAttribute("enemyID");
            name = xml.GetAttribute("name");
            hp = XmlTools.GetIntAttribute(xml, "hp");
            atk = XmlTools.GetIntAttribute(xml, "atk");
            sp = XmlTools.GetIntAttribute(xml, "sp");
            aDef = XmlTools.GetIntAttribute(xml, "aDef");
            mDef = XmlTools.GetIntAttribute(xml, "mDef");
            lostItem = xml.GetAttribute("lostItem");
            explain = xml.GetAttribute("explain");
        }
    }
    static class XmlTools
    {
        public static int GetIntAttribute(XmlElement xml,string attr)
        {
            return int.Parse(xml.GetAttribute(attr));
        }
    }
}
