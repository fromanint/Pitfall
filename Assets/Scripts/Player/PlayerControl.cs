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


    [SerializeField]float dieDelay;

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
        GetComponent<Rigidbody2D>().gravityScale = gravity;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isAttach)
        {
            float x = Input.GetAxis("Horizontal");
            jump = Input.GetKeyDown("space");
            if (!climb)
            {
                CM.Move(x, jump);
            }
            else
            {
                float y = Input.GetAxis("Vertical");
                if (jump)
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
    IEnumerator Detach() {
        // Detaches the transform from its parent.
        
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
            if (lifes <0)
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
