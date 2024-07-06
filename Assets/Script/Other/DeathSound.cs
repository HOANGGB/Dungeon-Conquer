
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    AudioSource au;
    AudioClip Sound;
    void Start()
    {
        au = GetComponent<AudioSource>();
        au.clip = Sound;
    }

    // Update is called once per frame
}
