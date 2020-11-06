﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceCameraScript : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target.transform.position);
        this.transform.Rotate(new Vector3(0, 180, 0));
    }
}
