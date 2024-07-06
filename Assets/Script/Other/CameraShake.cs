
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    float time;
    CinemachineVirtualCamera Camera;
    CinemachineBasicMultiChannelPerlin _cbmcp;

    void Awake() {
        Camera = GetComponent<CinemachineVirtualCamera>(); 
    }
    void Start(){
        StopShakeCamera();
    }
    public void ShakeCamera(float shakeIntensity,float shakeTime){
        CinemachineBasicMultiChannelPerlin _cbmcp = Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = shakeIntensity;
        time = shakeTime;
    }
    void StopShakeCamera(){
        CinemachineBasicMultiChannelPerlin _cbmcp = Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0;
        time = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if(time>0){
            time -= Time.deltaTime;
            if(time<=0){
                StopShakeCamera();
            }
        }
    }
}
