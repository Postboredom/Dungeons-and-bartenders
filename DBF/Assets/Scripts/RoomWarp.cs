using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWarp : MonoBehaviour {

    /// <summary>
    /// this script handles the teleportation between rooms 
    /// it is attached to each door item
    /// </summary>

    public GameObject player;
    public GameObject destination;

    void Update()
    {
        
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
        /////////////
        //going between tavern and enchanting room
        else if (this.GetComponent<ItemProperties>().itemName == "Enchanting Room")
        {
            destination = GameObject.Find("Enchanting Room Entrance");
           player = GameObject.FindGameObjectWithTag("Player");
        }
        else if(this.tag == "Enchanting Room Exit" ) 
        {
            destination = GameObject.FindGameObjectWithTag("Enchanting Room Exit 1");
            
        }
        /////////
        //going between tavern and planning room and enchanting room
        else if (this.GetComponent<ItemProperties>().itemName == "Planning Room")
        {
            destination = GameObject.Find("Meeting Room Entrance");
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else if (this.tag == "Meeting Room Exit") 
        {
            destination = GameObject.FindGameObjectWithTag("Meeting Room Exit 1");
        }
        ////////
        ///going between tavern and kitchen
        else if (this.GetComponent<ItemProperties>().itemName == "Kitchen")
        {
            destination = GameObject.Find("Kitchen Room Entrance");
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else if (this.tag == "Kitchen Room Exit") 
        {
            destination = GameObject.FindGameObjectWithTag("Kitchen Room Exit 1");
        }
        /////////
        //going between bedroom and tavern
        else if (this.GetComponent<ItemProperties>().itemName == "Bedroom")
        {
            destination = GameObject.Find("Bedroom Entrance");
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else if (this.tag == "Bedroom Exit") //this line doesn't work and I don't know why
        {

            destination = GameObject.FindGameObjectWithTag("Bedroom Exit 1");

        }
        ///////

    }

    /////// teleport the player to their destination
	void OnTriggerEnter()
    {
        player.transform.position = destination.transform.position;
    }
    

}
