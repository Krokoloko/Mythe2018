using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    private Animator _animator;
    private AnimatorStateInfo _currentAnimationInfo;

    //scripts from the player
    private Walk _walk;
    private Jump _jump;
    private Crouch _crouch;
    private Climb _climb;
    private Drag _drag;

    void Start()
    {
        _walk = GetComponent<Walk>();
        _jump = GetComponent<Jump>();
        _crouch = GetComponent<Crouch>();
        _climb = GetComponent<Climb>();
        _drag = GetComponent<Drag>();

        _animator = GetComponent<Animator>();
        _currentAnimationInfo = _animator.GetCurrentAnimatorStateInfo(0);

    }

    void Update()
    {
        _currentAnimationInfo = _animator.GetCurrentAnimatorStateInfo(0);
    }

    public void AnimatorSwitch(string animation)
    {
        switch (animation)
        {
            case "walk":
                break;
            case "jump":
                break;
            case "crouch":
                break;
            case "climb":
                break;
            case "drag":
                break;
        }
    }
}
