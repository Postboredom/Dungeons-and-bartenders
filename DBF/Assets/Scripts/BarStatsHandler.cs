using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarStatsHandler : MonoBehaviour {

    /// <summary>
    /// this script handles all relevant data to the bar such as gold, attractiveness and upgrades
    /// the gameobject this script is attached to is referenced by placement controller to determine various things about item placement
    /// </summary>

    //these values represent how much gold the player has at their disposal and how attractive their bar is
    public int totalGold;
    public float totalBarAttractiveness;

    //these text fields display the text for the amount of gold a player has and how attractive their bar is
    [SerializeField]
    private Text goldAmountText;
    [SerializeField]
    private Text barAttractivenessText;

    private GameObject spawner;
    private int spawns;

    //these booleans keep track of whether or not a certain upgrade has been purchased for your bar
    //this is to prevent the player from buying more than one of the same upgrade
    public bool secondStory;
    public bool hearthFirePurchased;
    public bool bedroomPurchased;
    public bool kitchenPurchased;
    public bool enchantingRoomPurchased;
    public bool planningRoomPurchased;

    

	// Use this for initialization
	void Start () {
        spawner = GameObject.FindGameObjectWithTag("GameController");
        spawns = 0;

        //these are your starting amounts of gold and attractiveness
        totalGold = 1500;
        totalBarAttractiveness = 10;

        //all upgrades for your bar start off as false 
        secondStory = false;
        planningRoomPurchased = false;
        enchantingRoomPurchased = false;
        kitchenPurchased = false;
        bedroomPurchased = false;
        hearthFirePurchased = false;
	}
	
	// Update is called once per frame
	void Update () {

        //constantly updates the text field to show how much gold you have accurately after you purchase an item or upgrade
        goldAmountText.text = totalGold.ToString();
        BarRating();
	}

    //based on the total attractiveness of your bar, the text to describe your bar is updated
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
