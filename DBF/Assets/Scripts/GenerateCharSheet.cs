using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCharSheet : MonoBehaviour {

   public double CreateLuck()
    {
        return Mathf.Sqrt(Random.Range(0, 10));
    }

    public double CreateStrenght()
    {
        return Mathf.Sqrt(Random.Range(0, 10));
    }

    public double CreateDex()
    {
        return Mathf.Sqrt(Random.Range(0, 10));
    }

    public double CreateSpeed()
    {
        return Mathf.Sqrt(Random.Range(0, 10));
    }
}
