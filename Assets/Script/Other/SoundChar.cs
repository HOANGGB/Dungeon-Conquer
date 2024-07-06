
using UnityEngine;

public class SoundChar : MonoBehaviour
{
    AudioSource au;
    [SerializeField] AudioClip dashSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip hitEnemySound;

    [SerializeField] AudioClip dbJumpSound;

    [SerializeField] AudioClip[] attackSound;
    [SerializeField] AudioClip skillSound;
    [SerializeField] AudioClip ultiSound;


    void Start()
    {
        au = GetComponent<AudioSource>();
    }

    
    public void PlaySound(string NameAudio){
        if(NameAudio == "dash"){
            au.clip = dashSound;
        }else if(NameAudio == "attack1"){
            au.clip = attackSound[0];
        }else if(NameAudio == "attack2"){
            au.clip = attackSound[1];
        }else if(NameAudio == "attack3"){
            au.clip = attackSound[2];
        }else if(NameAudio == "attack4"){
            au.clip = attackSound[3];
        }else if(NameAudio == "skill"){
            au.clip = skillSound;
        }else if(NameAudio == "ulti"){
            au.clip = ultiSound;
        }else if(NameAudio == "hit"){
            au.clip = hitSound;
        }else if(NameAudio == "hitEnemy"){
            au.clip = hitEnemySound;
        }else if(NameAudio == "dbJump"){
            au.clip = dbJumpSound;
        }
        au.Play(); 
    }
}
