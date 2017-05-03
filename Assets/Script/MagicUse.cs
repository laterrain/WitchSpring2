using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicUse : MonoBehaviour 
{
    Image image;
    Text text;
    public string id;
    public string type;
    public float power;
    public int needMp;
    Magic magic;
    GameObject player;
    GameObject target;
    float policeRange = 3f;//怪物警戒距离
    PlayerModel playerModel;
    MonsterInfo monsterInfo;
    CharacterController characterController;
    AudioSource fireAudio;
    AudioSource magicAudio;

    public void Awake()
    {
        if (image == null)
        {
            image = transform.FindChild("MagicIco").GetComponent<Image>();
        }
        if (text == null)
        {
            text = transform.FindChild("MagicIfo").FindChild("Text").GetComponent<Text>();
        }
    }
	// Use this for initialization
    
	void Start () 
    {
        //if (image == null)
        //{
        //    image = transform.FindChild("MagicIco").GetComponent<Image>();
        //}
        //if (text == null)
        //{
        //    text = transform.FindChild("MagicIfo").FindChild("Text").GetComponent<Text>();
        //}
        player = GameObject.Find("luna");
        playerModel = PlayerModel.GetInstance();
        characterController = player.GetComponent<CharacterController>();
        fireAudio= GameObject.Find("FireAudio").GetComponent<AudioSource>();
        magicAudio= GameObject.Find("MagicAudio").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        GetTarget();
	}
    public void SetDatas(Magic _magic)
    {
        magic = _magic;
        if (magic != null)
        {
            image.sprite = Resources.Load<Sprite>(magic.id);
            text.text = magic.name;
            id = magic.id;
            type = magic.type;
            power = magic.power;
            needMp = magic.needMp;
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
    public void UseMagic()
    {
        if (magic != null)
        {
            if (id.Equals("m#innc3f") && characterController.isFight && playerModel.Playerinfo[0].sp >= needMp)
            {
                fireAudio.Play();
                GameObject fire = Resources.Load<GameObject>("HitFire");
                GameObject go = Instantiate(fire);
                go.transform.position = target.transform.position + new Vector3(0, 0.5f, 0);
                go.transform.SetParent(target.transform.parent);                
            }
            if (id.Equals("m#innc1r") && playerModel.Playerinfo[0].sp >= needMp)
            {
                magicAudio.Play();
                GameObject cure = Resources.Load<GameObject>("MagicAuraRunic");
                GameObject go = Instantiate(cure);
                go.transform.position = player.transform.FindChild("TargetPos").position + new Vector3(0, 0.1f, 0);
                go.transform.SetParent(player.transform);
            }
            if (id.Equals("m#innc1n") && characterController.isFight && playerModel.Playerinfo[0].sp >= needMp)
            {
                magicAudio.Play();
                GameObject ice = Resources.Load<GameObject>("HitIce");
                GameObject go = Instantiate(ice);
                go.transform.position = target.transform.position + new Vector3(0, 0.5f, 0);
                go.transform.SetParent(target.transform.parent);
            }
        }
        StartCoroutine("MagicEffect");
    }
    IEnumerator MagicEffect()
    {
        yield return new WaitForSeconds(1);
        if (magic != null)
        {
            if (type.Equals("damage") && playerModel.Playerinfo[0].sp >= needMp && characterController.isFight)
            {
                monsterInfo.hp -= (int)(playerModel.Playerinfo[0].magic * power);
                if (monsterInfo.hp <= 0)
                {
                    monsterInfo.hp = 0;
                }
                playerModel.Playerinfo[0].sp -= needMp;
            }
            if (type.Equals("health") && playerModel.Playerinfo[0].sp >= needMp)
            {
                playerModel.Playerinfo[0].hp += (int)(playerModel.Playerinfo[0].magic * power);
                if (playerModel.Playerinfo[0].hp >= playerModel.Playerinfo[0].maxHp)
                {
                    playerModel.Playerinfo[0].hp = playerModel.Playerinfo[0].maxHp;
                }
                playerModel.Playerinfo[0].sp -= needMp;
            }
        }
    }
}
