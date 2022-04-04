using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : AgentWeapon
{
    //���߿� �÷��̾�� ���� ���ⱳü, ������, ������ �ڵ尡 ���� ���ɴϴ�.

    //

    //����׿� �ڵ� ������ ����
    [SerializeField] private PoolableMono _bulletPrefab;

    protected void Start()
    {
        //����׿� �ڵ�. ������ ����
        PoolManager.Instance.CreatePool(_bulletPrefab, 20);
    }
}
