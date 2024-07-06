
using TMPro;
using UnityEngine;

public class UpdateCurrency : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI GemText;
    [SerializeField] GameObject GemPrefab;
    [SerializeField] GameObject GoldPrefab;

    void Start(){
        GoldText.text = PlayerPrefs.GetInt(Data.Gold).ToString();
        GemText.text = PlayerPrefs.GetInt(Data.Gem).ToString();
    }
    public void UpdateCurrencyy(){
        GoldText.text = PlayerPrefs.GetInt(Data.Gold).ToString();
        GemText.text = PlayerPrefs.GetInt(Data.Gem).ToString();
    }
    public  void PlusGem(int gem){
        PlayerPrefs.SetInt(Data.Gem,PlayerPrefs.GetInt(Data.Gem) + gem);
        PlayerPrefs.Save();
        UpdateCurrencyy();
        Debug.Log("Gem = "+PlayerPrefs.GetInt(Data.Gem));

    }
    public  void PlusGold(int gold){
        PlayerPrefs.SetInt(Data.Gold,PlayerPrefs.GetInt(Data.Gold) + gold);
        PlayerPrefs.Save();
        UpdateCurrencyy();
        Debug.Log("Gold = "+PlayerPrefs.GetInt(Data.Gold));
    }
    public void Spawn(string goldOrGem,int number,Transform transform){
        if(goldOrGem == "gold"){
            for(int i =0;i<number;i++){
                Instantiate(GoldPrefab,transform.position,Quaternion.identity);
            }
        }
    }


}
