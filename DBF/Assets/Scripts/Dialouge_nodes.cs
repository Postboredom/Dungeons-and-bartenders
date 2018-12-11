using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialouge_nodes : MonoBehaviour {
    public string dialogue;
    private float additude;

    public float getAdditude()
    {
        return additude;
    }

    public void setAdditude(float additude)
    {
        this.additude = additude;
    }
}
