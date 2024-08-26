using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoofusController : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        float xDirection=Input.GetAxis("Horizontal");
        float zDirection=Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(xDirection,0.0f,zDirection);
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}