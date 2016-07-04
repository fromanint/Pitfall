using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    [SerializeField]
    Text txtScore;

    float score;
	// Use this for initialization
	void Start () {
        score = 0;
        if (txtScore)
        {
            txtScore.text = score.ToString();
        }
        else { Debug.Log("No score text box"); }


	}

    IEnumerator OnTriggerEnter2D(Collider2D other) {
        yield return new WaitForSeconds(4);
        Debug.Log(score);
        if (other.gameObject.tag == "Player")
        {
            score++;
            txtScore.text = score.ToString();
        }
        
    }

}
