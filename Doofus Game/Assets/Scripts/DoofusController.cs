using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoofusController : MonoBehaviour
{
    private const string WALK_ANIM_PARAMETER = "Walk";
    public float speed = 10f;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    void Update()
    {
        float xDirection=Input.GetAxis("Horizontal");
        float zDirection=Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(xDirection,0.0f,zDirection);
        
        transform.position += moveDirection * speed * Time.deltaTime;

        bool isMoving = xDirection != 0 || zDirection != 0;

        if (isMoving) transform.rotation = Quaternion.LookRotation(moveDirection);

        animator.speed = isMoving ? 1f : 0f;
    }
}
