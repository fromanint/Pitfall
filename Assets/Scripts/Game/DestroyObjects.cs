using UnityEngine;
using System.Collections;

public class DestroyObjects : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
        //Destroy all the enemy bombs when they arrive
        //this is to avoid we have a lot of bombs just running throug all the level and waist memory
        
        if (other.gameObject.tag=="Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
