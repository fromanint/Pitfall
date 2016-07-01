using UnityEngine;
using System.Collections;


[RequireComponent (typeof(EnemyControl))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bomb : MonoBehaviour {

    [SerializeField] float speed;

    Rigidbody2D RB;
    EnemyControl EC;

    float edgeBack;
    float camHorizontalExtend;
    // Use this for initialization
    void Start () {
        RB = GetComponent<Rigidbody2D>();
        RB.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);
        EC = GetComponent<EnemyControl>();

        camHorizontalExtend = Camera.main.GetComponent<Camera>().orthographicSize * Screen.width / Screen.height;
        
        edgeBack = transform.position.x - camHorizontalExtend;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerControl>().ManageLife(EC.damage, EC.kill);
            Debug.Log("DamagePlayer");
            gameObject.layer = LayerMask.NameToLayer("PassPlayer");
        }
    }	

}
