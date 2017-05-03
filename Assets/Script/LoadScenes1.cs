using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes1 : MonoBehaviour 
{        
    PlayerPos playerPos;
    public GameObject story1;
    public GameObject story4;
    public GameObject story7;
    // Use this for initialization
    void Start () 
    {
        playerPos = PlayerPos.GetInstance();        
    }
	
	// Update is called once per frame
	void Update () 
    {//剧情的触发
        if (playerPos.storyIndex == 1)
        {
            story1.SetActive(true);
            playerPos.storyIndex = 2;
        }
        if (playerPos.storyIndex == 4)
        {
            story4.SetActive(true);
            playerPos.storyIndex = 5;
        }
        if (playerPos.storyIndex == 7)
        {
            story7.SetActive(true);
            playerPos.storyIndex = 8;
        }
    }
    public void OnTriggerEnter(Collider other)
    {//跳转到HillFoot场景
        if (other.gameObject.tag.Equals("Player"))
        {            
            SceneManager.LoadSceneAsync("HillFoot");
            playerPos.index = 2;
        }
    }


}
