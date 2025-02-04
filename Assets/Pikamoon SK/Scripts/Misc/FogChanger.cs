using Unity.VisualScripting;
using UnityEngine;

public class FogChanger : MonoBehaviour
{

    public LayerMask WaterLayer;

    [SerializeField] Color UnderWaterFogColor;
    Color DefaultFogColor;

    [SerializeField] float UnderWaterFogIntensity;
    float DefaultFogIntensity;



    bool FogAllowed;
    private void Start()
    {
        FogAllowed = RenderSettings.fog;

        DefaultFogColor = RenderSettings.fogColor;
        DefaultFogIntensity = RenderSettings.fogDensity;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("MainCamera"))
        {
            return;
        }

        if (!RenderSettings.fog)
        {
            RenderSettings.fog = true;
        }


        RenderSettings.fogColor = UnderWaterFogColor;
        RenderSettings.fogDensity = UnderWaterFogIntensity;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("MainCamera"))
        {
            return;
        }

        if (!FogAllowed)
        {
            RenderSettings.fog = false;
        }

        RenderSettings.fogColor = DefaultFogColor;
        RenderSettings.fogDensity = DefaultFogIntensity;
    }
}

