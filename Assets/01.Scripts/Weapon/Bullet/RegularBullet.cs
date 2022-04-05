using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class RegularBullet : Bullet
{
    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriterRenderer;
    protected Animator _animator;
    protected float _timeToLive;

    private int _enemyLayer;
    private int _obstacleLayer;

    private bool _isDead = false; //�Ѱ��� �Ѿ��� �������� ���� �����ִ� ���� ���� ����.

    public override BulletDataSO BulletData { 
        get => _bulletData;
        set
        {
            //_bulletData = value;
            base.BulletData = value;

            if(_rigidbody2D == null)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
            _rigidbody2D.drag = _bulletData.friction;
            
            if(_spriterRenderer == null)
            {
                _spriterRenderer = GetComponent<SpriteRenderer>();
            }
            _spriterRenderer.sprite = _bulletData.sprite;
            _spriterRenderer.material = _bulletData.bulletMat;
            if(_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            _animator.runtimeAnimatorController = _bulletData.animatorController;

            if (_isEnemy)
                _enemyLayer = LayerMask.NameToLayer("Player");
            else
                _enemyLayer = LayerMask.NameToLayer("Enemy");
        }
    }

    private void Awake()
    {
        _obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }

    private void FixedUpdate()
    {
        _timeToLive += Time.fixedDeltaTime;

        if(_timeToLive >= _bulletData.lifeTime)
        {
            _isDead = true;
            PoolManager.Instance.Push(this);
        }

        if(_rigidbody2D != null && _bulletData != null)
        {
            _rigidbody2D.MovePosition(
                transform.position + 
                _bulletData.bulletSpeed * transform.right * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead) return;  //���� ����ź�̸� ���⼭ ���� �ٸ� �۾��� �ؾ� �Ѵ�.

        //���⿡�� �ǰ��ؼ� �������� �ְ� �˹��Ű�� �ڵ尡 ���⿡ ���ߵȴ�.

        if(collision.gameObject.layer == _obstacleLayer)
        {
            HitObstacle(collision);
        }

        if(collision.gameObject.layer == _enemyLayer)
        {
            HitEnemy(collision);
        }
        _isDead = true;
        PoolManager.Instance.Push(this);
    }

    private void HitEnemy(Collider2D collider)
    {

    }

    private void HitObstacle(Collider2D collider)
    {
        Debug.Log(collider.gameObject.transform.position);
        //���� �¾��� �� ������ ȸ�������� ȸ���� ImpactObject �����Ǽ� �浹��ġ�� ��Ȯ�ϰ� ��Ÿ���� �������.
    }

    public override void Reset() 
    {
        damageFactor = 1;
        _timeToLive = 0;
        _isDead = false;
    }

}
