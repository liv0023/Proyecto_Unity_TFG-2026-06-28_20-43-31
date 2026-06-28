using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localRotation = Quaternion.Euler((Time.time/60) * 360 - 0, 0, 0); 
    }
}
