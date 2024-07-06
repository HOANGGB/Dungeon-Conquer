
using UnityEngine;

public class SwordBlood : MonoBehaviour
{
    [SerializeField] float speed = 20;
   Rigidbody2D rb;
    Vampie vampie;
    character player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<character>();
        vampie = FindObjectOfType<Vampie>();
        Destroy(gameObject,5);
        rb.velocity = new Vector2(vampie.directionAttack * speed,rb.velocity.y);

    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(Mathf.Abs(rb.velocity.x) > 10){
                player.takeDamge(vampie.damge,transform.position.x);
            }
            Destroy(gameObject,0.05f);
        }
        if(other.tag == "ground" || other.tag == "skillChar") Destroy(gameObject,0.05f);
    }
}
