
using UnityEngine;

public class UseHealPosion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject healPoIcon;
    [SerializeField] int healNumber;
    character player;
    HealBar healBar;
    void Start()
    {
        player = FindObjectOfType<character>();
        healBar = FindObjectOfType<HealBar>();
    }

    void Update()
    {
        if(PlayerPrefs.GetInt(Data.HealPotionNum)>0){
            healPoIcon.SetActive(true);
        }else{
            healPoIcon.SetActive(false);
        }
    }
    public void UseHealPo(){
        if(player.HealCharCurrent<=0) return;
        healPoIcon.SetActive(false);

        if(player.HealCharCurrent == player.HealCharMax){
            return;
        }
        else if(healNumber+player.HealCharCurrent>player.HealCharMax){
            PlayerPrefs.SetInt(Data.HealPotionNum,0);
            player.HealCharCurrent = player.HealCharMax;
            healBar .UpdateHealBar(player.HealCharCurrent,player.HealCharMax);
        }else{
            PlayerPrefs.SetInt(Data.HealPotionNum,0);
            player.HealCharCurrent += healNumber;
            healBar .UpdateHealBar(player.HealCharCurrent,player.HealCharMax);
        }

    }
}
