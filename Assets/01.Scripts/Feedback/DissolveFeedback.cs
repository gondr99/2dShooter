using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class DissolveFeedback : FeedBack
{
    private SpriteRenderer _spriteRenderer = null;
    [SerializeField]
    private float _duration = 0.05f;

    public UnityEvent DeathCallback;

    private void Awake()
    {
        _spriteRenderer = transform.parent.Find("VisualSprite").GetComponent<SpriteRenderer>();
    }

    public override void CompletePrevFeedBack()
    {
        _spriteRenderer.material.DOComplete();
    }

    public override void CreateFeedBack()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_spriteRenderer.material.DOFloat(0, "_Dissolve", _duration));
        if(DeathCallback != null)
        {
            seq.AppendCallback(() => DeathCallback.Invoke());
        }
    }
}
