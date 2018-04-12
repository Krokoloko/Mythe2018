using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public enum EnemyState { none, idle, scouting, moving, attack, dead };
    public enum EnemyAirState { none, midair, falling, landing };

    public Rigidbody rb;
    public SpriteRenderer spriteRend;
    public Collider2D collision;
    public Animator animator;
    public EnemyState State;
    public EnemyAirState AirState;


    public virtual void Damage()
    {
        health--;
    }

    public virtual void Death()
    {
        if (health <= 0)
        {
            animator.SetTrigger("death");
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {

    }

    public virtual bool RandomBool()
    {
        float sug = Random.Range(0, 1);
        if (sug <= 0.5f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
