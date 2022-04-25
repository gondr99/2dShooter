using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarTest : MonoBehaviour
{
    [SerializeField] private StatRadarChart _statRadarChart = null;

    private Stats _stats;
    private void Start()
    {
        _stats = new Stats(10);
        _statRadarChart.SetStats(_stats);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            _stats.IncStatAmount();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            _stats.DecStatAmount();
        }
    }

}
