
using UnityEngine;

public class SoundBackGround : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip soundBackGround;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundBackGround;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
