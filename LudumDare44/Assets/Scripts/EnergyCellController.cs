using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnergyCellController : MonoBehaviour {

    private const int HEALTH_RESTORED = 2;
    private const string FADE_OUT_TRIGGER = "FadeOut";

    private Animator _animator;
    private bool _isConsumed;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
        _isConsumed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (_isConsumed) {
            return;
        }

        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if (playerController != null) {
            _isConsumed = true;

            playerController.PickedUpHealth(HEALTH_RESTORED);
            _animator.SetTrigger(FADE_OUT_TRIGGER);
        }
    }

    private void onConsumedFully() {
        gameObject.SetActive(false);
    }

}
