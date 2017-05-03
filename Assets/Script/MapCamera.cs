using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour 
{
    Transform playerPos;
    Vector3 cameraPos;
	// Use this for initialization
	void Start () 
    {
        playerPos = GameObject.FindWithTag("Player").transform;
        cameraPos = transform.position - playerPos.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = playerPos.position + cameraPos;
	}
}
