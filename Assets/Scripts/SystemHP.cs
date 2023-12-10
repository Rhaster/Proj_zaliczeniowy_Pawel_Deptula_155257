using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemHP : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    [SerializeField] private int healthAmountMax;
    [SerializeField]private int healthAmount;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }
    public void Damage(int damageAmount)
    {
        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDead()
    {
        return healthAmount == 0;
    }

    public bool IsFullHealth()
    {
        return healthAmount == healthAmountMax;
    }

    public int GetHealthAmount()
    {
        return healthAmount;
    }

    public int GetHealthAmountMax()
    {
        return healthAmountMax;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / (float)healthAmountMax;
    }

}
