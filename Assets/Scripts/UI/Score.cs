using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    [SerializeField]
    Text txtScore;

    float score;

    bool haveScore = false;
    // Use this for initialization
	void Start () {

        //We will need to add initialize the score 
        score = 0;
        if (txtScore)
        {
            txtScore.text = score.ToString();
        }
        else { Debug.Log("No score text box"); }


	}

    IEnumerator OnTriggerEnter2D(Collider2D other) {

        Debug.Log(score);
        if (other.gameObject.tag == "Player" && !haveScore)
        {
            haveScore = true;
            score++;
            txtScore.text = score.ToString();
        }
        yield return new WaitForSeconds(4);
        haveScore = false;
        
    }

}
