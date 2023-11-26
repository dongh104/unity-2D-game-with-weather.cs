using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fenemy_stat : MonoBehaviour
{
    private Image healthPointBar;

    private flying_enemy enemyStat;

    public float lerpSpeed;

    private float currentFill;

    private float myMaxValue { get; set; }

    private float myCurrentValue;

    private float currentHp;
    private float maxHp;

    private void Start()
    {
        healthPointBar = GetComponent<Image>();

        enemyStat = GetComponentInParent<flying_enemy>();

        Initialize(enemyStat.currentHp, enemyStat.maxHp);

        currentHp = enemyStat.currentHp;
        maxHp = enemyStat.maxHp;
    }

    private void Update()
    {
        if (currentHp != enemyStat.currentHp || maxHp != enemyStat.maxHp)
        {
            currentHp = enemyStat.currentHp;
            maxHp = enemyStat.maxHp;
            UpdateHealthBar();
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        myMaxValue = maxValue;
        myCurrentValue = currentValue;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        //currentFill = 
        healthPointBar.fillAmount = enemyStat.currentHp / enemyStat.maxHp;
    }
}
