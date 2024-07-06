
using UnityEngine;

public class SoundUi : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource au;
    [SerializeField] AudioClip soundUpgrade;
    void Start()
    {
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaySoundUi(string nameSoundUi)
    {
        if(nameSoundUi == "up"){
            au.clip = soundUpgrade;
        }
        au.Play();
    }
}
