using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exp_bar : MonoBehaviour
{
    private Image healthPointBar;

    play_game playerStat;

    public float lerpSpeed;

    private float currentFill;

    public float myMaxValue { get; set; }

    private float currentValue;

    float Exp;
    public float MaxExp;

    public Text Level;
    public float myCurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value > myMaxValue) currentValue = myMaxValue;
            else if (value < 0) currentValue = 0;
            else currentValue = value;

            //currentFill = currentValue / myMaxValue;
        }
    }

    private void Start()
    {
        healthPointBar = GetComponent<Image>();

        playerStat = FindObjectOfType<play_game>();

        Initialize(playerStat.Exp, MaxExp);
    }

    private void Update()
    {
        MaxExp = 100f + 10f * (playerStat.Level - 1);
        if (currentFill != healthPointBar.fillAmount)
        {
            //Mathf.Lerp(���۰�, ����, ����) -> �ε巴�� ���� ���� ����

            float targetFill = playerStat.Exp / MaxExp; // ��ǥ ä�� ���� ���
            // Mathf.Lerp�� ����Ͽ� ���� ������ ��ǥ ������ �ε巴�� ��ȯ
            healthPointBar.fillAmount = Mathf.Lerp(healthPointBar.fillAmount, targetFill, Time.deltaTime * lerpSpeed);
        }

        Level.text = "Lv." + playerStat.Level.ToString();
        
    }

    public void Initialize(float currentValue, float maxValue)
    {
        myMaxValue = maxValue;

        myCurrentValue = currentValue;
    }
    public void Level_Up()
    {
        if (playerStat.Exp >= 100)
        {
            playerStat.Level += 1;
            playerStat.Exp = 0f;
        }
    }
}