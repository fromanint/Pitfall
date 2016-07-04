using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

    //more than an enemy Rope is just and obstacle
    float gravity;
    void Start()
    {
        gravity = FindObjectOfType<PlayerControl>().GetComponent<Rigidbody2D>().gravityScale;
    }

   

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            if(!other.gameObject.GetComponent<PlayerControl>().dead)
            {
                //when the player touches the Rope then chage it to pass player to avoid anoter contact  but we still need player
                //attach to rope so parent him to the rope util he jumps

                 other.gameObject.transform.parent = transform;
                 other.gameObject.transform.position = transform.FindChild("PlayerAttach").transform.position;
                 other.gameObject.transform.rotation = transform.FindChild("PlayerAttach").transform.rotation;
                 other.gameObject.GetComponent<PlayerControl>().isAttach = true;
                 other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                 other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                 gameObject.layer = LayerMask.NameToLayer("PassPlayer");

            }


          

            

        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            //when the player stops climbing we need to reset things like gravity and the ignored mask make them collide again.
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"),false);
            other.gameObject.GetComponent<PlayerControl>().climb = false;
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
       
        }      

    }
    //This basically works with the stairs and will set the player to climb
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            //if the player is in the stairs then start climbing if player hit Vertical.
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"));
            other.gameObject.GetComponent<PlayerControl>().climb = true;
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
	
}
