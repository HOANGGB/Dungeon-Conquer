
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] string LevelName;
    [SerializeField] int timeLoadScene;
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;

    Animator anim;
    GameObject showCanvas;

    void Start(){
        showCanvas = transform.GetChild(0).gameObject;
        showCanvas.SetActive(true);
        anim = GetComponent<Animator>();
    }


    private void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Player"){
            showCanvas.SetActive(true);
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene(){
        anim.SetTrigger("start");
        PlayerPrefs.SetFloat(Data.TeleportX,x);
        PlayerPrefs.SetFloat(Data.TeleportY,y);
        PlayerPrefs.SetFloat(Data.TeleportZ,z);
        PlayerPrefs.SetString(Data.TeleportMap,LevelName);
        yield return new WaitForSeconds(timeLoadScene);
        SceneManager.LoadScene(LevelName);
    }
    ///in animator
    public void HideCanvas(){
        showCanvas.SetActive(false);
    }
}
