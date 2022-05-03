using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable
{
    [SerializeField] private AgentStatusSO _agentStatusSO;
    public AgentStatusSO PlayerStatus { get => _agentStatusSO; }

    private int _health;
    public int Health { 
        get => _health; 
        set { _health = Mathf.Clamp(value, 0, _agentStatusSO.maxHP); } 
    }

    //���ó���� ���� �Ҹ��� ���� �ϳ� �߰�
    private bool _isDead = false;
    //�÷��̾� ���������� �ҷ����� ���� ����
    private PlayerWeapon _playerWeapon;
    public PlayerWeapon PWeapon { get => _playerWeapon; }

    [field : SerializeField] public UnityEvent OnDie { get; set; }
    [field : SerializeField] public UnityEvent OnGetHit { get; set; }

    public bool IsEnemy => false;

    public Vector3 HitPoint { get; private set; }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead) return;

        Health -= damage;
        OnGetHit?.Invoke();
        if(Health <= 0)
        {
            OnDie?.Invoke();
            _isDead = true;
        }
    }

    private void Awake()
    {
        _playerWeapon = transform.Find("WeaponParent").GetComponent<PlayerWeapon>();
    }

    private void Start()
    {
        Health = _agentStatusSO.maxHP;
    }
}