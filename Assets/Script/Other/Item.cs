
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int hp = 100;
    private Animator anim;
    float time = 1f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        anim.SetInteger("Hp",hp);
    }
    public void takeDamge(int damge){
        if(time<=0){
            hp -= damge;
            time = 0.1f;
            if(hp<=0){
            Destroy(gameObject);
            FindObjectOfType<UpdateCurrency>().Spawn("gold",1,transform);
        }
        // Debug.Log("HP BOX = "+hp);
        }
    }

}
