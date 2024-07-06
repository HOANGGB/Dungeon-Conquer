
using System;
using System.Collections;
using UnityEngine;

public class SnakeMan : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAttack;
    [SerializeField]public int direction = 1;

    [SerializeField] public int damge;
    [SerializeField] int speed;
    [SerializeField] float coolDownAtack;
    [SerializeField] float attackRange;
    [SerializeField] float attackRange1;

    
    [SerializeField] float distanceCheckObstacle;
    [SerializeField] float timeAttack;
    [SerializeField]int timeVenom;
    public bool canHit;
    [SerializeField] bool checkVenom;

    public bool hitPlayer = false;
    [SerializeField] bool isWalk;
    [SerializeField] bool isDeath = false;
    [SerializeField] bool seeObstacle;


    [SerializeField] LayerMask layerWall;
    [SerializeField] GameObject lance;
    [SerializeField] Transform lancePoint;
    [SerializeField] Transform skillGameOject;


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
        sound = GetComponentInChildren<SoundEnemy>();
        anim = GetComponent<Animator>();
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
        var distanceAttack = Vector2.Distance(transform.position,player.transform.position);
        if(seeObstacle && distanceAttack<=attackRange){
            Attack();
        }else if(seeObstacle){
            ComeBack();
        }else if( distanceAttack > attackRange){
            Follow();
        }else if(distanceAttack <= attackRange){
            Attack();
        }
    }
    
    void Follow(){
        if(isAttack){
            return;
        }
        isWalk = true;
        isAttack = false;
        rb.velocity = new Vector2(direction*speed,rb.velocity.y);
        if(timeAttack <= 0){

            int rd = UnityEngine.Random.Range(1,3);
            if(rd == 1){
                anim.SetBool("isAttack2",true);
                sound.PlaySoundDelay("attack2",.55f);
                timeAttack = coolDownAtack*2;
            }else{
                anim.SetTrigger("Attack4");
                timeAttack = coolDownAtack*2;
            }
        }
        // Debug.Log("walkl");
        // transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }
    void Attack(){
        isWalk = false;
        if(timeAttack <=0){
            timeAttack = coolDownAtack;
            int rd = UnityEngine.Random.Range(1,3);
            if(rd == 1){
                anim.SetBool("isAttack3",true);
                sound.PlaySound("attack3");
            }else{
                anim.SetBool("isAttack1",true);
                sound.PlaySoundDelay("attack1",.25f);
            }
        }
                      
    }
    void checkAttack(){
        if(hitPlayer && canHit && isAttack){
            timeAttack = coolDownAtack;
            player.takeDamge(damge,transform.position.x);
            if(checkVenom) StartCoroutine(EffectVenom());
        }    
        var distanceAttack = Vector2.Distance(transform.position,player.transform.position);
        if(timeAttack<=0 && distanceAttack > attackRange1){
            anim.SetBool("isAttack1",false);
            anim.SetBool("isAttack2",false);
            anim.SetBool("isAttack3",false);
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
        Quaternion rota = Quaternion.Euler(new Vector3(0,0,0));
        if(direction == 1) rota = Quaternion.Euler(new Vector3(0,0,180));
        Instantiate(lance,lancePoint.position,rota);
        
    }
    public void StartAttack3(){
        rb.velocity = new Vector2(direction * speed * 5,rb.velocity.y);
    }
    public void StartAttack4(){
        Vector2 skillPoint = new Vector2(player.transform.position.x,-20);
        Instantiate(skillGameOject,skillPoint,Quaternion.identity);
    }
    void ComeBack(){
        isWalk = false;
    }

    IEnumerator EffectVenom(){
        for(int i=0;i<timeVenom ; i++){
            player.takeDamge(damge/3,transform.position.x);
            yield return new WaitForSeconds(1f);
        }
    }
     void flip(){
        if(isAttack){
            return;
        }
        if(direction ==-1) transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        else if(direction == 1) transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
    }
     void checkDiretion(){
        if(isAttack) return;
        direction = player.transform.position.x >= transform.position.x?1:-1;
    }
    void runAnim(){
        anim.SetBool("isWalk",isWalk);
        // anim.SetFloat("timeAttack",timeAttack);
        anim.SetBool("isAttack1",isAttack);
        anim.SetBool("isAttack2",isAttack);
        anim.SetBool("isAttack3",isAttack);
    }
    void checkObstacle(){
        Vector3 dir = new Vector3(direction,0,0);
        RaycastHit2D checkObs = Physics2D.Raycast(transform.position, dir, distanceCheckObstacle,layerWall);
        Debug.DrawRay(transform.position, dir * distanceCheckObstacle, Color.green);

    if(checkObs.collider != null){
        if(checkObs.collider.tag == "ground"){
            seeObstacle = true;
            Debug.DrawRay(transform.position, dir * distanceCheckObstacle, Color.red);           
        } else {
            seeObstacle = false;
        }
    } else {
        seeObstacle = false;
    }
    }
    void Deal(){
        PlayerPrefs.SetInt(Data.UnlockUlti,1);
        anim.SetBool("deal",true);
        if(!isDeath) sound.PlaySound("death");
        isDeath = true;
        Destroy(gameObject,1.5f);
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
