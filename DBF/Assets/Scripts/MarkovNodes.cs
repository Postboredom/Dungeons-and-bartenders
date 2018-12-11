using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarkovNodes<T> : MonoBehaviour {
    static int num = 0;
    public T data;
    protected float percentage;
    public bool balanced = false;
    private Vector2 range = new Vector2();
    protected List<MarkovNodes<T>> Nodes = new List<MarkovNodes<T>>();
    private void Awake()
    {
        num += 1;
    }
    private void balancePercentages(List<MarkovNodes<T>> node)
    {
        if (!balanced)
        {
            float total = 0;
            for (int ii = 0; ii < node.Count; ii++)
            {
                total += node[ii].getPercentages();
            }
            for (int ii = 0; ii < node.Count; ii++)
            {
                node[ii].setPercentage(node[ii].getPercentages() / total);
            }
            balanced = true;
        }
    }

    private bool checkEqual(List<MarkovNodes<T>> nodes)
    {
        float temp = nodes[0].getPercentages();
        for(int ii = 0;ii<nodes.Count;ii++)
        {
            if(!(temp == nodes[ii].getPercentages()))
            {
                return false;
            }
        }
        return true;
    }

    private MarkovNodes<T> randNode(List<MarkovNodes<T>> node)
    {
        float rangestart = 0;
        for(int ii = 0;ii < node.Count;ii++)
        {
            node[ii].range.Set(rangestart, rangestart + percentage);
            rangestart += percentage;
        }
        float randnum = Random.Range(0, 100);
        randnum /= 100;
        for(int ii = 0;ii<node.Count;ii++)
        {
            if(randnum> node[ii].range.x && randnum < node[ii].range.y)
            {
                return node[ii];
            }
        }
        Debug.Log("Randnum broke!");
        return null;
    }

    public float getPercentages()
    {
        return percentage;
    }

    public T getData()
    {
        return data;
    }

    public MarkovNodes<T> getNode(T nodeData)
    {
        for(int ii = 0;ii < Nodes.Count;ii++)
        {
            if(Nodes[ii].getData().Equals(nodeData))
            {
                return Nodes[ii];
            }
        }
        Debug.Log("Node not found");
        return null;
    }

    public void setPercentage(float percentage)
    {
        this.percentage = percentage;
        balanced = false;
    }

    public void setData(T data)
    {
        this.data = data;
        balanced = false;
    }

    public void createNode(MarkovNodes<T> Node)
    {
        Nodes.Add(Node);
        balanced = false;
    }

    public void deleteNode(MarkovNodes<T> Node)
    {
        if(Nodes.Contains(Node))
        {
            Nodes.Remove(Node);
        }
        balanced = false;
    }

    public void deleteNode(T data)
    {
        Nodes.Remove(getNode(data));
        balanced = false;
    }

    public MarkovNodes<T> chooseNode()
    {
        if (Nodes.Count != 0)
        {
            if(checkEqual(Nodes)) { return Nodes[Random.Range(0, Nodes.Count)]; }

            balancePercentages(Nodes);
            return randNode(Nodes);
        }
        else
        {
            Debug.Log("No Nodes");
            return null;
        }
    }

    override
    public string ToString()
    {
        string print = ""+ num + "( ";
        for(int ii = 0;ii<Nodes.Count;ii++)
        {
            print = print + Nodes[ii].ToString();
        }

       return print = print + ")";
    }
}
