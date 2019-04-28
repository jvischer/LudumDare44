using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LowerBodyController : MonoBehaviour {

    private const string IS_MOVING_BOOL = "IsMoving";
    private const string IS_GROUNDED_BOOL = "IsGrounded";
    private const string DEATH_TRIGGER = "Dead";

    private Animator _animator;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void SetMoving(bool isMoving) {
        _animator.SetBool(IS_MOVING_BOOL, isMoving);
    }

    public void SetGrounded(bool isGrounded) {
        _animator.SetBool(IS_GROUNDED_BOOL, isGrounded);
    }

    public void Died() {
        _animator.SetTrigger(DEATH_TRIGGER);
    }

}
