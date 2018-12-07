using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarStatsHandler : MonoBehaviour {


    public int totalGold;
    public int totalBarAttractiveness;

    [SerializeField]
    private Text goldAmountText;

	// Use this for initialization
	void Start () {

        totalGold = 50;
	}
	
	// Update is called once per frame
	void Update () {

        goldAmountText.text = totalGold.ToString();
	}
}
