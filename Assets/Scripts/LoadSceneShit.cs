using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneShit : MonoBehaviour
{
    public GameObject PausePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadLobbyScene()
    {
        SceneManager.LoadScene("Lobby"); //go to lobby scene
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit"); //quit game
    }
    public void PlayOrRestartGame()
    {
        SceneManager.LoadScene(""); //Input your scene name or just do restart current scene(your choice lol)
        Debug.Log("Play lol");
    }
    public void PauseGame()
    {
        Time.timeScale = 0; //pause game
        PausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1; //resume game
        PausePanel.SetActive(false);
    }
}
