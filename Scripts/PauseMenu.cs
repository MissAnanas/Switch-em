using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool isPaused = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                Resume();
            }
            else if (isPaused == false)
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Debug.Log("Test");
        pauseMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }
    public void Level()
    {
        SceneManager.LoadScene(1);
        isPaused = false;
        Time.timeScale = 1;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isPaused = false;
        Time.timeScale = 1;
    }
    public void Home()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
}