
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canInteract;
    public string GateName = "";
    character character;

    void Start()
    {
        character = FindObjectOfType<character>();
    }

    // Update is called once per frame
    void Update()
    {
        checkInteractGate();
    }
    private void checkInteractGate(){
        if(Input.GetKeyDown(KeyCode.U) && canInteract){
            SceneManager.LoadScene(GateName);
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "Player"){
            canInteract = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            canInteract = false;           
        }
    }
}
