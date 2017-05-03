using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float smooth = 1.5f;
    private Transform player;
    private Vector3 relCameraPos;   //player到摄像机的向量
    private float relCameraPosMag;     //player到摄像机的向量的长度
    private Vector3 newPos;
    CharacterController character;
    //public Transform fightView;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindWithTag("Player").transform;
        relCameraPos = transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
        character = player.GetComponent<CharacterController>();        
	}
    void FixedUpdate()
    {
        if (!character.isFightView)
        {
            //transform.position = fightView.position;
            //transform.forward = -fightView.right;
            Vector3 standardPos = player.position + relCameraPos;   //摄像机的位置
            Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;  //Player正上方relCameraPosMag距离的位置
            Vector3[] checkPoints = new Vector3[5];
            checkPoints[0] = standardPos;
            checkPoints[1] = Vector3.Lerp(standardPos, abovePos, 0.25f);
            checkPoints[2] = Vector3.Lerp(standardPos, abovePos, 0.5f);
            checkPoints[3] = Vector3.Lerp(standardPos, abovePos, 0.75f);
            checkPoints[4] = abovePos;
            //得到摄像机的新位置
            for (int i = 0; i < checkPoints.Length; i++)
            {
                if (ViewingPositionCheck(checkPoints[i]))
                {
                    break;
                }
            }
            transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
            SmoothLookAt();
        }
    }
    // Update is called once per frame
    void Update ()
    {
		
	}
    bool ViewingPositionCheck(Vector3 checkPos)
    {
        RaycastHit hit;
        //从checkPos发射一条对准Player，长度为relCameraPosMag的射线
        if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
        {
            if (hit.transform != player)
            {
                return false;
            }
        }
        //如果射线碰到Player，将位置赋值给newPos，并返回true
        newPos = checkPos;
        return true;
    }
    void SmoothLookAt()
    {
        //摄像机到player的向量
        Vector3 relPlayerPosition = player.position - transform.position;
        //摄像机从当前位置旋转到Player位置的旋转量
        Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
        //将摄像机的旋转量从当前值均匀插值到目标值
        transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
    }    
}
