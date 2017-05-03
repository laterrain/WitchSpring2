using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemUIEvent : MonoBehaviour 
{
    Animator anim;//UI上的动画组件
    PlayerPos playerPos;
    public GameObject story3;//剧情
    public GameObject story6;
    public GameObject story10;
    // Use this for initialization
    void Start () 
    {
        anim = GetComponent<Animator>();
        playerPos = PlayerPos.GetInstance();
    }
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    public void OpenThisUI()
    {
        anim.SetBool("IsOpen", !anim.GetBool("IsOpen"));//打开/关闭UI
        //剧情的触发，根据剧情编号触发对应的剧情
        if (playerPos.storyIndex == 3)
        {
            story3.SetActive(true);
            playerPos.storyIndex = 4;
        }
        if (playerPos.storyIndex == 6)
        {
            story6.SetActive(true);
            playerPos.storyIndex = 7;
        }
        if (playerPos.storyIndex == 10)
        {
            story10.SetActive(true);
            playerPos.storyIndex = 11;
        }
    }
}
