using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFightView : MonoBehaviour 
{
    Transform playerPos;//角色的transform
    Vector3 cameraPos;  //vector3向量，表示从相机指向角色的向量
    // Use this for initialization
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
        cameraPos = transform.position - playerPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position + cameraPos;//让相机以cameraPos的距离跟随人物移动
    }
}
