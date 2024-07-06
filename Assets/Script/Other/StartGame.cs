using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    Animator anim;
    void Start(){

        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    public void StartGameClick(){
        gameObject.SetActive(true);
            StartCoroutine(Load("Home"));
        // if(!PlayerPrefs.HasKey(Data.TeleportMap)){
        // }else{
        //     StartCoroutine(Load(PlayerPrefs.GetString(Data.TeleportMap)));
        // }
        Data.CheckAndSetDefaultValues();
    } 
    IEnumerator Load(string NameScene){
            anim.SetTrigger("start");
            yield return new  WaitForSeconds(1);
            SceneManager.LoadScene(NameScene);
    }
}
