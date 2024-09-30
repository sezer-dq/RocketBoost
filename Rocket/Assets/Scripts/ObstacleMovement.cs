using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField][Range(0f, 1f)] float movementFactor;
    [SerializeField] float period=2f;
    Vector3 startPos;
    const float radian = Mathf.PI * 2;
    void Start()
    {
        startPos = transform.position;    
    }
    void Update()
    {
        if (period<= Mathf.Epsilon) { return; }
        float cyles = Time.time / period;
        
        float rawSinWave=Mathf.Sin(cyles*radian);
        movementFactor = (rawSinWave + 1f) / 2f;
        Vector3 offset = movementVector*movementFactor;
        transform.position = offset+startPos;
    }
}
