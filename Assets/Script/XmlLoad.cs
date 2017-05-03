using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using Assets.Script;


public class XmlLoad : MonoBehaviour 
{
    public TextAsset xmlEnemy;
    public TextAsset xmlItem;
    public TextAsset xmlPlayer;
    public TextAsset xmlMagic;
    MonsterModel monsterModel;
    ItemModel itemModel;
    MagicModel magicModel;
    PlayerModel playerModel;
	// Use this for initialization
	void Start () 
    {
        monsterModel = MonsterModel.GetInstance();
        itemModel = ItemModel.GetInstance();
        magicModel = MagicModel.GetInstance();
        playerModel = PlayerModel.GetInstance();
        OnXmlLoadEnemy();
        OnXmlLoadItem();
        OnXmlLoadPlayer();
        OnXmlLoadMagic();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    void OnXmlLoadEnemy()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlEnemy.text);
        XmlNodeList list = xmlDoc.SelectSingleNode("root").ChildNodes;
        for (int i = 0; i < list.Count; i++)
        {
            MonsterVo monsterVo = new MonsterVo();
            monsterVo.SetData(list[i] as XmlElement);
            monsterModel.monsterList.Add(monsterVo);
        }
    }
    void OnXmlLoadItem()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlItem.text);

        XmlNodeList list = xmlDoc.SelectSingleNode("root").ChildNodes;
        for (int i = 0; i < list.Count; i++)
        {
            Item item = new Item();
            item.SetData(list[i] as XmlElement);
            itemModel.itemList.Add(item);
        }
    }
    void OnXmlLoadPlayer()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlPlayer.text);

        XmlNodeList list = xmlDoc.SelectSingleNode("root").ChildNodes;
        for (int i = 0; i < list.Count; i++)
        {
            PlayerInfo playerInfo = new PlayerInfo();
            playerInfo.SetData(list[i] as XmlElement);
            playerModel.Playerinfo.Add(playerInfo);
        }
    }
    void OnXmlLoadMagic()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlMagic.text);
        XmlNodeList list = xmlDoc.SelectSingleNode("root").ChildNodes;
        for (int i = 0; i < list.Count; i++)
        {
            Magic magic = new Magic();
            magic.SetData(list[i] as XmlElement);
            magicModel.magicList.Add(magic);
        }
    }
}
