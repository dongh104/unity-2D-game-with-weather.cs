using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.IO;
using UnityEngine.SceneManagement;

public class OpenWeatherMapAPI : MonoBehaviour
{
    public string KEY_ID; // ������Ʈ â���� API key�� ���ڿ��� �޾ƿ�
    public Text weatherText; // ���� ������ ǥ�� �� text ������Ʈ
    public WeatherData weatherInfo;// ���� ������ ���� ��ü

    void Start()
    {
    }
    private void Update() // if ������ �ɾ� ���� ���� ���� ȭ�鿡���� api ��û�� �ȵ�
    {
        if (SceneManager.GetActiveScene().name == "game_play" || SceneManager.GetActiveScene().name == "Chapter1")
            CheckCityWeather("Seongnam"); //�ٵ� �Ѿ�� �˴� ���� ���� ������ Ƚ���� �ö�
        else if (SceneManager.GetActiveScene().name == "Chapter2")
            CheckCityWeather("Incheon");
        else if (SceneManager.GetActiveScene().name == "Chapter3")
            CheckCityWeather("Suwon");
        else if (SceneManager.GetActiveScene().name == "Chapter4")
            CheckCityWeather("Wonju");
        else if (SceneManager.GetActiveScene().name == "Chapter5")
            CheckCityWeather("busan");
        else if (SceneManager.GetActiveScene().name == "Chapter6")
            CheckCityWeather("gwangju");
        else if (SceneManager.GetActiveScene().name == "Chapter7")
            CheckCityWeather("seoul");
        else if (SceneManager.GetActiveScene().name == "Chapter8")
            CheckCityWeather("vatican");
    }

    public void CheckCityWeather(string city)
    {
        StartCoroutine(GetWeather(city));
    }

    IEnumerator GetWeather(string city)
    {
        string c = "��� : ";
        string w = "���� : ";
        city = UnityWebRequest.EscapeURL(city);
        string url = "https://api.openweathermap.org/data/2.5/weather?units=metric&appid="; // �����͸� ��û�ϴ� ��ũ�� �⺻ ������
        url += KEY_ID;
        url += "&q=" + city; // https://api.openweathermap.org/data/2.5/weather?units=metric&appid="apikey"&q="�����̸�����" <- �̷�����

        UnityWebRequest www = UnityWebRequest.Get(url); //url ��ũ�� �����͸� ��û
        yield return www.SendWebRequest(); 

        string json = www.downloadHandler.text; // �װ��� json �׸��� ����
        json = json.Replace("\"base\":", "\"basem\":"); // �̰� ���� ��ü�ϴ°�
        weatherInfo = JsonUtility.FromJson<WeatherData>(json); // �� �׸��� �ִ°� json ���� ���ڵ�? �Ѵٰ� �����ϸ� ��
        //url�� ��û�� ������ json���� ����Ǿ� �ְ� �� �������� ���´� WeatherData�� ��ü ���¿� ����

        if (SceneManager.GetActiveScene().name == "game_play" || SceneManager.GetActiveScene().name == "Chapter1")
            c = c + "��⵵ ������";
        else if (SceneManager.GetActiveScene().name == "Chapter2")
            c = c + "��õ������";
        else if (SceneManager.GetActiveScene().name == "Chapter3")
            c = c + "��⵵ ������";
        else if (SceneManager.GetActiveScene().name == "Chapter4")
            c = c + "������ ���ֽ�";
        else if (SceneManager.GetActiveScene().name == "Chapter5")
            c = c + "�λ걤����";
        else if (SceneManager.GetActiveScene().name == "Chapter6")
            c = c + "���ֱ�����";
        else if (SceneManager.GetActiveScene().name == "Chapter7")
            c = c + "����� ������";
        else if (SceneManager.GetActiveScene().name == "Chapter8")
            c = c + "��Ƽĭ ����";

        if (weatherInfo.weather[0].main == "Clear") 
            w = w + "����";
        else if (weatherInfo.weather[0].main == "Clouds")
            w = w + "�帲";
        else if (weatherInfo.weather[0].main == "Snow")
            w = w + "��";
        else if (weatherInfo.weather[0].main == "Rain")
            w = w + "��";
        else if (weatherInfo.weather[0].main == "Thunderstorm")
            w = w + "��/õ�չ���";
        else if (weatherInfo.weather[0].main == "Mist")
            w = w + "�Ȱ�";

        weatherText.text = c + "\n" + w; // �̰� ȭ�鿡 ���°�
    }
}