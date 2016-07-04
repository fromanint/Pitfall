using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CharacterMovement))]
public class OldPlayerControl : MonoBehaviour {

    

    [SerializeField]
    float dieDelay;


    public float maxHealth;
    public int maxLifes;
    [HideInInspector]
    public float health;
    [HideInInspector]
    public int lifes;
    [HideInInspector]
    public bool dead;

    CharacterMovement CM;

    float gravity;
    bool jump;
    bool climb;
    LifesManager LM;
	// Use this for initialization
	void Start () {
        CM = GetComponent<CharacterMovement>();
        LM = FindObjectOfType(typeof(LifesManager)) as LifesManager;
        health = maxHealth;
        lifes = maxLifes;
        climb = false;
        dead = false;
        gravity = GetComponent<Rigidbody2D>().gravityScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
     
        float x = Input.GetAxis("Horizontal");
        jump = Input.GetKeyDown("space");
        if (!climb)
        {
            CM.Move(x, jump);
        }
        else {
            float y = Input.GetAxis("Vertical");
            CM.Climb(x, y, jump);
        }
    }

    IEnumerator OnTriggerExit2D(Collider2D other) {

        if (other.gameObject.tag == "Rope")
        {
            climb = false;
            GetComponent<Rigidbody2D>().gravityScale = gravity;
            Debug.Log("norope");
        }
        yield return new WaitForSeconds(dieDelay);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"),false);
           

    }




    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Rope")
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"));
            Debug.Log("rope");
            climb = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            
        }
            if (other.gameObject.tag == "Hole")
        {


            EnemyControl ec = other.gameObject.GetComponent<EnemyControl>();
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"));
            if (ec)
            {
                ManageLife(ec.damage, ec.kill);
            }
            else { Debug.Log(other.gameObject.name + " :missing EnemyControl"); }



        } 

        
    }


    IEnumerator Die()
    {
        Camera cam = Camera.main;
         
          yield return new WaitForSeconds(dieDelay);
          if(cam)
          {
              Transform StartPos = cam.transform.FindChild("StartPos");
              if (StartPos)
              {
                  transform.position = new Vector3(StartPos.position.x, StartPos.position.y, 0);
                  LM.UpdateLifes();
                  LM.UpdateHealth();
                  dead = false;
              }
              else { Debug.Log("startpos not found"); }

          }

          Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), false);
    }
    public void ManageLife(float damage,bool kill)
    {
        if(LM)
        { 
             if (kill) {
                  health = 0;
               }
        
            if (health > 0)
            {

                health -= damage;
                LM.UpdateHealth();
            
            }
            else {
              
                
               if (lifes > 1)
                {
                    
                    lifes--;
                    health = maxHealth;
                    StartCoroutine("Die");

                }
                else {
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), false);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }

            }
        }
        else { Debug.Log("No life Manager UI"); }
    }

        
        

    
}
