using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour 
{    
    GameObject player;
    CharacterController fight;
    GameObject bag;
    Animator anim;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.Find("luna");
        fight = player.GetComponent<CharacterController>();
        bag = GameObject.Find("Bag");
        if (anim == null)
        {
            anim = bag.GetComponent<Animator>();
        }       
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    
    public void OpenOtherUI(GameObject ui)
    {
        ui.gameObject.SetActive(!ui.gameObject.activeInHierarchy);
    }   
    public void PhysicalAttack(GameObject ui)
    {
        ui.gameObject.SetActive(!ui.gameObject.activeInHierarchy);
        fight.PhysicalAttack();
    }
    public void EscapeFromEnemy(GameObject ui)
    {
        ui.gameObject.SetActive(!ui.gameObject.activeInHierarchy);
        fight.EscapeEnemy();
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene("LoadScenes");
    }
    public void OpenBagUI()
    {
        anim.SetBool("isOpen",!anim.GetBool("isOpen"));
    }
}
