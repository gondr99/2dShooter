using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIActionData _aiActionData;
    protected AIMovementData _aiMovementData;
    protected EnemyAIBrain _enemyBrain;

    private void Awake()
    {
        _aiActionData = transform.GetComponentInParent<AIActionData>();
        _aiMovementData = transform.GetComponentInParent<AIMovementData>();
        _enemyBrain = transform.GetComponentInParent<EnemyAIBrain>();

        ChildAwake();
    }

    protected virtual void ChildAwake()
    {
        //자식에서 해줄것이 있다면 여기서
    }

    public abstract void TakeAction();
}
