
using TMPro;
using UnityEngine.UI;
using UnityEngine;



public class HealBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healBar;
    public Image delayHealBar;
    [SerializeField] float delaySpeed;
    public Image manaBar;
    public TextMeshProUGUI healNumnber;
    void Update(){
        if(healBar.fillAmount != delayHealBar.fillAmount){
            delayHealBar.fillAmount = Mathf.Lerp(delayHealBar.fillAmount,healBar.fillAmount,delaySpeed);
        }
    }

    // Update is called once per frame
    public void UpdateHealBar(int heal,int healmax)
    {
        healBar.fillAmount = (float)heal / (float)healmax;
        healNumnber.text = heal.ToString() + "/" + healmax.ToString();
    }
    public void UpdateManaBar(int mana,int manaMax){
        manaBar.fillAmount = (float)mana / (float)manaMax;
    }

}
