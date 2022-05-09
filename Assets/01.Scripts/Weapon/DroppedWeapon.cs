using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedWeapon : MonoBehaviour
{
    private BoxCollider2D _boxCol;
    private Weapon _weapon = null;
    public Weapon weapon { get => _weapon; }

    private bool _isActive = false;
    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            _boxCol.enabled = _isActive;
        }
    }

    private void Awake()
    {
        _boxCol = GetComponent<BoxCollider2D>();
        _weapon = GetComponent<Weapon>();
        IsActive = false;
    }

    public void ShowInfoPanel()
    {
        //���� ���� �г� ���̱�
    }

    public void HideInfoPanel()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsActive == false) return;
        if(collision.gameObject.CompareTag("Player"))
        {
            ShowInfoPanel();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsActive == false) return;
        if(collision.gameObject.CompareTag("Player"))
        {
            HideInfoPanel();
        }
    }

    public void PickUpWeapon()
    {
        HideInfoPanel();
        IsActive = false;
    }
}
