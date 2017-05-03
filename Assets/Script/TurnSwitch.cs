using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSwitch : MonoBehaviour 
{
    public bool playerTurn = false; //角色回合
    public bool monsterTurn = false;    //怪物回合
    public GameObject player;   //角色
    Animator animPlayer;    //角色动画
    CharacterController character;  //角色身上的脚本
    GameObject target = null;   //目标怪物
    Animator animMonster;   //怪物动画
    MonsterController monster;  //怪物身上的脚本
    float policeRange = 3f; //怪物警戒范围
    public GameObject fightMenu;
    AudioSource turnChange;    
    // Use this for initialization
    void Start () 
    {
        animPlayer = player.GetComponent<Animator>();
        character = player.GetComponent<CharacterController>();
        turnChange = GameObject.Find("TurnChangeAudio").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetTarget();
        if (playerTurn && !character.isFight)
        {
            turnChange.Play();
            character.isFight = true;
            character.isFightView = true;
            animPlayer.SetBool("inAttack", true);
            fightMenu.SetActive(true);
            player.transform.LookAt(target.transform.FindChild("targetPos"));
            target.transform.LookAt(player.transform.FindChild("TargetPos"));
        }
        else if (monsterTurn && !monster.isFight)
        {
            turnChange.Play();
            monster.isFight = true;
            monster.MoveToAttack();
        }
	}
    public void GetTarget()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemys.Length; i++)
        {
            if (Vector3.Distance(player.transform.position, enemys[i].transform.position) <= policeRange + 1 && target == null)
            {
                target = enemys[i];
                animMonster = target.GetComponent<Animator>();
                monster = target.GetComponent<MonsterController>();
                break;
            }            
        }
        if (target != null && Vector3.Distance(player.transform.position, target.transform.position) > policeRange * 2)
        {
            target = null;
        }
        if (target == null)
        {
            return;
        }
    }
}
