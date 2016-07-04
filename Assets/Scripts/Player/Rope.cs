using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

    float gravity;
    void Start()
    {
        gravity = FindObjectOfType<PlayerControl>().GetComponent<Rigidbody2D>().gravityScale;
    }

   

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player")
        {
            Vector3 scale = other.transform.localScale;
            other.gameObject.transform.parent = transform;
            other.gameObject.transform.position = transform.FindChild("PlayerAttach").transform.position;
            other.gameObject.transform.rotation = transform.FindChild("PlayerAttach").transform.rotation;
            other.gameObject.GetComponent<PlayerControl>().isAttach = true;
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.transform.localScale = scale;
           
            
            
            gameObject.layer = LayerMask.NameToLayer("PassPlayer");

            

        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"),false);
            other.gameObject.GetComponent<PlayerControl>().climb = false;
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
            Debug.Log(gravity);
        }      

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"));
            other.gameObject.GetComponent<PlayerControl>().climb = true;
            other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }
	
}
