using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public Action OnStatChanged = null;

    public static int STAT_MIN = 0;
    public static int STAT_MAX = 20;

    private int _attackStat;
    
    public Stats(int amount)
    {
        SetAttackStatAmount(amount);
    }

    public void SetAttackStatAmount(int amount)
    {
        _attackStat = Mathf.Clamp( amount, STAT_MIN, STAT_MAX);
        OnStatChanged?.Invoke();
    }

    public int GetAttackStatAmount()
    {
        return _attackStat;
    }

    public float GetAttackStatAmountNormalize()
    {
        return (float)_attackStat / STAT_MAX;
    }

    public void IncStatAmount()
    {
        SetAttackStatAmount(_attackStat + 1);
    }

    public void DecStatAmount()
    {
        SetAttackStatAmount(_attackStat - 1);
    }
}
