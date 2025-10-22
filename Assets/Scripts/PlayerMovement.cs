using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce;
    public float HorizontalDirection;
    private Rigidbody2D rigidBody2D;
    public float speed;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public LayerMask groundLayer;
    public bool isGrounded;
    public float groundCheckRadius;
    public Vector2 groundCheckSize;
    public Vector2 raySize;
    public float rayDir;
    public Vector2 gravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rigidBody2D = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        

    }

    // Update is called once per frame
    void Update()
    {
      

        HorizontalDirection = Input.GetAxisRaw(axisName: "Horizontal");

        animator.SetFloat("movement", Mathf.Abs(HorizontalDirection));

        if (HorizontalDirection > 0)
            transform.localScale = new Vector3(1, 1, 1);

        if (HorizontalDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        rigidBody2D.linearVelocityX = HorizontalDirection * speed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody2D.AddForceY(jumpForce, ForceMode2D.Impulse);

        }

        raySize = transform.TransformDirection(Vector2.down);


        if (Physics2D.Raycast(transform.position, raySize, rayDir, groundLayer))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", isGrounded);
        }
        else
        {
            isGrounded = false;
            animator.SetBool("isGrounded", isGrounded);
        }

        Physics2D.gravity = gravity;
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, raySize);
    }
}

 

       
        
   




