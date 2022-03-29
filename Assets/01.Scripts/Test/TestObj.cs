using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //�̰� �־�� ��Ʈ���� Ȯ��ż��带 �� �� �־�

public class TestObj : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Vector3 _destination;
    private Camera _mainCam;

    void Start()
    {
        _mainCam = Camera.main;
        _destination = transform.position;
        
    }
    [SerializeField]
    private float _str;
    [SerializeField]
    private int _vibe;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _destination = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            _destination.z = 0;
            MoveToDirection();
            //�����ϸ� 720�� ȸ���Ѵ�. 
            // ȸ���� ������ �ܼ�â�� �� �̶�� ����Ѵ�.
        }

    }

    private void OnMouseEnter()
    {
        //transform.DOKill(); //transform���� ����ǰ� �ִ� ��� Ʈ���� �����Ѵ�.
        //���⿡ ����ŷ ���� �����ִ� ������ �����ִ� �ڵ嵵 �ʿ��ϴ�.
        //transform.DOShakePosition(0.5f, _str, _vibe);
    }

    [SerializeField]
    private Ease _ease;
    private void MoveToDirection()
    {
        Sequence seq = DOTween.Sequence(); //�ڵ����� ������ ��ü�� �ϳ� ���� ������

        Vector3 dir = _destination - transform.position;
        float time = dir.magnitude / _speed;

        //Append�� �༮���� ������� ����ȴ�.
        //�̵��� ������
        seq.Append(transform.DOMove(_destination, time));
        //ȸ���� ����
        seq.Append(transform.DORotate(new Vector3(0, 0, 720f), 0.8f, RotateMode.FastBeyond360));

        seq.AppendCallback(() =>
        {
            Debug.Log(time); //Ŭ���� (closure)
            Debug.Log("Ʈ�� ����");
        });        
    }
}
