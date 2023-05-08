using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPLAYER : MonoBehaviour
{
    public CharacterController cc;
    public float Speed = 10;
    // Update is called once per frame
    void Update()
    {
        var movX = Input.GetAxis("Horizontal");
        var movZ = Input.GetAxis("Vertical");

        Vector3 mov = transform.forward * movZ * Speed * Time.deltaTime;
        mov += transform.right * movX * Speed * Time.deltaTime;

        cc.Move(mov);
    }
}
