using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour {

    private Animator _animator;
    private AnimatorStateInfo _currentAnimationInfo;
    private EnemyWalker _enemy;

	void Start ()
    {
        _enemy = GetComponent<EnemyWalker>();
        _animator = GetComponent<Animator>();
	}
	
	void Update ()
    {
        _currentAnimationInfo = _animator.GetCurrentAnimatorStateInfo(0);
        AnimatorSwitch();
	}

    private void AnimatorSwitch()
    {
        switch (_enemy.State)
        {
            case Enemy.EnemyState.idle:
                _animator.SetTrigger("idle");
                if (_enemy.spooked == true)
                {
                    _animator.ResetTrigger("idle");
                    _animator.SetTrigger("spooked");
                }
                break;
            case Enemy.EnemyState.moving:
                if (_enemy.spooked == false)
                {
                    _animator.ResetTrigger("spooked");
                    _animator.SetTrigger("moving");
                }
                break;
        }
    }
}
