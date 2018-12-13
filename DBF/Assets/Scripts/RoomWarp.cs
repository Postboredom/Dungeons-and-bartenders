using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWarp : MonoBehaviour {

    public GameObject player;
    public GameObject destination;

    void Update()
    {
        Debug.Log("literally are you running??");
        Debug.Log(this.tag + this.name);
        //going too and from second story
        if (this.tag == "Second Story Exit" )
        {
            destination = GameObject.FindGameObjectWithTag("Second Story Exit 1");
        }
        else if(this.GetComponent<ItemProperties>().itemName == "Second Story")
        {
            destination = GameObject.Find("Second Story Entrance");
            player = GameObject.FindGameObjectWithTag("Player");
        }
        //going between tavern and enchanting room
        else if(this.tag == "Enchanting Room Exit")
        {
            //Debug.Log("yo do this k thanks");
            destination = GameObject.FindGameObjectWithTag("Enchanting Room Exit 1");
            
        }
        else if(this.GetComponent<ItemProperties>().itemName == "Enchanting Room")
        {
            destination = GameObject.Find("Enchanting Room Entrance");
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
    }

	void OnTriggerEnter()
    {
        Debug.Log(this.tag);
        player.transform.position = destination.transform.position;
    }
    

}
