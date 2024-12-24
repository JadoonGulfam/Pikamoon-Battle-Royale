//Copyright Awesome Technologies https://www.awesometech.no/

void InjectSetup_float(float3 A, out float3 Out)
{
	Out = A;
}

#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
    #if defined(SHADER_API_D3D11) || defined(SHADER_API_VULKAN) || defined(SHADER_API_METAL) || defined(SHADER_API_GLCORE) || defined(SHADER_API_GLES3) || defined(SHADER_API_PS4) || defined(SHADER_API_PS5) || defined(SHADER_API_XBOXONE) || defined(SHADER_API_SWITCH)
        struct IndirectShaderData
        {
        	float4x4 PositionMatrix;
        	float4x4 InversePositionMatrix;
        	float4 ControlData;
        };
        uniform StructuredBuffer<IndirectShaderData> VisibleShaderDataBuffer;
    #endif
#endif

void setupVSPro()
{
#if defined(UNITY_PROCEDURAL_INSTANCING_ENABLED)
    #if defined(SHADER_API_D3D11) || defined(SHADER_API_VULKAN) || defined(SHADER_API_METAL) || defined(SHADER_API_GLCORE) || defined(SHADER_API_GLES3) || defined(SHADER_API_PS4) || defined(SHADER_API_PS5) || defined(SHADER_API_XBOXONE) || defined(SHADER_API_SWITCH)
	    #undef unity_ObjectToWorld
        #undef unity_WorldToObject
        unity_LODFade.x = VisibleShaderDataBuffer[unity_InstanceID].ControlData.x;
	    unity_ObjectToWorld = VisibleShaderDataBuffer[unity_InstanceID].PositionMatrix;
	    unity_WorldToObject = VisibleShaderDataBuffer[unity_InstanceID].InversePositionMatrix;
        #if SHADERPASS == SHADERPASS_MOTION_VECTORS && defined(SHADERPASS_CS_HLSL)
            unity_MatrixPreviousM = unity_ObjectToWorld;
            unity_MatrixPreviousMI = unity_WorldToObject;
        #endif
    #endif
#endif
}