using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LowerBodyController : MonoBehaviour {

    private Animator _animator;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
    }

}
