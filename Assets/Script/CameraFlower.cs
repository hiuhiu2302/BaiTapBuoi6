using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFlow : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform nhanvat;

    void Start()
    {
        nhanvat = GameObject.Find("player").transform;

    }

    // Update is called once per frame


    //cap nhap vi tri cua camera theo nv 
    void Update()
    {
        Vector3 cam = transform.position;
        cam.x = nhanvat.position.x;
        cam.y = nhanvat.position.y;

        transform.position = cam;
    }
}
