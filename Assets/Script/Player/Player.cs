
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed;
    [SerializeField] float horizontal;
    [SerializeField] public int direction = 1;
    Rigidbody2D rb;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Action();
    }
    void FixedUpdate() {
        
    }
    void Action(){
        horizontal = Input.GetAxis("Horizontal");
        if(horizontal != 0){
            Run();
        }

        
    }
    void Run(){
        var runDir = speed * horizontal;
        var runDif = runDir - rb.velocity.x;
        rb.AddForce(new Vector2(runDif,0),ForceMode2D.Force);
    }
}
