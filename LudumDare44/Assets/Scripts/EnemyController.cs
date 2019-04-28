using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class EnemyController : MonoBehaviour {

    private const string ATTACK_TRIGGER = "Attack";
    private const string DEATH_TRIGGER = "Dead";
    private static readonly EnemyActions[] RANDOM_ACTIONS = new EnemyActions[] { EnemyActions.Idle, EnemyActions.MoveLeft, EnemyActions.MoveRight };

    [SerializeField] private Image _healthFillBar;
    [SerializeField] private Vector3 _damageOffset;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private Vector2 _basicAttackOffset;
    [SerializeField] private Vector2 _basicAttackSize;

    [Space]

    [SerializeField] private float _logicUpdateCooldown;
    [SerializeField] private float _playerDetectionRadius;
    [SerializeField] private float _playerAttackRadius;
    [SerializeField] private float _movementSpeed;

    [Space]

    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    private Animator _animator;
    private Rigidbody2D _rb2d;
    private BoxCollider2D _boxCol2d;
    private EnemyActions _currAction;
    private int _initialHealth;
    private bool _isAttacking;
    private bool _isDead;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
        _rb2d = gameObject.GetComponent<Rigidbody2D>();
        _boxCol2d = gameObject.GetComponent<BoxCollider2D>();
        _initialHealth = _health;
        _healthFillBar.fillAmount = 1.0F;
        _currAction = EnemyActions.Idle;
        _isAttacking = false;
        _isDead = false;
    }

    private void Start() {
        StartCoroutine(HandleUpdatingLogic());
    }

    private void Update() {
        if (_isAttacking || _isDead) {
            return;
        }

        Vector2 velocity = _rb2d.velocity;
        switch (_currAction) {
            case EnemyActions.MoveLeft:
                if (!TryMoveLeft(ref velocity)) {
                    UpdateLogic(new HashSet<EnemyActions>() { EnemyActions.MoveLeft });
                    return;
                }
                break;
            case EnemyActions.MoveRight:
                if (!TryMoveRight(ref velocity)) {
                    UpdateLogic(new HashSet<EnemyActions>() { EnemyActions.MoveRight });
                    return;
                }
                break;
            case EnemyActions.AttackPlayer:
                Debug.DrawLine(transform.position, _playerAttackRadius * (PlayerController.instance.transform.position - transform.position).normalized, Color.blue, 0.2F);
                if (PlayerController.instance.isDead) {

                } else if (Vector3.Distance(PlayerController.instance.transform.position, transform.position) <= _playerAttackRadius) {
                    AttackPlayer(PlayerController.instance);
                } else {
                    if (Math.Sign(PlayerController.instance.transform.position.x - transform.position.x) >= 0) {
                        if (!TryMoveRight(ref velocity)) {
                            UpdateLogic(new HashSet<EnemyActions>() { EnemyActions.MoveRight });
                            return;
                        }
                    } else {
                        if (!TryMoveLeft(ref velocity)) {
                            UpdateLogic(new HashSet<EnemyActions>() { EnemyActions.MoveLeft });
                            return;
                        }
                    }
                }
                break;
        }
        _rb2d.velocity = velocity;

        if (_rb2d.velocity.x > 0) {
            Vector3 localScale = transform.localScale;
            localScale.x = -1;
            transform.localScale = localScale;
        } else {
            Vector3 localScale = transform.localScale;
            localScale.x = 1;
            transform.localScale = localScale;
        }
    }

    private IEnumerator HandleUpdatingLogic() {
        WaitForSeconds wfs = new WaitForSeconds(_logicUpdateCooldown);
        while (!_isDead) {
            UpdateLogic(null);
            yield return wfs;
        }
    }

    private bool TryMoveLeft(ref Vector2 velocity) {
        Vector2 origin = transform.position;
        origin += (_boxCol2d.size.x / 2) * Vector2.left;
        origin += (_boxCol2d.size.y / 2) * Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.15F, _groundLayerMask);
        if (hit.collider == null) {
            return false;
        }

        velocity.x = -_movementSpeed;
        return true;
    }

    private bool TryMoveRight(ref Vector2 velocity) {
        Vector2 origin = transform.position;
        origin += (_boxCol2d.size.x / 2) * Vector2.right;
        origin += (_boxCol2d.size.y / 2) * Vector2.down;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, 0.15F, _groundLayerMask);
        if (hit.collider == null) {
            return false;
        }

        velocity.x = _movementSpeed;
        return true;
    }

    private void UpdateLogic(HashSet<EnemyActions> blacklistedActions) {
        if (_isAttacking) {
            // Then do nothing
            return;
        }

        Debug.DrawLine(transform.position, _playerDetectionRadius * (PlayerController.instance.transform.position - transform.position).normalized, Color.red, 0.2F);
        EnemyActions chosenAction = EnemyActions.Invalid;
        while (chosenAction == EnemyActions.Invalid || blacklistedActions.Contains(chosenAction)) {
            if (Vector3.Distance(PlayerController.instance.transform.position, transform.position) <= _playerDetectionRadius) {
                chosenAction = EnemyActions.AttackPlayer;
            } else {
                chosenAction = RANDOM_ACTIONS[UnityEngine.Random.Range(0, RANDOM_ACTIONS.Length)];
            }
        }
        _currAction = chosenAction;
    }

    private void AttackPlayer(PlayerController player) {
        _isAttacking = true;

        _animator.SetTrigger(ATTACK_TRIGGER);
    }

    public void TakeDamage(int damage) {
        if (_isDead) {
            return;
        }

        _health -= damage;
        _healthFillBar.fillAmount = Mathf.Clamp01((float) _health / _initialHealth);
        if (_health <= 0) {
            _health = 0;
            _isDead = true;

            _rb2d.velocity = Vector2.zero;
            _animator.SetTrigger(DEATH_TRIGGER);
        }

        DamageManager.DisplayDamageAt(damage, transform.position + _damageOffset);
    }

    public void TryDealBasicDamage() {
        Vector2 origin = new Vector2(transform.position.x + (transform.localScale.x >= 0 ? _basicAttackOffset.x : -_basicAttackOffset.x), transform.position.y + _basicAttackOffset.y);
        Collider2D[] hits = Physics2D.OverlapBoxAll(origin, _basicAttackSize, 0, _playerLayerMask);
        TryDealDamageToPlayer(_damage, hits);
    }

    private void TryDealDamageToPlayer(int damage, Collider2D[] hits) {
        for (int i = 0; i < hits.Length; i++) {
            PlayerController playerController = hits[i].gameObject.GetComponent<PlayerController>();
            if (playerController != null) {
                playerController.TakeDamage(GameManager.gameData.GetPlayerDamage());
            }
        }
    }

    private void onAttackEnded() {
        _isAttacking = false;
    }

    private enum EnemyActions {
        Invalid, AttackPlayer, Idle, MoveLeft, MoveRight,
    }

}
