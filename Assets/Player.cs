using Assets;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IComponents
{
    // --- COMPONENTS ----------
    public Rigidbody2D Rigidbody2D {  get; private set; }
    public Collider2D Collider2D { get; private set; }
    public Animator Animator { get; private set; }

    // --- LAYER MASK ----------
    public LayerMask ground;

    // --- MOVEMENT VARIABLES ----------
    public float jumpForce = 14f;
    public float moveSpeed = 10f;

    // --- INPUT AXIS RAW -----------
    private float posX, posY;

    // --- FLIPPING VARIABLES ----------
    private bool IsFacingRight = true;
    private bool flipped = true;
    private Vector3 flip;

    private bool Attacking = false;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovementInput(out posX, out posY);
        Rigidbody2D.linearVelocity = new(posX * moveSpeed, Rigidbody2D.linearVelocity.y);

        if (!Attacking)
        {
            if (IsGrounded())
            {
                string animation = posX == 0 ? "Idle" : "Run";
                Animator.Play(animation);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Rigidbody2D.linearVelocity = new(Rigidbody2D.linearVelocity.x, jumpForce);
                }

                if (Input.GetKeyDown(KeyCode.K))
                {
                    Attacking = true;
                }
            }
            if (!IsGrounded())
            {
                Animator.Play("Jump");
            }
        }
        else
        {
            StartCoroutine(AttackProcess());
        }
    }

    IEnumerator AttackProcess()
    {
        Animator.Play("Attack");

        yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        Attacking = false;
    }

    private void FixedUpdate()
    {
        FlipModifier();
    }

    #region Input Method
    private void MovementInput(out float horizontal, out float vertical)
    {
        horizontal = !flipped ? 0f : Input.GetAxisRaw("Horizontal");
        vertical = !flipped ? 0f : Input.GetAxisRaw("Vertical");
    }

    private void CheckFacing()
    {
        if (!flipped) return;

        IsFacingRight = !IsFacingRight;

        flip = transform.localScale;
        flip.x = -1;

        float yRotation = flip.x > 0 ? 0f : 180f;
        transform.Rotate(0, yRotation, 0);
    }

    private void FlipModifier()
    {
        if (posX > 0 && !IsFacingRight)
        {
            CheckFacing();
        }
        else if (posX < 0 && IsFacingRight)
        {
            CheckFacing();
        }
    }
    #endregion

    #region Condition Method
    public bool IsGrounded()
    {
        return Physics2D.BoxCast(Collider2D.bounds.center, Collider2D.bounds.size, 0f, Vector2.down, .1f, ground);
    }
    #endregion
}
