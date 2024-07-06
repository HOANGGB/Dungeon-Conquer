
using UnityEngine;

public class DestroyDamage : MonoBehaviour
{
    float timeDestroy = 1;
    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
        timeDestroy -= Time.deltaTime;
        if(timeDestroy <=0) Destroy(this.gameObject);
    }
}
