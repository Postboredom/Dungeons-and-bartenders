using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Markov : MonoBehaviour {
    public int levels;
    public List<List<MarkovNodes<Dialouge_nodes>>> diaTree = new List<List<MarkovNodes<Dialouge_nodes>>>();
    public List<MarkovNodes<Dialouge_nodes>> dialogue = new List<MarkovNodes<Dialouge_nodes>>();
    public MarkovNodes<Dialouge_nodes> nodes = new MarkovNodes<Dialouge_nodes>();
    private void Awake()
    {
        assignlevel();
        filltree();
        createFlowGraph();
        Debug.Log(displayGraph().ToString());
    }

    public string displayGraph()
    {
        string print = "";
        for(int ii = 0; ii< diaTree.Count;ii++)
        {
            print = print + "Level " + ii + 1 + " nodes: " + "\n";
            for (int nodecount = 0; nodecount < diaTree[ii].Count; nodecount++)
            {
                print = print + "\t" + diaTree[ii][nodecount].ToString() + "\n";
                    }
        }
        return print;
    }

    public void createFlowGraph()
    {
        for(int ii = 0;ii<diaTree.Count;ii++)
        {
            for(int nodecount = 0; nodecount < diaTree[ii].Count;ii++)
            {
                for (int randcount = 0; randcount < Random.Range(0, 4); randcount++)
                {
                    nodes = new MarkovNodes<Dialouge_nodes>();
                    if (ii == 0)
                    {
                        nodes = diaTree[ii + 1][Random.Range(0, diaTree[ii+1].Count)];
                    }
                    else
                    {
                        nodes = diaTree[ii + Random.Range(-1,1)][Random.Range(0, diaTree[ii + 1].Count)];
                    }
                    diaTree[ii][nodecount].createNode(nodes);
                }
            }
        }
    }

    public void assignlevel()
    {
        for(int ii =0; ii<levels;ii++)
        {
            diaTree.Add(dialogue);
            dialogue = new List<MarkovNodes<Dialouge_nodes>>();
        }
    }

    public void filltree()
    {
        Dialouge_nodes temp = new Dialouge_nodes();
        StreamReader reader = new StreamReader("Assets/Scripts/Node Data/node.txt");
        if(reader == null)
        {
            File.Create("Assets/Scripts/Node Data/node.txt");
        }
        while (!reader.EndOfStream)
        {
            string nodetxt = reader.ReadToEnd();
            string[] notes  = nodetxt.Split(',');
            foreach(string note in notes)
            {
                nodes = new MarkovNodes<Dialouge_nodes>();
                temp.dialogue = note;
                nodes.data = temp;
                int num;
                num = note[note.Length - 1] - '0';
                Debug.Log(num);
                if(diaTree[num - 1] == null)
                {
                    dialogue = new List<MarkovNodes<Dialouge_nodes>>();
                    diaTree.Add(dialogue);
                }
                diaTree[num - 1].Add(nodes);
                
            }
        }
    }
}

