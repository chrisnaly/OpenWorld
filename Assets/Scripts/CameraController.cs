using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSmoothingFactor = 1;
    public float lookUpMax = 60;
    public float lookUpMin = -50;
    public Transform t_camera;
    private Quaternion camRotation;
    private RaycastHit hit;
    private Vector3 camera_offset;

    // Start is called before the first frame update
    void Start()
    {
        camRotation = transform.localRotation;
        camera_offset = t_camera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        camRotation.x += Input.GetAxis("Mouse Y")*cameraSmoothingFactor*(-1);       // Look up&down
        camRotation.y += Input.GetAxis("Mouse X")*cameraSmoothingFactor;            // Look left&right
        camRotation.x = Mathf.Clamp(camRotation.x, lookUpMin, lookUpMax);
        
        transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);

        if(Physics.Linecast(transform.position, transform.position + transform.localRotation * camera_offset, out hit))
        {
            t_camera.localPosition = new Vector3(0, 0, - Vector3.Distance(transform.position, hit.point));
        }
        else
        {
            t_camera.localPosition = Vector3.Lerp(t_camera.localPosition, camera_offset, Time.deltaTime);
        }

    }
}
