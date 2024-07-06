
using UnityEngine;

public class SkillOrge : MonoBehaviour
{
    // Start is called before the first frame update
    character player;
    float coolDownTime;

    [SerializeField] int damge;
    void Start()
    {
        player = FindObjectOfType<character>();
        Destroy(transform.parent.gameObject,2f);
        coolDownTime = 0;
        
    }

    private void Update() {
        coolDownTime -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player"){
            player.takeDamge(damge,transform.position.x);
        }
    }
}
