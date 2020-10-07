using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    
    private float MaxCapacityValue;
    private float CurrentHealthValue;
    private float DischargeHealthValue;

    private bool isDecharging;
    
    public delegate void OnPlayerDeth();
    public event OnPlayerDeth OnDeth;

    private void Start()
    {
        MaxCapacityValue = 100;
        CurrentHealthValue = 100;
        DischargeHealthValue = 1;
        isDecharging = true;
        _healthBar.SetMaxHealth(MaxCapacityValue);
        DOTween.Init();
    }

    private void Update()
    {
        if (isDecharging)
        {
            CurrentHealthValue -= DischargeHealthValue * Time.deltaTime;
            _healthBar.SetHealth(CurrentHealthValue);
        }

        if (CurrentHealthValue <= 0)
        {
            if (OnDeth != null)
                OnDeth();
        }
    }
    
    public void StartCharging()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        isDecharging = false;
        _healthBar.slider.DOValue(MaxCapacityValue, 1);
        CurrentHealthValue = MaxCapacityValue;
        yield return new WaitForSeconds(1);
        isDecharging = true;
    }
}


















