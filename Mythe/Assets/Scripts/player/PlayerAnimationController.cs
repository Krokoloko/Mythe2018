using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    private Animator _animator;
    private AnimatorStateInfo _currentAnimationInfo;
    private string _currentAnimation;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentAnimationInfo = _animator.GetCurrentAnimatorStateInfo(0);
        _currentAnimation = "idle";
        AnimatorSwitchTo(_currentAnimation);
    }

    void Update()
    {
        Debug.Log("");
        _currentAnimationInfo = _animator.GetCurrentAnimatorStateInfo(0);
    }

    public void AnimatorSwitchTo(string str)
    {
        _currentAnimationInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (_currentAnimation != str)
        {
            _animator.ResetTrigger(_currentAnimation);

            _currentAnimation = str;

            _animator.SetTrigger(str);
        }
    }
}
