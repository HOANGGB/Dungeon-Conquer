
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject skillHit;
    [SerializeField] GameObject hitEffect;
    Rigidbody2D rb;
    Animator ani;
    character player;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        player = FindObjectOfType<character>();
        Destroy(gameObject,5);


    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy") || other.CompareTag("ground") || other.CompareTag("Boss") || other.CompareTag ("swordBlood")){
            rb.velocity = Vector2.zero;
            Instantiate(hitEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);

            if(other.CompareTag("Enemy")){
                var skillLV = PlayerPrefs.GetInt(Data.SkillLevel);
                other.GetComponent<HealEnemy>().takeDamge(player.damge/3*skillLV+(player.damge*skillLV / 3),transform.position.x,10);
            }else if(other.CompareTag("Boss")){
                var skillLV = PlayerPrefs.GetInt(Data.SkillLevel);
                other.transform.parent.GetComponent<HealEnemy>().takeDamge(player.damge/3*skillLV+(player.damge*skillLV / 3),transform.position.x,10);
            }
        }
        if(other.CompareTag("Enemy") || other.CompareTag("Boss")){
            float randomRotationZ = Random.Range(0f, 180f);
            Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomRotationZ);
            Instantiate(skillHit,other.transform.position,randomRotation);
        }
    }
}
