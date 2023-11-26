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
        if (instance == null) // �÷��̾� é�� �̵� �� �÷��̾� ������Ʈ �ı��Ǵ� ���� ����
        {
            DontDestroyOnLoad(this.gameObject); // ���� ������Ʈ �ı� ����
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
