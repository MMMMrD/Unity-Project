using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderBird : Bullet
{
    bool canMove;
    public override void InitEnvironment(Vector2 point, Transform userTransform, float targetDiration, CharacterStats attacker)
    {
        base.InitEnvironment(point, userTransform, targetDiration, attacker);
        transform.localScale = new Vector3(diraction, transform.localScale.y, transform.localScale.z);
    }
    public override void Movement()
    {
        if(canMove)
        {
            rb.velocity = new Vector2(currentSpeed * diraction, rb.velocity.y);
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.player.characterStats.TakeDamage(characterStats);
        }

        if(other.CompareTag("Ground"))
        {
            currentSpeed = 0;
            anim.SetTrigger("Attack");
        }

        if(other.CompareTag("ThunderShield"))
        {
            
            diraction = -diraction;
            transform.localScale = new Vector3(diraction, transform.localScale.y, transform.localScale.z);
        }
    }

    //Animation Event
    public void SetMoveBool()
    {
        canMove = true;
    }

    public override void Disappear()
    {
        base.Disappear();
        canMove = false;
    }
}
