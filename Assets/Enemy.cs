using Assets;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IComponents
{
    // --- COMPONENTS ----------
    public Rigidbody2D Rigidbody2D { get; private set; }
    public Collider2D Collider2D { get; private set; }
    public Animator Animator { get; private set; }

    // --- TRIGGER ----------
    public bool InAttackRange = false;
    public bool Attacking = false;


    public Transform player;

    private bool IsFacingRight = true;

    // --- ATTACKING -----------
    public GameObject arrowPrefab;
    public Transform shootPoint;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!InAttackRange)
        {
            Animator.Play("Idle");
        }
        else if (InAttackRange)
        {
            StartCoroutine(AttackProcess());
        }
    }

    IEnumerator AttackProcess()
    {
        Animator.Play("Attack");

        yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
    }

    public void FlipTowardPlayer()
    {
        float direction = player.position.x - transform.position.x;

        if (direction > 0 && !IsFacingRight)
        {
            Flip();
        }
        else if (direction < 0 && IsFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
