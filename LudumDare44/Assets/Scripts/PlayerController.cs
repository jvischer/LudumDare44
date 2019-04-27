using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour {

    [SerializeField] private UpperBodyController _upperBodyController;
    [SerializeField] private LowerBodyController _lowerBodyController;
    [SerializeField] private LayerMask _groundLayerMask;

    [Space]

    [SerializeField] private float _horizontalMovementAcc = 1;
    [SerializeField] private float _maxHorizontalMovementSpeed = 1;
    [SerializeField] private float _horizontalGroundedFriction = 0.75F;
    [SerializeField] private float _horizontalAirborneFriction = 0.9F;
    [SerializeField] private float _upwardGravitationalForceMagnitude = 1;
    [SerializeField] private float _downwardGravitationalForceMagnitude = 1;
    [SerializeField] private float _jumpBoost = 1;

    private RaycastHit2D[] _raycastHit2DCache;
    private Rigidbody2D _rb2d;
    private BoxCollider2D _col2d;
    private float _horizVelocity;
    private bool _isGrounded;
    private bool _hasJumped;

    private void Awake() {
        _raycastHit2DCache = new RaycastHit2D[1];
        _rb2d = gameObject.GetComponent<Rigidbody2D>();
        _col2d = gameObject.GetComponent<BoxCollider2D>();
        _hasJumped = false;
    }

    private void Update() {
        float horizInputAxis = Input.GetAxis("Horizontal");
        float vertInputAxis = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Jump")) {
            _hasJumped = true;
        }
        if (Input.GetButtonDown("RegAttack")) {
            _upperBodyController.ThrowPunch();
        }

        int movingDir = horizInputAxis > 0.01F ? 1 : horizInputAxis < -0.01F ? -1 : 0;
        _upperBodyController.SetFacingDir(movingDir);
        _lowerBodyController.SetMoving(movingDir != 0);

        Vector3 localScale = transform.localScale;
        localScale.x = movingDir == 0 ? localScale.x : movingDir;
        transform.localScale = localScale;

        Vector2 velocity = _rb2d.velocity;
        float horizontalVelocity = velocity.x + _horizontalMovementAcc * horizInputAxis;
        velocity.x = Mathf.Clamp(horizontalVelocity, -_maxHorizontalMovementSpeed, _maxHorizontalMovementSpeed);
        velocity.x *= (_isGrounded ? _horizontalGroundedFriction : _horizontalAirborneFriction);
        _rb2d.velocity = velocity;
    }

    private void FixedUpdate() {
        float distance = 0.15F;
        float width = _col2d.size.x;
        float height = _col2d.size.y;
        Vector3 rayDir = Vector3.down;
        Vector2 bottomOfPlayer = ((Vector2) transform.position) + height / 2 * Vector2.down;
        if (TryHitGround(new Ray(bottomOfPlayer, rayDir), distance)) {
            _isGrounded = true;
        } else if (TryHitGround(new Ray(bottomOfPlayer + width / 2 * Vector2.left, rayDir), distance)) {
            _isGrounded = true;
        } else if (TryHitGround(new Ray(bottomOfPlayer + width / 2 * Vector2.right, rayDir), distance)) {
            _isGrounded = true;
        } else {
            _isGrounded = false;
        }

        _lowerBodyController.SetGrounded(_isGrounded);

        // Consume the jump charge if it has been pressed
        if (_hasJumped) {
            _hasJumped = false;

            // Only jump if currently grounded
            if (_isGrounded) {
                _rb2d.AddForce(_jumpBoost * Vector2.up);
            }
        }

        if (!_isGrounded) {
            float gravitationalForceMagnitude = _rb2d.velocity.y >= 0 ? _upwardGravitationalForceMagnitude : _downwardGravitationalForceMagnitude;
            _rb2d.AddForce(gravitationalForceMagnitude * Vector2.down);
        }
    }

    private bool TryHitGround(Ray ray, float distance) {
        Debug.DrawRay(ray.origin, distance * ray.direction, Color.blue, 0.1F);
        int hits = Physics2D.RaycastNonAlloc(ray.origin, ray.direction, _raycastHit2DCache, distance, _groundLayerMask);
        if (hits > 0) {
            return true;
        }
        return false;
    }

}
