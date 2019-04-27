using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController2D : MonoBehaviour {

    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _centerOffset;
    [SerializeField] private float _lookAheadFactor;
    //[SerializeField] private float _lookAheadSpeed;
    [SerializeField] private float _movementSpeed;

    private Camera _camera;
    private float _initialZ;

    private void Awake() {
        _camera = gameObject.GetComponent<Camera>();
        _initialZ = transform.position.z;
    }

    private void LateUpdate() {
        Vector3 lookAheadOffset = _target.localScale.x * _lookAheadFactor * Vector3.right;
        Vector3 targetPos = _target.position + _centerOffset;
        Vector3 lookAheadPos = targetPos + lookAheadOffset;

        Vector3 newPos = Vector3.Lerp(transform.position, lookAheadPos, _movementSpeed * Time.fixedDeltaTime);
        newPos.z = _initialZ;
        transform.position = newPos;
    }

}
