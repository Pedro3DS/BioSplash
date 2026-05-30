using UnityEngine;

public class ScreenShaderController : MonoBehaviour
{
    // Assign the material that is plugged into your Renderer Feature
    [SerializeField] private Material targetMaterial; 
    
    [Range(0f, 1f)]
    public float targetIntensity = 0f;
    public float changeSpeed = 5f;

    // Cache the shader property ID for better performance
    private int intensityID;
    private float currentIntensity = 0f;

    void Start()
    {
        if (targetMaterial != null)
        {
            // Must match the exact Reference name in your Shader Graph Blackboard
            intensityID = Shader.PropertyToID("_Intensity"); 
        }
        else
        {
            Debug.LogError("Please assign the Screen Effect Material to this controller.");
        }
    }

    void Update()
    {
        if (targetMaterial == null) return;

        // Smoothly interpolate the intensity value over time
        currentIntensity = Mathf.MoveTowards(currentIntensity, targetIntensity, changeSpeed * Time.deltaTime);
        
        // Update the shader property
        targetMaterial.SetFloat(intensityID, currentIntensity);
    }

    // Example function you can call from external scripts
    public void TriggerEffect(float intensity)
    {
        targetIntensity = intensity;
    }
}
