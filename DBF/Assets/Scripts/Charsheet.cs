using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charsheet : MonoBehaviour {
    public double luck;
    public double strength;
    public double dex;
    public double speed;

    private void Start()
    {
        luck = GetComponent<GenerateCharSheet>().CreateLuck();
        strength = GetComponent<GenerateCharSheet>().CreateStrenght(); 
        dex = GetComponent<GenerateCharSheet>().CreateDex();
        speed = GetComponent<GenerateCharSheet>().CreateSpeed();
    }

    double getLuck()
    {
        return luck;
    }

    double getStrength()
    {
        return strength;
    }

    double getDex()
    {
        return dex;
    }

    double getSpeed()
    {
        return speed;
    }
}
