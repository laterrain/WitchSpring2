using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicShow : MonoBehaviour 
{//魔法界面的显示
    public GameObject[] magicPanels;
    MagicModel magicModel;
	// Use this for initialization
	void Start () 
    {
        magicModel = MagicModel.GetInstance();
        SetMagicData();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    public void SetMagicData()
    {//给魔法界面的UI赋值
        for (int i = 0; i < magicModel.magicList.Count; i++)
        {
            magicPanels[i].GetComponent<MagicUse>().SetDatas(magicModel.magicList[i]);
        }
    }
}
