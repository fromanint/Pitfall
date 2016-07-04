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
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"),false);
        }
    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerControl>().ManageLife(EC.damage, EC.kill);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"));
        }
    }




}
