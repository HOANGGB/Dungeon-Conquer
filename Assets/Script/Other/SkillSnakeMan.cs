
using UnityEngine;

public class SkillSnakeMan : MonoBehaviour
{
    // Start is called before the first frame update character player;
    character player;
    float coolDownTime;

    [SerializeField] int damge;
    void Start()
    {
        player = FindObjectOfType<character>();
        Destroy(gameObject,1f);
        coolDownTime = 0;
        
    }

    private void Update() {
        coolDownTime -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player" && coolDownTime<=0){
            player.takeDamge(damge,transform.position.x);
            coolDownTime = 1;
        }
    }
}
