
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeChar : MonoBehaviour
{
    private int Gem;
     UpdateCurrency updateCurrency;
     SoundUi sound;
    [SerializeField] int maxLv;
    [SerializeField] GameObject MaxAttack;
    [SerializeField] GameObject MaxSkill;
    [SerializeField] GameObject MaxUlti;
    [SerializeField] TextMeshProUGUI LevelAttack;
    [SerializeField] TextMeshProUGUI LevelSkil;
    [SerializeField] TextMeshProUGUI LevelUlti;

    [SerializeField] GameObject Skil;
    [SerializeField] GameObject Ulti;



    void Start(){
        CheckMaxLv();
        if(LevelAttack != null  && LevelSkil != null && LevelUlti != null){
            LevelAttack.text = PlayerPrefs.GetInt(Data.AttackLevel).ToString();
            LevelSkil.text = PlayerPrefs.GetInt(Data.SkillLevel).ToString();
            LevelUlti.text = PlayerPrefs.GetInt(Data.UltiLevel).ToString();
        }
        if(Skil != null && Ulti != null){
            if(PlayerPrefs.GetInt(Data.UnlockSkill) == 1){
                Skil.SetActive(true);
            }
            if(PlayerPrefs.GetInt(Data.UnlockUlti) ==1){
                Ulti.SetActive(true);
            }
        }

    }
    void Awake(){
        updateCurrency = FindObjectOfType<UpdateCurrency>();
        sound = FindObjectOfType<SoundUi>();
        Gem = PlayerPrefs.GetInt(Data.Gem);
    }
    void CheckMaxLv(){
        if(SceneManager.GetActiveScene().name != "Home") return;
        if(PlayerPrefs.GetInt(Data.AttackLevel) == maxLv){
            MaxAttack.SetActive(true);
        }else  MaxAttack.SetActive(false);
        if(PlayerPrefs.GetInt(Data.SkillLevel) == maxLv){
            MaxSkill.SetActive(true);
        }else  MaxSkill.SetActive(false);
        if(PlayerPrefs.GetInt(Data.UltiLevel) == maxLv){
            MaxUlti.SetActive(true);
        }else  MaxUlti.SetActive(false);
    }
    public void UpgradeAttack(int gemNum){
        // Debug.Log("GEM = "+ PlayerPrefs.GetInt(Data.Gem));

        if(Gem >= gemNum && PlayerPrefs.GetInt(Data.AttackLevel) < maxLv){
            sound.PlaySoundUi("up");
           Gem -= gemNum;
            PlayerPrefs.SetInt(Data.Gem,Gem);
            PlayerPrefs.SetInt(Data.AttackLevel,PlayerPrefs.GetInt(Data.AttackLevel)+1);
            updateCurrency.UpdateCurrencyy();
            LevelAttack.text = PlayerPrefs.GetInt(Data.AttackLevel).ToString();
        }else{
            // Debug.Log("Not Enough Gem");
        }
        CheckMaxLv();
    }
    public void UpgradeSkill(int gemNum){
        
        if(Gem >= gemNum  && PlayerPrefs.GetInt(Data.SkillLevel) < maxLv){
            sound.PlaySoundUi("up");
           Gem -= gemNum;
            PlayerPrefs.SetInt(Data.Gem,Gem);
            PlayerPrefs.SetInt(Data.SkillLevel,PlayerPrefs.GetInt(Data.SkillLevel)+1);
            updateCurrency.UpdateCurrencyy();
            LevelSkil.text = PlayerPrefs.GetInt(Data.SkillLevel).ToString();
        }else{
            // Debug.Log("Not Enough Gem");
        }
        CheckMaxLv();

    }
    public void UpgradeUlti(int gemNum){

        if(Gem >= gemNum && PlayerPrefs.GetInt(Data.UltiLevel) < maxLv){
            sound.PlaySoundUi("up");
           Gem -= gemNum;
            PlayerPrefs.SetInt(Data.Gem,Gem);
            PlayerPrefs.SetInt(Data.UltiLevel,PlayerPrefs.GetInt(Data.UltiLevel)+1);
            updateCurrency.UpdateCurrencyy();
            LevelUlti.text = PlayerPrefs.GetInt(Data.UltiLevel).ToString();
        }else{
            // Debug.Log("Not Enough Gem");
        }
        CheckMaxLv();
    }

}
