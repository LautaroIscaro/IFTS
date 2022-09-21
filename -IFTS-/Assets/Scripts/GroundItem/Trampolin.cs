using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    public float jumpForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
            //Le quito la fuerza de salto remanente que tuviera
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = (Vector2.up * jumpForce);
            collision.gameObject.GetComponent<playerAnim>().Jump(true);
            Debug.Log(collision.gameObject.name);
    }
}
