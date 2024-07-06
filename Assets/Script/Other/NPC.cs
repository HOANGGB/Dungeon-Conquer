
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject uiShow;
    public GameObject ButtonOpenShow;

    private bool canInteract = false;
    Animator anim;
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open(){
        if(canInteract){
            uiShow.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collider) {
        if(collider.tag == "Player"){
            canInteract = true;
            ButtonOpenShow.SetActive(true);
            anim.SetTrigger("interact");
        }
    }
    private void OnTriggerExit2D(Collider2D collider) {
        if(collider.tag == "Player"){
            canInteract = false;
            ButtonOpenShow.SetActive(false);
            if(uiShow.activeSelf){
                uiShow.SetActive(false);
            }
        }
    }
}
