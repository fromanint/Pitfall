using UnityEngine;
using System.Collections;

public class GameVarControl : MonoBehaviour {

    public int countBg=0; //Controls that there won't be more screens.
    public int firstBg=0; //Controls the index of what is the firstBg.

  // public int[] obstaclesIndex; //Controls the random creation of the obstacles in each screen
    public GameObject[] obstacles; //GameObjects of the obstacles
    public int countObstacle; //how many obstacles we have

    void Awake()
    {
        //obstaclesIndex = new int[obstacles.Length];//Before everything get how many obstacles we have.
        Shuffle();
        countObstacle = 0;
    }

    public void Shuffle() {
        for (var i = obstacles.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            GameObject tmp = obstacles[i];
            obstacles[i] = obstacles[r];
            obstacles[r] = tmp;
        }
    }


}
