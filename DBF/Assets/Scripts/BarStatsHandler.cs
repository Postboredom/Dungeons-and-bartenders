using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarStatsHandler : MonoBehaviour {


    public int totalGold;
    public float totalBarAttractiveness;
    public bool secondStory;
    private GameObject spawner;
    private int spawns;

    [SerializeField]
    private Text goldAmountText;
    [SerializeField]
    private Text barAttractivenessText;

	// Use this for initialization
	void Start () {
        spawner = GameObject.FindGameObjectWithTag("GameController");
        spawns = 0;
        totalGold = 1500;
        totalBarAttractiveness = 10;
        secondStory = false;
	}
	
	// Update is called once per frame
	void Update () {

        goldAmountText.text = totalGold.ToString();
        BarRating();
	}

    void BarRating()
    {
        if (totalBarAttractiveness >= 20)
        {
            barAttractivenessText.text = "Unkempt Hovel";
        }
        if (totalBarAttractiveness >= 45)
        {
            barAttractivenessText.text = "Grubby Taproom";
        }
        if (totalBarAttractiveness >= 80)
        {
            barAttractivenessText.text = "Tolerable Tavern";
        }
        if (totalBarAttractiveness >= 110)
        {
            barAttractivenessText.text = "Noteworthy Ale House";
        }
        if (totalBarAttractiveness >= 140)
        {
            barAttractivenessText.text = "Impressive Inn";
        }
        if (totalBarAttractiveness >= 160)
        {
            barAttractivenessText.text = "Exemplary Establishment";
        }
        if (totalBarAttractiveness >= 205)
        {
            barAttractivenessText.text = "Phenomenal Bistro";
        }
        if (totalBarAttractiveness >= 250)
        {
            barAttractivenessText.text = "6 Star Bar";
        }
    }
}
