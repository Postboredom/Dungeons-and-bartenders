using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;


public class Dialogue_test : MonoBehaviour {

    public VIDEUIManager1 diagUI;

    public VIDE_Assign inTrigger;



    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VIDE_Assign>() != null)
            inTrigger = other.GetComponent<VIDE_Assign>();
    }

    void OnTriggerExit()
    {
        inTrigger = null;
    }


    void Update () {
        //Interact with NPCs when pressing E
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    public void TryInteract()
    {
        /* Prioritize triggers */

        if (inTrigger)
        {
            diagUI.Interact(inTrigger);
            return;
        }

        /* If we are not in a trigger, try with raycasts */

        RaycastHit rHit;

        if (Physics.Raycast(transform.position, transform.forward, out rHit, 2))
        {
            //Lets grab the NPC's VIDE_Assign script, if there's any
            VIDE_Assign assigned;
            if (rHit.collider.GetComponent<VIDE_Assign>() != null)
                assigned = rHit.collider.GetComponent<VIDE_Assign>();
            else return;

            //if (assigned.alias == "QuestUI")
            //{
            //    questUI.Interact(); //Begins interaction with Quest Chart
            //}
            //else
            //{
                diagUI.Interact(assigned); //Begins interaction
            //}
        }
    }
}
