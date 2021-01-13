using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public Vector2 Speed;

    // Update is called once per frame
    void Update()
    {
        Transform currentTransform = transform;

        Vector3 targetPosition = currentTransform.TransformPoint(new Vector3(Speed.x, 0, Speed.y));

        currentTransform.position = Vector3.Lerp(currentTransform.position, targetPosition, Time.deltaTime);
    }
}
