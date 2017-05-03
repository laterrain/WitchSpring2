using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour 
{
    GameObject grid;    //道具
    GameObject content; //场景中道具的父物体
    ItemModel itemModel;    //道具的单例
	// Use this for initialization
	void Start () 
    {
        grid = Resources.Load<GameObject>("item");  //从resources文件夹读取道具的预制物
        content = GameObject.Find("Content").gameObject;
        itemModel = ItemModel.GetInstance();
        OpenBag();
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
    public void OpenBag()   //显示道具
    {
        for (int i = 0; i < itemModel.bagList.Count; i++)   //遍历背包道具列表中的道具
        {
            GameObject go = Instantiate(grid);  //将读取的道具预制物在场景中创建出来
            Grid gridScript = go.GetComponent<Grid>();  //获取道具身上的脚本
            gridScript.SetDatas(itemModel.bagList[i]);  //给道具赋值
            go.transform.SetParent(content.transform);  //设置道具的父物体
            (go.transform as RectTransform).localPosition = Vector3.zero;   //设置道具相对于父物体的位置
            (go.transform as RectTransform).localScale = Vector3.one;   //设置道具相对于父物体的缩放
        }
    }
    public void AddItem(Item item)  //获得道具
    {
        for (int i = 0; i < itemModel.bagList.Count; i++)   //遍历背包道具列表中的每个道具
        {
            Item _item = itemModel.bagList[i];  //判断背包中是否有该道具
            if (_item.id == item.id)
            {
                _item.num++;    //有则直接增加该道具的数量
                return;
            }
        }
        item.num++;
        itemModel.bagList.Add(item);//没有则向背包中添加该道具
        GameObject go = Instantiate(grid);
        Grid gridScript = go.GetComponent<Grid>();
        gridScript.SetDatas(item);
        go.transform.SetParent(content.transform);
        (go.transform as RectTransform).localPosition = Vector3.zero;
        (go.transform as RectTransform).localScale = Vector3.one;
    }
}
