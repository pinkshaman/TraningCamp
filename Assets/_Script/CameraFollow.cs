using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target; // Đối tượng mà camera sẽ theo dõi
    public float smoothSpeed = 0.125f; // Tốc độ mượt mà của camera
    public Vector2 offset; // Khoảng cách giữa camera và đối tượng

    void LateUpdate()
    {
       
        Vector2 smoothedPosition = Vector2.Lerp(transform.position, target.position, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}


