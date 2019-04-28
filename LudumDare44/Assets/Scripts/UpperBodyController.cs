using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UpperBodyController : MonoBehaviour {

    private const string PUNCH_TRIGGER = "Punch";
    private const string DEATH_TRIGGER = "Dead";

    [SerializeField] private ParticleSystem _punchFX;
    [SerializeField] private ParticleSystemRenderer _punchRendererFX;

    [Space]

    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private Vector2 _punchZoneOffset;
    [SerializeField] private Vector2 _punchZoneSize;

    private Animator _animator;
    private ParticleSystem.MainModule _mainModule;
    private float _initialPunchSpeed;
    private int _facingDir;
    private bool _isMidAttack;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
        _mainModule = _punchFX.main;
        _initialPunchSpeed = _mainModule.startSpeedMultiplier;
        _facingDir = 1;
    }

    public void ThrowPunch() {
        if (_isMidAttack) {
            return;
        }

        _isMidAttack = true;
        _animator.SetTrigger(PUNCH_TRIGGER);
    }

    public void DamagePunchZone() {
        Vector2 origin = new Vector2(transform.position.x + (_facingDir >= 0 ? _punchZoneOffset.x : -_punchZoneOffset.x), transform.position.y + _punchZoneOffset.y);
        Collider2D[] hits = Physics2D.OverlapBoxAll(origin, _punchZoneSize, 0, _enemyLayerMask);
        for (int i = 0; i < hits.Length; i++) {
            EnemyController enemyController = hits[i].gameObject.GetComponent<EnemyController>();
            if (enemyController != null) {
                enemyController.TakeDamage(GameManager.gameData.GetPlayerDamage());
            }
        }
    }

    //private void OnDrawGizmos() {
    //    Vector2 origin = new Vector2(transform.position.x + _facingDir >= 0 ? _punchZoneOffset.x : -_punchZoneOffset.x, transform.position.y + _punchZoneOffset.y);
    //    Gizmos.DrawCube(origin, _punchZoneSize);
    //}

    public void PlayPunchFX() {
        _punchFX.Play();
    }

    public void CancelAttack() {
        _isMidAttack = false;
    }

    public void SetFacingDir(int facingDir) {
        _facingDir = facingDir == 0 ? _facingDir : facingDir;

        _mainModule.startSpeedMultiplier = facingDir > 0 ? _initialPunchSpeed : facingDir < 0 ? -_initialPunchSpeed : _mainModule.startSpeedMultiplier;

        Vector3 flip = _punchRendererFX.flip;
        flip.x = facingDir > 0 ? 0 : facingDir < 0 ? 1 : _punchRendererFX.flip.x;
        _punchRendererFX.flip = flip;
    }

    public void Died() {
        _animator.SetTrigger(DEATH_TRIGGER);
    }

}
