using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour {

    private Animator _animator;
    private AnimatorStateInfo _currentAnimationInfo;
    private EnemyWalker _enemy;
    private string _currentAnimation;

    public EnemyAnimationController(string startAnim,GameObject enemy)
    {
        _enemy = enemy.GetComponent<EnemyWalker>();
        _animator = enemy.GetComponent<Animator>();
        _currentAnimation = startAnim;
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
