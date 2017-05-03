using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour 
{
    Bag bag;
    ItemModel itemModel;
	// Use this for initialization
	void Start () 
    {
        itemModel = ItemModel.GetInstance();
        bag = GameObject.Find("Bag").GetComponent<Bag>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Item item = itemModel.itemList[Random.Range(0, itemModel.itemList.Count)];
            bag.AddItem(item);
        }
	}
}
