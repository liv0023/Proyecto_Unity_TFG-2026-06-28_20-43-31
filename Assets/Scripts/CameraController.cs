using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera [] cameras;
    private bool ok = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameras = GetComponentsInChildren<Camera>();
        ok = (cameras == null ? false : true);
        cameras[0].enabled = true;
        cameras[1].enabled = false;
        cameras[2].enabled = false;
        cameras[3].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (ok)
            if(Input.GetKey("1")){
                Debug.Log("Camara 1 activa");
                
                cameras[0].enabled = true;
                cameras[1].enabled = false;
                cameras[2].enabled = false;
                cameras[3].enabled = false;
            }
            else if(Input.GetKey("2")){
                Debug.Log("Cámara 2 activa");
                
                cameras[0].enabled = false;
                cameras[1].enabled = true;
                cameras[2].enabled = false;
                cameras[3].enabled = false;
            }
            else if(Input.GetKey("3")){
                Debug.Log("Cámara 3 activa");
                
                cameras[0].enabled = false;
                cameras[1].enabled = false;
                cameras[2].enabled = true;
                cameras[3].enabled = false;
            }
            else if(Input.GetKey("4")){
                Debug.Log("Cámara 4 activa");
                
                cameras[0].enabled = false;
                cameras[1].enabled = false;
                cameras[2].enabled = false;
                cameras[3].enabled = true;
            }
    }
}
