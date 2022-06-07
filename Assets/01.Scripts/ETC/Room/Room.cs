using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private List<EnemySpawner> _spawnList;
    protected List<Door> _doorList;
    public List<Door> DoorList => _doorList;

    [SerializeField]
    protected bool _roomCleared = false; //모든 포탈이 사라지면 클리어

    private Transform _startPosTrm;
    public Vector3 StartPosition => _startPosTrm.position;

    private int _closedPortalCount; //현재 폐쇄된 포탈의 갯수

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
                    OpenAllDoors(); //모든 문 개방
                }
            });
        }

        _doorList = new List<Door>();
        transform.Find("Doors").GetComponentsInChildren<Door>(_doorList);
        _startPosTrm = transform.Find("StartPosition");
    }

    public void ActiveRoom()
    {
        _spawnList.ForEach(x => x.ActivePortalSensor()); //모든 포탈의 센서 활성화
        
    }

    private void OpenAllDoors()
    {
        _doorList.ForEach(x => x.OpenDoor(true));
    }
}
