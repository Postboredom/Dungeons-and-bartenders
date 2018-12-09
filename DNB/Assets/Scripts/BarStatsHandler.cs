using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarStatsHandler : MonoBehaviour {


    public int totalGold;
    public int totalBarAttractiveness;
    public bool secondStory;

    [SerializeField]
    private Text goldAmountText;
    [SerializeField]
    private Text barAttractivenessText;

	// Use this for initialization
	void Start () {

        totalGold = 50;
        totalBarAttractiveness = 0;
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
        if (totalBarAttractiveness >= 40)
        {
            barAttractivenessText.text = "Grubby Taproom";
        }
        if (totalBarAttractiveness >= 60)
        {
            barAttractivenessText.text = "Tolerable Tavern";
        }
        if (totalBarAttractiveness >= 80)
        {
            barAttractivenessText.text = "Noteworthy Ale House";
        }
        if (totalBarAttractiveness >= 100)
        {
            barAttractivenessText.text = "Impressive Inn";
        }
        if (totalBarAttractiveness >= 120)
        {
            barAttractivenessText.text = "Exemplary Establishment";
        }
        if (totalBarAttractiveness >= 140)
        {
            barAttractivenessText.text = "Phenomenal Bistro";
        }
        if (totalBarAttractiveness >= 160)
        {
            barAttractivenessText.text = "6 Star Bar";
        }
    }
}
