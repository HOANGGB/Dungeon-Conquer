
using UnityEngine;

public class BuyShop : MonoBehaviour
{
     private int Gold;
     UpdateCurrency updateCurrency;
     SoundUi sound;
    
    private void Awake()
    {
        sound = FindObjectOfType<SoundUi>();
        updateCurrency = FindObjectOfType<UpdateCurrency>();
        Gold = PlayerPrefs.GetInt(Data.Gold);
    }
    public void BuyHealPositon(int GoldNum){
        if(Gold >= GoldNum && PlayerPrefs.GetInt(Data.HealPotionNum)<1){
            sound.PlaySoundUi("up");
            Gold -= GoldNum;
            PlayerPrefs.SetInt(Data.Gold,Gold);
            PlayerPrefs.SetInt(Data.HealPotionNum,1);
            updateCurrency.UpdateCurrencyy();
            // Debug.Log("Gold = "+ PlayerPrefs.GetInt(Data.Gold));

        }else{
            // Debug.Log("Not Enough Gold");
        }
    }
}
