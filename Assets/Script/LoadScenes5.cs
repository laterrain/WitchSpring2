﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes5 : MonoBehaviour 
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {//跳转到HillTop场景
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadSceneAsync("HillTop");
        }
    }

}