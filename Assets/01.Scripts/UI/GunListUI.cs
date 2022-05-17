using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunListUI : MonoBehaviour
{
    [SerializeField] private GunPanel _gunPanelPrefab;
    private List<GunPanel> _panelList;

    [SerializeField] private AudioClip _changeClip;
    [SerializeField] private float _transitionTime = 0.2f;

    private AudioSource _audioSource;

    [Header("초기 위치값")]
    [SerializeField] private Vector2 _initAnchorPos;
    private float _xDelta = 7f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _panelList = new List<GunPanel>();
    }

    public void InitUIPanel(List<Weapon> weaponList)
    {
        for(int i = weaponList.Count -1; i >= 0; i--)
        {
            GunPanel panel = Instantiate(_gunPanelPrefab, transform);
            RectTransform rectTrm = panel.GetComponent<RectTransform>();
            rectTrm.anchoredPosition = _initAnchorPos + new Vector2(i * _xDelta, 0);

            if(i != 0)
            {
                rectTrm.localScale = Vector3.one * 0.9f;
            }

            panel.Init(weaponList[i]);
            _panelList.Add(panel);
        }
        _panelList.Reverse();
    }

    private void PlaySound(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    #region 무기 변경 UI 닷트윈
    public void ChangeWeaponUI(bool isPrev, Action CallBack = null)
    {
        GunPanel first = _panelList.First();
        GunPanel last = _panelList.Last();
        GunPanel next = _panelList[1];

        Sequence seq = DOTween.Sequence();
        if(isPrev)
        {
            seq.Append(first.RectTrm.DOScale(Vector3.one * 0.9f, _transitionTime));
            seq.Join(first.RectTrm.DOAnchorPos(_initAnchorPos + new Vector2(_xDelta, 0), _transitionTime));
            for(int i = 1; i < _panelList.Count - 1; i++)
            {
                seq.Join(_panelList[i].RectTrm.DOAnchorPos(
                    _initAnchorPos + new Vector2(_xDelta * (i + 1), 0), 
                    _transitionTime));
            }
            seq.Join(last.RectTrm.DOScale(Vector3.one, _transitionTime));
            seq.Join(last.RectTrm.DOAnchorPos(_initAnchorPos + new Vector2(0, 82), _transitionTime));

            seq.AppendCallback(() =>
            {
                last.RectTrm.SetAsLastSibling();
                _panelList.RemoveAt(_panelList.Count - 1);
                _panelList.Insert(0, last); //맨 앞으로
            });

            seq.Append(last.RectTrm.DOAnchorPos(_initAnchorPos, _transitionTime));

        }else
        {
            seq.Append(first.RectTrm.DOScale(Vector3.one * 0.9f, _transitionTime));
            seq.Join(first.RectTrm.DOAnchorPos(_initAnchorPos + new Vector2(0, 82), _transitionTime));

            seq.Join(next.RectTrm.DOScale(Vector3.one, _transitionTime));
            seq.Join(next.RectTrm.DOAnchorPos(_initAnchorPos, _transitionTime));

            for(int i = 2; i < _panelList.Count; i++)
            {
                seq.Join(_panelList[i].RectTrm.DOAnchorPos(
                    _initAnchorPos + new Vector2( _xDelta * (i - 1),  0),
                    _transitionTime));
            }

            seq.AppendCallback(() =>
            {
                first.RectTrm.SetAsFirstSibling(); //첫번째 자식으로 설정한다.
                _panelList.RemoveAt(0);
                _panelList.Add(first);
            });

            seq.Append(first.RectTrm.DOAnchorPos(
                _initAnchorPos + new Vector2(_xDelta * (_panelList.Count - 1), 0), 
                _transitionTime));

        }

        seq.AppendCallback(() =>
        {
            PlaySound(_changeClip);
            CallBack?.Invoke();
        });
    }
    #endregion
}
