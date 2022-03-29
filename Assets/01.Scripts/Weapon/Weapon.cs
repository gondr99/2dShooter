using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    #region �� ������
    [SerializeField] protected WeaponDataSO _weaponData;
    [SerializeField] protected GameObject _muzzle; //�ѱ��� ��ġ
    [SerializeField] protected Transform _shellEjectPos; //ź�� ���� ����
    #endregion

    #region Ammo���� �ڵ�
    public UnityEvent<int> OnAmmoChange; 
    [SerializeField] protected int _ammo; //���� �Ѿ� ��
    public int Ammo
    {
        get
        {
            return _weaponData.infiniteAmmo ? -1 : _ammo; //���� źȯ���� ����ź���̸� -1�ƴϸ� ���� �� ����
        }
        set
        {
            _ammo = Mathf.Clamp(value, 0, _weaponData.ammoCapacity);
            OnAmmoChange?.Invoke(_ammo);
        }
    }
    public bool AmmoFull { get => Ammo == _weaponData.ammoCapacity || _weaponData.infiniteAmmo; }
    public int EmptyBulletCnt { get => _weaponData.ammoCapacity - _ammo; } //������ źȯ�� ��ȯ
    #endregion

    #region �߻����
    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;

    protected bool _isShooting = false;
    [SerializeField] protected bool _delayCoroutine = false;
    #endregion

    private void Awake()
    {
        _ammo = _weaponData.ammoCapacity;
    }

    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        //���� ������̰� ���������� �ƴ϶��
        if(_isShooting && _delayCoroutine == false)
        {
            if(Ammo > 0 || _weaponData.infiniteAmmo)
            {
                if(!_weaponData.infiniteAmmo)
                {
                    Ammo--; //���� ź�� ���� �ƴϸ� ź�� ����
                }

                OnShoot?.Invoke(); //���� ����
                for(int i = 0; i < _weaponData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet(); //���� �����մϴ�. �Ƹ��� ������?
                }
            }
            else
            {
                _isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishShooting(); //�ѹ� ����� ���������� ���´ٸ� ����� ����
        }
    }

    protected void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if(_weaponData.automaticFire == false)
        {
            _isShooting = false;
        }
    }

    protected IEnumerator DelayNextShootCoroutine()
    {
        _delayCoroutine = true;
        yield return new WaitForSeconds(_weaponData.weaponDelay);
        _delayCoroutine = false;
    }

    private void ShootBullet()
    {
        Debug.Log("�߻�");
    }

    //��� �����ϴٸ� ��� ����
    public void TryShooting()
    {
        _isShooting = true;
    }

    public void StopShooting()
    {
        _isShooting = false;
    }
}
