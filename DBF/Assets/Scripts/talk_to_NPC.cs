using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talk_to_NPC : MonoBehaviour {
    public GameObject dialogue;
    void OnTriggerEnter(Collider other)
    {
        dialogue.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        dialogue.SetActive(false);
    }


}
