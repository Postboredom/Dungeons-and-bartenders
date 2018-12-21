using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingMenu : MonoBehaviour {

    //called from play game button in main menu, loads the tavern scene
	public void PlayGame()
    {
        SceneManager.LoadScene("Tavern");

    }

    //called from the quit game button in the main menu, closes the game
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
