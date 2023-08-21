using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThirdPersonMovement : MonoBehaviour
{
    AIPlayerController playerController;
    public CharacterController controller;
    public Transform cam;
    public Vector3 dir;
    public List<FloatModifier> speedModifiers = new List<FloatModifier>();
    public float speed => playerController.characterFeatures.Speed;

    private float rotVelocity;
    public float smoothAmount = 0.1f;
    public bool stealth = false;

    public void Start()
    {
        TryGetComponent(out controller);
        TryGetComponent(out playerController);
    }

    public float GetSpeed()
    {
        float totalValue = speed;
        for (int i = 0; i < speedModifiers.Count; i++)
        {
            totalValue = speedModifiers[i].Operate(totalValue);

        }
        return totalValue;
    }
    public void AddModifier(FloatModifier id)
    {
        speedModifiers.Add(id);
    }

    public void RemoveModifier(FloatModifier id)
    {
        speedModifiers.Remove(id);
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
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;// + cam.eulerAngles.y;
            float currentAngle = transform.eulerAngles.y;

            float actualAnlge = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref rotVelocity, smoothAmount);

            Vector3 anglesForLooking = Vector3.up * actualAnlge;
            transform.eulerAngles = anglesForLooking;

            Vector3 anglesForMov = Vector3.up * targetAngle;
            var movDir = Quaternion.Euler(anglesForMov) * Vector3.forward;
            //Changed
            controller.Move(movDir.normalized * GetSpeed() * Time.deltaTime);
        }
    }

}
[Serializable]
public class FloatModifier
{
    public string id;
    public Operations operation;
    public float value;

    public float Operate(float baseValue)
    {
        return operation.Operate(baseValue, value);
    }
}