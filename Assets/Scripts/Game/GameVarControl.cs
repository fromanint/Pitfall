using UnityEngine;
using System.Collections;

public class GameVarControl : MonoBehaviour {

    public int countBg=0; //Controls that there won't be more screens.
    public int firstBg=0; //Controls the index of what is the firstBg.


    public GameObject[] obstacles; //GameObjects of the obstacles
    public int countObstacle; //how many obstacles we have and if we have more screens than obscales will shuffle again

    void Awake()
    {
        
        Shuffle();  //We need to shuffle the obstacles so they don't repeat but never instansiate in the same order
        countObstacle = 0;
    }


    //"this function comes from forum.unity3d.com/threads/randomize-array-in-c.86871/"
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
