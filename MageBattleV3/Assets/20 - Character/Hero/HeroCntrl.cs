using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroCntrl : MonoBehaviour
{
    [SerializeField] private float maximumSpeed = 2.0f;
    [SerializeField] private float rotationSpeed = 400.0f;

    private Vector2 playerMove;

    private Vector3 moveDirection;

    private CharacterController charCntrl;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        charCntrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement(Time.deltaTime);
    }

    private void PlayerMovement(float dt)
    {
        moveDirection.x = playerMove.x; // Horizontal
        moveDirection.y = 0.0f;
        moveDirection.z = playerMove.y; // Vertical

        float inputMagnitude = Mathf.Clamp01(moveDirection.magnitude);

        animator.SetFloat("Speed", inputMagnitude, 0.05f, dt);

        moveDirection.Normalize();

        if (moveDirection != Vector3.zero)
        {
            Vector3 velocity = inputMagnitude * maximumSpeed * moveDirection;

            charCntrl.Move(velocity * dt);

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * dt);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMove = context.ReadValue<Vector2>();
        Debug.Log("OnMove ...");
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("OnFire ...");
    }
}
