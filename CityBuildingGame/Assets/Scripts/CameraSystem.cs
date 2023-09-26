using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public float moveSpeed = 50f;
    public float rotateSpeed = 300f;
    private float targetFieldOfView = 50f;
    public float zoomSpeed = 30f;
    private float maxZoom = 80f;
    private float minZoom = 10f;
    private void Update()
    {
        MoveCamera();

        RotateCamera();

        ZoomCamera();
    }
    void ZoomCamera()
    {
        targetFieldOfView += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        targetFieldOfView = Mathf.Clamp(targetFieldOfView, minZoom, maxZoom);
        cinemachineVirtualCamera.m_Lens.FieldOfView = targetFieldOfView;
    }
    void MoveCamera()
    {
        Vector3 inputDir = new Vector3(0f, 0f, 0f);
        if (Input.GetKey(KeyCode.W)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = +1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = +1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    void RotateCamera()
    {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = 1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0f);
    }
}
