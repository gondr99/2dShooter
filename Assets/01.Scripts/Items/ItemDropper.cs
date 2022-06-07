using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    [SerializeField]
    private ItemDropTableSO _dropTable;
    private float[] _itemWeights;

    [SerializeField]
    private bool _dropEffect = false; //��������Ʈ

    [SerializeField]
    [Range(0, 1f)]
    private float _dropChance; //�� ��? �� �������� ���Ȯ��

    //3�� 
    //1�� : �������� ���� ���ΰ�?
    //2�� : � �������� �������ΰ�? (�� ���⼭ ����ġ�� �����ؼ� ����ġ�� ���� ���

    private void Start()
    {
        _itemWeights = _dropTable.dropList.Select(item => item.rate).ToArray();
    }

    public void DropItem()
    {
        float dropVariable = Random.value; //0 ~1;
        if(dropVariable < _dropChance) //������� �ɷȴٸ� ������ ���
        {
            int idx = GetRandomWeightedIndex();
            PoolableMono resource = PoolManager.Instance.Pop(_dropTable.dropList[idx].itemPrefab.name);

            resource.transform.position = transform.position;

            if(_dropEffect)
            {
                Vector3 offset = Random.insideUnitCircle;

                resource.transform.DOJump(transform.position + offset, 1f, 1, 0.3f);
            }
        }
        // �ƴϸ� �ƹ��͵� �ȹ���.
    }

    private int GetRandomWeightedIndex()
    {
        float sum = 0f;
        for(int i = 0; i < _itemWeights.Length; i++)
        {
            sum += _itemWeights[i]; //�̷��� ��� �������� ���Ȯ���� �ջ�ȴ�.
        }

        float randomValue = Random.Range(0, sum);
        float tempSum = 0;

        for(int i = 0; i < _itemWeights.Length; i++)
        {
            if(randomValue >= tempSum && randomValue < tempSum + _itemWeights[i])
            {
                return i;
            }else
            {
                tempSum += _itemWeights[i];
            }
        }

        return 0;
    }
}
