using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEvent : MonoBehaviour 
{
    GameObject magicAttack;//魔法界面的UI
    Animator anim;
    public GameObject fightMenu;//战斗菜单的UI
    GameObject player;
    CharacterController characterController;
	// Use this for initialization
	void Start () 
    {
        magicAttack = GameObject.Find("MagicAttack");
        anim = magicAttack.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        characterController = player.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    public void OpenMagicUI()
    {//打开/关闭魔法界面的UI
        anim.SetBool("isOpen", !anim.GetBool("isOpen"));
    }
    public void MagicAttackEvent()
    {//发动魔法攻击
        anim.SetBool("isOpen", !anim.GetBool("isOpen"));        
        if (characterController.isFight)
        {
            fightMenu.SetActive(!fightMenu.activeInHierarchy);
            player.GetComponent<CharacterController>().MagicAttack();//调用角色身上的脚本发动魔法攻击
        }        
    }
}
