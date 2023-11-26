using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUI : MonoBehaviour
{
    private Image healthPointBar;

    play_game playerStat;

    public float lerpSpeed;

    private float currentFill;

    public float myMaxValue { get; set; }

    private float currentValue;

    float hp;

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

        Initialize(playerStat.Hp, playerStat.Hp);

        hp = playerStat.Hp;
    }

    private void Update()
    {
        if (currentFill != healthPointBar.fillAmount)
        {
            //Mathf.Lerp(���۰�, ����, ����) -> �ε巴�� ���� ���� ����
            float targetFill = playerStat.Hp / hp; // ��ǥ ä�� ���� ���

            // Mathf.Lerp�� ����Ͽ� ���� ������ ��ǥ ������ �ε巴�� ��ȯ
            healthPointBar.fillAmount = Mathf.Lerp(healthPointBar.fillAmount, targetFill, Time.deltaTime * lerpSpeed);
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        myMaxValue = maxValue;

        myCurrentValue = currentValue;
    }
}