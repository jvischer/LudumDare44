using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UiPanelController : MonoBehaviour {

    private const string FADE_IN_TRIGGER = "FadeIn";
    private const string FADE_OUT_TRIGGER = "FadeOut";

    private Animator _animator;
    private bool _isFading;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
        _isFading = false;
    }

    public void FadeIn() {
        if (_isFading) {
            return;
        }

        _isFading = true;
        _animator.SetTrigger(FADE_IN_TRIGGER);
    }

    public void FadeOut() {
        if (_isFading) {
            return;
        }

        _isFading = true;
        _animator.SetTrigger(FADE_OUT_TRIGGER);
    }

    private void OnFinishedFading() {
        _isFading = false;
    }

}
