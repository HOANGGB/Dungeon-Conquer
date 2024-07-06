
using System.Linq;
using UnityEngine;

public class SoundEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource au;
    [SerializeField] AudioClip[] attackSound;
    [SerializeField] AudioClip[] hitSound;
    [SerializeField] AudioClip deathSound;

    
    void Start()
    {
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaySound(string NameSound)
    {
        if(NameSound == "attack1"){
            au.clip = attackSound[0];
        }else if(NameSound == "attack2"){
            au.clip = attackSound[1];
        }else if(NameSound == "attack3"){
            au.clip = attackSound[2];
        }else if(NameSound == "attack4"){
            au.clip = attackSound[3];   
        }
        else if(NameSound == "hit"){
            var rd = Random.Range(0,hitSound.Count());
            au.clip = hitSound[rd]; 
        }
        else if(NameSound == "death"){
            au.clip = deathSound;
        }
            au.Play();
    }
     public void PlaySoundDelay(string NameSound,float timeDelay)
    {
        if(NameSound == "attack1"){
            au.clip = attackSound[0];
        }else if(NameSound == "attack2"){
            au.clip = attackSound[1];
        }else if(NameSound == "attack3"){
            au.clip = attackSound[2];
        }else if(NameSound == "attack4"){
            au.clip = attackSound[3];   
        }
        else if(NameSound == "hit"){
            var rd = Random.Range(0,hitSound.Count());
            au.clip = hitSound[rd];  
        }else if(NameSound == "death"){
            au.clip = deathSound;
        }
            au.PlayDelayed(timeDelay);
    }

}
