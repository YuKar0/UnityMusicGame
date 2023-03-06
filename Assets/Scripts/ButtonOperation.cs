using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonOperation : MonoBehaviour
{
    string scene_Main = "Default";
    string sceneName = "Music01";

    public GameObject changeBG;
    public GameObject choice01;
    
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void BackToMainScene()
    {
        SceneManager.LoadScene(scene_Main);
    }

    public void changeMusic()
    {
        changeBG.SetActive(true);
        choice01.SetActive(true);
    }

    public void Select1()
    {
        changeBG.SetActive(false);
        choice01.SetActive(false);

    }
}
