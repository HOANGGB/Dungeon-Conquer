
using UnityEngine;

public class Vampie : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAttack;
    [SerializeField] int direction = 1;
    [SerializeField]public int directionAttack;

    [SerializeField]public int damge;
    [SerializeField] int speed;
    [SerializeField] bool stateSecond;
    [SerializeField] float coolDownAtack;
    [SerializeField] float attackRange;
    [SerializeField] float attackRange1;

    
    [SerializeField] float distanceCheckObstacle;
    [SerializeField] float timeAttack;
    public bool canHit;
    public bool hitPlayer = false;
    [SerializeField] bool isWalk;
    [SerializeField] bool seeObstacle;
    [SerializeField] bool lockAction = false;


    [SerializeField] LayerMask layerWall;
    [SerializeField] Transform bloodArrowPoint;
    [SerializeField] Transform batPoint;
    [SerializeField] Transform swordBloodPoint;
    [SerializeField] GameObject bloodArrowPrefab;
    [SerializeField] GameObject batPrefab;
    [SerializeField] GameObject swordBlood;



    Animator anim;
    Rigidbody2D rb;
    character player;
    HealEnemy healEnemy;
    SoundEnemy sound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        healEnemy = GetComponent<HealEnemy>();
        player = FindObjectOfType<character>();
        anim = GetComponent<Animator>();
        sound = GetComponentInChildren<SoundEnemy>();
        timeAttack = coolDownAtack;
    }

    // Update is called once per frame
    void Update()
    {
        checkDiretion();
        checkObstacle();
        checkAttack();
        flip();
        runAnim();
        Action();
        timeAttack -= Time.deltaTime;
    }


    void Action(){
        if(healEnemy.healCurrent<=0){
            Deal();
            return;
        }
        if(healEnemy.healCurrent<= (healEnemy.healMax * 0.5) && !stateSecond){
            StateSecond();
            return;
        }
        if(lockAction) return;
        var distanceAttack = Vector3.Distance(transform.position,player.transform.position);
        if(!stateSecond){
            Attack1();
        }else{
            if(distanceAttack > attackRange1 ){
                Follow();
            }else{
                Attack2(1);
            }
             
        }

    }

    void Follow(){
        if(isAttack){
            isWalk = false;
            return;
        }
        isWalk = true;
        isAttack = false;
        rb.velocity = new Vector2(direction*speed,rb.velocity.y);
        if(timeAttack <=0){
            anim.SetBool("isAttack3",true);
            directionAttack = direction;
            timeAttack = coolDownAtack;
        }
        // transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }
    void Attack1(){
        isWalk = false;
        if(timeAttack <=0){
            timeAttack = coolDownAtack;
            int rd = UnityEngine.Random.Range(1,10);
            var distanceAttack = Vector3.Distance(transform.position,player.transform.position);

            if(distanceAttack <attackRange1){
                anim.SetBool("isAttack3",true);
            }
            else if(rd == 1){
                anim.SetBool("isAttack2",true);
                sound.PlaySound("attack2");
            }else if(rd < 5){
                anim.SetBool("isAttack3",true);
            }
            else{
                anim.SetBool("isAttack1",true);
                sound.PlaySound("attack1");
                directionAttack = direction;
                Quaternion rota = Quaternion.Euler(new Vector3(0,0,0));
                if(directionAttack == 1) rota = Quaternion.Euler(new Vector3(0,0,180));
                Instantiate(bloodArrowPrefab,bloodArrowPoint.position,rota);
            }
        }
        if(hitPlayer && canHit && isAttack){
            timeAttack = coolDownAtack;
            player.takeDamge(damge,transform.position.x);
        }                  
    }
    void Attack2(int distance){
        rb.velocity = Vector2.zero;
        isWalk = false;
        if(timeAttack <=0){
            timeAttack = coolDownAtack;
            if(distance == 1){
                int rd = UnityEngine.Random.Range(1,3);
                if(rd == 1) anim.SetBool("isAttack1",true);
                else anim.SetBool("isAttack2",true);
            }else{
                anim.SetBool("isAttack3",true);
            }
        }
        if(hitPlayer && canHit && isAttack){
            timeAttack = coolDownAtack;
            player.takeDamge(damge,transform.position.x);
        }                  
    }
    void checkAttack(){
        // if(anim.GetBool("isAttack")) rb.velocity = Vector2.zero;
        var distanceAttack = Vector2.Distance(transform.position,player.transform.position);
        if(timeAttack<=0 && distanceAttack > attackRange){
            anim.SetBool("isAttack1",false);
            anim.SetBool("isAttack2",false);
        }
        //  if(timeAttack<=0 && distanceAttack > attackRange){
        //     Debug.Log("StopAtack");

        //     isAttack = false;
        // }
    }
    public void StartAttack1(){
        rb.velocity = Vector2.zero;
        
    }

    public void StartAttack2(){
        rb.velocity = Vector2.zero;
        for(int i = 1;i<=3;i++){
            Instantiate(batPrefab,batPoint.position,Quaternion.identity);
        }
    }
    public void StartAttack3(){
        if(seeObstacle) rb.velocity = new Vector2(direction * speed * 20,rb.velocity.y);
        else rb.velocity = new Vector2(-direction * speed * 20,rb.velocity.y);
    }
    public void EndAttack3(){
        rb.velocity = Vector2.zero;
        timeAttack = 0;
    }
    public void StartAttack1_State2(){
        rb.velocity = Vector2.zero;
    }
    public void StartAttack2_State2(){
        rb.velocity = Vector2.zero;
    }
    public void StartAttack3_State2(){
        rb.velocity = Vector2.zero;
         Quaternion rota = Quaternion.Euler(new Vector3(0,0,0));
        if(directionAttack == 1) rota = Quaternion.Euler(new Vector3(0,0,180));
        Instantiate(swordBlood,swordBloodPoint.position,rota);
    }

     void flip(){
        if(isAttack){
            return;
        }
        if(direction ==-1) transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        else if(direction == 1) transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
    }
     void checkDiretion(){
        direction = player.transform.position.x >= transform.position.x?1:-1;
    }
    void runAnim(){
        anim.SetBool("isWalk",isWalk);
        anim.SetBool("isAttack1",isAttack);
        anim.SetBool("isAttack2",isAttack);
        anim.SetBool("isAttack3",isAttack);
    }

    void checkObstacle(){
        Vector3 dir = new Vector3(direction,0,0);
        RaycastHit2D checkObs = Physics2D.Raycast(transform.position, -dir, distanceCheckObstacle,layerWall);
        Debug.DrawRay(transform.position, -dir * distanceCheckObstacle, Color.green);

    if(checkObs.collider != null){
        if(checkObs.collider.tag == "wall"){
            seeObstacle = true;
            Debug.DrawRay(transform.position, -dir * distanceCheckObstacle, Color.red);           
        } else {
            seeObstacle = false;
        }
    } else {
        seeObstacle = false;
    }
    }
    void Deal(){
        rb.velocity = new Vector2(0,rb.velocity.y);
        anim.SetBool("deal",true);
        Destroy(gameObject,2.5f);
    }
    void StateSecond(){
        stateSecond = true;
        lockAction = true;
        anim.SetTrigger("StartState2");
    }
    public void StartStateSecond(){
        healEnemy.immunity = true;
    }
    public void EndStateSecond(){
        healEnemy.immunity = false;
        lockAction = false;
        coolDownAtack /=2;
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.DrawWireSphere(transform.position,attackRange1);
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
