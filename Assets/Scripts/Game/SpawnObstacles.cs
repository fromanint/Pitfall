using UnityEngine;
using System.Collections;

public class SpawnObstacles : MonoBehaviour {




    GameVarControl GC;
    // Use this for initialization
    void Start() {
        //Will instantiate the obstacle of a particular screen
        GC = FindObjectOfType<GameVarControl>();
        if (GC.countObstacle >= GC.obstacles.Length) //If we have more screens than obstacles shuffle again
        {
            GC.Shuffle();
            GC.countObstacle = 0;
        }
        //instantiate the obstacle 
        GameObject newObstacle = Instantiate(GC.obstacles[GC.countObstacle], transform.position, transform.rotation) as GameObject;
        //Set the parent a rezize to it (Mid platform)
        newObstacle.transform.SetParent(transform.parent);
        newObstacle.transform.localScale = new Vector3(1, 1, 1);
        //Move the parent to the obstacles area so it will be independiet of the screen
        newObstacle.transform.SetParent(GameObject.FindGameObjectWithTag("Obstacles").transform);
        //Local in z its turning into 0 so to avoid bugs related to it change the local scale in z to 1
        Vector3 scale = newObstacle.transform.localScale;
        scale.z = 1;
        newObstacle.transform.localScale = scale;
        //We have a new obstacle :D yei!! now we add 1 in our obstacle count
        GC.countObstacle++;

    }
	

}
