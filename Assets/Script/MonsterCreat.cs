using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreat : MonoBehaviour 
{
    public Transform[] enemyPos;
    GameObject enemy;
    MonsterModel monsterModel;
    MonsterVo monsterVo;
	// Use this for initialization
	void Start () 
    {
        monsterModel = MonsterModel.GetInstance();
        enemy = Resources.Load("goblinGuard") as GameObject;
        monsterVo = monsterModel.monsterList[2];
        SpawnMonster();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    public void SpawnMonster()
    {
        for (int i = 0; i < enemyPos.Length; i++)
        {
            if (enemyPos[i].childCount <= 0)
            {
                GameObject go = Instantiate(enemy);
                go.transform.SetParent(enemyPos[i]);
                go.transform.position = enemyPos[i].position;
                go.GetComponent<MonsterInfo>().GetEnemyInfo(monsterVo);
            }
        }        
    }
}
