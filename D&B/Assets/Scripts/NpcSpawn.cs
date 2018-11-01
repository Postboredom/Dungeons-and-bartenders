using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawn : MonoBehaviour
{
    private GameObject[] chars;
    public GameObject controller;
    private void Awake()
    {
        chars = GameObject.FindGameObjectsWithTag("NPC");
    }
    void SpawnNPCrandom()
    {
        GameObject newchar;

        newchar = chars[Random.Range(0, chars.Length - 1)];
        newchar.transform.parent = controller.transform;
        newchar.AddComponent<Charsheet>();

        Instantiate(controller);
    }

    void SpawnNPC(string type)
    {
        Debug.Log("Work in Progress");
    }
}
