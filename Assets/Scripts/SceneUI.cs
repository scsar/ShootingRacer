using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneUI : MonoBehaviour
{
    public static SceneUI Instance= null;
    string buttonName;
    public string Button
    {
        get
        {
            return buttonName;
        }
        set
        {
            buttonName=value;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance==null)
        {
            Instance=this;
        }
    }
    public void Restart()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Lobby()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene("Intro");
    }
    public void StartLv()
    {
        Time.timeScale=1f;
        Debug.Log(buttonName);
        SceneManager.LoadScene(buttonName);
    }

    public void Tutorial()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene("Tutorial");
    }

    public void LevelSelect()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene("Stage_Select");
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
