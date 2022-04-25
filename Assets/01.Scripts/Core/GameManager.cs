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
                //나중에 플레이어 스크립트 만들면 타입으로 변경할께
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

        PoolManager.Instance =  new PoolManager(transform); //풀매니저 생성

        //여기다 각종 매니저 로직을 넣을꺼야
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

    //나중에 지울 임시값입니다.
    public float criticalChance = 0.3f; //30퍼 확률로 크리티컬
    public float criticalMinDamage = 1.5f; //크리티컬 최소 뎀
    public float criticalMaxDamage = 2.5f; //크리티컬 맥스뎀
}
