
using UnityEngine;

public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 respawnPoint;
    void Start()
    {
        respawnPoint = transform.position;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag == "spawnPoint"){
            respawnPoint = other.transform.position;
            // Debug.Log("respawnpoint : "+respawnPoint);
        }
        if(other.tag == "deathZone"){
            transform.position = respawnPoint;
        }
    }
}
