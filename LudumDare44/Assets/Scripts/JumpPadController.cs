using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadController : MonoBehaviour {

    [SerializeField] private float _boostSpeed;

    private void OnTriggerEnter2D(Collider2D collision) {
        Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb2d != null) {
            Vector3 velocity = rb2d.velocity;
            velocity.y = _boostSpeed;
            rb2d.velocity = velocity;
        }
    }

}
