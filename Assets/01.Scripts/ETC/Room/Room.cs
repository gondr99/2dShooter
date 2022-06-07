using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private List<EnemySpawner> _spawnList;
    protected List<Door> _doorList;
    public List<Door> DoorList => _doorList;

    [SerializeField]
    protected bool _roomCleared = false; //��� ��Ż�� ������� Ŭ����

    private Transform _startPosTrm;
    public Vector3 StartPosition => _startPosTrm.position;

    private int _closedPortalCount; //���� ���� ��Ż�� ����

    private void Awake()
    {
        _spawnList = new List<EnemySpawner>();

        transform.Find("Portals").GetComponentsInChildren<EnemySpawner>(_spawnList);
        _closedPortalCount = 0;
        foreach(EnemySpawner es in _spawnList)
        {
            es.OnClosePortal.AddListener(() => {
                _closedPortalCount++;
                if(_closedPortalCount >= _spawnList.Count)
                {
                    OpenAllDoors(); //��� �� ����
                }
            });
        }

        _doorList = new List<Door>();
        transform.Find("Doors").GetComponentsInChildren<Door>(_doorList);
        _startPosTrm = transform.Find("StartPosition");
    }

    public void ActiveRoom()
    {
        _spawnList.ForEach(x => x.ActivePortalSensor()); //��� ��Ż�� ���� Ȱ��ȭ
        
    }

    private void OpenAllDoors()
    {
        _doorList.ForEach(x => x.OpenDoor(true));
    }
}
