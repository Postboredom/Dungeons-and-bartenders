using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWarp : MonoBehaviour {

    public GameObject player;
    public GameObject destination;

    void Update()
    {
        if(this.tag == "Second Story Exit" )
        {
            destination = GameObject.FindGameObjectWithTag("Second Story Exit 1");
        }
        if(this.GetComponent<ItemProperties>().itemName == "Second Story")
        {
            destination = GameObject.Find("Second Story Entrance");
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
    }

	void OnTriggerEnter()
    {
        player.transform.position = destination.transform.position;
    }
    

}
