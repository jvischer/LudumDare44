using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(TMPro.TextMeshProUGUI))]
public class DamageTextController : MonoBehaviour {

    private const string POP_TRIGGER = "Pop";

    private Animator _animator;
    private TMPro.TextMeshProUGUI _text;

    private Action<DamageTextController> _onTextReadyForReuse;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
        _text = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void DisplayAt(string text, Vector2 position, Action<DamageTextController> onTextReadyForReuse) {
        _text.text = text;
        transform.position = position;
        _onTextReadyForReuse = onTextReadyForReuse;

        _text.enabled = true;
        _animator.SetTrigger(POP_TRIGGER);
    }
    
    public void OnTextReadyForReuse() {
        _text.enabled = false;

        if (_onTextReadyForReuse != null) {
            _onTextReadyForReuse.Invoke(this);
        }
    }

}
