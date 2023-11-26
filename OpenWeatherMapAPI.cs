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
    public string KEY_ID; // 컴포넌트 창에서 API key를 문자열로 받아옴
    public Text weatherText; // 날씨 정보를 표현 할 text 오브젝트
    public WeatherData weatherInfo;// 날씨 정보를 담을 객체

    void Start()
    {
    }
    private void Update() // if 문으로 걸어 놔서 게임 메인 화면에서는 api 요청이 안됨
    {
        if (SceneManager.GetActiveScene().name == "game_play" || SceneManager.GetActiveScene().name == "Chapter1")
            CheckCityWeather("Seongnam"); //근데 넘어가면 알다 시피 존나 빠르게 횟수가 올라감
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
        string c = "장소 : ";
        string w = "날씨 : ";
        city = UnityWebRequest.EscapeURL(city);
        string url = "https://api.openweathermap.org/data/2.5/weather?units=metric&appid="; // 데이터를 요청하는 링크의 기본 구조임
        url += KEY_ID;
        url += "&q=" + city; // https://api.openweathermap.org/data/2.5/weather?units=metric&appid="apikey"&q="지역이름영어" <- 이렇게임

        UnityWebRequest www = UnityWebRequest.Get(url); //url 링크로 데이터를 요청
        yield return www.SendWebRequest(); 

        string json = www.downloadHandler.text; // 그것을 json 그릇에 저장
        json = json.Replace("\"base\":", "\"basem\":"); // 이건 글자 대체하는거
        weatherInfo = JsonUtility.FromJson<WeatherData>(json); // 그 그릇에 있는걸 json 언어로 인코딩? 한다고 생각하면 됨
        //url에 요청한 정보가 json으로 저장되어 있고 그 데이터의 형태는 WeatherData의 객체 형태와 같음

        if (SceneManager.GetActiveScene().name == "game_play" || SceneManager.GetActiveScene().name == "Chapter1")
            c = c + "경기도 성남시";
        else if (SceneManager.GetActiveScene().name == "Chapter2")
            c = c + "인천광역시";
        else if (SceneManager.GetActiveScene().name == "Chapter3")
            c = c + "경기도 수원시";
        else if (SceneManager.GetActiveScene().name == "Chapter4")
            c = c + "강원도 원주시";
        else if (SceneManager.GetActiveScene().name == "Chapter5")
            c = c + "부산광역시";
        else if (SceneManager.GetActiveScene().name == "Chapter6")
            c = c + "광주광역시";
        else if (SceneManager.GetActiveScene().name == "Chapter7")
            c = c + "서울시 강남구";
        else if (SceneManager.GetActiveScene().name == "Chapter8")
            c = c + "바티칸 제국";

        if (weatherInfo.weather[0].main == "Clear") 
            w = w + "맑음";
        else if (weatherInfo.weather[0].main == "Clouds")
            w = w + "흐림";
        else if (weatherInfo.weather[0].main == "Snow")
            w = w + "눈";
        else if (weatherInfo.weather[0].main == "Rain")
            w = w + "비";
        else if (weatherInfo.weather[0].main == "Thunderstorm")
            w = w + "비/천둥번개";
        else if (weatherInfo.weather[0].main == "Mist")
            w = w + "안개";

        weatherText.text = c + "\n" + w; // 이건 화면에 띄우는거
    }
}