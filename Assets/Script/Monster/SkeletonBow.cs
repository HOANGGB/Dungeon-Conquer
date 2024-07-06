
using UnityEngine;

public class SkeletonBow : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float moveArea;
    [SerializeField] int direction = 1;
    public int damge;
    [SerializeField] float vision;
    [SerializeField] float coolDownAtack;
    [SerializeField] float attackRange;
    [SerializeField] float distanceCheckObstacle;
    [SerializeField] float speedArrow;


    float timeAttack;
    public bool isAttack;
    [SerializeField] bool isMoving;
    [SerializeField] bool seePlayer;
    [SerializeField] bool seeObstacle;
    [SerializeField] Transform arrowPos;
    [SerializeField] GameObject arrow;
    character player;
    [SerializeField] LayerMask layer;
    Vector3 targetPos;
    [SerializeField] GameObject soundDeath;


    Rigidbody2D rb;
    Animator anim;
    HealEnemy healEnemy;
    public bool canHit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healEnemy = GetComponent<HealEnemy>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<character>();
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
        timeAttack -= Time.deltaTime;
    }
      void Deal(){
        Instantiate(soundDeath,transform.position,Quaternion.identity);
        Destroy(transform.parent.gameObject);
    }
    void moveToTarget(){
        if(isAttack){
            rb.velocity = Vector2.zero;
            return;
        }
        if(healEnemy.healCurrent <=0){
            Deal();
            return;
        }

        if(seeObstacle || transform.position.x > transform.parent.position.x+moveArea || transform.position.x < transform.parent.position.x-moveArea){
            // Debug.Log("VE");
                float rdPosX = UnityEngine.Random.Range(transform.parent.position.x+moveArea,transform.parent.position.x-moveArea);
                targetPos = new Vector2(rdPosX,transform.position.y);
            isMoving = true;
            isAttack = false;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if(Vector2.Distance(rb.position,targetPos)<0.5f){
                isMoving = false;

            }  
        }
        else if(!seePlayer){
            if(!isMoving){
                float rdPosX = UnityEngine.Random.Range(transform.parent.position.x+moveArea,transform.parent.position.x-moveArea);
                targetPos = new Vector2(rdPosX,transform.position.y);
            }
            isMoving = true;
            isAttack = false;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if(Vector2.Distance(rb.position,targetPos)<0.5f){
                isMoving = false;

            }   
        }else if(seePlayer){

            if(Vector2.Distance(transform.position,player.transform.position) > attackRange){
                // Debug.Log("FOOLOW");

                if(!isMoving){
                    float rdPosX = UnityEngine.Random.Range(player.transform.position.x + 2,player.transform.position.x - 2);
                    targetPos = new Vector2(rdPosX,transform.position.y);
                }
                isMoving = true;
                isAttack = false;
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                if(Vector2.Distance(rb.position,targetPos)<0.5f){
                    isMoving = false;
                }
            }else{
                isAttack = true;
                isMoving = false;
                rb.velocity = Vector2.zero;
                targetPos = new Vector2(player.transform.position.x,transform.position.y);
                if(timeAttack <=0){
                    Attack();
                }
            }
        }
        
    }
    void Attack(){
        anim.SetTrigger("Attack");
    }
   public void StartAttack(){
        // Debug.Log("FIRE ARROW");
        timeAttack = coolDownAtack;
        Vector2 dir = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        Quaternion rota = Quaternion.Euler(new Vector3(0,0,angle));
        var arrowGameOj = Instantiate(arrow,arrowPos.position,rota);
        arrowGameOj.GetComponent<Rigidbody2D>().velocity = dir * speedArrow;

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
        if(direction ==-1) transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        else if(direction == 1) transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
    }
    void StartAnim(){
        anim.SetBool("isWalk",isMoving);
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Vector3 fromArea = new Vector3(transform.parent.position.x-moveArea,transform.position.y);
        Vector3 toArea = new Vector3(transform.parent.position.x+moveArea,transform.position.y);
        Gizmos.DrawLine(fromArea,toArea);
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,vision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
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
