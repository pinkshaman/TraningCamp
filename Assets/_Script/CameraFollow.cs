using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target; // Đối tượng mà camera sẽ theo dõi
    public float smoothSpeed = 30.0f; // Tốc độ mượt mà của camera
    public Vector3 offset; // Khoảng cách giữa camera và đối tượng
    
    private void Start()
    {

    }
    void Update()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position, smoothSpeed);
        transform.position = smoothedPosition + offset; ;        
    }
}


