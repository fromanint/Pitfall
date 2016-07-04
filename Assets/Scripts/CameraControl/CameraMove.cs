using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour
{

    Transform StartPos; //To move the player to the next screen
    GameObject Player; //Move the player and know where he is

    [SerializeField]
    float offset; //how much the camera will be moving;

    float edgeFront;
    float edgeBack;
    float camHorizontalExtend;

    float no_screen;
    GameVarControl maxScreens;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartPos = transform.FindChild("StartPos");
        no_screen = 1; //At first always will be 3 screens(0,1,2) the middle is two so we are in the middle
        maxScreens = FindObjectOfType<GameVarControl>();
        camHorizontalExtend = GetComponent<Camera>().orthographicSize * Screen.width / Screen.height;
        edgeFront = transform.position.x + camHorizontalExtend;
        edgeBack = transform.position.x - camHorizontalExtend;

    }

    // Update is called once per frame
    void Update()
    {
        //Check if the player and the start position at screen are in the scen
        if (Player && StartPos)
        {
          //check if the player has hit the back edge of the screen if yes change to previous screen
            if (Player.transform.position.x <= edgeBack)
            {
            
                no_screen--;
                if (no_screen <= maxScreens.firstBg)
                {
                    no_screen = maxScreens.firstBg + maxScreens.countBg;
                    ChangeScreen((no_screen - 1) * offset);
                    Debug.Log("First Screen");
                }
                else
                {
                 
                    ChangeScreen((no_screen - 1) * offset);
                }
            }
            //check if the player has hit the front edge of the screen if yes change to next screen
            if (Player.transform.position.x >= edgeFront)
            {
                Debug.Log("Player>=edgeFront");
                no_screen++;
                if (no_screen > (maxScreens.countBg + maxScreens.firstBg))
                {
                    no_screen = maxScreens.firstBg;
                    ChangeScreen((no_screen) * offset);
                }
                else
                {
                    ChangeScreen((no_screen - 1) * offset);
                }

            }

        }
        else { enabled = false; }
    }
    //Change the screen when the player hit the edge
    void ChangeScreen(float distance)
    {
        Vector3 newpos = new Vector3(distance, transform.position.y, transform.position.z);
        transform.position = newpos;

        edgeFront = distance + camHorizontalExtend;
        edgeBack = distance - camHorizontalExtend;


        newpos = new Vector3(StartPos.position.x, StartPos.position.y, 0);
        Player.transform.position = newpos;

    }
}