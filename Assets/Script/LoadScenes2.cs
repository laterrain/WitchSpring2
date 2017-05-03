using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes2 : MonoBehaviour 
{
    PlayerPos playerPos;
    public GameObject story2;
    // Use this for initialization
    void Start ()
    {
        playerPos = PlayerPos.GetInstance();
    }
	
	// Update is called once per frame
	void Update ()
    {//剧情的触发
        if (playerPos.storyIndex == 2)
        {
            story2.SetActive(true);
            playerPos.storyIndex = 3;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //跳转到LunaHome场景
        if (other.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene("LunaHome");
            SceneManager.LoadSceneAsync("LunaHome");
        }
    }

}
