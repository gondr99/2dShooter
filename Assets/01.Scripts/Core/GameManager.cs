using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Texture2D cursorTexture = null;
    [SerializeField] private PoolingListSO _initList = null;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple GameManager is running");
        Instance = this;

        PoolManager.Instance =  new PoolManager(transform); //풀매니저 생성

        //여기다 각종 매니저 로직을 넣을꺼야

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
}
