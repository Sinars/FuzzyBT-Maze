using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    
    public float smoothSpeed = 0.075f;
    public Vector3 offset;
    public float cameraRotationSpeed;
    private Vector3 targetPos;
    private float rotationX = 2.0f;
    private Vector3 beforePos;

    private void Start() {
        beforePos = target.transform.position;
    }

    private void LateUpdate() {
        if (!InGameUI.GameIsPaused && !GameManagement.GM.playerDead && !GameManagement.GM.levelFinished) {
            var xAxisInput = Input.GetAxis("Mouse X");
            Quaternion camTurnAngle = Quaternion.AngleAxis(xAxisInput * cameraRotationSpeed, Vector3.up);
            offset = camTurnAngle * offset;
            rotationX += cameraRotationSpeed * xAxisInput;
            Vector3 targetTurn = new Vector3(transform.eulerAngles.x, rotationX, transform.eulerAngles.z);
            transform.eulerAngles = targetTurn;
            if (beforePos != target.transform.position) {
                target.eulerAngles = new Vector3(target.eulerAngles.x, targetTurn.y, target.eulerAngles.z);
            }
            beforePos = target.transform.position;
        }
    }
    void Update() {
        Vector3 cameraMoveDirection = Vector3.zero;
        RaycastHit hitPoint;
        float distance = 0;
        if (Physics.Linecast(transform.position, target.position, out hitPoint)) {
            cameraMoveDirection = hitPoint.point - transform.position;
            distance = hitPoint.distance > 4 ? 4 : hitPoint.distance;
        }
        targetPos = target.position + offset + (cameraMoveDirection / cameraMoveDirection.magnitude) * distance;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

}
