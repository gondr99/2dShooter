using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestScript : MonoBehaviour
{
    //�븮�� 
    public delegate int Add(int x);

    public delegate int Add2(int a, int b);
    Action<int> a; //int�� �Ķ���� �ϳ��� �޴� void Ÿ�� �Լ�
    UnityEvent<int> c;

    Func<string, int> b; // �Ķ���͸� ��Ʈ�� �ϳ��� �ް� ����Ÿ���� int�� �Լ��� ���� �� �־�

    private void Start()
    {
        
        int a;
        a = 10; // ������ ������ 
        // �Լ��� ������ ��� �ʹ�.
        //
        // 1. ���ͳݿ��� ������ �ٿ�ް�   void DownFile(  ShowScreen   ) { ShowScreen(); }
        // ------�ð� ----
        // 2. �ٿ��� �Ϸ�Ǹ� ȭ�鿡 ����� 

        //�͸��Լ� .. �̸��� ���� �Լ�  ���ټ���
        // 1 . delegate�� �����ϰ� ���ٱ�ȣ =>
        // 2. �Ķ������ Ÿ���� ���� �����ϴ�
        // 3. �Ķ���Ͱ� �Ѱ���� �Ұ�ȣ ���� ����
        // 4. ����¥�� �ڵ�� �װԸ����̵� �ƴϵ� �߰�ȣ ��������
        Add b = x => x + 4;

        List<GameObject> list = new List<GameObject>();

        list.FindAll(x => x.activeSelf).ForEach(x => x.SetActive(false));
        
    }

    public int Add4(int value)
    {
        //alt + shift + .
        return value + 4;
    }
    public int Add8(int value)
    {
        return value + 8;
    }

}
