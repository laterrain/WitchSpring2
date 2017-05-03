using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//角色属性面板
public class CharacterHealth : MonoBehaviour 
{
    Slider hpBar;   //血条
    Text hpValue;   //血量
    Slider mpBar;   //蓝条
    Text mpValue;   //蓝量
    PlayerModel playerModel;//存储角色属性的类
    //角色各个属性的UI
    public Text hp;
    public Text sp;
    public Text magic;
    public Text str;
    public Text agile;
    public Text def;
	// Use this for initialization
	void Start () 
    {
        hpBar = GameObject.Find("Hp").GetComponent<Slider>();
        hpValue = GameObject.Find("Hpvalue").GetComponent<Text>();
        mpBar = GameObject.Find("Mp").GetComponent<Slider>();
        mpValue = GameObject.Find("Mpvalue").GetComponent<Text>();
        playerModel = PlayerModel.GetInstance();
	}
	
	// Update is called once per frame
	void Update () 
    {//角色属性面板的显示
        hpBar.value = (float)playerModel.Playerinfo[0].hp / (float)playerModel.Playerinfo[0].maxHp;
        hpValue.text = playerModel.Playerinfo[0].hp + "/" + playerModel.Playerinfo[0].maxHp;
        mpBar.value = (float)playerModel.Playerinfo[0].sp / (float)playerModel.Playerinfo[0].maxSp;
        mpValue.text = playerModel.Playerinfo[0].sp + "/" + playerModel.Playerinfo[0].maxSp;
        hp.text = playerModel.Playerinfo[0].maxHp.ToString();
        sp.text = playerModel.Playerinfo[0].maxSp.ToString();
        magic.text = playerModel.Playerinfo[0].magic.ToString();
        str.text = playerModel.Playerinfo[0].str.ToString();
        agile.text = playerModel.Playerinfo[0].agile.ToString();
        def.text = playerModel.Playerinfo[0].def.ToString();
	}
}
