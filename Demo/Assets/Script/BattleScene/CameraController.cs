using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject A;

    Transform AT;
    void Start()
    {
        AT = A.transform;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, AT.position, 2f * Time.deltaTime);

        //카메라를 원래 z축으로 이동
        Vector3 pos = transform.position;
        pos.z = -10f;
        transform.position =  pos; 
    }
}

