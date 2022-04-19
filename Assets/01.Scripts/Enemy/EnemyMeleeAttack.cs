using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack(int damage)
    {
        if(_waitBeforeNextAttack == false)
        {
            IHittable hittable = GetTarget().GetComponent<IHittable>();

            hittable?.GetHit(damage: damage, damageDealer: gameObject);
            AttackFeedback?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine());

            Debug.Log("¿Í±×ÀÛ!");
        }
    }
}
