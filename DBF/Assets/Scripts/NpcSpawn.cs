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
        Debug.Log(material.Length);
        tempmat = new List<Material>();
    }

    private void Update()
    {
        for(int ii = 0; ii < amount; ii++)
        {
            CreateRandNPC();
            if(type != null)
            {
                SpawnNPC(type);
            }
            
        }
        type = null;
        amount = 0;
    }

    public void CreateRandNPC()
    {
        chars = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.tag == "NPC").ToArray();
        GameObject newchar = chars[Random.Range(0, chars.Length - 1)];
        newchar.GetComponentInChildren<SkinnedMeshRenderer>().material = material[Random.Range(0, material.Length - 1)];
        newchar.GetComponent<AICharacterControl>().target = GameObject.FindGameObjectWithTag("Player").transform;
        Instantiate(newchar);
    }

    void SpawnNPC(string type)
    {
        chars = Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().Where(g => g.name.Contains(type)).ToArray();
        GameObject newchar = chars[Random.Range(0, chars.Length - 1)];
        newchar.GetComponentInChildren<SkinnedMeshRenderer>().material = material[Random.Range(0, material.Length - 1)];
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
