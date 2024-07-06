
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    character player;
    void Start()
    {
        player = FindObjectOfType<character>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Enemy"){
            player.canHit = true;
            if(player.allEnemyCanHit.Count <2){
                if(!player.allEnemyCanHit.Contains(other.gameObject)){
                    player.allEnemyCanHit.Add(other.gameObject);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Enemy"){
            player.canHit = false;
            if(player.allEnemyCanHit.Contains(other.gameObject)){
                player.allEnemyCanHit.Remove(other.gameObject);
            }           
        }
    }
}
