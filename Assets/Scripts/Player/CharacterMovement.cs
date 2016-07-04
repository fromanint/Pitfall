using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour {

    [SerializeField] float jumpForce; //How high Player is going to jump
    [SerializeField] float speed; // how fast he's going to run
    [SerializeField] float climbSpeed;


    [SerializeField] LayerMask whatIsGround; // A mask determining what is ground to the character

    Animator anim; //controls animation of the character
    Transform groundCheck; // A position marking where to check if the player is grounded.
    bool grounded;  // Check if the player is in the ground
    float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    bool doubleJump; //Check if the player has double jump

    void Start()
    {
        anim = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
    }

    //this function will control only the movement when the player is in the ground or jumping 
    //I allow double jump to keep some of the obstacles a bit easier
    public void Move(float front, bool jump = false)
    {
       
     
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * front, 0);
       
        if (jump && (grounded || !doubleJump))
        {
            anim.SetTrigger("Jump");
            if (!grounded)
            {
                doubleJump = true;
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce),ForceMode2D.Impulse);
        }

        Flip(front);
    }

    //check where the player is going and flip in that direction.
    void Flip(float x)
    {
       
        if (x <= -.01f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (x >= .01f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    //this funcion will work only when the player is climbing (stairs)
    public void Climb(float front, float vertical, bool jump = false)
    {
        anim.SetBool("Climb", true);
        anim.SetFloat("Speed", Mathf.Abs(vertical));
        if (!jump)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * front, vertical * climbSpeed);
        }
        else
        {
            if (front == 0)
                { GetComponent<Rigidbody2D>().velocity = new Vector2(speed * front,  jumpForce); }
        }
            
        
    }

    private void FixedUpdate()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        if (grounded)
        {
            doubleJump = false;
        }
    }

 }
