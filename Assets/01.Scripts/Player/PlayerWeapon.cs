using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : AgentWeapon
{
    //나중에 플레이어만을 위한 무기교체, 무기드랍, 무기얻기 코드가 여기 들어옵니다.

    //

    //디버그용 코드 삭제할 예정
    [SerializeField] private PoolableMono _bulletPrefab;

    protected void Start()
    {
        //디버그용 코드. 삭제할 예정
        PoolManager.Instance.CreatePool(_bulletPrefab, 20);
    }
}
