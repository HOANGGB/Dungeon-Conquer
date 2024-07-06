
using UnityEngine;

public class TaurusDemon : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAttack;
    [SerializeField] int direction = 1;
    [SerializeField] int directionAttack2;

    [SerializeField] int damge;
    [SerializeField] int speed;
    [SerializeField] float coolDownAtack;
    [SerializeField] float attackRange;
    
    [SerializeField] float distanceCheckObstacle;
    [SerializeField] float timeAttack;
    [SerializeField] float timeSkill;
    [SerializeField] float CoolDownSkill;


    public bool canHit;
    public bool hitPlayer = false;
    [SerializeField] bool isWalk;
    [SerializeField] bool isDeath = false;

    [SerializeField] bool seeObstacle;


    [SerializeField] LayerMask layerWall;
    [SerializeField] Transform SpawnSkillGameoject;
    [SerializeField] GameObject SkillGameoject;



    Animator anim;
    Rigidbody2D rb;
    character player;
    HealEnemy healEnemy;
    SoundEnemy sound;
    public CameraShake cameraShake;


    void Start()
    {
        // Debug.Log("MITAU : "+ PlayerPrefs.GetInt("Enemy"+transform.position.ToString()));
        rb = GetComponent<Rigidbody2D>();
        healEnemy = GetComponent<HealEnemy>();
        player = FindObjectOfType<character>();
        anim = GetComponent<Animator>();
        sound = GetComponentInChildren<SoundEnemy>();
        cameraShake = FindObjectOfType<CameraShake>();
        timeAttack = coolDownAtack;

        timeSkill = CoolDownSkill;
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
        timeSkill -=Time.deltaTime;
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
        if(timeSkill<=0){
                isWalk = false;
                rb.velocity  = Vector2.zero;
                sound.PlaySoundDelay("attack1",1f);

                anim.SetTrigger("Attack3");
                timeSkill = CoolDownSkill;
        }
        else{
            if(isAttack){
                return;
            }
            isWalk = true;
            isAttack = false;

            Vector2 playerPos = new Vector2(player.transform.position.x,transform.position.y);
            rb.velocity = new Vector2(direction*speed,rb.velocity.y);
        } 
        
        // transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
    }
    void Attack(){
        isWalk = false;
        if(timeAttack <=0){
            timeAttack = coolDownAtack;
            int rd = UnityEngine.Random.Range(1,4);
            if(rd == 1){
                directionAttack2 = direction;
                sound.PlaySound("attack2");
                anim.SetTrigger("Attack2");
            }else{
                anim.SetTrigger("Attack1");
                sound.PlaySoundDelay("attack1",.2f);
            }
        }
        if(hitPlayer && canHit && isAttack){
            timeAttack = coolDownAtack;
            player.takeDamge(damge,transform.position.x);
        }
    }
    void Skill(){

    }
    void checkAttack(){
        // if(anim.GetBool("isAttack")) rb.velocity = Vector2.zero;
        var distanceAttack = Vector2.Distance(transform.position,player.transform.position);
        if(timeAttack<=0 && distanceAttack > attackRange){

        }
        //  if(timeAttack<=0 && distanceAttack > attackRange){
        //     Debug.Log("StopAtack");

        //     isAttack = false;
        // }
    }
    public void StartAttack1(){
        rb.velocity = Vector2.zero;
    }
    public void ShakeAttack(){
        cameraShake.ShakeCamera(5,0.05f);
    }
    public void StartAttack2(){
        rb.velocity = new Vector2(direction * speed * 5,rb.velocity.y);
        damge *=2;
    }
    public void EndAttack2(){
       damge /=2;
    }
    public void StartAttack3(){
        cameraShake.ShakeCamera(5,0.05f);
        Quaternion rota = Quaternion.Euler(new Vector3(0,0,0));
        if(direction == 1) rota = Quaternion.Euler(new Vector3(0,180,0));
        Instantiate(SkillGameoject,SpawnSkillGameoject.position,rota);
    }
    public void EndAttack3(){
       
    }
    
    void ComeBack(){
        isWalk = false;
    }
    void ResetCooldown(){
        int rd = Random.Range(1,6);
        if(rd == 1){
            timeAttack = 0;
        }
    }

     void flip(){
        if(isAttack || isDeath){
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
        anim.SetFloat("timeAttack",timeAttack);
        anim.SetBool("isAttack",isAttack);
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
        PlayerPrefs.SetInt(Data.UnlockSkill,1);
        rb.velocity = new Vector2(0,rb.velocity.y);
        
        if(!isDeath){
            sound.PlaySound("death");
            anim.SetTrigger("dead");
            isDeath = true;
        } 
        Destroy(gameObject,2f);
    }
    private void OnDrawGizmosSelected(){
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
