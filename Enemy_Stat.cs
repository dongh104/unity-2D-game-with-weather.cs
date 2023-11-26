using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Stat : MonoBehaviour
{
    private Image healthPointBar;

    private enemyMove enemyStat;

    public float lerpSpeed;

    private float currentFill;

    private float myMaxValue { get; set; }

    private float myCurrentValue;

    private float currentHp;
    private float maxHp;

    private void Start()
    {
        healthPointBar = GetComponent<Image>();

        enemyStat = GetComponentInParent<enemyMove>();

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
