//
// This file is a part of Screen Fader asset
// For any help, support or documentation
// follow www.patico.pro
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class RotatableObject : MonoBehaviour
{
    public float angleX = 0;
    public float angleY = 0;
    public float angleZ = 0;
    Transform thistransform = null;

    void Awake()
    {
        this.thistransform = GetComponent<Transform>();
    }

    void Update()
    {
        if (angleX > 0 | angleY > 0 | angleZ > 0)
        {
            thistransform.Rotate(Vector3.up, angleY * Time.deltaTime);
            thistransform.Rotate(Vector3.forward, angleX * Time.deltaTime);
            thistransform.Rotate(Vector3.right, angleZ * Time.deltaTime);
        }
    }
}
