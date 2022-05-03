using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : PoolableMono, IAgent, IHittable
{
    [SerializeField] private EnemyDataSO _enemytData;
    public EnemyDataSO EnemyData => _enemytData;

    private bool _isDead = false;
    private AgentMovement _agentMovement; //���� �˹�ó���Ϸ��� �̸� �����´�.
    private EnemyAnimation _enemyAnimation;
    private EnemyAttack _enemyAttack;

    //�׾����� ó���� �Ͱ�
    //��Ƽ�� ���¸� ������ �ְ�

    #region �������̽� ������
    public int Health { get; private set;}

    [field : SerializeField] public UnityEvent OnDie { get; set; }
    [field : SerializeField] public UnityEvent OnGetHit { get; set; }

    public bool IsEnemy => true;
    public Vector3 HitPoint { get; private set; }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead) return;
        //���׾����� ����ٰ� �ǰ� ���� ���� �ۼ�
        float critical = Random.value; // 0 ~ 1 
        bool isCritical = false;

        if(critical <= GameManager.Instance.CriticalChance)
        {
            
            float ratio = Random.Range(GameManager.Instance.CriticalMinDamage, 
                GameManager.Instance.CriticalMaxDamage);
            damage = Mathf.CeilToInt((float)damage * ratio);
            isCritical = true;
        }

        Health -= damage;
        HitPoint = damageDealer.transform.position; //���� ���ȴ°�? 
        //�̰� �˾ƾ� normal�� ����ؼ� �ǰ� Ƣ���� �� �� �ִ�.
        OnGetHit?.Invoke(); //�ǰ� �ǵ�� ���

        //���⿡ ������ ���� ����ִ� ������ ���� �Ѵ�.
        DamagePopup popup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        popup.Setup(damage, transform.position + new Vector3(0,0.5f,0), isCritical);


        if(Health <= 0)
        {
            _isDead = true;
            _agentMovement.StopImmediatelly(); //��� ����
            _agentMovement.enabled = false; //�̵��ߴ�
            OnDie?.Invoke(); //��� �̺�Ʈ �κ�ũ
        }
    }
    #endregion

    private void Awake()
    {
        _agentMovement = GetComponent<AgentMovement>();
        _enemyAnimation = transform.Find("VisualSprite").GetComponent<EnemyAnimation>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _enemyAttack.attackDelay = _enemytData.attackDelay;
    }

    public void PerformAttack()
    {
        if (!_isDead)
        {
            //���⿡ �������� ������ ������ �Ŵ�.
            _enemyAttack.Attack(_enemytData.damage);
        }
    }

    public override void Reset()
    {
        Health = _enemytData.maxHealth;
        _isDead = false;
        _agentMovement.enabled = true;
        _enemyAttack.Reset(); //ó�� �����ÿ� ��Ÿ�� �ٽ� ���ư��� 
        //��Ƽ�� ���� �ʱ�ȭ
        //Reset�� ���� �̺�Ʈ ����
    }

    private void Start()
    {
        Health = _enemytData.maxHealth;
    }

    public void Die()
    {
        //��� �̺�Ʈ �κ�ũ �����ְ�
        //Ǯ�Ŵ����� �־��ְ�
        PoolManager.Instance.Push(this);
    }

}
