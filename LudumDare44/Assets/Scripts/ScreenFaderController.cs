using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ScreenFaderController : MonoBehaviour {

    public static ScreenFaderController instance;

    private const string FADE_OUT_TRIGGER = "FadeOut";

    private Animator _animator;
    private Action _onScreenFaded;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        _animator = gameObject.GetComponent<Animator>();
    }

    public static void FadeOut(Action onScreenFaded) {
        instance._onScreenFaded = onScreenFaded;
        instance._animator.SetTrigger(FADE_OUT_TRIGGER);
    }
    
    private void onScreenFaderFadedOut() {
        if (instance._onScreenFaded != null) {
            instance._onScreenFaded.Invoke();
        }
    }

}
