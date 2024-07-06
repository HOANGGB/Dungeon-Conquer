using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] List<GameObject> ListCheck = new List<GameObject>();
    string IdChest;
    [SerializeField] int quantityGold;
    [SerializeField] int quantityGem;
    [SerializeField] bool canOpen = false;
    [SerializeField] bool clearEnemy = false;

    Animator anim;
    [SerializeField] GameObject ShowButtonOpen;

    [SerializeField] GameObject Gold_Prefab;
    [SerializeField] GameObject Gem_Prefab;

    void Start()
    {
        anim = GetComponent<Animator>();
        CheckIsOpen();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("clearEnemy",clearEnemy);
        ListCheck.RemoveAll(x=>x==null);
        if(ListCheck.Count == 0){
            clearEnemy = true;
        }else {
            clearEnemy = false;
        }
        
    }
    public void Open()
    {
        if(canOpen && clearEnemy && PlayerPrefs.GetInt(IdChest) == 1){
            anim.SetTrigger("isOpen");
            StartCoroutine(Spawn());
            canOpen = false;
            PlayerPrefs.SetInt(IdChest,0);
            ShowButtonOpen.SetActive(false);
        }
    }

     IEnumerator Spawn()
    {
        for (int i = 0; i < quantityGold; i++)
        {
            var gold = Instantiate(Gold_Prefab,new Vector3(transform.position.x,transform.position.y,Gold_Prefab.transform.position.z),Quaternion.identity);
            var xForce = UnityEngine.Random.Range(-2, 3);
            var yForce = UnityEngine.Random.Range(1, 10);
            gold.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < quantityGem; i++)
        {
            var gem = Instantiate(Gem_Prefab,new Vector3(transform.position.x,transform.position.y,Gem_Prefab.transform.position.z),Quaternion.identity);
            var xForce = UnityEngine.Random.Range(-2, 3);
            var yForce = UnityEngine.Random.Range(1, 10);
            gem.GetComponent<Rigidbody2D>().AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.05f);
        }
    }
    void CheckIsOpen()
    {
        IdChest = "Chest"+ transform.position.ToString();
        if(!PlayerPrefs.HasKey(IdChest)){
            PlayerPrefs.SetInt(IdChest,1);
        }else{
            if(PlayerPrefs.GetInt(IdChest) == 0){
                anim.SetTrigger("isOpen");

            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {

        if(other.tag == "Player"){
            canOpen = true;
            if(canOpen && clearEnemy && PlayerPrefs.GetInt(IdChest) == 1){
                ShowButtonOpen.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {

         if(other.tag == "Player"){
            canOpen = false;
            ShowButtonOpen.SetActive(false);
        }
    }
}
