using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class NpcSpawn : MonoBehaviour
{
    private static string Dflag;        //Dirty Flag Implimentation
    public string type;                 //Choses the type of Npc to spawn in SpawnNpc
    public GameObject[] chars;          //All the Character Prefabs pulled from the Resource folder
    public Transform spawnpoint; //Current Spawnpoint for the Npc
    public Material[] material;         //The Material the Prefab will use
    private List<Material> tempmat;     //The list that will pull the materials and implement them

    private void Awake()                //initilazation
    {
        chars = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "NPC").ToArray(); //Get all Prefabs on awake
        tempmat = new List<Material>();
        CreateRandNPC();
    }

    public void CreateRandNPC()
    {
        chars = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "NPC").ToArray();
        GameObject newchar = chars[Random.Range(0, chars.Length - 1)];
        //tempmat.Add(material[Random.Range(0, material.Length - 1)]); //adds material
        newchar.GetComponentInChildren<SkinnedMeshRenderer>().GetSharedMaterials(tempmat);
        newchar.GetComponent<AICharacterControl>().target = GameObject.FindGameObjectWithTag("Player").transform;


        if(newchar.GetComponent<NavMeshAgent>().isActiveAndEnabled)
        {
            newchar.GetComponent<NavMeshAgent>().Warp(spawnpoint.transform.position);
        }
        else
        {
            newchar.transform.position = spawnpoint.transform.position;
        }


        Instantiate(newchar);
    }

    public void SpawnNPC(string type)
    {
        chars = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.name.Contains(type)).ToArray();
        Debug.Log(chars.Length);
        int chosen = Random.Range(0, chars.Length - 1);
        Debug.Log(chosen);
        GameObject newchar = chars[chosen];
        tempmat.Add(material[Random.Range(0, material.Length - 1)]);
        newchar.GetComponentInChildren<SkinnedMeshRenderer>().GetSharedMaterials(tempmat);
        newchar.GetComponent<AICharacterControl>().target = GameObject.FindGameObjectWithTag("Player").transform;

        if (newchar.GetComponent<NavMeshAgent>().isActiveAndEnabled)
        {
            newchar.GetComponent<NavMeshAgent>().Warp(spawnpoint.transform.position);
        }
        else
        {
            newchar.transform.position = spawnpoint.transform.position;
        }

        Instantiate(newchar);
    }

    void SpawnNPC(string type, string materialtyp)
    {
        chars = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.name.Contains(type)).ToArray();
        if (!Dflag.Equals(materialtyp))
        {
            tempmat.Clear();
            for (int ii = 0; ii < material.Length; ii++)
            {
                if (material[ii].name.Contains(materialtyp))
                {
                    tempmat.Add(material[ii]);
                }
            }
        }
        Dflag = materialtyp;
    }
}
