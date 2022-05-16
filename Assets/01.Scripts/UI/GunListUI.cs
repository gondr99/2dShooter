using System.Collections;
using System.Collections.Generic;
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
        weaponList.Reverse();
        for(int i = 0; i < weaponList.Count; i++)
        {
            GunPanel panel = Instantiate(_gunPanelPrefab, transform);
            RectTransform rectTrm = panel.GetComponent<RectTransform>();
            rectTrm.anchoredPosition = _initAnchorPos + new Vector2();
        }
    }
}
