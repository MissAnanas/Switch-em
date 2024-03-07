using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }
    public void Level2()
    {
        SceneManager.LoadSceneAsync("Level 2");
    }
    public void Level3()
    {
        SceneManager.LoadSceneAsync("Level 3");
    }
    public void Level4()
    {
        SceneManager.LoadSceneAsync("Level 4");
    }
    public void Level5()
    {
        SceneManager.LoadSceneAsync("Level 5");
    }
    public void Level6()
    {
        SceneManager.LoadSceneAsync("Level 6");
    }
    public void Level7()
    {
        SceneManager.LoadSceneAsync("Level 7");
    }
    public void Level8()
    {
        SceneManager.LoadSceneAsync("Boss");
    }
}
