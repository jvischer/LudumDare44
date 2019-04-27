using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UpperBodyController : MonoBehaviour {

    private const string PUNCH_TRIGGER = "Punch";

    private Animator _animator;
    private bool _isMidAttack;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void ThrowPunch() {
        if (_isMidAttack) {
            return;
        }

        _isMidAttack = true;
        _animator.SetTrigger(PUNCH_TRIGGER);
    }

    public void DamagePunchZone() {
        Debug.Log("Damaging punch zone");
    }

    public void CancelAttack() {
        _isMidAttack = false;
    }

}
