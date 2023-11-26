using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Unity.VisualScripting;

public class imshi : MonoBehaviour
{
    public Text ttt;
    public string weatherText;
    public GameObject weather_selection;

    public weather_manager wm;

    public string c;
    public string w;
    private void Awake()
    {
        wm = FindObjectOfType<weather_manager>();
    }

    void Update()
    {
        ttt = GetComponent<Text>();

        if (SceneManager.GetActiveScene().name == "game_play")
        {
            c = "지역 : 경기도 성남시 \n";
            wm.rainstop();
            wm.snowstop();
        }
        else if (SceneManager.GetActiveScene().name == "Chapter1")
            c = "지역 : 경기도 성남시 \n";
        else if (SceneManager.GetActiveScene().name == "Chapter2")
            c = "지역 : 인천광역시 \n";
        else if (SceneManager.GetActiveScene().name == "Chapter3")
            c = "지역 : 경기도 수원시 \n";
        else if (SceneManager.GetActiveScene().name == "Chapter4")
            c = "지역 : 강원도 원주시 \n";
        else if (SceneManager.GetActiveScene().name == "Chapter5")
            c = "지역 : 부산광역시 \n";
        else if (SceneManager.GetActiveScene().name == "Chapter6")
            c = "지역 : 광주광역시 \n";
        else if (SceneManager.GetActiveScene().name == "Chapter7")
            c = "지역 : 서울시 강남구 \n";
        else if (SceneManager.GetActiveScene().name == "Chapter8")
            c = "지역 : 바티칸 제국 \n";

        if(SceneManager.GetActiveScene().name != "game_play" && w == "날씨 : 비")
        {
            wm.snowstop();
            wm.rain(); // 비 파티클 재생
        } else if (SceneManager.GetActiveScene().name != "game_play" && w == "날씨 : 눈")
        {
            wm.rainstop();
            wm.snow(); // 눈 파티클 재생
        }


        if (ttt != null)
        {
            ttt.text = c + w; // 이건 화면에 띄우는거
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
    void ddad()
    {

    }
    public void weather_select()
    {
        weather_selection.SetActive(true);
        Pause();
    }
    public void sunny()
    {
        w = "날씨 : 맑음";
        Time.timeScale = 1f;
        weather_selection.SetActive(false);
        wm.rainstop(); // 비 파티클 중지
        wm.snowstop();
    }
    public void rainny()
    {
        w = "날씨 : 비";
        Time.timeScale = 1f;
        weather_selection.SetActive(false);
    }
    public void snow()
    {
        w = "날씨 : 눈";
        Time.timeScale = 1f;
        weather_selection.SetActive(false);
    }
}
