using UnityEngine;
using UnityEngine.SceneManagement;

public class GuiButtonHandler : MonoBehaviour
{

    public GameObject menu;
    private bool sceneLoaded = false;
    private bool ispaused = false;
    public void Update()
    {
        showPauseMenu();
    }

    public void LoadGame()
    {
        // load level 1
        // when i load i want the canvus populate to the new level
        DontDestroyOnLoad(this.gameObject);
        menu.SetActive(false);
        sceneLoaded = true;
        SceneManager.LoadScene("SampleScene");
    }
 public void exitGame()
    {
               Application.Quit();
    }

    private void showPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P) && sceneLoaded)
        {
            if (!ispaused)
            {
                menu.SetActive(true);
                Time.timeScale = 0f;
                ispaused = true;
            }
            else
            {
                menu.SetActive(false);
                Time.timeScale = 1f;
                ispaused = false;
            }

        }
    }
}
