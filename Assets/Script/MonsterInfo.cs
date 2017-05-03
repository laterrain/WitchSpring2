using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInfo : MonoBehaviour 
{
    public string enemyID; //怪物的ID
    public string name;    //怪物的名字
    public int hp;         //怪物的生命
    public int maxHp;       //怪物的最大生命值
    public int atk;        //怪物的攻击
    public int sp;         //怪物的sp
    public int aDef;       //怪物的物理防御
    public int mDef;       //怪物的魔法防御
    public string lostItem;//怪物掉落的道具
    public string explain; //怪物的描述
    MonsterVo monsterVo;
    Slider monsterHpBar;
    Text hpvalue;
	// Use this for initialization
	void Start () 
    {
        monsterHpBar = transform.FindChild("MonsterHpBar").GetComponent<Slider>();
        hpvalue = transform.FindChild("MonsterHpBar").FindChild("HpValue").GetComponent<Text>();
        if (monsterVo != null)
        {
            SetEnemyInfo();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        monsterHpBar.value = (float)hp / (float)maxHp;
        hpvalue.text = hp + "/" + maxHp;
	}
    public void GetEnemyInfo(MonsterVo _monsterVo)
    {
        monsterVo = _monsterVo;
    }
    public void SetEnemyInfo()
    {
        enemyID = monsterVo.enemyID;
        name = monsterVo.name;
        maxHp = monsterVo.hp;
        hp = maxHp;
        atk = monsterVo.atk;
        sp = monsterVo.sp;
        aDef = monsterVo.aDef;
        mDef = monsterVo.mDef;
        lostItem = monsterVo.lostItem;
        explain = monsterVo.explain;
    }
}
