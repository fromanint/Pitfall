using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyControl))]
[RequireComponent(typeof(SpriteRenderer))]
public class LandMines : MonoBehaviour {
    //Crocodile closed mouth
    [SerializeField] Sprite Normal; //It's the normal state of the land mine
    [SerializeField] Sprite Press; //shows up when the player is in the land mine
    //Crocodile open mouth
    [SerializeField] Sprite Danger; //shows when player can't jump in it
    [SerializeField] Sprite DangerPress; //if the player is pressing and it chages, well player is dead.
    
    [SerializeField] float change_time;

    bool pressed;

    EnemyControl EC;
    SpriteRenderer SR;
   
	// Use this for initialization
	void Start () {
        pressed = false;

        EC = GetComponent<EnemyControl>();
        SR = GetComponent<SpriteRenderer>();
        StartCoroutine("Change");
    }


    IEnumerator Change()
    {
        yield return new WaitForSeconds(change_time);
        if (!EC.kill)
        {
            EC.kill = true;
            if (pressed)
            {
                SR.sprite = DangerPress;
            }
            else
            {
                SR.sprite = Danger;
            }

        }
        else {
            EC.kill = false;
            if (pressed)
            {
                SR.sprite = Press;
            }
            else
            {
                SR.sprite = Normal;
            }
        }
        StartCoroutine("Change");

    }



    void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Player")
        { pressed = true;
            if(EC.kill)
            { 
                SR.sprite = DangerPress;
                other.gameObject.GetComponent<PlayerControl>().ManageLife(EC.damage, EC.kill);
            }
            else { SR.sprite = Press; }
         
        }

        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        { pressed = false;
            if (EC.kill)
            { SR.sprite = Danger; }
            else { SR.sprite = Normal; }
           
        }
    }

}
