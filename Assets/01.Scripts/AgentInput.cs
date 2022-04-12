using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

// https://github.com/gondr99/2dShooter.git

public class AgentInput : MonoBehaviour, IAgentInput
{
    [field: SerializeField] public UnityEvent<Vector2> OnMovementKeyPress { get; set; }
    [field: SerializeField] public UnityEvent<Vector2> OnPointerPositionChanged { get; set; }

    //´Ü¹ßÇü ÃÑ, 
    [field: SerializeField] public UnityEvent OnFireButtonPress { get; set; }
    [field: SerializeField]public UnityEvent OnFireButtonRelease { get; set; }

    public UnityEvent OnReloadButtonPress = null;

    private bool _fireButtonDown = false;

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
        GetReloadInput();
    }

    private void GetReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
            OnReloadButtonPress?.Invoke();
    }

    private void GetFireInput()
    {
        if(Input.GetAxisRaw("Fire1") > 0)
        {
            if(!_fireButtonDown)
            {
                _fireButtonDown = true;
                OnFireButtonPress?.Invoke();
            }
        }
        else
        {
            if(_fireButtonDown)
            {
                _fireButtonDown = false;
                OnFireButtonRelease?.Invoke();
            }
        }
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector2 mouseInWorldPos = MainCam.ScreenToWorldPoint(mousePos);
        OnPointerPositionChanged?.Invoke(mouseInWorldPos);
    }

    private void GetMovementInput()
    {
        OnMovementKeyPress?.Invoke(
            new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
        );
    }
}
