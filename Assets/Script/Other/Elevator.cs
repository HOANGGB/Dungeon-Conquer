using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] float startPoint;
    [SerializeField] float endPoint;
    [SerializeField] float speed;
    bool isMove;
    [SerializeField] bool areGoingDown = true;
    [SerializeField] bool areGoingUp = false;



    Rigidbody2D rb;

    void Awake() {
        startPoint = transform.position.y + startPoint;
        endPoint = transform.position.y - endPoint;
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        Debug.DrawLine(new Vector2(transform.position.x,startPoint),new Vector2(transform.position.x,endPoint),Color.red);
        MoveElevator();
    }
    void MoveElevator(){
        if(areGoingDown){
            rb.velocity = new Vector2(rb.velocity.x,-speed);
            if(Vector2.Distance(transform.position,new Vector2(transform.position.x,endPoint)) < 0.5f){
                areGoingDown = false;
                StartCoroutine(Wait());
            }
        }else if(areGoingUp){
            rb.velocity = new Vector2(rb.velocity.x,speed);
            if(Vector2.Distance(transform.position,new Vector2(transform.position.x,startPoint)) < 0.5f){
                areGoingUp = false;
                StartCoroutine(Wait1());
            }
        }else{
            rb.velocity = Vector2.zero;
        }

    }
        private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Vector3 fromArea = new Vector3(transform.position.x,transform.position.y + startPoint);
        Vector3 toArea = new Vector3(transform.position.x,transform.position.y - endPoint);
        Gizmos.DrawLine(fromArea,toArea);
    }
    IEnumerator Wait(){
            yield return new WaitForSeconds(1f);
            areGoingUp = true;
    }
    IEnumerator Wait1(){
            yield return new WaitForSeconds(1f);
            areGoingDown = true;
    }
}
