
using UnityEngine;

public class RemovEnemyfDeath : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float TimeRemove;
    void Start()
    {
        Destroy(gameObject,TimeRemove);
    }

}
