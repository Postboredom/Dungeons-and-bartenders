using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class NpcSpawn : MonoBehaviour
{
    private static string Dflag;
    public string type;
    public GameObject[] chars;
    public Transform spawnpoint;
    public int amount;
    public GameObject walk;
    public Material[] material;
    private List<Material> tempmat;

    private void Awake()
    {
        chars = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "NPC").ToArray();
        tempmat = new List<Material>();
    }

    private void Update()
    {
        for(int ii = 0; ii < amount; ii++)
        {
            CreateRandNPC();
            if(type != "")
            {
               // SpawnNPC(type);

            }

        }
        type = "";
        amount = 0;
    }

    public void CreateRandNPC()
    {
        chars = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "NPC").ToArray();
        GameObject newchar = chars[Random.Range(0, chars.Length - 1)];
        tempmat.Add(material[Random.Range(0, material.Length - 1)]);
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

    void SpawnNPC(string type)
    {
        chars = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.name.Contains(type)).ToArray();
        Debug.Log(chars.Length);
        GameObject newchar = chars[Random.Range(0, chars.Length - 1)];
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
