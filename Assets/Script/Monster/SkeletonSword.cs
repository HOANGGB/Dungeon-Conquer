
using TMPro;
using UnityEngine;


public class SkeletonSword : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float moveArea;
    [SerializeField] int direction = 1;
    [SerializeField] int damge;
    [SerializeField] float vision;
    [SerializeField] float distanceCheckObstacle;
    [SerializeField] float attackRange;


    [SerializeField] bool isMoving;
    [SerializeField] float coolDownAtack;

    [SerializeField] float timeHit = 1;
    [SerializeField] bool seePlayer;
    [SerializeField] bool seeObstacle;
    [SerializeField] bool isAttack;
    [SerializeField] bool canHit;
    [SerializeField] bool hit;
    [SerializeField] bool isBosser = false;




    character player;
    [SerializeField] LayerMask layer;
    float timeAttack;
    Vector3 targetPos;
    [SerializeField] GameObject soundDeath;
    [SerializeField] TextMeshProUGUI nameEnemy;

    Rigidbody2D rb;
    Animator anim;
    HealEnemy healEnemy;
    SoundEnemy sound;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<character>();
        sound = GetComponentInChildren<SoundEnemy>();
        healEnemy = GetComponent<HealEnemy>();
        timeAttack = coolDownAtack;
        if (isBosser)
        {
            nameEnemy.color = Color.red;
            nameEnemy.fontStyle = FontStyles.Bold;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkDiretion();
        checkVision();
        checkObstacle();
        CheckAttack();
        moveToTarget();
        flip();
        StartAnim();
        timeAttack -= Time.deltaTime;
        timeHit -= Time.deltaTime;
    }
    void Deal(){
        Instantiate(soundDeath,transform.position,Quaternion.identity);

        Destroy(transform.parent.gameObject);
    }
    void moveToTarget(){

        if(healEnemy.healCurrent<=0){
            Deal();
            return;         
        }
        if(isAttack){
            // rb.velocity = Vector2.zero;
            return;
        } 

        if(seeObstacle || transform.position.x > transform.parent.position.x+moveArea || transform.position.x < transform.parent.position.x-moveArea){
            // Debug.Log("VE");
                float rdPosX = UnityEngine.Random.Range(transform.parent.position.x+moveArea,transform.parent.position.x-moveArea);
                targetPos = new Vector2(rdPosX,transform.position.y);
            isMoving = true;
            isAttack = false;
            // transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            rb.velocity = new Vector2(direction * speed,rb.velocity.y);
            if(Vector2.Distance(rb.position,targetPos)<0.5f){
                isMoving = false;
            }
        }
        else if(!seePlayer){
            // Debug.Log("di VE");
            if(!isMoving){
                float rdPosX = UnityEngine.Random.Range(transform.parent.position.x+moveArea,transform.parent.position.x-moveArea);
                targetPos = new Vector2(rdPosX,transform.position.y);
            }
            isMoving = true;
            isAttack = false;
            // transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            rb.velocity = new Vector2(direction * speed,rb.velocity.y);
            if(Vector2.Distance(rb.position,targetPos)<0.5f){
                isMoving = false;
            }   
        }else if(seePlayer && player.transform.position.x < transform.parent.position.x+moveArea && player.transform.position.x > transform.parent.position.x-moveArea){
            targetPos = new Vector2(player.transform.position.x,transform.position.y);
            if(player.transform.position.x >= transform.parent.position.x - moveArea &&
             player.transform.position.x <= transform.parent.position.x +  moveArea &&
             Vector2.Distance(transform.position,player.transform.position) > attackRange
             && timeHit <=0  && isBosser){
                    if(timeAttack<=0){
                    isMoving = false;
                    anim.SetTrigger("attack3");
                    timeAttack = coolDownAtack;
                }

            }
            else if(Vector2.Distance(transform.position,player.transform.position)>attackRange){
                // Debug.Log("FOOLOW");

                if(!isMoving){
                    float rdPosX = UnityEngine.Random.Range(player.transform.position.x ,player.transform.position.x );
                    targetPos = new Vector2(rdPosX,transform.position.y);
                }
                isMoving = true;
                isAttack = false;
                // transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                rb.velocity = new Vector2(direction * speed,rb.velocity.y);
                if(Vector2.Distance(rb.position,targetPos)<0.5f){
                    isMoving = false;
                }

            }else if(Vector2.Distance(transform.position,player.transform.position)<= attackRange){
                if(timeAttack<=0){
                    isMoving = false;
                    Attack();
                }
                // else if(timeAttack>=0){
                //     Debug.Log("WAITING FOR COOLDOWN");
                //     if(!isMoving){
                //         float rdPosX = UnityEngine.Random.Range(player.transform.position.x + 5,player.transform.position.x - 5);
                //         targetPos = new Vector2(rdPosX,transform.position.y);
                //     }
                //     isMoving = true;
                //     // transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                //     rb.velocity = new Vector2(direction * speed,rb.velocity.y);
                //     if(Vector2.Distance(rb.position,targetPos)<0.5f){
                //         isMoving = false;
                //     }
                // }
            }
        }
        
    }
   void Attack(){
    int rd = Random.Range(1,4);
    if(rd == 1){
        anim.SetTrigger("attack1");
        // sound.PlaySound("attack2");
        timeAttack = coolDownAtack;

    }else {
        anim.SetTrigger("attack");
        // sound.PlaySound("attack1");
        timeAttack = coolDownAtack;
    }
    // if(canHit){
    //     player.takeDamge(damge,transform.position.x);
    // }
    
    
   }
   public void CheckAttack(){
    if(isAttack && canHit && hit && timeHit <=0){
        player.takeDamge(damge,transform.position.x);
        timeHit = 1;
    }
   }
   public void StartAttack2(){
        damge *= 2;
   }
   public void EndAttack2(){
        damge /= 2;
   }
    public void StartAttack3(){
        damge *= 2;
        rb.velocity = new Vector2(direction * speed* 15,rb.velocity.y);
    }
   public void EndAttack3(){
        rb.velocity = Vector2.zero;
        damge /= 2;
        timeHit = 1;
   }

   public void StopMove(){
        rb.velocity = Vector2.zero;
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
    //check seeObstacle
    RaycastHit2D checkObs = Physics2D.Raycast(transform.position, (targetPos - transform.position).normalized, distanceCheckObstacle,layer);
    Debug.DrawRay(transform.position, (targetPos - transform.position).normalized * distanceCheckObstacle, Color.red);

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
        if(isAttack) return;
        if(direction ==-1) transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        else if(direction == 1) transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
    }
    void StartAnim(){
        anim.SetBool("isWalk",isMoving);
        anim.SetBool("isAttack",isAttack);

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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player"){
            hit = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            hit = false;
        }
    }
}
