
using UnityEngine;

public class Lance : MonoBehaviour
{
    [SerializeField] float speed = 20;
    Rigidbody2D rb;
    SnakeMan snakeMan;
    character player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<character>();
        snakeMan = FindObjectOfType<SnakeMan>();
        Destroy(gameObject,5);
        rb.velocity = new Vector2(snakeMan.direction * speed,rb.velocity.y);

    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player" ){
            if(Mathf.Abs(rb.velocity.x) > 10){
                player.takeDamge(snakeMan.damge,transform.position.x);
            }
            Destroy(gameObject,0.05f);
        }

        if(other.tag == "ground") Destroy(gameObject,0.05f);
    }
}
