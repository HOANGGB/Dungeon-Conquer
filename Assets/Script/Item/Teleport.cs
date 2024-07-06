
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string nameTeleportGate;
    bool IsOpen;
    bool canInteract;
    // bool showButton = false;

    Animator anim;
    [SerializeField] GameObject Map;
    [SerializeField] GameObject ButtonLoadMap;
    [SerializeField] List<GameObject> Port = new List<GameObject>();

    float x;
    float y;
    float z;
    string NameMap;





    void Start()
    {
        x = PlayerPrefs.GetFloat(Data.TeleportX);
        y = PlayerPrefs.GetFloat(Data.TeleportY);
        z = PlayerPrefs.GetFloat(Data.TeleportZ);
        NameMap = PlayerPrefs.GetString(Data.TeleportMap);
        anim = GetComponent<Animator>();
        if(PlayerPrefs.GetInt("TELEPORT_"+nameTeleportGate)==1) anim.SetTrigger("IsOpened");
    }
    void CheckIsopenPort(){
            PlayerPrefs.SetInt("TELEPORT_"+nameTeleportGate,1);
            // Debug.Log("TELEPORT_"+nameTeleportGate);
        if(PlayerPrefs.GetInt("TELEPORT_"+nameTeleportGate)==1) anim.SetTrigger("IsOpened");
        for(int i=0; i<4;i++){
            if(i == 0){
                if(PlayerPrefs.GetInt("TELEPORT_Home") == 1){
                    Port[0].SetActive(true);
                }else{
                    Port[0].SetActive(false);
                }
            }else{
                if(PlayerPrefs.GetInt("TELEPORT_"+"Level"+i+"_2") == 1){
                        Port[i].SetActive(true);
                }else{
                        Port[i].SetActive(false);
                }
            }
        }

    }

     void OnMouseDown()
    {
        if(canInteract){
            CheckIsopenPort();
            Map.SetActive(true);   
            ButtonLoadMap.SetActive(false);
            
        }
        
    }
    public void ExitMap(){
        Map.SetActive(false);
        PlayerPrefs.SetFloat(Data.TeleportX,x);
        PlayerPrefs.SetFloat(Data.TeleportY,y);
        PlayerPrefs.SetFloat(Data.TeleportZ,z);
        PlayerPrefs.SetString(Data.TeleportMap,NameMap);
    }
    public void LoadMap(){
        SceneManager.LoadScene(PlayerPrefs.GetString(Data.TeleportMap));
    }
    public void SelectedMap(float x, float y, float z,string map){
        PlayerPrefs.SetFloat(Data.TeleportX,x);
        PlayerPrefs.SetFloat(Data.TeleportY,y);
        PlayerPrefs.SetFloat(Data.TeleportZ,z);
        PlayerPrefs.SetString(Data.TeleportMap,map);
    }
    public void SelectHome()
    {
        SelectedMap(12.34f, 3, 0f, "Home");
        ButtonLoadMap.SetActive(true);
    }
    public void SelectGame1()
    {
        SelectedMap(169.52f, 37f, 0f, "Level1_2");
        ButtonLoadMap.SetActive(true);
    }
    public void SelectGame2()
    {
        SelectedMap(154.39f, 4f, 0f, "Level2_2");
        ButtonLoadMap.SetActive(true);
    }
    public void SelectGame3()
    {
        SelectedMap(146f, 104f, 0f, "Level3_2");
        ButtonLoadMap.SetActive(true);
    }
    private void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Player"){
            canInteract = true;
            anim.SetBool("canInteract",true);
            if(!IsOpen){
                if(nameTeleportGate == "Game1"){
                    PlayerPrefs.SetInt(Data.TeleportGateGame1,1);
                }else if(nameTeleportGate == "Game2"){
                    PlayerPrefs.SetInt(Data.TeleportGateGame1,1);
                }else if(nameTeleportGate == "Game3"){
                    PlayerPrefs.SetInt(Data.TeleportGateGame1,1);
                }else if(nameTeleportGate == "Home"){
                    PlayerPrefs.SetInt(Data.TeleportGateHome,1);
                }
                IsOpen = true;
            }


        }
    }
    private void OnTriggerExit2D(Collider2D other){
        canInteract = false;
        anim.SetBool("canInteract",false);
        Map.SetActive(false);
    }
}
