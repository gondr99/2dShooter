using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeapon : AgentWeapon
{
    //나중에 플레이어만을 위한 무기교체, 무기드랍, 무기얻기 코드가 여기 들어옵니다.

    [field : SerializeField]
    public UnityEvent<int, int> OnChangeTotalAmmo { get; set; }  //현재값, 최대값

    [SerializeField] private ReloadGaugeUI _reloadUI = null;
    [SerializeField] private AudioClip _cannotSound = null; //재장전이 안될때 


    [SerializeField] private int _maxTotalAmmo = 2000; //최대 2000발까지 가질 수 있어
    [SerializeField] private int _totalAmmo = 200; //처음 시작시에 2000발 가지고 시작

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
            //코루틴
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
        //현재 총에 부족한 분량과 남은 총탄중에 작은분량으로 선택해서
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
