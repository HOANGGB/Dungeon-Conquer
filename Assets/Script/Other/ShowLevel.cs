
using TMPro;
using UnityEngine;

public class ShowLevel : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI Level;
    void Start()
    {
        Level = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
