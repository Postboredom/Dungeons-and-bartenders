using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuHandler : MonoBehaviour {

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

    void MenuToggler()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
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
    public void MenuToggleOn()
    {
        visibleUI = true;
        areaDecoratorUI.SetActive(true);
        playerObject.GetComponent<FirstPersonController>().enabled = false;
        playerObject.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = false;
        Cursor.lockState = CursorLockMode.None;
       // Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
      
       // Debug.Log(Cursor.visible);
       // Debug.Log(playerObject.GetComponent<FirstPersonController>().m_MouseLook.lockCursor);
    }
    public void MenuToggleOff()
    {
        playerObject.GetComponent<FirstPersonController>().enabled = true;
        playerObject.GetComponent<FirstPersonController>().m_MouseLook.lockCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        areaDecoratorUI.SetActive(false);
        Cursor.visible = false;
        visibleUI = false;
    }
    public void ObjectButtonPressed()
    {
        currentPlaceableObjectName = EventSystem.current.currentSelectedGameObject.name;
        isObjectPlaced = false;
    }
}
