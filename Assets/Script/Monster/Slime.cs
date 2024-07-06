
using UnityEngine;


public class Slime : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    [SerializeField] float moveArea;
    [SerializeField] int direction = 1;
    [SerializeField] int damge;
    [SerializeField] float vision;
    [SerializeField] float distanceCheckObstacle;

    [SerializeField] bool isMoving;
    [SerializeField] float coolDownAtack;
    [SerializeField] bool seePlayer;
    [SerializeField] bool seeObstacle;
    character player;
    [SerializeField] LayerMask layer;
    float timeAttack;
    Vector3 targetPos;
    [SerializeField] GameObject effectDeath;

    Rigidbody2D rb;
    Animator anim;
    HealEnemy healEnemy;
    private bool canHit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<character>();
        healEnemy = GetComponent<HealEnemy>();
        timeAttack = coolDownAtack;
    }

    // Update is called once per frame
    void Update()
    {
        checkDiretion();
        checkVision();
        checkObstacle();
        moveToTarget();
        flip();
        StartAnim();
        Attack();
        timeAttack -= Time.deltaTime;
    }
    void Deal(){
        Instantiate(effectDeath,transform.position,Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }
        void moveToTarget(){
        if(healEnemy.healCurrent <=0){
            Deal();
            return;
        }

        if(seeObstacle || transform.position.x > transform.parent.position.x+moveArea || transform.position.x < transform.parent.position.x-moveArea){
                float rdPosX = Random.Range(transform.parent.position.x+moveArea,transform.parent.position.x-moveArea);
                targetPos = new Vector2(rdPosX,transform.position.y);
            isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if(Vector2.Distance(rb.position,targetPos)<0.5f){
                isMoving = false;
            } 
        }
        else if(!seePlayer){
            if(!isMoving){
                float rdPosX = Random.Range(transform.parent.position.x+moveArea,transform.parent.position.x-moveArea);
                targetPos = new Vector2(rdPosX,transform.position.y);
            }
            isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if(Vector2.Distance(rb.position,targetPos)<0.5f){
                isMoving = false;
            }   
        }else if(seePlayer){
            if(!isMoving){
                float rdPosX = Random.Range(player.transform.position.x + 2,player.transform.position.x - 2);
                targetPos = new Vector2(rdPosX,transform.position.y);
            }
            isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if(Vector2.Distance(rb.position,targetPos)<0.5f){
                isMoving = false;
            }
        }
        
    }
   void Attack(){
    if(canHit && timeAttack<=0){
        timeAttack = coolDownAtack;
        player.takeDamge(damge,transform.position.x);
    }
   }
   void checkVision(){
        if(Vector2.Distance(transform.position,player.transform.position) < vision){
            seePlayer = true;
        }else{
            seePlayer = false;
        }
   }
    void checkDiretion(){
        direction = targetPos.x >= transform.position.x?1:-1;
    }
   void checkObstacle(){
    RaycastHit2D checkObs = Physics2D.Raycast(transform.position, (targetPos - transform.position).normalized, distanceCheckObstacle,layer);
    Debug.DrawRay(transform.position, (targetPos - transform.position).normalized * distanceCheckObstacle, Color.green);

    if(checkObs.collider != null){
        if(checkObs.collider.tag == "ground"){
            seeObstacle = true;
            Debug.DrawRay(transform.position, (targetPos - transform.position).normalized * distanceCheckObstacle, Color.red);           
        } else {
            seeObstacle = false;
        }
    } else {
        seeObstacle = false;
    }
}

    void flip(){
        if(direction ==-1) transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        else if(direction == 1) transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
    }
    void StartAnim(){
        anim.SetBool("isWalk",isMoving);
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Vector3 fromArea = new Vector3(transform.parent.position.x-moveArea,transform.position.y);
        Vector3 toArea = new Vector3(transform.parent.position.x+moveArea,transform.position.y);
        Gizmos.DrawLine(fromArea,toArea);
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,vision);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player"){
            canHit = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            canHit = false;
        }
    }
}
