using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour {

    [SerializeField] float jumpForce; //How high Player is going to jump
    [SerializeField] float speed; // how fast he's going to run
    [SerializeField] float climbSpeed;


    [SerializeField] LayerMask whatIsGround; // A mask determining what is ground to the character

    Transform groundCheck; // A position marking where to check if the player is grounded.
    float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded

    void Start()
    {
        groundCheck = transform.Find("GroundCheck");
    }

    public void Move(float front, bool jump = false)
    {
        if (jump)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * front, jumpForce);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * front, 0);
        }
    }

    public void Climb(float front, float vertical, bool jump = false)
    {
        if (!jump)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * front, vertical * climbSpeed);
           

        }
        else
        {
            if (front ==0)
                { GetComponent<Rigidbody2D>().velocity = new Vector2(speed * front,  jumpForce); }
        }
            
        
    }

    private void FixedUpdate()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
      //  grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
    }

 }
