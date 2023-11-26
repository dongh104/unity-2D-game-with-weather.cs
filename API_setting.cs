using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class API_setting : MonoBehaviour
{
    public Text ApI_key;
    public Text ApI_key2;

    OpenWeatherMapAPI owd;

    public string new_key;
    public string old_key;

    private void Awake()
    {
        owd = FindAnyObjectByType<OpenWeatherMapAPI>();
    }
    private void Update()
    {
        old_key = owd.KEY_ID;
        ApI_key.text = old_key;
    }
    public void API_set()
    {
        new_key = ApI_key2.text;
        owd.KEY_ID = new_key;
    }
}
