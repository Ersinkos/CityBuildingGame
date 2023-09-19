using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIndicator : MonoBehaviour
{
    public static RotateIndicator instance;
    public Vector3 buildingRotation;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Turn();
        }
    }
    void Turn()
    {
        float baseAngle = gameObject.transform.eulerAngles.y;
        float newAngle = gameObject.transform.eulerAngles.y + 90;
        float angle = Mathf.LerpAngle(baseAngle, newAngle, Time.time);
        transform.eulerAngles = new Vector3(0, angle, 0);
        buildingRotation = transform.eulerAngles;
    }
}
