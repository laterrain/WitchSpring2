using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes4 : MonoBehaviour 
{
    PlayerPos playerPos;
    public GameObject story9;
	// Use this for initialization
	void Start ()
    {
        playerPos = PlayerPos.GetInstance();
	}
	
	// Update is called once per frame
	void Update ()
    {//剧情的触发
        if (playerPos.storyIndex==9)
        {
            story9.SetActive(true);
            playerPos.storyIndex = 10;
        }
	}

    public void OnTriggerEnter(Collider other)
    {//跳转到HillFoot场景
        if (other.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene("HillFoot");
            SceneManager.LoadSceneAsync("HillFoot");
        }
    }

}
