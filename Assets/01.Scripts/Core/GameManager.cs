using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Texture2D cursorTexture = null;
    [SerializeField] private PoolingListSO _initList = null;

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

    //���߿� ���� �ӽð��Դϴ�.
    public float criticalChance = 0.3f; //30�� Ȯ���� ũ��Ƽ��
    public float criticalMinDamage = 1.5f; //ũ��Ƽ�� �ּ� ��
    public float criticalMaxDamage = 2.5f; //ũ��Ƽ�� �ƽ���
}
