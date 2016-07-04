using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CharacterMovement))]

public class PlayerControl : MonoBehaviour {

    public float maxHealth;
    public int maxLifes;
    public float gravity;

    [HideInInspector] public float health;
    [HideInInspector] public int lifes;
    [HideInInspector] public bool dead;
    [HideInInspector] public bool climb;
    [HideInInspector] public bool isAttach;


    [SerializeField]float dieDelay; //this variable controls all delays we need in courotines at first it was only die.

    CharacterMovement CM;
    Animator Anim;
    LifesManager LM;
    bool jump;


    //At start inizialice all the variables in the script
    void Awake() {
        health = maxHealth; //actual health is going to be maxhealth
        lifes = maxLifes; //actual lifes going to be maxLifes
        dead = false; //At start player won't be dead
        jump = false; //At start player won't be jumping
        climb = false; //At start player won't be climbing
        isAttach = false; //Look if the player is attach to something.
    }

    // Use this for initialization
    //initialization variables that depends on other scripts
    void Start () {
        CM = GetComponent<CharacterMovement>();  //Controls the movement of the player
        Anim = GetComponent<Animator>(); //Controls the animation of dead
        LM = FindObjectOfType(typeof(LifesManager)) as LifesManager; //controls the lifes in the UI.
        GetComponent<Rigidbody2D>().gravityScale = gravity; //Set the gravity of the player so we can reset it when we need to
    }
	
	// Update is called once per frame
	void Update () {
        //just move if player not dead
        if (!dead) { 
        //Check if player is attach to a rope if its attach wait until player hit "space"  to detach 
            if (!isAttach)
             {
            
                 float x = Input.GetAxis("Horizontal");
                 jump = Input.GetKeyDown("space");
                 //Check if the player is climbing a stairs if not player can move arround with the keyboard arrows
                  if (!climb)
                  {
                           Anim.SetBool("Climb", false);
                       Anim.SetFloat("Speed", Mathf.Abs(x));
                        CM.Move(x, jump);
                   }
                 else
                {
                //if its climbing player will move in vertical 
                      float y = Input.GetAxis("Vertical");
                       if (jump)//if player hits jump he will no be climbing anymore so he can fall.
                       {
                          climb = false;
                          GetComponent<Rigidbody2D>().gravityScale = gravity;
                      }
                      CM.Climb(x, y, jump);
                  }
               }
             else {
                   Anim.SetBool("Climb", true);
                   if(Input.GetKeyDown("space"))
                    {   
                        StartCoroutine("Detach");
                }
             }
            
        }


    }
    IEnumerator Detach() {
        // Detaches the transform from its parent.
        //we need to know who the parent is so we can reset the layer and make him collide with the player again after few seconds
        Transform parent = transform.parent; 
        GetComponent<Rigidbody2D>().gravityScale = gravity;
        transform.rotation = Quaternion.identity;
        transform.parent = null; 
        isAttach = false;
        Anim.SetBool("Climb", false);

        yield return new WaitForSeconds(dieDelay);
        if (parent)
        { parent.gameObject.layer = LayerMask.NameToLayer("NoTraspasing"); }
        
    }

    //whis function will manage the life of the player, and we will know if the player is dead if yes then reset player 
    public void ManageLife(float damage, bool kill)
    {
        //Verify that player is not dead
        if (!dead)
        {
            if (LM) {
                if (kill){//Verify that the enemy is a killer of the player if its reset player
                    StartCoroutine("ResetPlayer");
                }
                else
                {
                    //Take the damage
                    //if player still have health update in the UI if not player is dead
                    health -= damage;
                    if (health <= 0)
                    {
                        StartCoroutine("ResetPlayer");
                    }
                    else {
                        LM.UpdateHealth();
                    }
                }

            }
            else{ Debug.Log("No Life Manager UI found"); }
        }
    }

    //Now player is dead and we have to know if he has another chance (life) or is gameOver(Reload Scene)
    IEnumerator ResetPlayer() 
    {
        StartCoroutine("Detach");
            dead = true;
            Anim.SetBool("Dead", true);
                
            
            yield return new WaitForSeconds(dieDelay);
            //if player's last life reset the scene and the ignored layers
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Ground"), false);
            lifes--;
            if (lifes <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else{
                //if player still have a chance get the start pos in the camera and move the player to it
                Camera cam = Camera.main;
            if (cam)
            {
                Transform StartPos = cam.transform.FindChild("StartPos");
                if (StartPos)
                {
                    transform.position = new Vector3(StartPos.position.x, StartPos.position.y, 0);
                    LM.UpdateLifes();
                    LM.UpdateHealth();
                    transform.localScale = new Vector3(.7f, .7f, 1);
                    Anim.SetBool("Dead", false);
                    dead = false;
                }
            }
            else { Debug.Log("startpos not found"); }

            }
    }

    
}
