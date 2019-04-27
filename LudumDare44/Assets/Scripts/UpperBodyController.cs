using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UpperBodyController : MonoBehaviour {

    private const string PUNCH_TRIGGER = "Punch";

    [SerializeField] private ParticleSystem _punchFX;
    [SerializeField] private ParticleSystemRenderer _punchRendererFX;

    private Animator _animator;
    private ParticleSystem.MainModule _mainModule;
    private float _initialPunchSpeed;
    private bool _isMidAttack;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
        _mainModule = _punchFX.main;
        _initialPunchSpeed = _mainModule.startSpeedMultiplier;
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

    public void PlayPunchFX() {
        _punchFX.Play();
    }

    public void CancelAttack() {
        _isMidAttack = false;
    }

    public void SetFacingDir(int facingDir) {
        _mainModule.startSpeedMultiplier = facingDir > 0 ? _initialPunchSpeed : facingDir < 0 ? -_initialPunchSpeed : _mainModule.startSpeedMultiplier;

        Vector3 flip = _punchRendererFX.flip;
        flip.x = facingDir > 0 ? 0 : facingDir < 0 ? 1 : _punchRendererFX.flip.x;
        _punchRendererFX.flip = flip;
    }

}
