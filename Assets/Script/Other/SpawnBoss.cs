
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] GameObject Boss;
    [SerializeField] Collider2D CloseDoor;
    [SerializeField] Collider2D CloseDoor1;
    string IdEnemy;

    // Start is called before the first frame update
    void Start(){
        IdEnemy = "Enemy"+Boss.transform.position.ToString();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag =="Player"){
            if(Boss == null){
                CloseDoor.isTrigger = true;
                CloseDoor1.isTrigger = true;
                return;
            }
            else{
                Boss.SetActive(true);
                CloseDoor.isTrigger = false;
                CloseDoor1.isTrigger = false;
            }
        }
    }

}
