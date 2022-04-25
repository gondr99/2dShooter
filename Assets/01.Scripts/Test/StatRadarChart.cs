using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatRadarChart : MonoBehaviour
{
    private Stats _stats;
    private Transform _attackBar;

    private void Awake()
    {
        _attackBar = transform.Find("AttackBar");
    }

    public void SetStats(Stats stats)
    {
        this._stats = stats;
        _stats.OnStatChanged += UpdateStatsVisual;
        UpdateStatsVisual();
    }

    private void UpdateStatsVisual()
    {
        _attackBar.localScale = new Vector3(1, _stats.GetAttackStatAmountNormalize());
    }
}
