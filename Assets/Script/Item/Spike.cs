
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int damage;
    [SerializeField] float coolDown;
    [SerializeField] bool canDamaged;
    float time;
    character player;


    void Start()
    {
        player = FindObjectOfType<character>();
        time = coolDown ;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player" && time<=0 && canDamaged){
            player.takeDamge(damage,transform.position.x);
            time = coolDown;
        }
    }
}

