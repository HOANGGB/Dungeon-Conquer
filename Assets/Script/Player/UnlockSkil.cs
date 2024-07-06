
using UnityEngine;

public class UnlockSkil : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject SkillGameoject;
    [SerializeField] GameObject UltiGameoject;

    void Start()
    {
        if(PlayerPrefs.GetInt(Data.UnlockSkill) == 1){
            SkillGameoject.SetActive(true);
        }
        if(PlayerPrefs.GetInt(Data.UnlockUlti) == 1){
            UltiGameoject.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
