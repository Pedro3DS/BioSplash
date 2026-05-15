using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.Windows;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    Vector3 movement;
    public float speed = 5f;
    public float turningSpeed = 0.1f;
    public float grabRadius = 1f;
    [SerializeField] private Transform grabPoint;
    CharacterController charC;
    public GameObject grabbedObj;
    public bool hasObject;

    void Start()
    {
        charC = GetComponent<CharacterController>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.performed ? context.ReadValue<Vector2>() : Vector2.zero;
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
                IInteractable interactable = FindNearestObject<IInteractable>();
                if (interactable != null) interactable.Interact(this);
        }
    }
    void Update()
    {
        Vector3 fixedMovement = new Vector3(movement.x, 0, movement.y);
        charC.Move(fixedMovement * speed * Time.deltaTime);
        if (fixedMovement != Vector3.zero){
        float angle = Mathf.Atan2(fixedMovement.x, fixedMovement.z) * Mathf.Rad2Deg;
        Quaternion newRot = Quaternion.AngleAxis(angle, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, newRot, turningSpeed);
        }
    }


    public T FindNearestObject<T>() where T : IInteractable
    {
        var ObjectsOfType = FindObjectsOfType<MonoBehaviour>().OfType<T>().ToList();
        T nearestObject = default(T);
        float nearestDistance = Mathf.Infinity;
        foreach (T item in ObjectsOfType)
        {
            var obj = item as MonoBehaviour;

            float distance = Vector3.Distance(grabPoint.position, obj.transform.position);
            if (distance < grabRadius && distance < nearestDistance)
            {
                //Debug.Log(distance + " " + grabRadius);
                if (obj.gameObject != grabbedObj)
                {
                    nearestObject = item;
                    nearestDistance = distance;
                }
            }
        }
       // Debug.Log(nearestObject);
        return nearestObject;
    }

    public void GrabObject(GameObject go)
    {
            grabbedObj = go;
            go.transform.parent = grabPoint;
            go.transform.position = grabPoint.position;
            hasObject = true;
    }

   public void DropObject()
    {
        if (grabbedObj != null)
        {
            grabbedObj = null;
            hasObject = false;
        }
    }
}
