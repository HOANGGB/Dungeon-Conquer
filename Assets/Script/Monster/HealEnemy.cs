using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealEnemy : MonoBehaviour
{
    string IdEnemy;
    public int healCurrent;
    [SerializeField] public int healMax;
    [SerializeField] int gold;
    [SerializeField] int gem;

    [SerializeField] float knocBackTime;
    [SerializeField] float delaySpeedHealBar;
    public bool isKnockBack;

    public bool canTakeDamage;
    public bool immunity = false;

    public bool isWounded;
    public bool isDeal = false;


    float timeTakeDamage = .2f;
    float timeReSpawn = 180;

    [SerializeField] private Animator ani;
    [SerializeField] Image healBar;
    [SerializeField] Image delayHealBar;
    [SerializeField] GameObject GoldPrefab;
    [SerializeField] GameObject GemPrefab;

    public TextMeshProUGUI ShowTextDamage;
    public GameObject ShowDamage;
    public GameObject HitEffect;
    public GameObject deathEffect;



    private SpriteRenderer spr;
    private Material materialDefault;
    public Material noCritDamage;
    public Material critDamage;
    character player;
    Rigidbody2D rb;
    SoundEnemy sound;

    void Start()
    {
        CheckIsDied();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<character>();
        sound = GetComponentInChildren<SoundEnemy>();
        spr = GetComponent<SpriteRenderer>();
        materialDefault = GetComponent<SpriteRenderer>().material;
        healCurrent = healMax;
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform parentTransform = healBar.transform.parent.parent;
        parentTransform.rotation = Quaternion.identity;
        timeTakeDamage -= Time.deltaTime;
        timeReSpawn -= Time.deltaTime;
        canTakeDamage = timeTakeDamage <=0 ? true :false;
        ani.SetInteger("HP",healCurrent);
        DelayHeal();

    }
    void DelayHeal(){
         if(healBar.fillAmount != delayHealBar.fillAmount){
            delayHealBar.fillAmount = Mathf.Lerp(delayHealBar.fillAmount,healBar.fillAmount,delaySpeedHealBar);
        }
    }
    void CheckIsDied(){
        if(gameObject.tag == "Enemy") return;
        IdEnemy = "Enemy"+transform.position.ToString();
            if(PlayerPrefs.GetInt(IdEnemy) == 1){
                if (transform.parent != null && transform.parent.tag != "OjectEnemy") Destroy(transform.parent.gameObject);
                else Destroy(gameObject);
            }else if(PlayerPrefs.GetInt(IdEnemy) == 0){
                // Debug.Log("Spawn : ");
            }
        
    }
    public void takeDamge(int damge,float Posx, float force){
        if(canTakeDamage && !immunity){
            StartCoroutine(KnockBack(Posx,force));
            CreateHit();
            DamagePopUp(damge);
            DeathEffect(Posx,force);
            // sound.PlaySound("hit");
            timeTakeDamage = 0.5f;
            healBar.fillAmount = (float)healCurrent / (float)healMax;

        }
        
    }
    void DamagePopUp(int damge){
        Vector2 posDamageRd = new Vector2(
            UnityEngine.Random.Range(transform.position.x+1f,transform.position.x-1f),
            UnityEngine.Random.Range(transform.position.y+1f,transform.position.y-1f)
        );
        int damgeRd = UnityEngine.Random.Range(damge-1,damge+2);
        // ShowTextDamage.text = damgeRd.ToString();
        // Instantiate(ShowDamage,posDamageRd,Quaternion.identity);
        healCurrent -= damgeRd;
        StartCoroutine(FlashDamage(damge,damgeRd));


    }
        IEnumerator FlashDamage(int damge,int damgeRd){
        ani.SetTrigger("Hit");
        if(damgeRd > damge){
            spr.material = critDamage;
        }else{
            spr.material = noCritDamage;
        }
        yield return new WaitForSeconds(0.1f);
        spr.material = materialDefault;
    }
    void CreateHit(){
        float randomRotationZ = UnityEngine.Random.Range(0f, 180f);
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomRotationZ);
        Instantiate(HitEffect,transform.position,randomRotation);
    }
    void DeathEffect(float Posx, float force){   
        if(healCurrent<=0 && !isDeal){
            sound.PlaySound("hit");
            Quaternion rota = transform.position.x > Posx ? Quaternion.Euler(new Vector3(0,0,0)) : Quaternion.Euler(new Vector3(0,180,0));
            var effecDied = Instantiate(deathEffect,transform.position,rota);
            Rigidbody2D[] rb = effecDied.GetComponentsInChildren<Rigidbody2D>();
            var dir = transform.position.x > Posx ? new Vector2(1,0) : new Vector2(-1,0);
            foreach(var r in rb){
                var rd = UnityEngine.Random.Range(1,4);
                r.AddForce(dir * force * rd ,ForceMode2D.Impulse);
            }
            for (int i = 0; i < gold; i++)
            {
                var gold = Instantiate(GoldPrefab,transform.position,Quaternion.identity);
                var xForce = UnityEngine.Random.Range(-2, 3);
                var yForce = UnityEngine.Random.Range(1, 5);
                gold.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
            }
            for (int i = 0; i < gem; i++)
            {
                var gem = Instantiate(GemPrefab,transform.position,Quaternion.identity);
                var xForce = UnityEngine.Random.Range(-2, 3);
                var yForce = UnityEngine.Random.Range(1, 5);
                gem.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
            }
            isDeal = true;
            SaveDataDead();
        }
        isDeal = healCurrent <=0? true : false;
    }
    void SaveDataDead(){
            PlayerPrefs.SetInt(IdEnemy,1);
            StartCoroutine(ReactivateEnemy(10f));
    }
    private IEnumerator ReactivateEnemy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(true);
    }


    IEnumerator KnockBack(float posX,float Force){
        isKnockBack = true;
        int dirKnocBack = posX > transform.position.x ? -1 :1;
        if(gameObject.tag == "Enemy"){
            rb.AddForce(new Vector2(dirKnocBack* Force,2),ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(knocBackTime);
        isKnockBack = false;
    }

}
