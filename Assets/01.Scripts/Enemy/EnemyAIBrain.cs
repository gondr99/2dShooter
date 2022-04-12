using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAIBrain : MonoBehaviour, IAgentInput
{
    [field: SerializeField] public UnityEvent<Vector2> OnMovementKeyPress { get; set; }
    [field: SerializeField] public UnityEvent<Vector2> OnPointerPositionChanged {get; set;}
    [field: SerializeField] public UnityEvent OnFireButtonPress {get; set;}
    [field: SerializeField] public UnityEvent OnFireButtonRelease {get; set;}

    [SerializeField] private AIState _currentState;

    public Transform target;

    private void Start()
    {
        target = GameManager.Instance.PlayerTrm;
    }

    public void Attack()
    {
        OnFireButtonPress?.Invoke();
    }

    public void Move(Vector2 moveDirection, Vector2 targetPosition)
    {
        OnMovementKeyPress?.Invoke(moveDirection);
        OnPointerPositionChanged?.Invoke(targetPosition);
    }

    public void ChangeState(AIState state)
    {
        _currentState = state; //���� ����
    }

    private void Update()
    {
        if(target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero); //Ÿ�� ������ �����
        }
        else
        {
            _currentState.UpdateState();
        }
    }
}