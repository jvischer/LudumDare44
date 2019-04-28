using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Text))]
public class DamageTextController : MonoBehaviour {

    private const string POP_TRIGGER = "Pop";

    private Animator _animator;
    private Text _text;
    //private TMPro.TextMeshProUGUI _text;

    private Vector3 _worldPos;
    private Action<DamageTextController> _onTextReadyForReuse;
    private bool _isActive;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
        _text = gameObject.GetComponent<Text>();
        _isActive = false;
    }

    private void Update() {
        if (_isActive) {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(_worldPos);
            transform.position = screenPos;
        }
    }

    public void DisplayAt(string text, Vector2 worldPos, Action<DamageTextController> onTextReadyForReuse) {
        Debug.Log("Displaying text " + text + " for " + name + " " + _isActive);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        transform.position = screenPos;

        _text.text = text;
        _worldPos = worldPos;
        _onTextReadyForReuse = onTextReadyForReuse;

        //_text.enabled = true;
        _animator.SetTrigger(POP_TRIGGER);
        _isActive = true;
    }

    public void OnTextReadyForReuse() {
        //_text.enabled = false;
        _isActive = false;

        if (_onTextReadyForReuse != null) {
            _onTextReadyForReuse.Invoke(this);
        }
    }

}
