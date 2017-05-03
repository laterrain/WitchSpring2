using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes3 : MonoBehaviour 
{
    PlayerPos playerPos;
	// Use this for initialization
	void Start () 
    {
        playerPos = PlayerPos.GetInstance();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {//跳转到HillSide场景
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadSceneAsync("HillSide");
            playerPos.index = 3;
        }
    }

}
