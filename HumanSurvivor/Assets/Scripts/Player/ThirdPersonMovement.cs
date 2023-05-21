using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Vector3 dir;
    public float speed = 6f;
    private float rotVelocity;
    public float smoothAmount = 0.1f;
    public bool stealth = false;

    public void Start()
    {
        TryGetComponent(out controller);
    }

    public void Update()
    {
        if (GameManager.OnlyInstance.gameStates == GameStates.GameOver) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        dir = Vector3.zero;

        dir.x = horizontal;
        dir.z = vertical;
        dir = dir.normalized;

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float currentAngle = transform.eulerAngles.y;

            float actualAnlge = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref rotVelocity, smoothAmount);

            Vector3 anglesForLooking = Vector3.up * actualAnlge;
            transform.eulerAngles = anglesForLooking;

            Vector3 anglesForMov = Vector3.up * targetAngle;
            var movDir = Quaternion.Euler(anglesForMov) * Vector3.forward;
            controller.Move(movDir.normalized * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 newScale = Vector3.one + Vector3.up * -0.5f;
            //transform.localScale = newScale;
            stealth = true;
        }
        else
        {
            transform.localScale = Vector3.one;
            stealth = false;
        }

    }
}