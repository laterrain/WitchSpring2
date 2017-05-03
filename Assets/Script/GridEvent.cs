using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Assets.Script;

public class GridEvent : MonoBehaviour 
{
    GameObject itemInfo;//道具信息界面的UI
    Text name;//道具名称
    Animator anim;//动画
    Grid gridScript;//道具上的脚本
    ItemModel itemModel;//存储道具信息的类
    PlayerModel playerModel;//角色信息类
    PlayerPos playerPos;//角色位置信息类
    public GameObject story5;//剧情
    // Use this for initialization
    void Start () 
    {
        itemInfo = GameObject.FindGameObjectWithTag("ItemInfo");
        name = itemInfo.transform.FindChild("nameInf").FindChild("name").GetComponent<Text>();
        anim = itemInfo.GetComponent<Animator>();
        gridScript = GetComponent<Grid>();
        itemModel = ItemModel.GetInstance();
        playerModel = PlayerModel.GetInstance();
        playerPos = PlayerPos.GetInstance();
    }
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    public void OpenUI()
    {
        anim.SetBool("IsOpen", !anim.GetBool("IsOpen"));//打开/关闭UI
        if (gridScript != null)
        {
            gridScript.ShowInfo();//显示道具信息
        }
        if (playerPos.storyIndex == 5)
        {//触发剧情
            story5.SetActive(true);
            playerPos.storyIndex = 6;
        }        
    }
    public void UseItem()//使用道具
    {
        for (int i = 0; i < itemModel.bagList.Count; i++)//遍历背包道具
        {
            if (itemModel.bagList[i].name.Equals(name.text) && !itemModel.bagList[i].type.Equals("material"))
            {//找到对应的道具并且道具类型不是meterial，才可以使用
                if (itemModel.bagList[i].num>0)
                {
                    itemModel.bagList[i].num--;
                }
                else
                {
                    itemModel.bagList.Remove(itemModel.bagList[i]);
                }
                if (itemModel.bagList[i].type.Equals("magic"))
                {//道具类型是magic则提升角色的magic
                    playerModel.Playerinfo[0].magic += itemModel.bagList[i].power;
                }
                if (itemModel.bagList[i].type.Equals("hp"))
                {//道具类型是hp则恢复角色hp
                    playerModel.Playerinfo[0].hp += itemModel.bagList[i].power;
                    if (playerModel.Playerinfo[0].hp >= playerModel.Playerinfo[0].maxHp)
                    {
                        playerModel.Playerinfo[0].hp = playerModel.Playerinfo[0].maxHp;
                    }
                }
                if (itemModel.bagList[i].type.Equals("maxHp"))
                {//道具类型是maxhp则提升角色maxhp
                    playerModel.Playerinfo[0].maxHp += itemModel.bagList[i].power;
                }
            }
        }        
    }
}
