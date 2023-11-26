using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuSet;
    public GameObject Inven;
    public static bool GameIsPaused = false;

    public List<Item_info> getItem = new List<Item_info>();
    public Sprite sprite;
    public Texture2D demon;   
    public Text PlayerName;
    // public WeatherData wd;
    public GameObject asdf;
    private void Awake()
    {
        //wd = FindAnyObjectByType<WeatherData>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
            {
                menuSet.SetActive(false);
                Resume();
            }
            else if (!menuSet.activeSelf)
            {
                menuSet.SetActive(true);
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!Inven.activeSelf)
            {
                Inven.SetActive(true);
            } else
            {
                Inven.SetActive(false);
            }
        }
    }
    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void Save()
    {
        DataManager.instance.nowPlayer.position = play_game.instance.transform.position;
        Scene currentScene = SceneManager.GetActiveScene();
        DataManager.instance.nowPlayer.nowScene = currentScene;
        DataManager.instance.nowPlayer.nowChapter_name = currentScene.name;
        DataManager.instance.nowPlayer.Hp = play_game.instance.Hp;

        DataManager.instance.nowPlayer.item_count = getItem.Count;
        DataManager.instance.nowPlayer.itemImages = getItem[0].itemImage;
        //foreach (var item in getItem)
        //{
        //    DataManager.instance.nowPlayer.itemImages.Add(item.itemImage);
        //}

        if (sprite != null)
        {
            Texture2D tex = demon as Texture2D;
            byte[] imageBytes = tex.EncodeToPNG();
            string base64Image = Convert.ToBase64String(imageBytes);
            DataManager.instance.nowPlayer.itemin = base64Image;
        }


        //DataManager.instance.nowPlayer.weatherdata = wd;
        //DataManager.instance.nowPlayer.nowChapter = play_game.instance.scene;
        DataManager.instance.SaveData();
    }
    public void GameExit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
}