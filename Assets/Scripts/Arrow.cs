using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public void RotateY(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
