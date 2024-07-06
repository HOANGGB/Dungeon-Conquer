
using UnityEngine;

public class DestroyGameoject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float timeDestroy;
    void Start()
    {
        Destroy(gameObject,timeDestroy);
    }


}
