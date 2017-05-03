using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour 
{//道具信息的显示
    Image ico;//道具图标
    Image Infoico;//道具信息界面的图标
    Text num;//道具数量
    Text name;//道具名称
    Text explain;//道具描述
    Item item;//道具信息类
    GameObject ItemInfo;//道具信息的UI
	// Use this for initialization
	void Start () 
    {
        ico = GetComponent<Image>();
        ItemInfo = GameObject.FindGameObjectWithTag("ItemInfo");
        Infoico = ItemInfo.transform.FindChild("frame").FindChild("ico").GetComponent<Image>();
        num = ItemInfo.transform.FindChild("nameInf").FindChild("num").GetComponent<Text>();
        name = ItemInfo.transform.FindChild("nameInf").FindChild("name").GetComponent<Text>();
        explain = ItemInfo.transform.FindChild("explain").FindChild("explainText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        ShowGrid();
	}
    public void SetDatas(Item _item)//给道具赋值
    {
        item = _item;
    }
    public void ShowGrid()//背包中显示道具
    {
        if (item != null)
        {
            Sprite sprite = Resources.Load<Sprite>(item.sprite);//从Resources文件夹根据图片名称读取图片
            ico.sprite = sprite;            
            if (item.num <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void ShowInfo()//道具信息界面显示
    {
        if (item != null)
        {
            Infoico.sprite = Resources.Load<Sprite>(item.sprite);
            name.text = item.name;
            num.text = item.num.ToString();
            explain.text = item.explain;
        }
    }
    //public void UseItem()
    //{
    //    if (item.num > 0)
    //    {
    //        item.num--;
    //    }
    //    else
    //    {
    //        ItemModel itemModle = ItemModel.GetInstance();
    //        itemModle.bagList.Remove(this.GetComponent<Grid>().item);
    //        Destroy(gameObject);
    //    }
    //}
}
