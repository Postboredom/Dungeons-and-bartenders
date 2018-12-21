using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundPlacementController : MonoBehaviour
{
    /// <summary>
    /// This script handles everything with having to place, edit, or refund and object.
    /// The object this script is attached to holds a list of all the objects that could be placed and is referenced by other scripts in order to get the list of those objects
    /// </summary>

    public GameObject[] placeableObjectPrefabs; //array of objects that could be placed

    private GameObject currentPlaceableObject; //current object that the player is trying to place
    public GameObject currentPlaceableObjectNameHolder; //this object will hold the name of the object button that is pressed

    public GameObject BarStatsHandler;
    public GameObject refundMenu;
    public Text refundedGoldAmt;

    public bool editModeOn = false; // this boolean will determine whether or not the player is attempting to edit an object
    public bool deleteModeOn = false; //this boolean will determine whether or not the player is attempting to refund an object
    public bool onPlaceableSurface = false; // this boolean determines if the object the player is attempting to place is on the correct surface

    private float mouseWheelRotation; //this float stores the value the mouse wheel is rotated 

    //this material will hold the material of the current object whie it is being placed and will restore the object to it's original material upon placing it
    private Material objectCurrentMaterialHolder;

    //these materials are used while the object is being placed and are color coded for correct and incorrect placement
    [SerializeField]
    private Material correctPlacementMaterial;
    [SerializeField]
    private Material incorrectPlacementMaterial;

    //this is the ceiling of the bar, it is raised when the second story upgrade is purchased
    [SerializeField]
    private GameObject secondStoryCeiling;

    private void Update()
    {
        //if there is no object being placed or modified, check if there should be an item placed
        if (currentPlaceableObject == null)
        {
            GetObjectToPlace();
        }

        //if there is currently an object being placed or modified
        if (currentPlaceableObject != null) 
        {
            //if there is an object selected to be sold, keep the menu on
            if (deleteModeOn == true)
            {
                currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOn();
            }
            else
            {
                //move the object to the mouse position
                StartCoroutine(MoveCurrentObjectToMouse());
                //rotate the object based on the mouse wheel rotation
                StartCoroutine(RotateFromMouseWheel());
                //if you right click, cancel placing the object
                StartCoroutine(CancelIfClicked());
                //place the object if you left click
                StartCoroutine(ReleaseIfClicked());
                //turn off the menu when placing or editing the object
                currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
            }

        }

        //even if you haven't selected an item to be sold, keep the menu on if you are in sell mode
        if (deleteModeOn == true)
        {
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOn();
        }
    }

    //this function handles the changing of the current placeable object
    private void GetObjectToPlace()
    {
        //go through the list of prefabs that contains all the objects that are placeable
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            //if any item on the list of placeable objects matches the name of a button that was pressed and you are not currently placing that object
            if (placeableObjectPrefabs[i].name == currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().currentPlaceableObjectName && !currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().isObjectPlaced)
            {
                Debug.Log("I'd like to spawn a " + currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().currentPlaceableObjectName);

                //instantiate the object that matches the name of the button pressed
                currentPlaceableObject = Instantiate(placeableObjectPrefabs[i]);

                //if the item costs too much, destroy it and do not place it
                if (CostTooMuch())
                {
                    Destroy(currentPlaceableObject);
                    currentPlaceableObject = null;
                    Debug.Log("You are too broke");
                }
                //if the button your pressed is an upgrade you have already purchased, destroy it and do not place it
                else if (UpgradeAlreadyPurchased())
                {
                    Destroy(currentPlaceableObject);
                    currentPlaceableObject = null;
                    Debug.Log("Already got one of those");
                }
                //if the item requires a second story, check if you have a second story and either place it or destroy it accordingly
                else if (ThisItemRequiresSecondStory())
                {
                    if (BarStatsHandler.GetComponent<BarStatsHandler>().secondStory == true)
                    {
                        //store the object's material
                        objectCurrentMaterialHolder = currentPlaceableObject.GetComponent<Renderer>().material;
                        currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
                    }
                    else
                    {
                        Destroy(currentPlaceableObject);
                        currentPlaceableObject = null;
                        Debug.Log("Get a second story yo");
                    }
                }
                //if it doesn't cost too much, you don't already have the upgrade, and it doesn't require a second story, place it and store its material
                else
                {
                    objectCurrentMaterialHolder = currentPlaceableObject.GetComponent<Renderer>().material;
                    currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();

                }

            }

        }
        //if you are in edit mode and select an item, make sure it can be edited
        if (Input.GetMouseButtonDown(0) && editModeOn)
        {
            //gets your mouse position on screen and object it is over
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            //if your mouse is over something
            if (Physics.Raycast(ray, out hitInfo))
            {
                //if you are trying to sell an object, make sure it can be sold
                if (deleteModeOn)
                {
                    //determines if the object you clicked is editable. I.E. a table is editable but a wall is not
                    if (CanEditItem(hitInfo.transform.gameObject.tag) == false)
                    {
                        Debug.Log("Item Can't be sold");
                    }
                    else
                    {
                        //assign the object your clicked as the object you have selected
                        currentPlaceableObject = hitInfo.transform.gameObject;

                        //if the item you have selected isn't eligible to be refunded, do not allow the player to refund
                        if (currentPlaceableObject.GetComponent<ItemProperties>().noRefund == true)
                        {
                            Debug.Log("No refunds for this object");
                        }
                        //if the item you have selected is elible to be refunded, open the confirmation menu and display the amount of the refund
                        else
                        {
                            refundMenu.SetActive(true);
                            refundedGoldAmt.text = currentPlaceableObject.GetComponent<ItemProperties>().refund.ToString();
                        }

                    }

                }
                //if you aren't selling an object and instead are just moving it, make sure it can be moved
                else
                {
                    //determines if the object you clicked is editable. I.E. a table is editable but a wall is not
                    if (CanEditItem(hitInfo.transform.gameObject.tag) == false)
                    {
                        Debug.Log("Item Can't be Edited");
                    }
                    else
                    {
                        //assign the object your clicked as the object you have selected
                        currentPlaceableObject = hitInfo.transform.gameObject;
                        //change the layer of the object to be appropriate to placing the object
                        currentPlaceableObject.layer = 2;
                        //store the objects current material until the object is placed
                        objectCurrentMaterialHolder = currentPlaceableObject.GetComponent<Renderer>().material;
                        currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOff();
                    }

                }
            }
        }
    }

    //called from the edit button in the menu. Toggles if you're in edit mode and makes sure if you aren't editing that you can't sell either
    public void EditMode()
    {
        editModeOn = !editModeOn;
        if (deleteModeOn == true)
        {
            deleteModeOn = false;
        }
    }

    //called from the sell items button in the menu. Toggles if you are currently selling
    public void DeleteMode()
    {
        deleteModeOn = !deleteModeOn;
    }

    //called from the refund confirmation menu if you select to refund the item
    public void RefundIt()
    {
        //the player receieves the refund price of the object they are selling and lose the benefit of its attraction
        BarStatsHandler.GetComponent<BarStatsHandler>().totalGold += currentPlaceableObject.GetComponent<ItemProperties>().refund;
        BarStatsHandler.GetComponent<BarStatsHandler>().totalBarAttractiveness -= currentPlaceableObject.GetComponent<ItemProperties>().barAttraction;

        //destroy the object sold and turn off the refunding menu
        Destroy(currentPlaceableObject);
        currentPlaceableObject = null;
        refundMenu.SetActive(false);
    }

    //called from the refund confirmation menu if you select to not refund the item
    public void DontRefundIt()
    {
        //deselect the object and turn off the refunding the menu
        currentPlaceableObject = null;
        refundMenu.SetActive(false);
    }

    //if the object you are attempting to place costs more gold than you have at your disposal, return true
    private bool CostTooMuch()
    {
        if (currentPlaceableObject.GetComponent<ItemProperties>().goldCost > BarStatsHandler.GetComponent<BarStatsHandler>().totalGold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //if the object you are attempting to place requires a second story, return true
    private bool ThisItemRequiresSecondStory()
    {
        if (currentPlaceableObject.GetComponent<ItemProperties>().secondStoryRequired == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //if your bar already has the upgrade you are attempting to purchase, return true
    private bool UpgradeAlreadyPurchased()
    {
        if (BarStatsHandler.GetComponent<BarStatsHandler>().secondStory == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Second Story")
        {
            return true;
        }
        else if (BarStatsHandler.GetComponent<BarStatsHandler>().bedroomPurchased == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Bedroom")
        {
            return true;
        }
        else if (BarStatsHandler.GetComponent<BarStatsHandler>().planningRoomPurchased == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Planning Room")
        {
            return true;
        }
        else if (BarStatsHandler.GetComponent<BarStatsHandler>().kitchenPurchased == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Kitchen")
        {
            return true;
        }
        else if (BarStatsHandler.GetComponent<BarStatsHandler>().enchantingRoomPurchased == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Enchanting Room")
        {
            return true;
        }
        else if (BarStatsHandler.GetComponent<BarStatsHandler>().hearthFirePurchased == true && currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Hearthfire")
        {
            return true;
        }
        else
        {
            return false;
        }
    } 

    //if you haven't already purchased the upgrade, update the bar stats when you purchase it
    private void UpgradeInitialPurchase()
    {
        if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Second Story")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().secondStory = true;
        }
        else if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Bedroom")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().bedroomPurchased = true;
        }
        else if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Planning Room")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().planningRoomPurchased = true;
        }
        else if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Kitchen")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().kitchenPurchased = true;
        }
        else if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Enchanting Room")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().enchantingRoomPurchased = true;
        }
        else if (currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Hearthfire")
        {
            BarStatsHandler.GetComponent<BarStatsHandler>().hearthFirePurchased = true;
        }
        
       
    }

    //if the object doesn't have an appropriate tag, it can't be edited
    public bool CanEditItem (string objectTag)
    {
        if(objectTag != "Floor Object" && objectTag != "Wall Object" && objectTag != "Ceiling Object" && objectTag !="Table" && objectTag != "Table Object")
        {
            Debug.Log(objectTag);
            return false;
        }
        else
        {
            return true;
        }
    }

    //this function moves the object to where the mouse is and rotates it to fit the orientation of the environment
    IEnumerator MoveCurrentObjectToMouse()
    {
        yield return new WaitForSeconds(1);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            //the position of the object is the same as the position of the mouse
            currentPlaceableObject.transform.position = hitInfo.point;

            //the rotation of the object is matched to fit the surface it is placed on
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            
            // if the item is a door that you are trying to place, disable its teleportation while moving it
            if (currentPlaceableObject.GetComponent<RoomWarp>() != null && currentPlaceableObject.GetComponent<SphereCollider>() != null)
            {
                currentPlaceableObject.GetComponent<RoomWarp>().enabled = false;
                currentPlaceableObject.GetComponent<SphereCollider>().enabled = false;
               
            }

            //if the object you are placing is an object that can be placed on the floor and if you are currently trying to place it on the floor
            if (hitInfo.transform.gameObject.tag == "Floor" && currentPlaceableObject.tag == "Floor Object")
            {
                Debug.Log("I can place a floor object here");

                //if the surface you are placing on is appropriate, change the material to indicate it
                onPlaceableSurface = true;
                currentPlaceableObject.GetComponent<Renderer>().material = correctPlacementMaterial;
                
            }
            //if you are trying to place a table on the floor
            else if (hitInfo.transform.gameObject.tag == "Floor" && currentPlaceableObject.tag == "Table")
            {
                Debug.Log("I can place a floor object here");
                onPlaceableSurface = true;
                currentPlaceableObject.GetComponent<Renderer>().material = correctPlacementMaterial;

            }
            //if you are trying to place an object on the table
            else if (hitInfo.transform.gameObject.tag == "Table" && currentPlaceableObject.tag == "Table Object")
            {
                Debug.Log("I can place a table object here");
                onPlaceableSurface = true;
                currentPlaceableObject.GetComponent<Renderer>().material = correctPlacementMaterial;

            }
            //if you are attempting to place an object on the wall, rotate it accordingly
            else if (hitInfo.transform.gameObject.tag == "Wall" && currentPlaceableObject.tag == "Wall Object")
            {
                Debug.Log("I can place a wall object here");
                onPlaceableSurface = true;
                currentPlaceableObject.GetComponent<Renderer>().material = correctPlacementMaterial;

                //this makes sure that an object on the wall has the correct orientation
                currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.forward, (hitInfo.normal));
                if (hitInfo.normal.z == -1)
                {
                    Vector3 newdir = new Vector3(0,-1,-1);
                    currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.forward,Vector3.up*0);
                    currentPlaceableObject.transform.Rotate(Vector3.up, 180);

                }
         
            }
            //if you are attempting to place an object in an incorrect place like a table on the wall, do not allow placement
            else
            {
                Debug.Log("I can't place this object here");
                onPlaceableSurface = false;
                currentPlaceableObject.GetComponent<Renderer>().material = incorrectPlacementMaterial;
            }
        }
    }
    

    //this function allows the player to rotate objects using the mouse wheel
    IEnumerator RotateFromMouseWheel()
    {
        yield return new WaitForSeconds(1);
        
        //rotate based on the player scrolling the wheel
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    //if you are placing an object but want to cancel it's placement
    IEnumerator CancelIfClicked()
    {
        yield return new WaitForSeconds(1);
        //if you are editing, do not allow cancellation
        if(editModeOn == false)
        {
            //if you right click, destroy the object you are placing and turn the menu back on
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(currentPlaceableObject);
                currentPlaceableObject = null;
                currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().isObjectPlaced = true;
                currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOn();
            }
        }
        
    }

    //this function handles placing the object when the player clicks
    IEnumerator ReleaseIfClicked()
    {
        yield return new WaitForSeconds(1);

        //if you click and the object you are placing is on a correct surface, allow for placing
        if (Input.GetMouseButtonDown(0) && onPlaceableSurface == true)
        {
            if(editModeOn == false)//dont change total gold or bar attraction if in edit mode
            {
                BarStatsHandler.GetComponent<BarStatsHandler>().totalGold -= currentPlaceableObject.GetComponent<ItemProperties>().goldCost;
                BarStatsHandler.GetComponent<BarStatsHandler>().totalBarAttractiveness += currentPlaceableObject.GetComponent<ItemProperties>().barAttraction;
            }
            //set layer to default and give it it's original material
            currentPlaceableObject.layer = 0;
            currentPlaceableObject.GetComponent<Renderer>().material = objectCurrentMaterialHolder;
            //change ceiling height if purchasing the second story
            if(currentPlaceableObject.GetComponent<ItemProperties>().itemName == "Second Story")
            {
                Vector3 ceilingPosition = secondStoryCeiling.transform.position;
                ceilingPosition.y += 5;
                BarStatsHandler.GetComponent<BarStatsHandler>().secondStory = true;
                secondStoryCeiling.transform.position = ceilingPosition;
                currentPlaceableObject.GetComponent<RoomWarp>().enabled = true;
               
            }
            //enable door upgrades to work and mark that they are already purchased
            if(currentPlaceableObject.GetComponent<RoomWarp>() != null && currentPlaceableObject.GetComponent<SphereCollider>() != null)
            {
                currentPlaceableObject.GetComponent<RoomWarp>().enabled = true;
                currentPlaceableObject.GetComponent<SphereCollider>().enabled = true;
              
            }
            UpgradeInitialPurchase();

            //deselect the placed object and turn on the menu
            currentPlaceableObject = null;
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().isObjectPlaced = true;
            currentPlaceableObjectNameHolder.GetComponent<MenuHandler>().MenuToggleOn();

            if (editModeOn == true)
            {
                //if editing continue editing
            }
            else
            {
                editModeOn = false;
            }
            
        }
       
             
    }
}
