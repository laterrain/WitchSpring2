using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour 
{
    PlayerModel playerModel;
    AudioSource sleep;
    PlayerPos playerPos;
    public GameObject story8;
    // Use this for initialization
    void Start () 
    {
        playerModel = PlayerModel.GetInstance();
        sleep = GameObject.Find("SleepAudio").GetComponent<AudioSource>();
        playerPos = PlayerPos.GetInstance();
    }
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            sleep.Play();
            GameObject cure = Resources.Load<GameObject>("MagicAuraRunic");
            GameObject go = Instantiate(cure);
            go.transform.position = other.transform.FindChild("TargetPos").position + new Vector3(0, 0.1f, 0);
            go.transform.SetParent(other.transform);
            playerModel.Playerinfo[0].hp = playerModel.Playerinfo[0].maxHp;
            playerModel.Playerinfo[0].sp = playerModel.Playerinfo[0].maxSp;
            if (playerPos.storyIndex == 8)
            {
                story8.SetActive(true);
                playerPos.storyIndex = 9;
            }
        }
    }

}
