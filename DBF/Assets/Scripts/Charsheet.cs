using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charsheet : MonoBehaviour {
    private double luck;
    private double strength;
    private double dex;
    private double speed;

    private void Start()
    {
        luck = GetComponent<GenerateCharSheet>().CreateLuck();
        strength = GetComponent<GenerateCharSheet>().CreateStrenght(); ;
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
