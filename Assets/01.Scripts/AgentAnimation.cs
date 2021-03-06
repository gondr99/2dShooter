using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AgentAnimation : MonoBehaviour
{
    protected Animator _agentAnimator;
    //Hash
    protected readonly int _walkHashStr = Animator.StringToHash("Walk");
    protected readonly int _deathHashStr = Animator.StringToHash("Death");

    private void Awake()
    {
        _agentAnimator = GetComponent<Animator>();
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {
        // do nothing
    }

    public void SetWalkAnimation(bool value)
    {
        _agentAnimator.SetBool(_walkHashStr, value);
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }
    
    public void PlayDeathAnimation()
    {
        _agentAnimator.SetTrigger(_deathHashStr);
    }
    
}