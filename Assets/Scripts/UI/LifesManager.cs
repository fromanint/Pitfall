using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LifesManager : MonoBehaviour {

    [SerializeField] RectTransform parentLifes;//this is going to be the last life
    [SerializeField] RectTransform lifeIcon;//will show the user the lifes left
    [SerializeField] Image healthBar; //shows health left


    PlayerControl Player;
    [SerializeField]RectTransform[] lifes;
    // Use this for initialization
    void Start () {
        Player = FindObjectOfType(typeof(PlayerControl)) as PlayerControl;
        if (!Player)
        {
            enabled = false;
        }
        else {
            lifes = new RectTransform[Player.maxLifes];
            SpawnLifes(parentLifes, lifes.Length);
        }

	}

    //We are going to instansiate the number of lifes given in player control
    int SpawnLifes(RectTransform Parent, int max)
    {
        //If we reache the number of lifes given to the player then stop  if not we need to instansiate another life.
        if (max != 0)
        {
            RectTransform newLife = Instantiate(lifeIcon); 
            newLife.transform.SetParent(Parent.transform);

//Move the parameters of the new life to look good in the UI
            newLife.localScale = new Vector3(1, 1, 1);
            newLife.localPosition= new Vector3(Parent.rect.width+.4f, 0, 0);

            lifes[max - 1] = newLife;
            max = SpawnLifes(newLife, --max);
            return max;
        }
        else {
            return 0;
        }
    }
    

    //This updates the number of lifes when the player is kill it will be call from player control
    
    public void UpdateLifes() {
        Debug.Log("UpdateLife");
        lifes[Player.maxLifes- Player.lifes-1].gameObject.SetActive(false);
    }
    //This updates the health of player is hitten it will be call from player control
    public void UpdateHealth() {
        Debug.Log("UpdateHealth");
        healthBar.fillAmount = Player.health / Player.maxHealth;
    }
}
