using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour {


    /// <summary>
    /// this script handles what happens when you hover over an item button in the menu
    /// </summary>
    
    private GameObject groundPlacementController;
    //number of items available to be placed
    int numOfItems;
    //curent button the player is hovering over
    private GameObject currentHoveringObject;

    //these UI fields will display the information of the object that the player is currently hovering over
    [SerializeField]
    private GameObject itemInfoPanel;
    [SerializeField]
    private Text hoveringItemName;
    [SerializeField]
    private Text hoveringItemDescription;
    [SerializeField]
    private Text hoveringItemCost;
    [SerializeField]
    private Image hoveringItemIcon;
    

    void Start()
    {
        //find the object that has the list of placeable items and set numOfItems to the amount of items on the list
        groundPlacementController = GameObject.Find("GroundPlacementController");
        numOfItems = groundPlacementController.GetComponent<GroundPlacementController>().placeableObjectPrefabs.Length;
    }

    //this is called when you are hovering over the button for any given object
    public void HoveringOverButton()
    {
        //if you are hovering over the button, turn on the information panel
        itemInfoPanel.SetActive(true);
        //go through the list and if the name of the item button you are hovering over matches the name of any item of the list, assign that object 
        for (int i = 0; i < numOfItems; i++)
        {
            if (this.name == groundPlacementController.GetComponent<GroundPlacementController>().placeableObjectPrefabs[i].GetComponent<ItemProperties>().itemName)
            {
                currentHoveringObject = groundPlacementController.GetComponent<GroundPlacementController>().placeableObjectPrefabs[i];
                
            }
        }
        //if you have an object assigned, display its statistics and information on the information panel
        if(currentHoveringObject != null)
        {
            hoveringItemName.text = currentHoveringObject.GetComponent<ItemProperties>().itemName;
            hoveringItemDescription.text = currentHoveringObject.GetComponent<ItemProperties>().itemDescription;
            hoveringItemCost.text = currentHoveringObject.GetComponent<ItemProperties>().goldCost.ToString();
            hoveringItemIcon.sprite = currentHoveringObject.GetComponent<ItemProperties>().itemIcon;
        }
    }
    //this is called from each button when you are no longer hovering over it. It turns off the information panel
    public void NoLongerHovering()
    {
        itemInfoPanel.SetActive(false);
    }
}
