using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Script;

public class MonsterController : MonoBehaviour 
{
    Animator anim;  //怪物的动画
    public bool isFight = false;    //怪物是否在战斗中
    GameObject player;  //角色
    TurnSwitch turn;    //回合管理脚本
    Vector3 dir;    //战斗中的移动方向
    float attackRange = 1f; //怪物攻击距离
    float policeRange = 3f; //怪物警戒范围
    Sequence mySequence;
    TakeDamage takedamage;
    CharacterController characterController;
    PlayerModel playerModel;
    MonsterInfo monsterInfo;
    ItemModel itemModel;
    Bag bag;
    Image ico;
    Text name;
    Text explain;
    AudioSource bgmBattle;
    AudioSource BGM_cench;
    AudioSource getSound;
    AudioSource lightPunchAudio;
    // Use this for initialization
    void Start () 
    {
        player = GameObject.Find("luna");        
        anim = GetComponent<Animator>();
        turn = GameObject.Find("GameControl").GetComponent<TurnSwitch>();
        takedamage = GameObject.Find("GameControl").GetComponent<TakeDamage>();
        characterController = player.GetComponent<CharacterController>();
        playerModel = PlayerModel.GetInstance();
        monsterInfo = gameObject.GetComponent<MonsterInfo>();
        itemModel = ItemModel.GetInstance();
        bag = GameObject.Find("Bag").GetComponent<Bag>();
        ico = GameObject.Find("GetItem").transform.FindChild("ItemInfo").FindChild("frame").FindChild("ico").GetComponent<Image>();
        name = GameObject.Find("GetItem").transform.FindChild("ItemInfo").FindChild("nameInfo").FindChild("name").GetComponent<Text>();
        explain = GameObject.Find("GetItem").transform.FindChild("ItemInfo").FindChild("explain").FindChild("explainText").GetComponent<Text>();
        bgmBattle = GameObject.Find("BGM_battleNormal").GetComponent<AudioSource>();
        BGM_cench = GameObject.Find("BGM_cench").GetComponent<AudioSource>();
        getSound = GameObject.Find("GetSound").GetComponent<AudioSource>();
        lightPunchAudio= GameObject.Find("LightPunchAudio").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, policeRange);
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bgmBattle.Play();
            BGM_cench.Pause();
            turn.playerTurn = true;                       
        }
    }
    public void MoveToAttack()
    {
        anim.SetBool("isWalk", true);
        dir = player.transform.position - transform.FindChild("targetPos").position;
        Vector3 dirPos = transform.position + dir.normalized * (dir.magnitude - attackRange);
        mySequence.Append(transform.DOMove(dirPos, 1f));
        StartCoroutine("Attack");
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("isWalk", false);
        anim.SetTrigger("isAttack");        
    }
    public void CharacterPanic()
    {
        lightPunchAudio.Play();
        player.GetComponent<Animator>().SetTrigger("isPanic");
    }
    public void TrunBack()
    {
        takedamage.EnemyAttackDamage();
        anim.SetBool("isWalk", true);
        Vector3 backPos = transform.position - dir.normalized * (policeRange - attackRange);
        //mySequence.Append(transform.DORotate(Vector3.zero, 0.3f));
        transform.Rotate(Vector3.up, 180);
        mySequence.Append(transform.DOMove(backPos, 1f));
        StartCoroutine("Backed");
    }
    IEnumerator Backed()
    {
        yield return new WaitForSeconds(1f);
        //mySequence.Append(transform.DORotate(new Vector3(0, 180, 0), 0.3f));
        transform.Rotate(Vector3.up, 180);
        anim.SetBool("isWalk", false);
        if (playerModel.Playerinfo[0].hp <= 0)
        {
            player.GetComponent<Animator>().SetBool("isDie", true);
        }
        else
        {
            turn.playerTurn = true;
        }
        turn.monsterTurn = false;
        isFight = false;
    }
    public void MonsterDead()
    {
        BGM_cench.Play();
        bgmBattle.Stop();
        for (int i = 0; i < itemModel.itemList.Count; i++)
        {
            if (monsterInfo.lostItem.Equals(itemModel.itemList[i].id))
            {
                bag.AddItem(itemModel.itemList[i]);
                GameObject.Find("GetItem").GetComponent<Animator>().SetBool("IsOpen", true);
                getSound.Play();
                ico.sprite = Resources.Load<Sprite>(itemModel.itemList[i].sprite);
                name.text=itemModel.itemList[i].name;
                explain.text = itemModel.itemList[i].explain;
                break;
            }
        }        
        Destroy(gameObject);
    }
}
