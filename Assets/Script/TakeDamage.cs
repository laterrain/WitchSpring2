using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour 
{
    public GameObject player;
    PlayerModel playerModel;
    GameObject target;
    MonsterInfo monsterInfo;
    float policeRange = 3f;//怪物警戒距离
	// Use this for initialization
	void Start () 
    {
        player = GameObject.Find("luna");
        playerModel = PlayerModel.GetInstance();
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetTarget();
	}
    public void GetTarget()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemys.Length; i++)
        {
            if (Vector3.Distance(player.transform.position, enemys[i].transform.position) <= policeRange + 1 && target == null)
            {
                target = enemys[i];
                monsterInfo = target.GetComponent<MonsterInfo>();
                break;
            }
        }
        if (target != null && Vector3.Distance(player.transform.position, target.transform.position) > policeRange * 2f)
        {
            target = null;
        }
        if (target == null)
        {
            return;
        }
    }
    public void PhysicAttackDamage()
    {
        monsterInfo.hp -= playerModel.Playerinfo[0].str * 2 - monsterInfo.aDef;
        if (monsterInfo.hp <= 0)
        {
            monsterInfo.hp = 0;
        }
    }
    public void EnemyAttackDamage()
    {
        playerModel.Playerinfo[0].hp -= monsterInfo.atk - playerModel.Playerinfo[0].def;
        if (playerModel.Playerinfo[0].hp <= 0)
        {
            playerModel.Playerinfo[0].hp = 0;
        }
    }
}
