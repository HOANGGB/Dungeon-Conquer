
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiScript : MonoBehaviour
{
    public GameObject Option;
    public GameObject Volume;
    public GameObject ControlUI;
    public GameObject ShopUI;
    public GameObject UpgradeUI;
    new AudioSource audio;
    // [SerializeField] AudioClip upgrade;
    void Start(){
        audio = GetComponent<AudioSource>();
    }


    public void showOption(){
        audio.Play();
        Option.SetActive(true);
    }
    public void hideOption(){
        audio.Play();
        Option.SetActive(false);
    }

    public void ShowVolume(){
        audio.Play();
        Option.SetActive(false);
        Volume.SetActive(true);

    }
    public void HideVolume(){
        audio.Play();
        Volume.SetActive(false);
        Option.SetActive(true);
    }

    public void ShowShopUI(){
        audio.Play();
        ShopUI.SetActive(true);
    }
    public void HideShopUI(){
        audio.Play();
        ShopUI.SetActive(false);
    }
    public void ShowUpgradeUI(){
        audio.Play();
        UpgradeUI.SetActive(true);
    }
    public void HideUpgradeUI(){
        audio.Play();
        UpgradeUI.SetActive(false);
    }
 
    public void ShowControlUI(){
        audio.Play();
        ControlUI.SetActive(true);
    }
    public void HideontrolUI(){
        audio.Play();
        ControlUI.SetActive(false);
    }


    public void Home(){
        SceneManager.LoadScene("Home");
    }
}
