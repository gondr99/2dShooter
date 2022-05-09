using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Texture2D cursorTexture = null;
    [SerializeField] private PoolingListSO _initList = null;
    [SerializeField] private TextureParticleManager _textureParticleManagerPrefab;

    private Transform _playerTrm;

    public Transform PlayerTrm
    {
        get
        {
            if(_playerTrm == null)
            {
                //���߿� �÷��̾� ��ũ��Ʈ ����� Ÿ������ �����Ҳ�
                _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return _playerTrm;
        }
    }
    public Player _player;
    public AgentStatusSO PlayerStatus
    {
        get
        {
            if (_player == null)
                _player = PlayerTrm.GetComponent<Player>();
            return _player.PlayerStatus;
        }
    }

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple GameManager is running");
        Instance = this;

        PoolManager.Instance =  new PoolManager(transform); //Ǯ�Ŵ��� ����

        //����� ���� �Ŵ��� ������ ��������
        GameObject timeController = new GameObject("TimeController");
        timeController.transform.parent = transform.parent;
        TimeController.instance = timeController.AddComponent<TimeController>();

        Instantiate(_textureParticleManagerPrefab, transform.parent);

        UIManager.Instance = new UIManager(); 

        SetCursorIcon();
        CreatePool();
    }

    private void CreatePool()
    {
        foreach (PoolingPair pair in _initList.list)
            PoolManager.Instance.CreatePool(pair.prefab, pair.poolCnt);
    }

    private void SetCursorIcon()
    {
        Cursor.SetCursor(cursorTexture,
            new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f),
            CursorMode.Auto);
    }

    public float CriticalChance { get => PlayerStatus.critical; }
    public float CriticalMinDamage { get => PlayerStatus.criticalMinDmg; }
    public float CriticalMaxDamage { get => PlayerStatus.criticalMaxDmg; }


}
