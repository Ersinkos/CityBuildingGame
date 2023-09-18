using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;

    #region CameraRotationVariables
    public float minXRot;
    public float maxXRot;
    private float curXRot;
    public float rotateSpeed;
    #endregion

    #region CamerazoomVariables
    public float minZoom;
    public float maxZoom;
    private float curZoom;
    public float zoomSpeed;
    #endregion

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        curZoom = cam.transform.localPosition.y;
        curXRot = -50;
    }
    private void Update()
    {
        //Zoom
        CameraZoom();

        //Rotation
        CameraRotation();

        //Movement
        CameraMovement();

    }
    void CameraZoom()
    {
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);

        cam.transform.localPosition = Vector3.up * curZoom;
    }
    void CameraRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            curXRot += -y * rotateSpeed;
            curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);

            transform.eulerAngles = new Vector3(curXRot, transform.eulerAngles.y + (x * rotateSpeed), 0);
        }
    }
    void CameraMovement()
    {
        Vector3 forward = cam.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = cam.transform.right;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 dir = forward * moveZ + right * moveX;

        dir.Normalize();

        dir *= moveSpeed * Time.deltaTime;

        transform.position += dir;
    }



}
