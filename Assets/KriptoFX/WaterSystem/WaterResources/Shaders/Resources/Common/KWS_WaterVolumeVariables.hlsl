//
// This file was automatically generated. Please don't edit by hand. Execute Editor command [ Edit > Rendering > Generate Shader Includes ] instead
//

#ifndef KWS_WATERVOLUMEVARIABLES_HLSL
#define KWS_WATERVOLUMEVARIABLES_HLSL
// Generated from KWS.KWS_WaterVolumeVariables+WaterVolumeData
// PackingRules = Exact
struct WaterVolumeData
{
    float3 Position;
    float WaterLevel;
    float4x4 Rotation;
    float3 Size;
    uint InstanceID;
    float Transparent;
    float4 WaterColor; // x: r y: g z: b w: a 
    float4 TurbidityColor; // x: r y: g z: b w: a 
    float Turbidity;
    float VolumetricLightDirLightIntensityMultiplier;
    float VolumetricLightAdditionalLightsIntensityMultiplier;
    float VolumetricLightOverWaterCausticStrength;
    float VolumetricLightUnderWaterCausticStrength;
};


#endif
