
using UnityEngine;

public class Arrow : MonoBehaviour
{    
    Rigidbody2D rb;
    SkeletonBow skeletonBow;
    character player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<character>();
        skeletonBow = FindObjectOfType<SkeletonBow>();
        Destroy(gameObject,5);

    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.collider.CompareTag("Player")){
            if(Mathf.Abs(rb.velocity.x) > 10){
                player.takeDamge(skeletonBow.damge,transform.position.x);
            }
            Destroy(gameObject);
        }
        if(other.collider.CompareTag("ground")) Destroy(gameObject,0.05f);

    }

}
