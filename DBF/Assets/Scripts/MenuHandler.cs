using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuHandler : MonoBehaviour {


    /// <summary>
    /// this script handles interaction with the menu
    /// </summary>

    public GameObject areaDecoratorUI;
    bool visibleUI = false;
    public GameObject playerObject;
    public string currentPlaceableObjectName;
    public bool isObjectPlaced;
   

    private void Awake()
    {
        currentPlaceableObjectName = "";
    }

    // Update is called once per frame
    void Update()
    {
        MenuToggler();
        
    }

    // When you press "I", turn the menu on or off
    void MenuToggler()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //if the menu is off turn it on
            if (visibleUI == false)
            {
                MenuToggleOn();
            }
            else 
            {
                MenuToggleOff();
            }
         
        }
       
    }
    //enable the menu and the cursor and disable the first person controller for the character
    public void MenuToggleOn()
    {
        visibleUI = true;
        areaDecoratorUI.SetActive(true);
        playerObject.GetComponent<FirstPersonController>().enabled = false;
        playerObject.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
      
  
    }
    //disable the menu and the cursor and enable the first person controller for the character
    public void MenuToggleOff()
    {
        playerObject.GetComponent<FirstPersonController>().enabled = true;
        playerObject.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        areaDecoratorUI.SetActive(false);
        Cursor.visible = false;
        visibleUI = false;
    }

    //this is attached to each item button. It stores the name of the button that was pressed in the variable currentPlaceableObjectName
    public void ObjectButtonPressed()
    {
        currentPlaceableObjectName = EventSystem.current.currentSelectedGameObject.name;
        isObjectPlaced = false;
    }
}
