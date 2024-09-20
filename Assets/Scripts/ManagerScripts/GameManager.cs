using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    new AudioSource audio;
    public AudioClip ClearSound;

    GameObject Player;
    GameObject GameOverText;
    GameObject RestartButton;
    GameObject ClearText;
    GameObject SelectButton;
    GameObject LobbyButton;
    
    bool isGameOver=false;
    bool GameClear=false;
    
    string SceneName;
    float BestMeter;
    float Meterx;

    public bool GameOver
    {
        get
        {
            return isGameOver;
        }
        set
        {
            isGameOver=value;
            if(isGameOver)
            {
                Time.timeScale=0f;
                audio.Play();
                GameOverText.SetActive(true);
                RestartButton.SetActive(true);        
                SelectButton.SetActive(true);
                LobbyButton.SetActive(true);
            }
        }
    }

    public bool Clear
    {
        get
        {
            return GameClear;
        }
        set
        {
            GameClear=value;
            if(GameClear)
            {
                Time.timeScale=0f;
                audio.clip=ClearSound;
                audio.Play();
                ClearText.SetActive(true);
                RestartButton.SetActive(true);
                SelectButton.SetActive(true);
                LobbyButton.SetActive(true);
            }
        }
    }


    public Text MeterText;
    public Text BestText;
    public static GameManager GM=null;
    void Awake()
    {
        if(GM==null)
        {
            GM=this;
        }

        audio=GetComponent<AudioSource>();
        Player=GameObject.Find("Player");
        GameOverText=GameObject.Find("GameOver");
        ClearText=GameObject.Find("Clear");
        RestartButton=GameObject.Find("Restart");
        SelectButton=GameObject.Find("Level_Select");
        LobbyButton=GameObject.Find("Lobby");
    }


    void Start()
    {
        SceneName=SceneManager.GetActiveScene().name;
        BestMeter=PlayerPrefs.GetFloat(SceneName,0);
        BestText.text="Best : "+BestMeter+" M";

        ClearText.SetActive(false);
        GameOverText.SetActive(false);
        RestartButton.SetActive(false);
        SelectButton.SetActive(false);
        LobbyButton.SetActive(false);
    }

    void Update()
    {
        if(!GameOver)
        {
            Vector3 Meter=Player.transform.position;
            Meterx=Mathf.Floor(Meter.x *10) *0.1f;
            MeterText.text="Meter : "+Meterx+" M";
        }
        else
        {
            if(Meterx>BestMeter)
            {
                BestMeter=Meterx;
                PlayerPrefs.SetFloat(SceneName,BestMeter);
            }
        }
    }


}
