using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProperties : MonoBehaviour
{
    /// <summary>
    /// this script contains all relevant data for each item that can be placed
    /// it is attached to each item and it's values are assigned in the inspector
    /// </summary>

    //these values determine how this item will effect your stats in terms of gold cost and attractiveness
    public int goldCost;
    public float barAttraction;
    public int refund;

    //these values are for flavor and are displayed when you hover an item in the menu
    public string itemDescription;
    public Sprite itemIcon;
    public string itemName;

    //these are restrictions for either buying or refunding the item
    public bool secondStoryRequired;
    public bool noRefund;
 
    
}
