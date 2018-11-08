using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCharSheet : MonoBehaviour {

   public double CreateLuck()
    {
        return Mathf.Sqrt(Random.Range(4, 100));
    }

    public double CreateStrenght()
    {
        return Mathf.Sqrt(Random.Range(4, 100));
    }

    public double CreateDex()
    {
        return Mathf.Sqrt(Random.Range(4, 100));
    }

    public double CreateSpeed()
    {
        return Mathf.Sqrt(Random.Range(4, 100));
    }
}
