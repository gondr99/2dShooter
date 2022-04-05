using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    #region �� ������
    [SerializeField] protected WeaponDataSO _weaponData;
    [SerializeField] protected GameObject _muzzle; //�ѱ��� ��ġ
    [SerializeField] protected Transform _shellEjectPos; //ź�� ���� ����

    [SerializeField] protected bool _isEnemyWeapon = false;
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
        WeaponAudio audio = transform.Find("WeaponAudio").GetComponent<WeaponAudio>();
        audio.SetAudioClip(_weaponData.shootClip, _weaponData.outOfAmmoClip, _weaponData.reloadClip);
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
        //Debug.Log("�߻�");
        SpawnBullet(_muzzle.transform.position, CalculateAngle(_muzzle), _isEnemyWeapon);
    }

    //�߻�ÿ� �Ѿ��� ���������� ���� �߻簢�� ������ִ� �Լ�
    private Quaternion CalculateAngle(GameObject muzzle)
    {
        float spread = Random.Range(-_weaponData.spreadAngle, _weaponData.spreadAngle);
        //Quaternion.AngleAxis(spread, Vector3.forward);
        Quaternion spreadRot = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * spreadRot;
    }

    private void SpawnBullet(Vector3 pos, Quaternion rot, bool isEnemyBullet)
    {
        Bullet b = PoolManager.Instance.Pop(_weaponData.bulletData.prefab.name) as Bullet;
        b.SetPositionAndRotation(pos, rot);
        b.IsEnemy = isEnemyBullet;
        b.BulletData = _weaponData.bulletData;
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
