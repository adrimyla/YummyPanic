using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform objectToFollow;

    public Transform ObjectToFollow { get => objectToFollow; set => objectToFollow = value; }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = new Vector3(ObjectToFollow.position.x, ObjectToFollow.position.y, this.transform.position.z);

    }


}