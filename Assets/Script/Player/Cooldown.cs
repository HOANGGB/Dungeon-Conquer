
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    // Start is called before the first frame update
    character player;
    public Image imgCooldownSkill;
    public Image imgCooldownUlti;


    float timeCooldownSkill;
    float timeCooldownUlti;
    float skillTimecount;
    float ultiTimecount;


    void Start()
    {
        player = FindObjectOfType<character>();
        timeCooldownSkill = player.skillCooldown;
        timeCooldownUlti  = player.ultiCooldown;

    }

    // Update is called once per frame
    void Update()
    {
        CoolDownUlti();
        CoolDownSkill();
        skillTimecount = player.skillTime;
        ultiTimecount = player.ultiTime;
    }
    void CoolDownSkill(){
        if(skillTimecount>0){
            imgCooldownSkill.fillAmount = skillTimecount / timeCooldownSkill;
        }else{
            imgCooldownSkill.fillAmount = 0;
        }
    }
    void CoolDownUlti(){
        if(ultiTimecount >0){
            imgCooldownUlti.fillAmount = ultiTimecount / timeCooldownUlti ;
        }else{
            imgCooldownUlti.fillAmount = 0;
        }


    }

}
