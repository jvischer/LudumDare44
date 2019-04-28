using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour {

    private const string DEATH_TRIGGER = "Dead";

    [SerializeField] private Vector3 _damageOffset;

    [Space]

    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    private Animator _animator;
    private bool _isDead;

    private void Awake() {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void TakeDamage(int damage) {
        if (_isDead) {
            return;
        }

        _health -= damage;
        if (_health <= 0) {
            _health = 0;
            _isDead = true;

            _animator.SetTrigger(DEATH_TRIGGER);
        }

        DamageManager.DisplayDamageAt(damage, transform.position + _damageOffset);
    }

}
