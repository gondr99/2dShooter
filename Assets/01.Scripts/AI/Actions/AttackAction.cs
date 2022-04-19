using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        _aiMovementData.direction = Vector2.zero; //멈춰서
        _aiMovementData.pointOfInterest = _enemyBrain.target.position;
        _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
        _aiActionData.attack = true;

        _enemyBrain.Attack(); //공격키가 눌리게 만드는거
    }
}