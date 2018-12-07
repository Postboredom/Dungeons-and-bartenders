using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour {

    public GameObject groundPlacementController;
    int numOfItems;
    private GameObject currentHoveringObject;

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
        numOfItems = groundPlacementController.GetComponent<GroundPlacementController>().placeableObjectPrefabs.Length;
    }

    public void HoveringOverButton()
    {
        Debug.Log("Hovering over "  + this.name);
        itemInfoPanel.SetActive(true);
        for (int i = 0; i < numOfItems; i++)
        {
            if (this.name == groundPlacementController.GetComponent<GroundPlacementController>().placeableObjectPrefabs[i].GetComponent<ItemProperties>().itemName)
            {
                currentHoveringObject = groundPlacementController.GetComponent<GroundPlacementController>().placeableObjectPrefabs[i];
                Debug.Log(currentHoveringObject.GetComponent<ItemProperties>().itemName);
            }
        }
        if(currentHoveringObject != null)
        {
            hoveringItemName.text = currentHoveringObject.GetComponent<ItemProperties>().itemName;
            hoveringItemDescription.text = currentHoveringObject.GetComponent<ItemProperties>().itemDescription;
            hoveringItemCost.text = currentHoveringObject.GetComponent<ItemProperties>().goldCost.ToString();
            hoveringItemIcon.sprite = currentHoveringObject.GetComponent<ItemProperties>().itemIcon;
        }
    }
    public void NoLongerHovering()
    {
        itemInfoPanel.SetActive(false);
    }
}
