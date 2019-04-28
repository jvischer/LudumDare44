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
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < spriteRenderers.Length; i++) {
            GameObject child = spriteRenderers[i].gameObject;
            PolygonCollider2D poly2d = child.GetComponent<PolygonCollider2D>();
            Rigidbody2D rb2d = child.AddComponent<Rigidbody2D>();

            poly2d.enabled = true;
        }
        _animator.enabled = false;
        //_animator.SetTrigger(DEATH_TRIGGER);
    }

}
