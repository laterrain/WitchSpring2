using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Script;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour 
{
    float speed = 3f;   //角色移动的速度
    public float turnSmoothing = 15;    //角色旋转的速度
    Rigidbody r;    //刚体组件
    Animator anim;  //角色动画
    float attackRange = 1f; //角色攻击距离
    float policeRange = 3f;//怪物警戒距离
    GameObject target = null;   //目标怪物
    Animator monsterAnim;   //怪物动画
    public bool isFight = false;   //是否在战斗中
    Vector3 dir;    //从角色指向怪物的向量
    Sequence mySequence;
    TurnSwitch turn;//TurnSwitch脚本，管理战斗回合的切换
    public bool isFightView = false;
    public string id;  //角色id
    public string name;//角色名字
    public int hp;     //角色生命
    public int maxHp;  //角色最大生命
    public int sp;     //角色蓝量
    public int maxSp;  //角色最大蓝量
    public int str;    //角色力量
    public int magic;  //角色魔力
    public int def;    //角色防御
    public int agile;  //角色敏捷
    PlayerModel playerModel;    //角色信息类
    PlayerPos playerPos;    //角色位置类
    public Transform playerPos1;    //角色位置
    public Transform playerPos2;
    public Transform maincameraPos1;//相机位置
    public Transform maincameraPos2;
    public Transform mapcameraPos1;//小地图相机位置
    public Transform mapcameraPos2;
    public GameObject mapCamera;
    TakeDamage takedamage;//战斗时计算伤害的脚本
    MonsterInfo monsterInfo;//怪物信息类
    AudioSource stepAudio;//音频组件
    AudioSource lightPunchAudio;
    AudioSource BGM_cench;
    AudioSource bgmBattle;
    float time = 0;
	// Use this for initialization
	void Start () 
    {
        playerPos = PlayerPos.GetInstance();
        playerModel = PlayerModel.GetInstance();
        if (playerPos.index == 2)//根据角色位置id来设置角色的位置
        {
            transform.position = playerPos1.position;//设置角色位置
            transform.forward = -playerPos1.right;//设置角色的面向
            Camera.main.transform.position = maincameraPos1.position;//设置主相机的位置
            mapCamera.transform.position = mapcameraPos1.position;//设置小地图相机的位置
            playerPos.index = 0;//重置角色位置id
        }
        if (playerPos.index == 3)
        {
            transform.position = playerPos2.position;
            transform.forward = -playerPos2.forward;
            Camera.main.transform.position = maincameraPos2.position;
            mapCamera.transform.position = mapcameraPos2.position;
            playerPos.index = 0;
        }
        r = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        mySequence = DOTween.Sequence();
        turn = GameObject.Find("GameControl").GetComponent<TurnSwitch>();
        id = playerModel.Playerinfo[0].id;
        name = playerModel.Playerinfo[0].name;
        hp = playerModel.Playerinfo[0].hp;
        maxHp = playerModel.Playerinfo[0].maxHp;
        sp = playerModel.Playerinfo[0].sp;
        maxSp = playerModel.Playerinfo[0].maxSp;
        str = playerModel.Playerinfo[0].str;
        magic = playerModel.Playerinfo[0].magic;
        def = playerModel.Playerinfo[0].def;
        agile = playerModel.Playerinfo[0].agile;
        takedamage = GameObject.Find("GameControl").GetComponent<TakeDamage>();
        stepAudio = GameObject.Find("FootStepAudio").GetComponent<AudioSource>();
        lightPunchAudio = GameObject.Find("LightPunchAudio").GetComponent<AudioSource>();
        BGM_cench= GameObject.Find("BGM_cench").GetComponent<AudioSource>();
        bgmBattle = GameObject.Find("BGM_battleNormal").GetComponent<AudioSource>();
    }
	void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");//获取键盘的水平输入
        float v = Input.GetAxis("Vertical");//获取键盘的垂直输入
        MovementManagement(h, v);        
    }
	// Update is called once per frame
	void Update () 
    {
        GetTarget();
        time += Time.deltaTime;
	}
    public void GetTarget()//获取目标怪物
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");//找到场景中的所有敌人
        for (int i = 0; i < enemys.Length; i++)
        {
            if (Vector3.Distance(transform.position, enemys[i].transform.position) <= policeRange + 1 && target == null)
            {//当角色与敌人距离小于一定距离时，将当前敌人赋给target
                target = enemys[i];
                monsterAnim = target.GetComponent<Animator>();
                monsterInfo = target.GetComponent<MonsterInfo>();
                break;
            }            
        }
        if (target != null && Vector3.Distance(transform.position, target.transform.position) > policeRange * 2f)
        {//当角色与敌人距离大于一定值时，将target清空
            target = null;
        }
        if (target == null)
        {
            return;
        }
    }
    void MovementManagement(float h,float v)
    {
        if ((h != 0 || v != 0) && !isFight)//判断是否有键盘输入
        {
            if (time>=1)
            {//每隔1s播放一次走路音频
                stepAudio.Play();
                time = 0;
            }
            Rotating(h, v);//角色旋转
            anim.SetFloat("isWalk", speed);
            transform.position += new Vector3(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime);//角色移动
        }
        else
        {
            anim.SetFloat("isWalk", 0);
        }
    }
    
    void Rotating(float h,float v)
    {//用四元数来表示旋转
        Vector3 targetDir = new Vector3(h, 0, v);
        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(r.rotation, targetRotation, turnSmoothing * Time.deltaTime);
        r.MoveRotation(newRotation);
    }
    
    public void PhysicalAttack()
    {//普通攻击
        anim.SetBool("fightToWalk", true);
        dir = target.transform.FindChild("targetPos").position - transform.position;
        Vector3 dirPos = transform.position + dir.normalized * (dir.magnitude - attackRange);        
        mySequence.Append(transform.DOMove(dirPos, 1f));  //移动到敌人面前              
        StartCoroutine("Attack");   //开启携程进行攻击        
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("fightToWalk", false);
        anim.SetTrigger("isAttack");
    }    
    public void TurnOver()
    {//攻击后返回
        takedamage.PhysicAttackDamage();//计算伤害
        anim.SetBool("fightToWalk", true);
        Vector3 backPos = transform.position - dir.normalized * (policeRange - attackRange);
        //mySequence.Append(transform.DORotate(new Vector3(0,180,0), 0.3f));
        //mySequence.Append(transform.DOLookAt(-dir, 0.3f));
        transform.Rotate(Vector3.up, 180);//转身
        mySequence.Append(transform.DOMove(backPos, 1f));//返回
        StartCoroutine("Backed");//返回后开启携程重新面对敌人               
    }
    IEnumerator Backed()
    {
        yield return new WaitForSeconds(1f);
        //mySequence.Append(transform.DORotate(Vector3.zero, 0.3f));
        //mySequence.Append(transform.DOLookAt(dir, 0.3f));
        transform.Rotate(Vector3.up, 180);
        anim.SetBool("fightToWalk", false);
        turn.playerTurn = false;//角色回合结束
        if (monsterInfo.hp <= 0)//判断敌人是否死亡
        {
            monsterAnim.SetBool("isDie", true);
            anim.SetBool("inAttack", false);
            isFightView = false;
        }
        else
        {
            turn.monsterTurn = true;            
        }
        isFight = false;
    }
    public void EscapeEnemy()//逃跑
    {
        BGM_cench.Play();
        bgmBattle.Stop();
        anim.SetBool("inAttack", false);
        anim.SetFloat("isWalk", speed);
        dir = transform.position - target.transform.FindChild("targetPos").position;
        Vector3 dirPos = transform.position + dir.normalized;//逃跑的距离
        transform.Rotate(Vector3.up, 180);//转身
        mySequence.Append(transform.DOMove(dirPos, 1f));//移动
        isFight = false;
        turn.playerTurn = false;
        isFightView = false;
    }
    public void MonsterPanic()//播放敌人被攻击的动画
    {
        lightPunchAudio.Play();
        monsterAnim.SetTrigger("isHit");
    }
    public void MagicAttack()//魔法攻击
    {
        anim.SetTrigger("isMagicAttack");
        StartCoroutine("MagicAttackOver");
    }
    IEnumerator MagicAttackOver()//魔法攻击结束
    {
        yield return new WaitForSeconds(1f);
        turn.playerTurn = false;
        if (monsterInfo.hp <= 0)
        {
            monsterAnim.SetBool("isDie", true);
            anim.SetBool("inAttack", false);
            isFightView = false;
        }
        else
        {
            turn.monsterTurn = true;
        }
        isFight = false;
    }
    public void BackHome()//角色死亡时传送回家
    {
        bgmBattle.Stop();
        BGM_cench.Play();
        StartCoroutine("LunaDead");
    }
    IEnumerator LunaDead()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("LunaHome");
    }
}
