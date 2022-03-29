using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Popup : MonoBehaviour
{
    [SerializeField] private Button _openBtn;
    [SerializeField] private Button _closeBtn;

    private CanvasGroup _canvasGroup;
    private RectTransform _rectTrm;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTrm = GetComponent<RectTransform>();
    }
    void Start()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;

        _rectTrm.localScale = Vector3.zero;
        _closeBtn.enabled = false;

        _openBtn.onClick.AddListener(() =>
        {
            _openBtn.enabled = false;
            Sequence seq = DOTween.Sequence();

            //DOTween.To(����, ����, ������, �ð�   ); 
            seq.Append(DOTween.To(() => _canvasGroup.alpha, v => _canvasGroup.alpha = v, 1f, 0.5f));

            //seq.Append();
            seq.Join(_rectTrm.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f));
            seq.Append(_rectTrm.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.2f));
            seq.Append(_rectTrm.DOScale(new Vector3(1f, 1f, 1f), 0.2f));
            seq.AppendCallback(() => {
                _closeBtn.enabled = true;
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.interactable = true;
            });
        });

        _closeBtn.onClick.AddListener(() =>
        {
            //����� ����� ���ϱ��� �������� �˴ϴ�. �����˻� �մϴ�.

            //�˾�â�� �����Ŀ��� �ٽ� ������ư�� Ȱ��ȭ�ǵ���
            //seq.AppendCallback(() => _openBtn.enabled = true);
        });
    }

}
