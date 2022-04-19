using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyAIBrain _enemyBrain;
    protected Enemy _enemy;

    public float attackDelay = 1;

    protected bool _waitBeforeNextAttack;

    public bool WaitingForNextAttack => _waitBeforeNextAttack;

    protected bool _isAttacking = false; //현재 공격중인가?
    public bool IsAttacking => _isAttacking;

    public UnityEvent AttackFeedback;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyBrain = GetComponent<EnemyAIBrain>();

        AwakeChild();
    }

    protected virtual void AwakeChild()
    {
        //do nothing here!
    }

    protected IEnumerator WaitBeforeAttackCoroutine()
    {
        _waitBeforeNextAttack = true;
        yield return new WaitForSeconds(attackDelay);
        _waitBeforeNextAttack = false;
    }

    public void Reset()
    {
        StartCoroutine(WaitBeforeAttackCoroutine());
    }

    public Transform GetTarget()
    {
        return _enemyBrain.target;
    }

    public abstract void Attack(int damage);
}