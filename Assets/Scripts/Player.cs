using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player controller for Overcooked 2 like gameplay.
/// </summary>

public class Player : MonoBehaviour
{

    public float speed = 15f;
    private Vector3 axis;
    private Rigidbody rb;


    #region Unity Life Cycle

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }


    void Update()
    {

    }

    void FixedUpdate()
    {
        // PlayerMovement();
        
    }

    void LateUpdate()
    {
        RotatePlayer();
    }

    #endregion

    #region Private Methods
    public void PlayerMovement(InputAction.CallbackContext context)
    {
        axis = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
        rb.linearVelocity = axis.normalized * speed;

    }

    private void RotatePlayer()
    {
        if (axis != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(axis, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360 * Time.deltaTime);
        }
    }

    #endregion

    public void OnMove(InputAction.CallbackContext context)
    {
        // PlayerMovement(context.ReadValue<Vector2>());
    }

}
