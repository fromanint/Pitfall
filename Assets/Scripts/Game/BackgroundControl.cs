using UnityEngine;
using System.Collections;


public class BackgroundControl : MonoBehaviour {

    public bool mirror = false; //Used to mirror the background 
    [HideInInspector] public bool hasnext=false, hasprevious=false; // Check if there is a next screen and a previous
   


    [SerializeField] int maxScreens; //Set the maximum of screens
    [SerializeField] float camOffset; //Set if camera is near the end of level

    GameVarControl GameControl;
    float next; // help us to move new or previous screen in their position
    Camera cam; 
    
    


	void Start()
	{
        //Set the first elements we will going to use
        cam = Camera.main;
        GameControl = FindObjectOfType(typeof(GameVarControl)) as GameVarControl; // will helps to control the number of screens
        next = GetComponent<SpriteRenderer>().bounds.size.x /2; //check the width of the sprite.
       
      
    }
    void Update()
    {
        //if already have a next and a previous Background, then disable the script so doesn't have lot of updates running
        if (hasnext && hasprevious) 
        {
            enabled = false;
        }
        //find if we have found a the a camera so we can continue with the script

            if (cam)
            { 
            //if we have a camera and doesn't have a next BackGround create a new screen
                if (!hasnext)
                {
                    float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;
                    float edgeVisible = (transform.position.x + next) - camHorizontalExtend;
                    if (cam.transform.position.x >= edgeVisible - camOffset)
                    {
                        BackgroundControl newbg = MakeNew(transform.position.x + next * 2);
                  
                        if (newbg)
                        {
                            
                            newbg.hasprevious = true;
                            hasnext = true;
                        }
                    }
                }
            //if we have a camera and doesn't have a previous BackGround create a new screen
            if (!hasprevious)
                {
                    float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;
                    float edgeVisible = (transform.position.x - next) + camHorizontalExtend;
                    if (cam.transform.position.x <= edgeVisible + camOffset)
                    {
                        BackgroundControl newbg = MakeNew(transform.position.x - next * 2);
                     
                        if (newbg)
                        {
                        GameControl.firstBg--;
                        newbg.hasnext = true;
                            hasprevious = true;
                        }
                    }
                }
            }
          
        
      
    }


    //Create a new screen if the number of the screens are less than the maximum of sceens
    //if not disable the script
    BackgroundControl MakeNew(float x) {
        if (GameControl.countBg < maxScreens)
        {
         
            Vector3 newPosition = new Vector3(x, transform.position.y, transform.position.z);
            Transform nextBg = Instantiate(transform, newPosition, transform.rotation) as Transform;
            nextBg.SetParent(transform.parent);
            nextBg.localScale = transform.localScale;
            if (mirror)
            { nextBg.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y); }
            else {
                nextBg.localScale = new Vector3(transform.localScale.x, transform.localScale.y);
            }

            nextBg.GetComponent<BackgroundControl>().mirror = !mirror;
            nextBg.GetComponent<BackgroundControl>().maxScreens = maxScreens;
            GameControl.countBg++;
            return (nextBg.GetComponent<BackgroundControl>());
        }
        else {
            enabled = false;
            return null;
        }


    }
 
      



 }
