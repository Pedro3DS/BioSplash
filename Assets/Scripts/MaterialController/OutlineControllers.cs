using Unity.VisualScripting;
using UnityEngine;

public class OutlineControllers : MonoBehaviour
{
    public GameObject[] objectsToOutline;

    public Material materialOutline;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter Check Outline");
        // if (other.gameObject.CompareTag("PlayerOutlineCheck"))
        // {
        //     Debug.Log("Enter Check Outline");
        //     foreach (GameObject obj in objectsToOutline)
        //     {
        //         MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        //         // 1. Extract the current materials array copy
        //         Material[] materialsArray = renderer.materials;

        //         // 2. Modify the specific slot (e.g., index 0)
        //         materialsArray[0] = materialOutline;

        //         // 3. Assign the updated array back to the MeshRenderer
        //         renderer.materials = materialsArray;
        //     }
        // }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter Check Outline");
    }

    void OnTriggerStay(Collider other)
    {

    }
    

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerOutlineCheck"))
        {
            Debug.Log("Enter Check Outline");
            foreach (GameObject obj in objectsToOutline)
            {
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
                // 1. Extract the current materials array copy
                Material[] materialsArray = renderer.materials;

                // 2. Modify the specific slot (e.g., index 0)
                materialsArray[0] = materialOutline;

                // 3. Assign the updated array back to the MeshRenderer
                renderer.materials = materialsArray;
            }
        }
    }
}
