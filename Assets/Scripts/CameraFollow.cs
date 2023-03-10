using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
