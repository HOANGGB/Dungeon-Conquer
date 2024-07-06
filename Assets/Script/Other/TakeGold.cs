using System.Collections;
using UnityEngine;

public class TakeGold : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string goldOrGem;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // StartCoroutine(Spanw());
    }
    void Update(){
    }
    IEnumerator Spanw(){
        var xForce = Random.Range(-2,3);
        var yForce = Random.Range(1,6);
        rb.AddForce(new Vector2(xForce,yForce),ForceMode2D.Impulse);
        yield return new WaitForSeconds(.1f);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(goldOrGem == "gold"){
               FindObjectOfType<UpdateCurrency>().PlusGold(1);
            }
            if(goldOrGem == "gem"){
               FindObjectOfType<UpdateCurrency>().PlusGem(1);
            }
            Destroy(gameObject);
        }
    }
}
