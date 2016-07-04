using UnityEngine;
using System.Collections;

public class SpawnObstacles : MonoBehaviour {




    GameVarControl GC;
    // Use this for initialization
    void Start() {
        GC = FindObjectOfType<GameVarControl>();
        if (GC.countObstacle >= GC.obstacles.Length)
        {
            GC.Shuffle();
            GC.countObstacle = 0;
        }
       
        GameObject newObstacle = Instantiate(GC.obstacles[GC.countObstacle], transform.position, transform.rotation) as GameObject;
        newObstacle.transform.SetParent(transform.parent);
        newObstacle.transform.localScale = new Vector3(1, 1, 1);
        newObstacle.transform.SetParent(GameObject.FindGameObjectWithTag("Obstacles").transform);
        GC.countObstacle++;

    }
	

}
