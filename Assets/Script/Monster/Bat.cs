
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float moveArea;
    [SerializeField] int direction = 1;
    [SerializeField] int damge;
    [SerializeField] float vision;
    [SerializeField] float visionWall;

    [SerializeField] float coolDownAtack;
    [SerializeField] bool isMoving;
    [SerializeField] bool seePlayer;
    [SerializeField] bool seeObstacle;
    [SerializeField] bool isAttack = false;
    character player;
    [SerializeField] LayerMask layer;
    float timeAttack;
    Vector3 targetPos;
    Vector3 targetHit;
    Vector3 point;
    Rigidbody2D rb;
    HealEnemy healEnemy;
    SoundEnemy sound;
    [SerializeField] GameObject effectDeath;
    [SerializeField] GameObject soundDeath;

    Animator anim;
    private bool canHit;

    void Start()
    {
        healEnemy = GetComponent<HealEnemy>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<character>();
        sound = GetComponentInChildren<SoundEnemy>();
        rb = GetComponent<Rigidbody2D>();
        point = transform.position;
        timeAttack = coolDownAtack;
    }

    // Update is called once per frame
    void Update()
    {
        checkDiretion();
        checkVision();
        checkObstacle();
        moveToTarget();
        Attack();
        flip();
        StartAnim();
        timeAttack -= Time.deltaTime;
    }
    void Deal(){
        Instantiate(effectDeath,transform.position,Quaternion.identity);
        Instantiate(soundDeath,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
    void moveToTarget(){

    if(healEnemy.healCurrent <=0){
        Deal();
        return;
    }

        if(seeObstacle){
                float rdPosX = Random.Range(point.x+moveArea,point.x-moveArea);
                targetPos = point;
            isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position,targetPos)<0.5f){
                isMoving = false;
            }
        }
        else if(!seePlayer){
            if(!isMoving){
                float rdPosX = Random.Range(point.x+moveArea,point.x-moveArea);
                targetPos = new Vector2(rdPosX,point.y);
            }
            isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position,targetPos)<0.5f){
                isMoving = false;
            }   
        }else if(seePlayer){
            if(timeAttack<=0){
                isAttack = true;
                targetHit = player.transform.position;
                timeAttack = coolDownAtack;
            }
            else{
                if(!isMoving){
                    float rdPosX = Random.Range(player.transform.position.x + 5,player.transform.position.x - 5);
                    targetPos = point;
                }
                isMoving = true;
                transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                if(Vector2.Distance(transform.position,targetPos)<0.5f){
                    isMoving = false;
                }
            }
        }
    }
   void Attack(){
    if(isAttack){
        anim.SetTrigger("Attack");
        transform.position = Vector2.MoveTowards(transform.position, targetHit, speed * 5 * Time.deltaTime);
        if(Vector2.Distance(transform.position,targetHit)<0.5f){
            isAttack = false;
        }
        if(canHit){
            player.takeDamge(damge,transform.position.x);
            isAttack = false;
        }
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
    // RaycastHit2D checkObs = Physics2D.Raycast(transform.position, (targetPos - transform.position).normalized, distanceCheckObstacle,layer);
    // Debug.DrawRay(transform.position, (targetPos - transform.position).normalized * distanceCheckObstacle, Color.green);
    Collider2D checkObs = Physics2D.OverlapCircle(transform.position,visionWall,layer);
    if(checkObs != null){
        if(checkObs.tag == "ground"){
            seeObstacle = true;
            isAttack = false;
        } else {
            seeObstacle = false;
        }
    } else {
        seeObstacle = false;
    }
}

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position,vision);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,visionWall);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Vector3 fromArea = new Vector3(transform.position.x-moveArea,transform.position.y);
        Vector3 toArea = new Vector3(transform.position.x+moveArea,transform.position.y);
        Gizmos.DrawLine(fromArea,toArea);
    }
    void flip(){
        if(direction ==-1) transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        else if(direction == 1) transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
    }
    void StartAnim(){

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
