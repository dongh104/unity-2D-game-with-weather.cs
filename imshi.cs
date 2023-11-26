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
            c = "���� : ��⵵ ������ \n";
            wm.rainstop();
            wm.snowstop();
        }
        else if (SceneManager.GetActiveScene().name == "Chapter1")
            c = "���� : ��⵵ ������ \n";
        else if (SceneManager.GetActiveScene().name == "Chapter2")
            c = "���� : ��õ������ \n";
        else if (SceneManager.GetActiveScene().name == "Chapter3")
            c = "���� : ��⵵ ������ \n";
        else if (SceneManager.GetActiveScene().name == "Chapter4")
            c = "���� : ������ ���ֽ� \n";
        else if (SceneManager.GetActiveScene().name == "Chapter5")
            c = "���� : �λ걤���� \n";
        else if (SceneManager.GetActiveScene().name == "Chapter6")
            c = "���� : ���ֱ����� \n";
        else if (SceneManager.GetActiveScene().name == "Chapter7")
            c = "���� : ����� ������ \n";
        else if (SceneManager.GetActiveScene().name == "Chapter8")
            c = "���� : ��Ƽĭ ���� \n";

        if(SceneManager.GetActiveScene().name != "game_play" && w == "���� : ��")
        {
            wm.snowstop();
            wm.rain(); // �� ��ƼŬ ���
        } else if (SceneManager.GetActiveScene().name != "game_play" && w == "���� : ��")
        {
            wm.rainstop();
            wm.snow(); // �� ��ƼŬ ���
        }


        if (ttt != null)
        {
            ttt.text = c + w; // �̰� ȭ�鿡 ���°�
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
        w = "���� : ����";
        Time.timeScale = 1f;
        weather_selection.SetActive(false);
        wm.rainstop(); // �� ��ƼŬ ����
        wm.snowstop();
    }
    public void rainny()
    {
        w = "���� : ��";
        Time.timeScale = 1f;
        weather_selection.SetActive(false);
    }
    public void snow()
    {
        w = "���� : ��";
        Time.timeScale = 1f;
        weather_selection.SetActive(false);
    }
}
