using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : AgentWeapon
{
    //���߿� �÷��̾�� ���� ���ⱳü, ������, ������ �ڵ尡 ���� ���ɴϴ�.

    [field : SerializeField]
    public UnityEvent<int, int> OnChangeTotalAmmo { get; set; }  //���簪, �ִ밪

    [SerializeField] private ReloadGaugeUI _reloadUI = null;
    [SerializeField] private AudioClip _cannotSound = null; //�������� �ȵɶ� 


    [SerializeField] private int _maxTotalAmmo = 2000; //�ִ� 2000�߱��� ���� �� �־�
    [SerializeField] private int _totalAmmo = 200; //ó�� ���۽ÿ� 2000�� ������ ����

    public bool AmmoFull { get => _totalAmmo == _maxTotalAmmo; }
    public int TotalAmmo
    {
        get => _totalAmmo;
        set
        {
            _totalAmmo = Mathf.Clamp(value, 0, _maxTotalAmmo);
            OnChangeTotalAmmo?.Invoke(_totalAmmo, _maxTotalAmmo);
        }
    }

    private AudioSource _audioSource;
    private bool _isReloading = false;
    public bool IsReloading { get => _isReloading; }

    protected override void AwakeChild()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected void Start()
    {
        OnChangeTotalAmmo?.Invoke(_totalAmmo, _maxTotalAmmo);
    }

    public void ReloadGun()
    {
        if(_weapon != null && !_isReloading && _totalAmmo > 0 && !_weapon.AmmoFull)
        {
            _isReloading = true;
            _weapon.StopShooting();
            //�ڷ�ƾ
            StartCoroutine(ReloadCoroutine());
        }
        else
        {
            PlayClip(_cannotSound);
        }
    }

    private void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    IEnumerator ReloadCoroutine()
    {
        _reloadUI.gameObject.SetActive(true);
        float time = 0;
        while(time <= _weapon.WeaponData.reloadTime)
        {
            _reloadUI.ReloadGaugeNormal(time / _weapon.WeaponData.reloadTime);
            time += Time.deltaTime;
            yield return null;
        }
        _reloadUI.gameObject.SetActive(false);
        PlayClip(_weapon.WeaponData.reloadClip);

        int reloadedAmmo = Mathf.Min(TotalAmmo, _weapon.EmptyBulletCnt);
        //���� �ѿ� ������ �з��� ���� ��ź�߿� �����з����� �����ؼ�
        TotalAmmo -= reloadedAmmo;
        _weapon.Ammo += reloadedAmmo;

        _isReloading = false;
    }

    public override void Shoot()
    {
        if(_weapon == null)
        {
            PlayClip(_cannotSound);
            return;
        }
        if(_isReloading)
        {
            PlayClip(_weapon.WeaponData.outOfAmmoClip);
            return;
        }
        base.Shoot();
    }
}
