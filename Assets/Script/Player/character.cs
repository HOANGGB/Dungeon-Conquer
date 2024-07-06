using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class character : MonoBehaviour
{

    [Header("==============  PARAMETERS  =========")]
    public List<GameObject> allEnemyCanHit = new List<GameObject>();

    public int HealCharMax; 
    public int HealCharCurrent;
    [SerializeField] int manaCharMax;
    public int manaCharCurrent;
    
    float dashTimeCout;
    float injuredTime;
    float recoveryTime;
    float attackTime;
    public float skillTime;
    public float ultiTime;
    [SerializeField]float dashTime;
    [SerializeField]float jumpTime;

    float resetAttackTime  = 1f;

    [SerializeField] public int damge = 10;
    [SerializeField] int jumpCout = 2;
    [SerializeField] int attackCout = 0;

    [SerializeField] float speed = 10f;
    [SerializeField] float highJump = 10f;
    [SerializeField] float injuredCooldown;
    [SerializeField] float attackCoolDown;
    [SerializeField] float dashCooldown;
    [SerializeField] float jumpCoolDown;

    public float skillCooldown;
    public float ultiCooldown;

    [SerializeField] float recoveryManaCooldown;
    [SerializeField] public float horizontal;
    public int direction = 1;
    [SerializeField] float fallingSpeed = 5;
    [SerializeField] float dashingPower = 10f;
    [SerializeField] int manaToDash;
    public int manaSkill;


    [SerializeField] float hightDamage;
    private int damgeInitial;
    private float attackCoolDownInitial;
    //[SerializeField] float bouncingForceHurts = 5000f;






    [Space]
    [Header("===============  STATUS  ===============")]
    [SerializeField] bool isRight = true;
    [SerializeField] bool isGround = true;
    [SerializeField] bool canMove = true;
    [SerializeField] bool isJumping = false;
    [SerializeField] bool isDashing = false;
    [SerializeField]public bool canDasing = true;
    [SerializeField] bool isAttack = false;
    public bool isUseSkill = false;
    private bool isUseUlti;
    bool moveAttackTF;

    [SerializeField] bool isWounded = false;
    [SerializeField] bool canWounded = true;
    [SerializeField] bool HitEnemy;
    [SerializeField] bool isDeath = false;

    // [SerializeField] bool canUseUlti = true;
    

    public bool canHit;
    public bool PointerDash;
    public bool PointerAttack;
    public bool PointerUlti;
    public bool PointerJump;

    




    [Space]
    [Header("================  COMPONENT ================")]

    Rigidbody2D rb;
    
    public Animator ani;

    public TrailRenderer trailRen;
    public HealBar healBar;
    public CameraShake cameraShake;
    SoundChar sound;
    private DirectionSkill directionSkill;
    private Vector3 savedPositionHome;
    public GameObject ShowDamage;
    public Transform groundCheck;
    public Transform skillTransform;

    public GameObject skill;
    [SerializeField] GameObject doubleJumpGameoject;
    [SerializeField] GameObject UIRevive;



    public TextMeshPro ShowTextDamage;
    private SpriteRenderer spr;
    private Material materialDefault;
    public Material noCritDamage;
    public Material critDamage;
    public LayerMask layerGround;
    public ParticleSystem pa;



    void Start()
    {
        LoadPositon();
        HealCharCurrent = HealCharMax;
        manaCharCurrent =  manaCharMax;
        healBar.UpdateHealBar(HealCharCurrent,HealCharMax);
        healBar.UpdateManaBar(manaCharCurrent,manaCharMax);
        injuredTime = injuredCooldown;
        attackTime = attackCoolDown;
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        directionSkill = FindObjectOfType<DirectionSkill>();
        cameraShake = FindObjectOfType<CameraShake>();

        sound = GetComponentInChildren<SoundChar>();
        materialDefault = GetComponent<SpriteRenderer>().material;


    }

    // Update is called once per frame
    void Update()
    {        
        actionChar();
        UpdateAnimator();
        flip();
        FlipUseSkill();
        coolDownAll();
        CheckAttack();
        // CheckEnemyDied()
        MoveAttack();
        RecoveryMana1();
        DrawCheckGround();

    }
    public void LoadPositon(){
        Vector3 pos = new Vector3(PlayerPrefs.GetFloat(Data.TeleportX)
                                ,PlayerPrefs.GetFloat(Data.TeleportY)
                                ,PlayerPrefs.GetFloat(Data.TeleportZ));
        transform.position = pos;
        if(transform.position.x ==0 && transform.position.y == 0 ) transform.position = new Vector2(30,3);
    }

    public void actionChar(){
        if(HealCharCurrent <=0){
            rb.velocity = new Vector2(0,rb.velocity.y);
            return;
        }
        if(isDashing || isUseUlti){
        }
        if (isJumping && rb.velocity.y < 0.5) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallingSpeed - 1) * Time.deltaTime;
        }
        if(canMove){
           Move();
        } 
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || PointerJump){
            PointerJump = false;
            Jump();
        }
       
        if(Input.GetKeyDown(KeyCode.LeftShift) || PointerDash){
            PointerDash = false;
            Dash();
        }
        //  if(Input.GetKeyDown(KeyCode.J)){
        //     PointerAttack = false;
        //     Attack();
        // }
        // if(Input.GetKeyDown(KeyCode.K)){
        //     Skill();
        // }
        // if(Input.GetKeyDown(KeyCode.L)){
        //     Ulti();
        // }

        if(PointerAttack){
            PointerAttack = false;
            Attack();
        }
        if(PointerUlti){
            Ulti();
        }
         if(Input.GetKeyDown(KeyCode.P)){
            Data.SetDefaultValues();
        }


        
        

        //  if(Input.GetKeyDown(KeyCode.L)){
        //     if(attackTime <=0) {
        //         ani.SetTrigger("attack3");
        //         attackTime = attackCoolDown;
        //     }
        // } if(Input.GetKeyDown(KeyCode.K)){
        //     ani.ResetTrigger("attack3");
        // }
    }

    void flip(){
        if(isAttack || isUseSkill || isDashing) return;
        if(isRight && horizontal <= -0.1){
            transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
            isRight = false;
            direction = -1;
        }else if(!isRight && horizontal >= 0.1){
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            isRight = true;
            direction = 1;
        }
    }
    void FlipUseSkill(){
        if(isAttack || !isUseSkill) return;
        // Debug.Log("FlipUseSkill");
        if( directionSkill.dirSkill.x < 0 && isUseSkill){
            transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
            isRight = false;
            direction = -1;
        }else if(directionSkill.dirSkill.x > 0  && isUseSkill){
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            isRight = true;
            direction = 1;
        }
    }
    
    void Move(){
        if(isDashing || isUseUlti) return;
        if(isUseSkill){
            rb.velocity = new Vector2(0,rb.velocity.y);
            return;
        }
        // horizontal = Input.GetAxisRaw("Horizontal");
        // if(isAttack) rb.velocity = new Vector2(direction * 10,rb.velocity.y);
        // else rb.velocity = new Vector2(speed * horizontal,rb.velocity.y);

        if(isAttack && isGround) rb.velocity = new Vector2(speed/3 * horizontal,rb.velocity.y);
        else rb.velocity = new Vector2(speed * horizontal,rb.velocity.y);
    }
    public void Jump(){
        if(isDashing || isUseSkill || isAttack || isUseUlti) return;
        if( jumpCout >=1 && jumpTime <=0){
            jumpTime = jumpCoolDown;
            if(jumpCout == 1) sound.PlaySound("dbJump");
            StartCoroutine(WaitingJumpCout());
            rb.velocity = new Vector2(rb.velocity.x , highJump);
            // isJumping = true;
        }
    }
    IEnumerator WaitingJumpCout(){
        yield return new WaitForSeconds(.1f);
        jumpCout--;
    }
    public void Dash(){
        StartCoroutine(DashWait());
    }
    IEnumerator DashWait(){
        if(manaCharCurrent > manaToDash && dashTimeCout <= 0 && isGround && !isAttack && !isUseSkill && !isDashing && !isUseUlti){
            canWounded = false;
            isDashing = true;
            ConsumesMana(manaToDash);
            rb.velocity = new Vector2(dashingPower * direction,rb.velocity.y); 
            sound.PlaySound("dash");
            yield return new WaitForSeconds(dashTime);
            isDashing = false;
            canWounded = true;
            dashTimeCout = dashCooldown;
            

        }
    }

    public void Attack(){
        if(attackTime <=0){
            if(isGround){
                attackCout = attackCout == 4 ? 0 : attackCout;
                string setAttack = "attack";
                if(resetAttackTime >=0){
                    attackCout ++;
                    string resultAnim = setAttack + attackCout.ToString();
                    sound.PlaySound(resultAnim);
                    ani.SetTrigger(resultAnim);
                    resetAttackTime = 1f;
                    attackTime = attackCoolDown;
                    // Debug.Log(resultAnim);
                }else{
                    attackCout = 1;
                    sound.PlaySound("attack1");
                    ani.SetTrigger("attack1");
                    resetAttackTime = 1.5f;
                    attackTime = attackCoolDown;
                }
            }else if(!isGround){
                if(rb.velocity.y >0){
                    ani.SetTrigger("jumpAttack1");
                    sound.PlaySound("attack1");
                    attackTime = attackCoolDown;
                }else{
                    isAttack = true;
                    ani.SetTrigger("jumpAttack2");
                    sound.PlaySound("attack2");
                    attackTime = attackCoolDown;
                }
            }
            
        }
    }

    private void CheckAttack(){
            if(canHit && HitEnemy){

                allEnemyCanHit.RemoveAll(enemy => enemy == null);

                for(var i=0;i<allEnemyCanHit.Count;i++){
                    var attackLV = PlayerPrefs.GetInt(Data.AttackLevel);
                    //  if(all.tag == "item" && all != null && all.activeSelf){
                    //     if(all.GetComponent<Item>().hp>0)  all.GetComponent<Item>().takeDamge(damge * attackLV);
                    // }
                    if(allEnemyCanHit[i].tag == "Enemy"){
                        if(allEnemyCanHit[i] == null || allEnemyCanHit[i].GetComponent<HealEnemy>().isDeal) continue;
                        allEnemyCanHit[i].GetComponent<HealEnemy>().takeDamge(damge/4 * attackLV,transform.position.x,5);
                        cameraShake.ShakeCamera(2,0.05f);
                        
                    }else if(allEnemyCanHit[i].tag == "Boss"){
                            if(allEnemyCanHit[i] == null || allEnemyCanHit[i].GetComponentInParent<HealEnemy>().isDeal) continue;
                            allEnemyCanHit[i].transform.parent.GetComponent<HealEnemy>().takeDamge(damge/4 * attackLV,transform.position.x,5);
                            cameraShake.ShakeCamera(2,0.05f);
                    }

                }
            }
        
    }
    public void StartMoveAttack(){
       moveAttackTF = true;
    }
    void MoveAttack(){
        if(moveAttackTF)
        rb.velocity = new Vector2(direction * 20,rb.velocity.y);
    }
    public void endAttack3(){
        moveAttackTF = false;
    }
    public void endAttack(){
        rb.velocity = Vector2.zero;
        ani.SetBool("isAttack",false);
    }
    
    public void DoubleJump(){
        Instantiate(doubleJumpGameoject,groundCheck.transform.position,Quaternion.identity);
    }


    void Skill(){
         if(manaCharCurrent >=manaSkill && skillTime<=0){
            ani.SetTrigger("isUseSkill");   
         }
    }
    public void StartSkill(){
        if(directionSkill == null){
            directionSkill = FindObjectOfType<DirectionSkill>();
        }
        if(manaCharCurrent >=manaSkill && skillTime<=0){
            manaCharCurrent -= manaSkill;
            skillTime = skillCooldown;
            sound.PlaySound("skill");
            float angle = Mathf.Atan2(directionSkill.dirSkill.y,directionSkill.dirSkill.x) * Mathf.Rad2Deg;
            Quaternion rota = Quaternion.Euler(new Vector3(0,0,angle));

            var skillGJ =  Instantiate(skill,skillTransform.position,rota);
            float speedFireBall = skill.GetComponent<FireBall>().speed;
            skillGJ.GetComponent<Rigidbody2D>().velocity = directionSkill.dirSkill * speedFireBall;
            skill.GetComponent<FireBall>().transform.rotation = Quaternion.Euler(0,0,0);
        }

        // skillGJ.GetComponent<Rigidbody2D>().velocity = new Vector3(speedFireBall * direction ,transform.position.y);
    }
    public void EndSkill(){
        isUseSkill = false;
    }

    public void Ulti(){
        if(ultiTime<=0 && isGround){
            ultiTime = ultiCooldown;
            // Debug.Log("isUseUlti");
            spr.color = Color.red;
            isUseUlti = true;
            rb.velocity = Vector2.zero;
            // ani.SetFloat("SpeedAnim",2);
            // attackCoolDownInitial = attackCoolDown;
            damgeInitial = damge;
            sound.PlaySound("ulti");
            ani.SetTrigger("isUseUlti");
            StartCoroutine(StartUlti());
        }
    }
     IEnumerator StartUlti(){
        // attackCoolDown -= 0.3f;
        damge =(int)(damge * (PlayerPrefs.GetInt(Data.UltiLevel)*0.1 +1));
        // Debug.Log("damage : "+(PlayerPrefs.GetInt(Data.UltiLevel)*0.1 +1));
        yield return new WaitForSeconds(10);
        // ani.SetFloat("SpeedAnim",1);
        // attackCoolDown = attackCoolDownInitial;
        spr.color = Color.white;
        damge = damgeInitial;
        StartCoroutine(CoolDownUlti());
    }
    IEnumerator CoolDownUlti(){
        yield return new WaitForSeconds(ultiCooldown);
        
    }
    public void EndUlti(){
        isUseUlti = false;
    }

    public void ShowUIRevive(){
        UIRevive.SetActive(true);
        rb.velocity = Vector2.zero;

    }
    public void Revive(){
        PlayerPrefs.SetFloat(Data.TeleportX,12.34f);
        PlayerPrefs.SetFloat(Data.TeleportY,3.5f);
        PlayerPrefs.SetFloat(Data.TeleportZ,0f);
        PlayerPrefs.SetString(Data.TeleportMap,"Home");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Home");
    }




    void RecoveryMana1(){
        if(manaCharCurrent == manaCharMax) return;
        if(recoveryTime<=0){
            manaCharCurrent++;
            healBar.UpdateManaBar(manaCharCurrent,manaCharMax);
            recoveryTime = recoveryManaCooldown;
        }
    }


    public void takeDamge(int damge,float xPos){
        if(canWounded && !isDeath){
            if(injuredTime <= 0){
               
                    // if(transform.position.x < xPos){
                    //     rb.AddForce(new Vector3(bouncingForceHurts * 10* (-1),bouncingForceHurts/5,bouncingForceHurts));
                    // }else{
                    //     rb.AddForce(new Vector3(bouncingForceHurts * 10* (1),bouncingForceHurts/5,bouncingForceHurts));
                    // }
                    pa.Play();
                    sound.PlaySound("hit");
                    injuredTime = injuredCooldown;
                    DamagePopUp(damge);
                    if(HealCharCurrent <= 0){
                            ani.SetTrigger("died");
                            isDeath = true;
                            // Debug.Log("died");
                    }
                
            }
        }
        healBar.UpdateHealBar(HealCharCurrent,HealCharMax);
    }
    void DamagePopUp(int damge){
        Vector2 posDamageRd = new Vector2(
            UnityEngine.Random.Range(transform.position.x+2f,transform.position.x-2f),
            UnityEngine.Random.Range(transform.position.y+3f,transform.position.y+4)
        );
        int damgeRd = UnityEngine.Random.Range(damge-1,damge+2);
        // ShowTextDamage.text = damgeRd.ToString();
        // Instantiate(ShowDamage,posDamageRd,Quaternion.identity);
        HealCharCurrent -= damgeRd;
        StartCoroutine(FlashDamage(damgeRd));

    }
    IEnumerator FlashDamage(int damge){
        isWounded = true;
        if(damge > this.damge){
            spr.material = critDamage;
        }else{
            spr.material = noCritDamage;
        }
        yield return new WaitForSeconds(0.1f);
        isWounded = false;
        spr.material = materialDefault;
    }


    private void ConsumesMana(int mana){
        manaCharCurrent -= mana;
        healBar.UpdateManaBar(manaCharCurrent,manaCharMax);
    }


    void DrawCheckGround(){
    Collider2D checkObs = Physics2D.OverlapCircle(groundCheck.position,.2f,layerGround);
    if(checkObs != null){
        if(checkObs.tag == "ground"){
            isGround = true;
            isJumping = false;
            jumpCout = 2;
            // Debug.Log("ISJUMPING FALSE");
        } else {
            isGround = false;
            // isJumping = true;
        }
    } else {
        isJumping = true;
        isGround = false;
    }
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position,.2f);
    }
    private void coolDownAll(){
        dashTimeCout -= Time.deltaTime;
        injuredTime -= Time.deltaTime;
        attackTime -= Time.deltaTime;
        skillTime -=Time.deltaTime;
        ultiTime -= Time.deltaTime;
        recoveryTime -= Time.deltaTime;
        resetAttackTime -= Time.deltaTime;
        jumpTime -= Time.deltaTime;

    }

    private void UpdateAnimator(){
        // ani.SetFloat("move",Mathf.Abs(horizontal));
        ani.SetFloat("yVelocity",rb.velocity.y);
        ani.SetFloat("XVelocity",Mathf.Abs(rb.velocity.x));
        ani.SetBool("isGround",isGround);
        ani.SetBool("isJumping",isJumping);
        ani.SetBool("isDashing",isDashing);
        ani.SetBool("isWounded",isWounded);
        ani.SetInteger("jumpCout",jumpCout);
    }
    void LoadScene(string scene){
        SceneManager.LoadScene(scene);
    }
    


    void OnTriggerEnter2D(Collider2D other){
        
       
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "ground"){
            canDasing = false;
            isGround = false;
        }
        if(other.tag == "Enemy"){
            canHit = false;
            if(allEnemyCanHit.Contains(other.gameObject)){
                allEnemyCanHit.Remove(other.gameObject);
            }           
        }else if(other.tag == "Boss"){
            canHit = false;
            if(allEnemyCanHit.Contains(other.gameObject)){
                allEnemyCanHit.Remove(other.gameObject);
            }           
        }
          

    }
    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Enemy" || other.tag == "item"){
            canHit = true;
            if(allEnemyCanHit.Count <2  && isAttack){
                if(!allEnemyCanHit.Contains(other.gameObject)){
                    allEnemyCanHit.Add(other.gameObject);
                }
            }
        }else if(other.tag == "Boss"){
            canHit = true;
            if(allEnemyCanHit.Count <2 && isAttack){
                if(!allEnemyCanHit.Contains(other.gameObject)){
                    allEnemyCanHit.Add(other.gameObject);
                }
            }
        }
       
    }

}

public class TextMeshPro
{
}