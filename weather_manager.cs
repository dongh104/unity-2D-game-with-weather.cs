using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather_manager : MonoBehaviour
{
    public weather_manager instance;
    public ParticleSystem Rain;
    public ParticleSystem Snow;
    private void Awake()
    {
        if (instance == null) // 플레이어 챕터 이동 시 플레이어 오브젝트 파괴되는 것을 방지
        {
            DontDestroyOnLoad(this.gameObject); // 게임 오브젝트 파괴 금지
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void rain()
    {
        Rain.Play();
    }
    public void rainstop()
    {
        Rain.Stop();
    }
    public void snow()
    {
        Snow.Play();
    }
    public void snowstop()
    {
        Snow.Stop();
    }
}
