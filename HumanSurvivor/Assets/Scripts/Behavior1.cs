using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior1 : MonoBehaviour
{
    public float speed = 0; 

    // Update is called once per frame
    void Update()
    {
        Debug.Log("I'M WRITTING THE NEXT LINE");
        float MoveZ = Input.GetAxisRaw("Vertical");
        // transform.Translate(new Vector3(MoveX, 0, 0));


        float MoveX = Input.GetAxisRaw("Horizontal");
        Vector3 movVector1 = Vector3.zero;

        movVector1.z = MoveZ;
        movVector1.x = MoveX;
        movVector1= movVector1.normalized;

        transform.position += movVector1 * Time.deltaTime * speed;

    }

}

