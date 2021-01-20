using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotateScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate the camera using the WASD keys. This feature is temporary until VR support is added
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Rotate(new Vector3(transform.rotation.x, Input.GetAxis("Horizontal"), transform.rotation.z));
        }
    }
}
