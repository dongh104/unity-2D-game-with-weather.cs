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
            //Mathf.Lerp(시작값, 끝값, 기준) -> 부드럽게 값을 변경 가능
            float targetFill = playerStat.Hp / hp; // 목표 채움 비율 계산

            // Mathf.Lerp를 사용하여 현재 값에서 목표 값으로 부드럽게 전환
            healthPointBar.fillAmount = Mathf.Lerp(healthPointBar.fillAmount, targetFill, Time.deltaTime * lerpSpeed);
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        myMaxValue = maxValue;

        myCurrentValue = currentValue;
    }
}