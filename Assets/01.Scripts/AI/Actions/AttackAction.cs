using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        _aiMovementData.direction = Vector2.zero; //���缭
        _aiMovementData.pointOfInterest = _enemyBrain.target.position;
        _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
        _aiActionData.attack = true;

        _enemyBrain.Attack(); //����Ű�� ������ ����°�
    }
}