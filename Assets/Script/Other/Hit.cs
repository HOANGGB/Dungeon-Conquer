
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] AudioClip hitsound1;
    [SerializeField] AudioClip hitsound2;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        int rd = Random.Range(1,3);
        if(rd == 1) audioSource.clip = hitsound1;
        else audioSource.clip = hitsound2;
        audioSource.Play();
        Destroy(gameObject,0.5f);
    }

}
