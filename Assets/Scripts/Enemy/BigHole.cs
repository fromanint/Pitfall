using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyControl))]
public class BigHole : MonoBehaviour {

    EnemyControl EC;
    // Use this for initialization
    void Start() {
        EC = GetComponent<EnemyControl>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //if player is no longer in the hole stop ignoring the layers
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"),false);
        }
    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //if player falls then update the health or if this hole kills then kill him 
            //ignore the collision of the layer with ground so he can pass through the hole.
            other.GetComponent<PlayerControl>().ManageLife(EC.damage, EC.kill);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"));
        }
    }




}
