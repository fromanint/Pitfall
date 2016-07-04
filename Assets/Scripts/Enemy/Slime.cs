using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(EnemyControl))]

public class Slime : MonoBehaviour {
    
    // Use this for initialization
  
    float direction = -1;
    CharacterMovement CM;
    EnemyControl EC;
    void Start() {
        EC = GetComponent<EnemyControl>();
        CM = GetComponent<CharacterMovement>();
    }
	// Update is called once per frame
	void Update () {
        //it will always be moving.
        CM.Move(direction);
	}


    
    void OnCollisionEnter2D(Collision2D other) {
        //change direction when hit a wall
        if (other.gameObject.tag == "Wall")
        {
            direction *= -1;
        }
        //make damage to the player when hit  1 time
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerControl>().ManageLife(EC.damage, EC.kill);

        }
    }

  
}
