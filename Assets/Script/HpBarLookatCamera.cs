using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarLookatCamera : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {//敌人头顶的血条一直面向摄像机
        transform.forward = Camera.main.transform.forward;
	}
}
