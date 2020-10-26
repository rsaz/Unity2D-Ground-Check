using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider2D;

    private Vector2 direction;

    public float speed;
    public float jumpForce = 5f;
    public float fallMultiplier = 2.5f;
    //public bool isGrounded = false;
    public LayerMask platformMask;    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        speed = 150f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        Jump();
        UpdateJumpVelocity();
        IsGrounded();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void UpdateDirection()
    {
        float x = Input.GetAxis("Horizontal");
        direction = new Vector2(x, 0f);

        if (x != 0)
        {
            anim.SetBool("walk", true);

            if (x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    private void Move()
    {
        rb.velocity = new Vector3(direction.x * speed * Time.deltaTime, rb.velocity.y);
    }

    private void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space)) && IsGrounded()) //&& isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //isGrounded = false;
        }
    }

    private void UpdateJumpVelocity()
    {
        rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    #region Ground Detection Strategies
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        isGrounded = true;
    //    }
    //}

    //private bool IsGrounded()
    //{
    //    var extraHeigh = .25f;
    //    var hit =  Physics2D.Raycast(capsuleCollider2D.bounds.center, Vector2.down, capsuleCollider2D.bounds.extents.y + extraHeigh, platformMask);
    //    Color rayColor;

    //    print(hit.collider);

    //    if (hit.collider != null)
    //        rayColor = Color.green;
    //    else
    //        rayColor = Color.red;

    //    Debug.DrawRay(capsuleCollider2D.bounds.center, Vector2.down * (capsuleCollider2D.bounds.extents.y + extraHeigh), rayColor);

    //    return hit.collider != null;
    //}

    private bool IsGrounded()
    {
        var extraHeigh = .01f;
        var hit = Physics2D.BoxCast(capsuleCollider2D.bounds.center, capsuleCollider2D.bounds.size,0f,Vector2.down, extraHeigh, platformMask);
        Color rayColor;

        print(hit.collider);

        if (hit.collider != null)
            rayColor = Color.green;
        else
            rayColor = Color.red;

        Debug.DrawRay(capsuleCollider2D.bounds.center + new Vector3(capsuleCollider2D.bounds.extents.x,0), Vector2.down * (capsuleCollider2D.bounds.extents.y + extraHeigh), rayColor);
        Debug.DrawRay(capsuleCollider2D.bounds.center - new Vector3(capsuleCollider2D.bounds.extents.x,0), Vector2.down * (capsuleCollider2D.bounds.extents.y + extraHeigh), rayColor);
        Debug.DrawRay(capsuleCollider2D.bounds.center - new Vector3(capsuleCollider2D.bounds.extents.x, capsuleCollider2D.bounds.extents.y), Vector2.right * (capsuleCollider2D.bounds.extents.x + extraHeigh), rayColor);

        return hit.collider != null;
    }

    #endregion
}
