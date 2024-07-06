using System.Collections;
using UnityEngine;

public class BloodArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public bool isFire = false;
    [SerializeField] Rigidbody2D rb;
    Vampie vampie;
    character player;
    AudioSource au;
    AudioClip playSound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<character>();
        vampie = FindObjectOfType<Vampie>();
        au = GetComponent<AudioSource>();
        StartCoroutine(Scale());
        StartCoroutine(StartFire());
        Destroy(gameObject,5f);
    }


    IEnumerator Scale(){
        while(!isFire){
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator StartFire(){
        yield return new WaitForSeconds(1f);//Time fire
        if(vampie.directionAttack == 1) transform.rotation = Quaternion.Euler(0,180,0);
        rb.velocity = new Vector2(vampie.directionAttack*speed,rb.velocity.y);
        isFire = true;
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(isFire){
                player.takeDamge(vampie.damge,transform.position.x);
                Destroy(gameObject,0.05f);
            }
        }
        if(other.tag == "ground"){
            Destroy(gameObject,0.05f);
        }
    }
}
