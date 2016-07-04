using UnityEngine;
using System.Collections;


[RequireComponent (typeof(EnemyControl))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bomb : MonoBehaviour {

    [SerializeField] float speed;

    Rigidbody2D RB;
    EnemyControl EC;

    // Use this for initialization
    void Start () {
        //add an force in -speed so it looks that someone is throwing them to the player
        RB = GetComponent<Rigidbody2D>();
        RB.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
        EC = GetComponent<EnemyControl>();

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            //if the player has been hit the bomb needs to continue so change the layer to passplayer and update the health of player
            other.gameObject.GetComponent<PlayerControl>().ManageLife(EC.damage, EC.kill);
            Debug.Log("DamagePlayer");
            gameObject.layer = LayerMask.NameToLayer("PassPlayer");
        }
    }	

}
