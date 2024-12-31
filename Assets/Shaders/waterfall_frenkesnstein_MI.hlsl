#define NUM_TEX_COORD_INTERPOLATORS 1
#define NUM_MATERIAL_TEXCOORDS_VERTEX 1
#define NUM_CUSTOM_VERTEX_INTERPOLATORS 0

struct Input
{
	//float3 Normal;
	float2 uv_MainTex : TEXCOORD0;
	float2 uv2_Material_Texture2D_0 : TEXCOORD1;
	float4 color : COLOR;
	float4 tangent;
	//float4 normal;
	float3 viewDir;
	float4 screenPos;
	float3 worldPos;
	//float3 worldNormal;
	float3 normal2;
};
struct SurfaceOutputStandard
{
	float3 Albedo;		// base (diffuse or specular) color
	float3 Normal;		// tangent space normal, if written
	half3 Emission;
	half Metallic;		// 0=non-metal, 1=metal
	// Smoothness is the user facing name, it should be perceptual smoothness but user should not have to deal with it.
	// Everywhere in the code you meet smoothness it is perceptual smoothness
	half Smoothness;	// 0=rough, 1=smooth
	half Occlusion;		// occlusion (default 1)
	float Alpha;		// alpha for transparencies
};

//#define HDRP 1
#define URP 1
#define UE5
//#define HAS_CUSTOMIZED_UVS 1
#define MATERIAL_TANGENTSPACENORMAL 1
//struct Material
//{
	//samplers start
SAMPLER( SamplerState_Linear_Repeat );
SAMPLER( SamplerState_Linear_Clamp );
TEXTURE2D(       Material_Texture2D_0 );
SAMPLER(  samplerMaterial_Texture2D_0 );
float4 Material_Texture2D_0_TexelSize;
float4 Material_Texture2D_0_ST;
TEXTURE2D(       Material_Texture2D_1 );
SAMPLER(  samplerMaterial_Texture2D_1 );
float4 Material_Texture2D_1_TexelSize;
float4 Material_Texture2D_1_ST;
TEXTURE2D(       Material_Texture2D_2 );
SAMPLER(  samplerMaterial_Texture2D_2 );
float4 Material_Texture2D_2_TexelSize;
float4 Material_Texture2D_2_ST;
TEXTURE2D(       Material_Texture2D_3 );
SAMPLER(  samplerMaterial_Texture2D_3 );
float4 Material_Texture2D_3_TexelSize;
float4 Material_Texture2D_3_ST;
TEXTURE2D(       Material_Texture2D_4 );
SAMPLER(  samplerMaterial_Texture2D_4 );
float4 Material_Texture2D_4_TexelSize;
float4 Material_Texture2D_4_ST;

//};

#ifdef UE5
	#define UE_LWC_RENDER_TILE_SIZE			2097152.0
	#define UE_LWC_RENDER_TILE_SIZE_SQRT	1448.15466
	#define UE_LWC_RENDER_TILE_SIZE_RSQRT	0.000690533954
	#define UE_LWC_RENDER_TILE_SIZE_RCP		4.76837158e-07
	#define UE_LWC_RENDER_TILE_SIZE_FMOD_PI		0.673652053
	#define UE_LWC_RENDER_TILE_SIZE_FMOD_2PI	0.673652053
	#define INVARIANT(X) X
	#define PI 					(3.1415926535897932)

	#include "LargeWorldCoordinates.hlsl"
#endif
struct MaterialStruct
{
	float4 PreshaderBuffer[17];
	float4 ScalarExpressions[1];
	float VTPackedPageTableUniform[2];
	float VTPackedUniform[1];
};
static SamplerState View_MaterialTextureBilinearWrapedSampler;
static SamplerState View_MaterialTextureBilinearClampedSampler;
struct ViewStruct
{
	float GameTime;
	float RealTime;
	float DeltaTime;
	float PrevFrameGameTime;
	float PrevFrameRealTime;
	float MaterialTextureMipBias;	
	float4 PrimitiveSceneData[ 40 ];
	float4 TemporalAAParams;
	float2 ViewRectMin;
	float4 ViewSizeAndInvSize;
	float MaterialTextureDerivativeMultiply;
	uint StateFrameIndexMod8;
	float FrameNumber;
	float2 FieldOfViewWideAngles;
	float4 RuntimeVirtualTextureMipLevel;
	float PreExposure;
	float4 BufferBilinearUVMinMax;
};
struct ResolvedViewStruct
{
	#ifdef UE5
		FLWCVector3 WorldCameraOrigin;
		FLWCVector3 PrevWorldCameraOrigin;
		FLWCVector3 PreViewTranslation;
		FLWCVector3 WorldViewOrigin;
	#else
		float3 WorldCameraOrigin;
		float3 PrevWorldCameraOrigin;
		float3 PreViewTranslation;
		float3 WorldViewOrigin;
	#endif
	float4 ScreenPositionScaleBias;
	float4x4 TranslatedWorldToView;
	float4x4 TranslatedWorldToCameraView;
	float4x4 TranslatedWorldToClip;
	float4x4 ViewToTranslatedWorld;
	float4x4 PrevViewToTranslatedWorld;
	float4x4 CameraViewToTranslatedWorld;
	float4 BufferBilinearUVMinMax;
	float4 XRPassthroughCameraUVs[ 2 ];
};
struct PrimitiveStruct
{
	float4x4 WorldToLocal;
	float4x4 LocalToWorld;
};

static ViewStruct View;
static ResolvedViewStruct ResolvedView;
static PrimitiveStruct Primitive;
uniform float4 View_BufferSizeAndInvSize;
uniform float4 LocalObjectBoundsMin;
uniform float4 LocalObjectBoundsMax;
static SamplerState Material_Wrap_WorldGroupSettings;
static SamplerState Material_Clamp_WorldGroupSettings;

#include "UnrealCommon.cginc"

static MaterialStruct Material;
void InitializeExpressions()
{
	Material.PreshaderBuffer[0] = float4(-0.010000,-0.000488,-0.040000,0.010000);//(Unknown)
	Material.PreshaderBuffer[1] = float4(-0.000651,0.040000,0.040000,-0.000391);//(Unknown)
	Material.PreshaderBuffer[2] = float4(0.010000,-0.040000,-0.000326,-0.010000);//(Unknown)
	Material.PreshaderBuffer[3] = float4(0.400000,0.000000,1.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[4] = float4(1.000000,1.000000,0.200000,0.000000);//(Unknown)
	Material.PreshaderBuffer[5] = float4(0.000000,0.000000,0.000000,0.500000);//(Unknown)
	Material.PreshaderBuffer[6] = float4(3.000000,-2.000000,2.000000,-1.000000);//(Unknown)
	Material.PreshaderBuffer[7] = float4(0.007843,0.035294,0.035294,0.000000);//(Unknown)
	Material.PreshaderBuffer[8] = float4(0.015000,0.138750,0.150000,-0.000651);//(Unknown)
	Material.PreshaderBuffer[9] = float4(-0.000488,-0.000391,-0.000326,0.750000);//(Unknown)
	Material.PreshaderBuffer[10] = float4(0.000651,2.578125,4.289887,5.000000);//(Unknown)
	Material.PreshaderBuffer[11] = float4(0.000000,-0.500000,4.000000,2.000000);//(Unknown)
	Material.PreshaderBuffer[12] = float4(1.000000,0.500000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[13] = float4(1.000000,0.380000,0.390333,0.000000);//(Unknown)
	Material.PreshaderBuffer[14] = float4(0.600000,0.866666,1.000000,0.020000);//(Unknown)
	Material.PreshaderBuffer[15] = float4(1.100000,0.080000,2.000000,-1.000000);//(Unknown)
	Material.PreshaderBuffer[16] = float4(0.013333,1.000000,0.750000,0.000000);//(Unknown)
}float3 GetMaterialWorldPositionOffset(FMaterialVertexParameters Parameters)
{
	return MaterialFloat3(0.00000000,0.00000000,0.00000000);;
}
void CalcPixelMaterialInputs(in out FMaterialPixelParameters Parameters, in out FPixelMaterialInputs PixelMaterialInputs)
{
	//WorldAligned texturing & others use normals & stuff that think Z is up
	Parameters.TangentToWorld[0] = Parameters.TangentToWorld[0].xzy;
	Parameters.TangentToWorld[1] = Parameters.TangentToWorld[1].xzy;
	Parameters.TangentToWorld[2] = Parameters.TangentToWorld[2].xzy;

	float3 WorldNormalCopy = Parameters.WorldNormal;

	// Initial calculations (required for Normal)
	MaterialFloat Local0 = (View.GameTime * Material.PreshaderBuffer[0].x);
	FWSVector3 Local1 = GetWorldPosition(Parameters);
	FWSVector3 Local2 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local1)), WSGetY(DERIV_BASE_VALUE(Local1)), WSGetZ(DERIV_BASE_VALUE(Local1)));
	FWSVector3 Local3 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[0].y));
	FWSVector2 Local4 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local3)), WSGetZ(DERIV_BASE_VALUE(Local3)));
	FWSVector2 Local5 = WSAdd(MaterialFloat2(Local0,0.00000000), DERIV_BASE_VALUE(Local4));
	MaterialFloat Local6 = (View.GameTime * Material.PreshaderBuffer[0].z);
	FWSVector2 Local7 = WSAdd(MaterialFloat2(0.00000000,Local6), DERIV_BASE_VALUE(Local4));
	FWSVector2 Local8 = WSAdd(DERIV_BASE_VALUE(Local5), DERIV_BASE_VALUE(Local7));
	MaterialFloat2 Local9 = WSApplyAddressMode(DERIV_BASE_VALUE(Local8), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local10 = MaterialStoreTexCoordScale(Parameters, Local9, 0);
	MaterialFloat4 Local11 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local9,View.MaterialTextureMipBias));
	MaterialFloat Local12 = MaterialStoreTexSample(Parameters, Local11, 0);
	FWSVector2 Local13 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local3)), WSGetZ(DERIV_BASE_VALUE(Local3)));
	FWSVector2 Local14 = WSAdd(MaterialFloat2(Local0,0.00000000), DERIV_BASE_VALUE(Local13));
	FWSVector2 Local15 = WSAdd(MaterialFloat2(0.00000000,Local6), DERIV_BASE_VALUE(Local13));
	FWSVector2 Local16 = WSAdd(DERIV_BASE_VALUE(Local14), DERIV_BASE_VALUE(Local15));
	MaterialFloat2 Local17 = WSApplyAddressMode(DERIV_BASE_VALUE(Local16), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local18 = MaterialStoreTexCoordScale(Parameters, Local17, 0);
	MaterialFloat4 Local19 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local17,View.MaterialTextureMipBias));
	MaterialFloat Local20 = MaterialStoreTexSample(Parameters, Local19, 0);
	MaterialFloat Local21 = abs(Parameters.TangentToWorld[2].r);
	MaterialFloat Local22 = lerp((0.00000000 - 0.00000000),(0.00000000 + 1.00000000),Local21);
	MaterialFloat Local23 = saturate(Local22);
	MaterialFloat3 Local24 = lerp(Local11.rgb,Local19.rgb,Local23.r.r);
	FWSVector2 Local25 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local3)), WSGetY(DERIV_BASE_VALUE(Local3)));
	FWSVector2 Local26 = WSAdd(MaterialFloat2(Local0,0.00000000), DERIV_BASE_VALUE(Local25));
	FWSVector2 Local27 = WSAdd(MaterialFloat2(0.00000000,Local6), DERIV_BASE_VALUE(Local25));
	FWSVector2 Local28 = WSAdd(DERIV_BASE_VALUE(Local26), DERIV_BASE_VALUE(Local27));
	MaterialFloat2 Local29 = WSApplyAddressMode(DERIV_BASE_VALUE(Local28), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local30 = MaterialStoreTexCoordScale(Parameters, Local29, 0);
	MaterialFloat4 Local31 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local29,View.MaterialTextureMipBias));
	MaterialFloat Local32 = MaterialStoreTexSample(Parameters, Local31, 0);
	MaterialFloat Local33 = abs(Parameters.TangentToWorld[2].b);
	MaterialFloat Local34 = lerp((0.00000000 - 0.00000000),(0.00000000 + 1.00000000),Local33);
	MaterialFloat Local35 = saturate(Local34);
	MaterialFloat3 Local36 = lerp(Local24,Local31.rgb,Local35.r.r);
	MaterialFloat Local37 = (Local36.b + 1.00000000);
	MaterialFloat Local38 = (View.GameTime * Material.PreshaderBuffer[0].w);
	FWSVector3 Local39 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[1].x));
	FWSVector2 Local40 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local39)), WSGetZ(DERIV_BASE_VALUE(Local39)));
	FWSVector2 Local41 = WSAdd(MaterialFloat2(Local38,0.00000000), DERIV_BASE_VALUE(Local40));
	MaterialFloat Local42 = (View.GameTime * Material.PreshaderBuffer[1].y);
	FWSVector2 Local43 = WSAdd(MaterialFloat2(0.00000000,Local42), DERIV_BASE_VALUE(Local40));
	FWSVector2 Local44 = WSAdd(DERIV_BASE_VALUE(Local41), DERIV_BASE_VALUE(Local43));
	MaterialFloat2 Local45 = WSApplyAddressMode(DERIV_BASE_VALUE(Local44), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local46 = MaterialStoreTexCoordScale(Parameters, Local45, 0);
	MaterialFloat4 Local47 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local45,View.MaterialTextureMipBias));
	MaterialFloat Local48 = MaterialStoreTexSample(Parameters, Local47, 0);
	FWSVector2 Local49 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local39)), WSGetZ(DERIV_BASE_VALUE(Local39)));
	FWSVector2 Local50 = WSAdd(MaterialFloat2(Local38,0.00000000), DERIV_BASE_VALUE(Local49));
	FWSVector2 Local51 = WSAdd(MaterialFloat2(0.00000000,Local42), DERIV_BASE_VALUE(Local49));
	FWSVector2 Local52 = WSAdd(DERIV_BASE_VALUE(Local50), DERIV_BASE_VALUE(Local51));
	MaterialFloat2 Local53 = WSApplyAddressMode(DERIV_BASE_VALUE(Local52), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local54 = MaterialStoreTexCoordScale(Parameters, Local53, 0);
	MaterialFloat4 Local55 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local53,View.MaterialTextureMipBias));
	MaterialFloat Local56 = MaterialStoreTexSample(Parameters, Local55, 0);
	MaterialFloat3 Local57 = lerp(Local47.rgb,Local55.rgb,Local23.r.r);
	FWSVector2 Local58 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local39)), WSGetY(DERIV_BASE_VALUE(Local39)));
	FWSVector2 Local59 = WSAdd(MaterialFloat2(Local38,0.00000000), DERIV_BASE_VALUE(Local58));
	FWSVector2 Local60 = WSAdd(MaterialFloat2(0.00000000,Local42), DERIV_BASE_VALUE(Local58));
	FWSVector2 Local61 = WSAdd(DERIV_BASE_VALUE(Local59), DERIV_BASE_VALUE(Local60));
	MaterialFloat2 Local62 = WSApplyAddressMode(DERIV_BASE_VALUE(Local61), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local63 = MaterialStoreTexCoordScale(Parameters, Local62, 0);
	MaterialFloat4 Local64 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local62,View.MaterialTextureMipBias));
	MaterialFloat Local65 = MaterialStoreTexSample(Parameters, Local64, 0);
	MaterialFloat3 Local66 = lerp(Local57,Local64.rgb,Local35.r.r);
	MaterialFloat2 Local67 = (Local66.rg * ((MaterialFloat2)-1.00000000));
	MaterialFloat Local68 = dot(MaterialFloat3(Local36.rg,Local37),MaterialFloat3(Local67,Local66.b));
	MaterialFloat3 Local69 = (MaterialFloat3(Local36.rg,Local37) * ((MaterialFloat3)Local68));
	MaterialFloat3 Local70 = (((MaterialFloat3)Local37) * MaterialFloat3(Local67,Local66.b));
	MaterialFloat3 Local71 = (Local69 - Local70);
	MaterialFloat Local72 = (Local71.b + 1.00000000);
	MaterialFloat Local73 = (View.GameTime * Material.PreshaderBuffer[1].z);
	FWSVector3 Local74 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[1].w));
	FWSVector2 Local75 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local74)), WSGetZ(DERIV_BASE_VALUE(Local74)));
	FWSVector2 Local76 = WSAdd(MaterialFloat2(Local73,0.00000000), DERIV_BASE_VALUE(Local75));
	MaterialFloat Local77 = (View.GameTime * Material.PreshaderBuffer[2].x);
	FWSVector2 Local78 = WSAdd(MaterialFloat2(0.00000000,Local77), DERIV_BASE_VALUE(Local75));
	FWSVector2 Local79 = WSAdd(DERIV_BASE_VALUE(Local76), DERIV_BASE_VALUE(Local78));
	MaterialFloat2 Local80 = WSApplyAddressMode(DERIV_BASE_VALUE(Local79), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local81 = MaterialStoreTexCoordScale(Parameters, Local80, 0);
	MaterialFloat4 Local82 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local80,View.MaterialTextureMipBias));
	MaterialFloat Local83 = MaterialStoreTexSample(Parameters, Local82, 0);
	FWSVector2 Local84 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local74)), WSGetZ(DERIV_BASE_VALUE(Local74)));
	FWSVector2 Local85 = WSAdd(MaterialFloat2(Local73,0.00000000), DERIV_BASE_VALUE(Local84));
	FWSVector2 Local86 = WSAdd(MaterialFloat2(0.00000000,Local77), DERIV_BASE_VALUE(Local84));
	FWSVector2 Local87 = WSAdd(DERIV_BASE_VALUE(Local85), DERIV_BASE_VALUE(Local86));
	MaterialFloat2 Local88 = WSApplyAddressMode(DERIV_BASE_VALUE(Local87), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local89 = MaterialStoreTexCoordScale(Parameters, Local88, 0);
	MaterialFloat4 Local90 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local88,View.MaterialTextureMipBias));
	MaterialFloat Local91 = MaterialStoreTexSample(Parameters, Local90, 0);
	MaterialFloat3 Local92 = lerp(Local82.rgb,Local90.rgb,Local23.r.r);
	FWSVector2 Local93 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local74)), WSGetY(DERIV_BASE_VALUE(Local74)));
	FWSVector2 Local94 = WSAdd(MaterialFloat2(Local73,0.00000000), DERIV_BASE_VALUE(Local93));
	FWSVector2 Local95 = WSAdd(MaterialFloat2(0.00000000,Local77), DERIV_BASE_VALUE(Local93));
	FWSVector2 Local96 = WSAdd(DERIV_BASE_VALUE(Local94), DERIV_BASE_VALUE(Local95));
	MaterialFloat2 Local97 = WSApplyAddressMode(DERIV_BASE_VALUE(Local96), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local98 = MaterialStoreTexCoordScale(Parameters, Local97, 0);
	MaterialFloat4 Local99 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local97,View.MaterialTextureMipBias));
	MaterialFloat Local100 = MaterialStoreTexSample(Parameters, Local99, 0);
	MaterialFloat3 Local101 = lerp(Local92,Local99.rgb,Local35.r.r);
	MaterialFloat Local102 = (Local101.b + 1.00000000);
	MaterialFloat Local103 = (View.GameTime * Material.PreshaderBuffer[2].y);
	FWSVector3 Local104 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[2].z));
	FWSVector2 Local105 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local104)), WSGetZ(DERIV_BASE_VALUE(Local104)));
	FWSVector2 Local106 = WSAdd(MaterialFloat2(Local103,0.00000000), DERIV_BASE_VALUE(Local105));
	MaterialFloat Local107 = (View.GameTime * Material.PreshaderBuffer[2].w);
	FWSVector2 Local108 = WSAdd(MaterialFloat2(0.00000000,Local107), DERIV_BASE_VALUE(Local105));
	FWSVector2 Local109 = WSAdd(DERIV_BASE_VALUE(Local106), DERIV_BASE_VALUE(Local108));
	MaterialFloat2 Local110 = WSApplyAddressMode(DERIV_BASE_VALUE(Local109), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local111 = MaterialStoreTexCoordScale(Parameters, Local110, 0);
	MaterialFloat4 Local112 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local110,View.MaterialTextureMipBias));
	MaterialFloat Local113 = MaterialStoreTexSample(Parameters, Local112, 0);
	FWSVector2 Local114 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local104)), WSGetZ(DERIV_BASE_VALUE(Local104)));
	FWSVector2 Local115 = WSAdd(MaterialFloat2(Local103,0.00000000), DERIV_BASE_VALUE(Local114));
	FWSVector2 Local116 = WSAdd(MaterialFloat2(0.00000000,Local107), DERIV_BASE_VALUE(Local114));
	FWSVector2 Local117 = WSAdd(DERIV_BASE_VALUE(Local115), DERIV_BASE_VALUE(Local116));
	MaterialFloat2 Local118 = WSApplyAddressMode(DERIV_BASE_VALUE(Local117), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local119 = MaterialStoreTexCoordScale(Parameters, Local118, 0);
	MaterialFloat4 Local120 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local118,View.MaterialTextureMipBias));
	MaterialFloat Local121 = MaterialStoreTexSample(Parameters, Local120, 0);
	MaterialFloat3 Local122 = lerp(Local112.rgb,Local120.rgb,Local23.r.r);
	FWSVector2 Local123 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local104)), WSGetY(DERIV_BASE_VALUE(Local104)));
	FWSVector2 Local124 = WSAdd(MaterialFloat2(Local103,0.00000000), DERIV_BASE_VALUE(Local123));
	FWSVector2 Local125 = WSAdd(MaterialFloat2(0.00000000,Local107), DERIV_BASE_VALUE(Local123));
	FWSVector2 Local126 = WSAdd(DERIV_BASE_VALUE(Local124), DERIV_BASE_VALUE(Local125));
	MaterialFloat2 Local127 = WSApplyAddressMode(DERIV_BASE_VALUE(Local126), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local128 = MaterialStoreTexCoordScale(Parameters, Local127, 0);
	MaterialFloat4 Local129 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local127,View.MaterialTextureMipBias));
	MaterialFloat Local130 = MaterialStoreTexSample(Parameters, Local129, 0);
	MaterialFloat3 Local131 = lerp(Local122,Local129.rgb,Local35.r.r);
	MaterialFloat2 Local132 = (Local131.rg * ((MaterialFloat2)-1.00000000));
	MaterialFloat Local133 = dot(MaterialFloat3(Local101.rg,Local102),MaterialFloat3(Local132,Local131.b));
	MaterialFloat3 Local134 = (MaterialFloat3(Local101.rg,Local102) * ((MaterialFloat3)Local133));
	MaterialFloat3 Local135 = (((MaterialFloat3)Local102) * MaterialFloat3(Local132,Local131.b));
	MaterialFloat3 Local136 = (Local134 - Local135);
	MaterialFloat2 Local137 = (Local136.rg * ((MaterialFloat2)-1.00000000));
	MaterialFloat Local138 = dot(MaterialFloat3(Local71.rg,Local72),MaterialFloat3(Local137,Local136.b));
	MaterialFloat3 Local139 = (MaterialFloat3(Local71.rg,Local72) * ((MaterialFloat3)Local138));
	MaterialFloat3 Local140 = (((MaterialFloat3)Local72) * MaterialFloat3(Local137,Local136.b));
	MaterialFloat3 Local141 = (Local139 - Local140);
	FWSVector3 Local142 = WSSubtract(DERIV_BASE_VALUE(Local2), GetObjectWorldPosition(Parameters));
	MaterialFloat3 Local143 = WSDemote(DERIV_BASE_VALUE(Local142));
	MaterialFloat3 Local144 = normalize(DERIV_BASE_VALUE(Local143));
	MaterialFloat2 Local145 = DERIV_BASE_VALUE(Local144).rg;
	MaterialFloat Local146 = DERIV_BASE_VALUE(Local144).b;
	MaterialFloat Local147 = (DERIV_BASE_VALUE(Local146) + 1.00000000);
	MaterialFloat3 Local148 = MaterialFloat3(DERIV_BASE_VALUE(Local145),DERIV_BASE_VALUE(Local147));
	MaterialFloat Local149 = (View.GameTime * Material.PreshaderBuffer[3].x);
	MaterialFloat Local150 = (View.GameTime * Material.PreshaderBuffer[3].y);
	MaterialFloat2 Local151 = Parameters.TexCoords[0].xy;
	MaterialFloat2 Local152 = (DERIV_BASE_VALUE(Local151) * ((MaterialFloat2)Material.PreshaderBuffer[3].z));
	MaterialFloat2 Local153 = (MaterialFloat2(-0.50000000,-0.50000000) + DERIV_BASE_VALUE(Local152));
	MaterialFloat Local154 = dot(DERIV_BASE_VALUE(Local153),MaterialFloat2(cos(1.57079637),-1.00000000));
	MaterialFloat Local155 = dot(DERIV_BASE_VALUE(Local153),MaterialFloat2(sin(1.57079637),cos(1.57079637)));
	MaterialFloat2 Local156 = MaterialFloat2(DERIV_BASE_VALUE(Local154),DERIV_BASE_VALUE(Local155));
	MaterialFloat2 Local157 = (MaterialFloat2(0.50000000,0.50000000) + DERIV_BASE_VALUE(Local156));
	MaterialFloat2 Local158 = (MaterialFloat2(Local149,Local150) + DERIV_BASE_VALUE(Local157));
	MaterialFloat Local159 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local158), 0);
	MaterialFloat4 Local160 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,DERIV_BASE_VALUE(Local158),View.MaterialTextureMipBias));
	MaterialFloat Local161 = MaterialStoreTexSample(Parameters, Local160, 0);
	MaterialFloat3 Local162 = (Local160.rgb * Material.PreshaderBuffer[4].xyz);
	MaterialFloat2 Local163 = (Local162.rg * ((MaterialFloat2)-1.00000000));
	MaterialFloat Local164 = dot(DERIV_BASE_VALUE(Local148),MaterialFloat3(Local163,Local162.b));
	MaterialFloat3 Local165 = (DERIV_BASE_VALUE(Local148) * ((MaterialFloat3)Local164));
	MaterialFloat3 Local166 = (((MaterialFloat3)DERIV_BASE_VALUE(Local147)) * MaterialFloat3(Local163,Local162.b));
	MaterialFloat3 Local167 = (Local165 - Local166);
	MaterialFloat Local168 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local151), 3);
	MaterialFloat4 Local169 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,DERIV_BASE_VALUE(Local151),View.MaterialTextureMipBias));
	MaterialFloat Local170 = MaterialStoreTexSample(Parameters, Local169, 3);
	MaterialFloat3 Local171 = lerp(Local141,Local167,Local169.g);

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = Local171;


#if TEMPLATE_USES_SUBSTRATE
	Parameters.SubstratePixelFootprint = SubstrateGetPixelFootprint(Parameters.WorldPosition_CamRelative, GetRoughnessFromNormalCurvature(Parameters));
	Parameters.SharedLocalBases = SubstrateInitialiseSharedLocalBases();
	Parameters.SubstrateTree = GetInitialisedSubstrateTree();
#if SUBSTRATE_USE_FULLYSIMPLIFIED_MATERIAL == 1
	Parameters.SharedLocalBasesFullySimplified = SubstrateInitialiseSharedLocalBases();
	Parameters.SubstrateTreeFullySimplified = GetInitialisedSubstrateTree();
#endif
#endif

	// Note that here MaterialNormal can be in world space or tangent space
	float3 MaterialNormal = GetMaterialNormal(Parameters, PixelMaterialInputs);

#if MATERIAL_TANGENTSPACENORMAL

#if FEATURE_LEVEL >= FEATURE_LEVEL_SM4
	// Mobile will rely on only the final normalize for performance
	MaterialNormal = normalize(MaterialNormal);
#endif

	// normalizing after the tangent space to world space conversion improves quality with sheared bases (UV layout to WS causes shrearing)
	// use full precision normalize to avoid overflows
	Parameters.WorldNormal = TransformTangentNormalToWorld(Parameters.TangentToWorld, MaterialNormal);

#else //MATERIAL_TANGENTSPACENORMAL

	Parameters.WorldNormal = normalize(MaterialNormal);

#endif //MATERIAL_TANGENTSPACENORMAL

#if MATERIAL_TANGENTSPACENORMAL || TWO_SIDED_WORLD_SPACE_SINGLELAYERWATER_NORMAL
	// flip the normal for backfaces being rendered with a two-sided material
	Parameters.WorldNormal *= Parameters.TwoSidedSign;
#endif

	Parameters.ReflectionVector = ReflectionAboutCustomWorldNormal(Parameters, Parameters.WorldNormal, false);

#if !PARTICLE_SPRITE_FACTORY
	Parameters.Particle.MotionBlurFade = 1.0f;
#endif // !PARTICLE_SPRITE_FACTORY

	// Now the rest of the inputs
	MaterialFloat3 Local172 = lerp(MaterialFloat3(0.00000000,0.00000000,0.00000000),Material.PreshaderBuffer[5].xyz,Material.PreshaderBuffer[4].w);
	MaterialFloat Local173 = dot(WorldNormalCopy,Parameters.CameraVector);
	MaterialFloat Local174 = max(0.00000000,Local173);
	MaterialFloat Local175 = (1.00000000 - Local174);
	MaterialFloat Local176 = abs(Local175);
	MaterialFloat Local177 = max(Local176,0.00010000);
	MaterialFloat Local178 = PositiveClampedPow(Local177,Material.PreshaderBuffer[5].w);
	MaterialFloat Local179 = (Local178 * Material.PreshaderBuffer[6].x);
	MaterialFloat Local180 = (Local179 + Material.PreshaderBuffer[6].y);
	MaterialFloat Local181 = lerp(Material.PreshaderBuffer[6].w,Material.PreshaderBuffer[6].z,Local180);
	MaterialFloat Local182 = saturate(Local181);
	MaterialFloat3 Local183 = lerp(Material.PreshaderBuffer[8].xyz,Material.PreshaderBuffer[7].xyz,Local182.r);
	MaterialFloat Local184 = (View.GameTime * 0.01000000);
	FWSVector3 Local185 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[8].w));
	FWSVector2 Local186 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local185)), WSGetZ(DERIV_BASE_VALUE(Local185)));
	FWSVector2 Local187 = WSAdd(MaterialFloat2(Local184,0.00000000), DERIV_BASE_VALUE(Local186));
	MaterialFloat Local188 = (View.GameTime * 0.04000000);
	FWSVector2 Local189 = WSAdd(MaterialFloat2(0.00000000,Local188), DERIV_BASE_VALUE(Local186));
	FWSVector2 Local190 = WSAdd(DERIV_BASE_VALUE(Local187), DERIV_BASE_VALUE(Local189));
	MaterialFloat2 Local191 = WSApplyAddressMode(DERIV_BASE_VALUE(Local190), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local192 = MaterialStoreTexCoordScale(Parameters, Local191, 4);
	MaterialFloat4 Local193 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local191,View.MaterialTextureMipBias));
	MaterialFloat Local194 = MaterialStoreTexSample(Parameters, Local193, 4);
	FWSVector2 Local195 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local185)), WSGetZ(DERIV_BASE_VALUE(Local185)));
	FWSVector2 Local196 = WSAdd(MaterialFloat2(Local184,0.00000000), DERIV_BASE_VALUE(Local195));
	FWSVector2 Local197 = WSAdd(MaterialFloat2(0.00000000,Local188), DERIV_BASE_VALUE(Local195));
	FWSVector2 Local198 = WSAdd(DERIV_BASE_VALUE(Local196), DERIV_BASE_VALUE(Local197));
	MaterialFloat2 Local199 = WSApplyAddressMode(DERIV_BASE_VALUE(Local198), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local200 = MaterialStoreTexCoordScale(Parameters, Local199, 4);
	MaterialFloat4 Local201 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local199,View.MaterialTextureMipBias));
	MaterialFloat Local202 = MaterialStoreTexSample(Parameters, Local201, 4);
	MaterialFloat3 Local203 = lerp(Local193.rgb,Local201.rgb,Local23.r.r);
	FWSVector2 Local204 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local185)), WSGetY(DERIV_BASE_VALUE(Local185)));
	FWSVector2 Local205 = WSAdd(MaterialFloat2(Local184,0.00000000), DERIV_BASE_VALUE(Local204));
	FWSVector2 Local206 = WSAdd(MaterialFloat2(0.00000000,Local188), DERIV_BASE_VALUE(Local204));
	FWSVector2 Local207 = WSAdd(DERIV_BASE_VALUE(Local205), DERIV_BASE_VALUE(Local206));
	MaterialFloat2 Local208 = WSApplyAddressMode(DERIV_BASE_VALUE(Local207), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local209 = MaterialStoreTexCoordScale(Parameters, Local208, 4);
	MaterialFloat4 Local210 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local208,View.MaterialTextureMipBias));
	MaterialFloat Local211 = MaterialStoreTexSample(Parameters, Local210, 4);
	MaterialFloat3 Local212 = lerp(Local203,Local210.rgb,Local35.r.r);
	MaterialFloat Local213 = (View.GameTime * -0.01000000);
	FWSVector3 Local214 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[9].x));
	FWSVector2 Local215 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local214)), WSGetZ(DERIV_BASE_VALUE(Local214)));
	FWSVector2 Local216 = WSAdd(MaterialFloat2(Local213,0.00000000), DERIV_BASE_VALUE(Local215));
	MaterialFloat Local217 = (View.GameTime * -0.04000000);
	FWSVector2 Local218 = WSAdd(MaterialFloat2(0.00000000,Local217), DERIV_BASE_VALUE(Local215));
	FWSVector2 Local219 = WSAdd(DERIV_BASE_VALUE(Local216), DERIV_BASE_VALUE(Local218));
	MaterialFloat2 Local220 = WSApplyAddressMode(DERIV_BASE_VALUE(Local219), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local221 = MaterialStoreTexCoordScale(Parameters, Local220, 4);
	MaterialFloat4 Local222 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local220,View.MaterialTextureMipBias));
	MaterialFloat Local223 = MaterialStoreTexSample(Parameters, Local222, 4);
	FWSVector2 Local224 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local214)), WSGetZ(DERIV_BASE_VALUE(Local214)));
	FWSVector2 Local225 = WSAdd(MaterialFloat2(Local213,0.00000000), DERIV_BASE_VALUE(Local224));
	FWSVector2 Local226 = WSAdd(MaterialFloat2(0.00000000,Local217), DERIV_BASE_VALUE(Local224));
	FWSVector2 Local227 = WSAdd(DERIV_BASE_VALUE(Local225), DERIV_BASE_VALUE(Local226));
	MaterialFloat2 Local228 = WSApplyAddressMode(DERIV_BASE_VALUE(Local227), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local229 = MaterialStoreTexCoordScale(Parameters, Local228, 4);
	MaterialFloat4 Local230 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local228,View.MaterialTextureMipBias));
	MaterialFloat Local231 = MaterialStoreTexSample(Parameters, Local230, 4);
	MaterialFloat3 Local232 = lerp(Local222.rgb,Local230.rgb,Local23.r.r);
	FWSVector2 Local233 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local214)), WSGetY(DERIV_BASE_VALUE(Local214)));
	FWSVector2 Local234 = WSAdd(MaterialFloat2(Local213,0.00000000), DERIV_BASE_VALUE(Local233));
	FWSVector2 Local235 = WSAdd(MaterialFloat2(0.00000000,Local217), DERIV_BASE_VALUE(Local233));
	FWSVector2 Local236 = WSAdd(DERIV_BASE_VALUE(Local234), DERIV_BASE_VALUE(Local235));
	MaterialFloat2 Local237 = WSApplyAddressMode(DERIV_BASE_VALUE(Local236), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local238 = MaterialStoreTexCoordScale(Parameters, Local237, 4);
	MaterialFloat4 Local239 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local237,View.MaterialTextureMipBias));
	MaterialFloat Local240 = MaterialStoreTexSample(Parameters, Local239, 4);
	MaterialFloat3 Local241 = lerp(Local232,Local239.rgb,Local35.r.r);
	MaterialFloat3 Local242 = lerp(Local212,Local241,0.50000000);
	FWSVector3 Local243 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[9].y));
	FWSVector2 Local244 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local243)), WSGetZ(DERIV_BASE_VALUE(Local243)));
	FWSVector2 Local245 = WSAdd(MaterialFloat2(Local188,0.00000000), DERIV_BASE_VALUE(Local244));
	FWSVector2 Local246 = WSAdd(MaterialFloat2(0.00000000,Local184), DERIV_BASE_VALUE(Local244));
	FWSVector2 Local247 = WSAdd(DERIV_BASE_VALUE(Local245), DERIV_BASE_VALUE(Local246));
	MaterialFloat2 Local248 = WSApplyAddressMode(DERIV_BASE_VALUE(Local247), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local249 = MaterialStoreTexCoordScale(Parameters, Local248, 4);
	MaterialFloat4 Local250 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local248,View.MaterialTextureMipBias));
	MaterialFloat Local251 = MaterialStoreTexSample(Parameters, Local250, 4);
	FWSVector2 Local252 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local243)), WSGetZ(DERIV_BASE_VALUE(Local243)));
	FWSVector2 Local253 = WSAdd(MaterialFloat2(Local188,0.00000000), DERIV_BASE_VALUE(Local252));
	FWSVector2 Local254 = WSAdd(MaterialFloat2(0.00000000,Local184), DERIV_BASE_VALUE(Local252));
	FWSVector2 Local255 = WSAdd(DERIV_BASE_VALUE(Local253), DERIV_BASE_VALUE(Local254));
	MaterialFloat2 Local256 = WSApplyAddressMode(DERIV_BASE_VALUE(Local255), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local257 = MaterialStoreTexCoordScale(Parameters, Local256, 4);
	MaterialFloat4 Local258 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local256,View.MaterialTextureMipBias));
	MaterialFloat Local259 = MaterialStoreTexSample(Parameters, Local258, 4);
	MaterialFloat3 Local260 = lerp(Local250.rgb,Local258.rgb,Local23.r.r);
	FWSVector2 Local261 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local243)), WSGetY(DERIV_BASE_VALUE(Local243)));
	FWSVector2 Local262 = WSAdd(MaterialFloat2(Local188,0.00000000), DERIV_BASE_VALUE(Local261));
	FWSVector2 Local263 = WSAdd(MaterialFloat2(0.00000000,Local184), DERIV_BASE_VALUE(Local261));
	FWSVector2 Local264 = WSAdd(DERIV_BASE_VALUE(Local262), DERIV_BASE_VALUE(Local263));
	MaterialFloat2 Local265 = WSApplyAddressMode(DERIV_BASE_VALUE(Local264), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local266 = MaterialStoreTexCoordScale(Parameters, Local265, 4);
	MaterialFloat4 Local267 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local265,View.MaterialTextureMipBias));
	MaterialFloat Local268 = MaterialStoreTexSample(Parameters, Local267, 4);
	MaterialFloat3 Local269 = lerp(Local260,Local267.rgb,Local35.r.r);
	FWSVector3 Local270 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[9].z));
	FWSVector2 Local271 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local270)), WSGetZ(DERIV_BASE_VALUE(Local270)));
	FWSVector2 Local272 = WSAdd(MaterialFloat2(Local217,0.00000000), DERIV_BASE_VALUE(Local271));
	FWSVector2 Local273 = WSAdd(MaterialFloat2(0.00000000,Local213), DERIV_BASE_VALUE(Local271));
	FWSVector2 Local274 = WSAdd(DERIV_BASE_VALUE(Local272), DERIV_BASE_VALUE(Local273));
	MaterialFloat2 Local275 = WSApplyAddressMode(DERIV_BASE_VALUE(Local274), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local276 = MaterialStoreTexCoordScale(Parameters, Local275, 4);
	MaterialFloat4 Local277 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local275,View.MaterialTextureMipBias));
	MaterialFloat Local278 = MaterialStoreTexSample(Parameters, Local277, 4);
	FWSVector2 Local279 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local270)), WSGetZ(DERIV_BASE_VALUE(Local270)));
	FWSVector2 Local280 = WSAdd(MaterialFloat2(Local217,0.00000000), DERIV_BASE_VALUE(Local279));
	FWSVector2 Local281 = WSAdd(MaterialFloat2(0.00000000,Local213), DERIV_BASE_VALUE(Local279));
	FWSVector2 Local282 = WSAdd(DERIV_BASE_VALUE(Local280), DERIV_BASE_VALUE(Local281));
	MaterialFloat2 Local283 = WSApplyAddressMode(DERIV_BASE_VALUE(Local282), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local284 = MaterialStoreTexCoordScale(Parameters, Local283, 4);
	MaterialFloat4 Local285 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local283,View.MaterialTextureMipBias));
	MaterialFloat Local286 = MaterialStoreTexSample(Parameters, Local285, 4);
	MaterialFloat3 Local287 = lerp(Local277.rgb,Local285.rgb,Local23.r.r);
	FWSVector2 Local288 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local270)), WSGetY(DERIV_BASE_VALUE(Local270)));
	FWSVector2 Local289 = WSAdd(MaterialFloat2(Local217,0.00000000), DERIV_BASE_VALUE(Local288));
	FWSVector2 Local290 = WSAdd(MaterialFloat2(0.00000000,Local213), DERIV_BASE_VALUE(Local288));
	FWSVector2 Local291 = WSAdd(DERIV_BASE_VALUE(Local289), DERIV_BASE_VALUE(Local290));
	MaterialFloat2 Local292 = WSApplyAddressMode(DERIV_BASE_VALUE(Local291), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local293 = MaterialStoreTexCoordScale(Parameters, Local292, 4);
	MaterialFloat4 Local294 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local292,View.MaterialTextureMipBias));
	MaterialFloat Local295 = MaterialStoreTexSample(Parameters, Local294, 4);
	MaterialFloat3 Local296 = lerp(Local287,Local294.rgb,Local35.r.r);
	MaterialFloat3 Local297 = lerp(Local269,Local296,0.50000000);
	MaterialFloat3 Local298 = lerp(Local242,Local297,0.50000000);
	MaterialFloat Local299 = PositiveClampedPow(Local298.r,Material.PreshaderBuffer[9].w);
	MaterialFloat Local300 = GetPixelDepth(Parameters);
	MaterialFloat Local301 = CalcSceneDepth(ScreenAlignedPosition(GetScreenPosition(Parameters)));
	MaterialFloat Local302 = (Local301 - DERIV_BASE_VALUE(Local300));
	MaterialFloat Local303 = (Local302 * Material.PreshaderBuffer[10].x);
	MaterialFloat Local304 = saturate(Local303);
	MaterialFloat Local305 = PositiveClampedPow(Local304,0.25000000);
	MaterialFloat Local306 = lerp((0.00000000 - 2.00000000),(2.00000000 + 1.00000000),Local305);
	MaterialFloat Local307 = saturate(Local306);
	MaterialFloat Local308 = (1.00000000 - Local307.r);
	MaterialFloat Local309 = (View.GameTime * -0.25000000);
	MaterialFloat Local310 = (Local308 * Local308);
	MaterialFloat Local311 = (Local310.r + Local310.r);
	MaterialFloat Local312 = sqrt(Local311);
	MaterialFloat2 Local313 = (MaterialFloat2(0.00000000,Local309) + ((MaterialFloat2)Local312));
	MaterialFloat2 Local314 = (Local313 * ((MaterialFloat2)0.50000000));
	MaterialFloat Local315 = MaterialStoreTexCoordScale(Parameters, Local314, 2);
	MaterialFloat4 Local316 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local314,View.MaterialTextureMipBias));
	MaterialFloat Local317 = MaterialStoreTexSample(Parameters, Local316, 2);
	MaterialFloat Local318 = saturate(Local316.g);
	MaterialFloat Local319 = (Local308 * Local318);
	MaterialFloat3 Local320 = lerp(((MaterialFloat3)Local319),MaterialFloat3(0.00000000,0.00000000,0.00000000).rgb,Local169.g);
	MaterialFloat3 Local321 = (((MaterialFloat3)Local299) * Local320);
	MaterialFloat3 Local322 = (Local321 * Material.PreshaderBuffer[10].yzw);
	MaterialFloat3 Local323 = (Local183 + Local322);
	MaterialFloat Local324 = (View.GameTime * Material.PreshaderBuffer[11].x);
	MaterialFloat Local325 = (View.GameTime * Material.PreshaderBuffer[11].y);
	MaterialFloat2 Local326 = (DERIV_BASE_VALUE(Local151) * ((MaterialFloat2)Material.PreshaderBuffer[11].z));
	MaterialFloat2 Local327 = (MaterialFloat2(Local324,Local325) + DERIV_BASE_VALUE(Local326));
	MaterialFloat Local328 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local327), 4);
	MaterialFloat4 Local329 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,DERIV_BASE_VALUE(Local327),View.MaterialTextureMipBias));
	MaterialFloat Local330 = MaterialStoreTexSample(Parameters, Local329, 4);
	MaterialFloat Local331 = (Local329.r + Local329.g);
	MaterialFloat2 Local332 = (DERIV_BASE_VALUE(Local151) * ((MaterialFloat2)Material.PreshaderBuffer[11].w));
	MaterialFloat2 Local333 = (MaterialFloat2(Local324,Local325) + DERIV_BASE_VALUE(Local332));
	MaterialFloat Local334 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local333), 4);
	MaterialFloat4 Local335 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,DERIV_BASE_VALUE(Local333),View.MaterialTextureMipBias));
	MaterialFloat Local336 = MaterialStoreTexSample(Parameters, Local335, 4);
	MaterialFloat Local337 = (Local335.r + Local335.g);
	MaterialFloat2 Local338 = (DERIV_BASE_VALUE(Local151) * ((MaterialFloat2)Material.PreshaderBuffer[12].x));
	MaterialFloat2 Local339 = (MaterialFloat2(Local324,Local325) + DERIV_BASE_VALUE(Local338));
	MaterialFloat Local340 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local339), 4);
	MaterialFloat4 Local341 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,DERIV_BASE_VALUE(Local339),View.MaterialTextureMipBias));
	MaterialFloat Local342 = MaterialStoreTexSample(Parameters, Local341, 4);
	MaterialFloat Local343 = (Local341.r + Local341.g);
	MaterialFloat Local344 = (Local337 + Local343);
	MaterialFloat Local345 = (Local331 + Local344);
	MaterialFloat Local346 = PositiveClampedPow(Local345,Material.PreshaderBuffer[12].y);
	MaterialFloat3 Local347 = (((MaterialFloat3)Local346) * Material.PreshaderBuffer[13].xyz);
	MaterialFloat3 Local348 = (((MaterialFloat3)1.00000000) - Local347);
	MaterialFloat4 Local349 = ProcessMaterialColorTextureLookup(Texture2DSample(Material_Texture2D_1,GetMaterialSharedSampler(samplerMaterial_Texture2D_1,View_MaterialTextureBilinearWrapedSampler),DERIV_BASE_VALUE(Local151)));
	MaterialFloat Local350 = MaterialStoreTexSample(Parameters, Local349, 3);
	MaterialFloat Local351 = (1.00000000 - Local349.g);
	MaterialFloat3 Local352 = lerp(Local348,MaterialFloat3(1.00000000,1.00000000,1.00000000).rgb,Local351);
	MaterialFloat3 Local353 = (Local323 * Local352);
	MaterialFloat Local354 = PositiveClampedPow(Local345,Material.PreshaderBuffer[13].w);
	MaterialFloat Local355 = (Local354 * Local349.b);
	MaterialFloat3 Local356 = (((MaterialFloat3)Local355) * Material.PreshaderBuffer[14].xyz);
	MaterialFloat3 Local357 = (Local353 + Local356);
	FWSVector2 Local358 = WSAdd(MaterialFloat2(Local184,0.00000000), DERIV_BASE_VALUE(Local40));
	FWSVector2 Local359 = WSAdd(MaterialFloat2(0.00000000,Local188), DERIV_BASE_VALUE(Local40));
	FWSVector2 Local360 = WSAdd(DERIV_BASE_VALUE(Local358), DERIV_BASE_VALUE(Local359));
	MaterialFloat2 Local361 = WSApplyAddressMode(DERIV_BASE_VALUE(Local360), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local362 = MaterialStoreTexCoordScale(Parameters, Local361, 1);
	MaterialFloat4 Local363 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local361,View.MaterialTextureMipBias));
	MaterialFloat Local364 = MaterialStoreTexSample(Parameters, Local363, 1);
	FWSVector2 Local365 = WSAdd(MaterialFloat2(Local184,0.00000000), DERIV_BASE_VALUE(Local49));
	FWSVector2 Local366 = WSAdd(MaterialFloat2(0.00000000,Local188), DERIV_BASE_VALUE(Local49));
	FWSVector2 Local367 = WSAdd(DERIV_BASE_VALUE(Local365), DERIV_BASE_VALUE(Local366));
	MaterialFloat2 Local368 = WSApplyAddressMode(DERIV_BASE_VALUE(Local367), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local369 = MaterialStoreTexCoordScale(Parameters, Local368, 1);
	MaterialFloat4 Local370 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local368,View.MaterialTextureMipBias));
	MaterialFloat Local371 = MaterialStoreTexSample(Parameters, Local370, 1);
	MaterialFloat3 Local372 = lerp(Local363.rgb,Local370.rgb,Local23.r.r);
	FWSVector2 Local373 = WSAdd(MaterialFloat2(Local184,0.00000000), DERIV_BASE_VALUE(Local58));
	FWSVector2 Local374 = WSAdd(MaterialFloat2(0.00000000,Local188), DERIV_BASE_VALUE(Local58));
	FWSVector2 Local375 = WSAdd(DERIV_BASE_VALUE(Local373), DERIV_BASE_VALUE(Local374));
	MaterialFloat2 Local376 = WSApplyAddressMode(DERIV_BASE_VALUE(Local375), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local377 = MaterialStoreTexCoordScale(Parameters, Local376, 1);
	MaterialFloat4 Local378 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local376,View.MaterialTextureMipBias));
	MaterialFloat Local379 = MaterialStoreTexSample(Parameters, Local378, 1);
	MaterialFloat3 Local380 = lerp(Local372,Local378.rgb,Local35.r.r);
	FWSVector2 Local381 = WSAdd(MaterialFloat2(Local213,0.00000000), DERIV_BASE_VALUE(Local4));
	FWSVector2 Local382 = WSAdd(MaterialFloat2(0.00000000,Local217), DERIV_BASE_VALUE(Local4));
	FWSVector2 Local383 = WSAdd(DERIV_BASE_VALUE(Local381), DERIV_BASE_VALUE(Local382));
	MaterialFloat2 Local384 = WSApplyAddressMode(DERIV_BASE_VALUE(Local383), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local385 = MaterialStoreTexCoordScale(Parameters, Local384, 1);
	MaterialFloat4 Local386 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local384,View.MaterialTextureMipBias));
	MaterialFloat Local387 = MaterialStoreTexSample(Parameters, Local386, 1);
	FWSVector2 Local388 = WSAdd(MaterialFloat2(Local213,0.00000000), DERIV_BASE_VALUE(Local13));
	FWSVector2 Local389 = WSAdd(MaterialFloat2(0.00000000,Local217), DERIV_BASE_VALUE(Local13));
	FWSVector2 Local390 = WSAdd(DERIV_BASE_VALUE(Local388), DERIV_BASE_VALUE(Local389));
	MaterialFloat2 Local391 = WSApplyAddressMode(DERIV_BASE_VALUE(Local390), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local392 = MaterialStoreTexCoordScale(Parameters, Local391, 1);
	MaterialFloat4 Local393 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local391,View.MaterialTextureMipBias));
	MaterialFloat Local394 = MaterialStoreTexSample(Parameters, Local393, 1);
	MaterialFloat3 Local395 = lerp(Local386.rgb,Local393.rgb,Local23.r.r);
	FWSVector2 Local396 = WSAdd(MaterialFloat2(Local213,0.00000000), DERIV_BASE_VALUE(Local25));
	FWSVector2 Local397 = WSAdd(MaterialFloat2(0.00000000,Local217), DERIV_BASE_VALUE(Local25));
	FWSVector2 Local398 = WSAdd(DERIV_BASE_VALUE(Local396), DERIV_BASE_VALUE(Local397));
	MaterialFloat2 Local399 = WSApplyAddressMode(DERIV_BASE_VALUE(Local398), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local400 = MaterialStoreTexCoordScale(Parameters, Local399, 1);
	MaterialFloat4 Local401 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local399,View.MaterialTextureMipBias));
	MaterialFloat Local402 = MaterialStoreTexSample(Parameters, Local401, 1);
	MaterialFloat3 Local403 = lerp(Local395,Local401.rgb,Local35.r.r);
	MaterialFloat3 Local404 = lerp(Local380,Local403,0.50000000);
	FWSVector2 Local405 = WSAdd(MaterialFloat2(Local188,0.00000000), DERIV_BASE_VALUE(Local75));
	FWSVector2 Local406 = WSAdd(MaterialFloat2(0.00000000,Local184), DERIV_BASE_VALUE(Local75));
	FWSVector2 Local407 = WSAdd(DERIV_BASE_VALUE(Local405), DERIV_BASE_VALUE(Local406));
	MaterialFloat2 Local408 = WSApplyAddressMode(DERIV_BASE_VALUE(Local407), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local409 = MaterialStoreTexCoordScale(Parameters, Local408, 1);
	MaterialFloat4 Local410 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local408,View.MaterialTextureMipBias));
	MaterialFloat Local411 = MaterialStoreTexSample(Parameters, Local410, 1);
	FWSVector2 Local412 = WSAdd(MaterialFloat2(Local188,0.00000000), DERIV_BASE_VALUE(Local84));
	FWSVector2 Local413 = WSAdd(MaterialFloat2(0.00000000,Local184), DERIV_BASE_VALUE(Local84));
	FWSVector2 Local414 = WSAdd(DERIV_BASE_VALUE(Local412), DERIV_BASE_VALUE(Local413));
	MaterialFloat2 Local415 = WSApplyAddressMode(DERIV_BASE_VALUE(Local414), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local416 = MaterialStoreTexCoordScale(Parameters, Local415, 1);
	MaterialFloat4 Local417 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local415,View.MaterialTextureMipBias));
	MaterialFloat Local418 = MaterialStoreTexSample(Parameters, Local417, 1);
	MaterialFloat3 Local419 = lerp(Local410.rgb,Local417.rgb,Local23.r.r);
	FWSVector2 Local420 = WSAdd(MaterialFloat2(Local188,0.00000000), DERIV_BASE_VALUE(Local93));
	FWSVector2 Local421 = WSAdd(MaterialFloat2(0.00000000,Local184), DERIV_BASE_VALUE(Local93));
	FWSVector2 Local422 = WSAdd(DERIV_BASE_VALUE(Local420), DERIV_BASE_VALUE(Local421));
	MaterialFloat2 Local423 = WSApplyAddressMode(DERIV_BASE_VALUE(Local422), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local424 = MaterialStoreTexCoordScale(Parameters, Local423, 1);
	MaterialFloat4 Local425 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local423,View.MaterialTextureMipBias));
	MaterialFloat Local426 = MaterialStoreTexSample(Parameters, Local425, 1);
	MaterialFloat3 Local427 = lerp(Local419,Local425.rgb,Local35.r.r);
	FWSVector2 Local428 = WSAdd(MaterialFloat2(Local217,0.00000000), DERIV_BASE_VALUE(Local105));
	FWSVector2 Local429 = WSAdd(MaterialFloat2(0.00000000,Local213), DERIV_BASE_VALUE(Local105));
	FWSVector2 Local430 = WSAdd(DERIV_BASE_VALUE(Local428), DERIV_BASE_VALUE(Local429));
	MaterialFloat2 Local431 = WSApplyAddressMode(DERIV_BASE_VALUE(Local430), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local432 = MaterialStoreTexCoordScale(Parameters, Local431, 1);
	MaterialFloat4 Local433 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local431,View.MaterialTextureMipBias));
	MaterialFloat Local434 = MaterialStoreTexSample(Parameters, Local433, 1);
	FWSVector2 Local435 = WSAdd(MaterialFloat2(Local217,0.00000000), DERIV_BASE_VALUE(Local114));
	FWSVector2 Local436 = WSAdd(MaterialFloat2(0.00000000,Local213), DERIV_BASE_VALUE(Local114));
	FWSVector2 Local437 = WSAdd(DERIV_BASE_VALUE(Local435), DERIV_BASE_VALUE(Local436));
	MaterialFloat2 Local438 = WSApplyAddressMode(DERIV_BASE_VALUE(Local437), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local439 = MaterialStoreTexCoordScale(Parameters, Local438, 1);
	MaterialFloat4 Local440 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local438,View.MaterialTextureMipBias));
	MaterialFloat Local441 = MaterialStoreTexSample(Parameters, Local440, 1);
	MaterialFloat3 Local442 = lerp(Local433.rgb,Local440.rgb,Local23.r.r);
	FWSVector2 Local443 = WSAdd(MaterialFloat2(Local217,0.00000000), DERIV_BASE_VALUE(Local123));
	FWSVector2 Local444 = WSAdd(MaterialFloat2(0.00000000,Local213), DERIV_BASE_VALUE(Local123));
	FWSVector2 Local445 = WSAdd(DERIV_BASE_VALUE(Local443), DERIV_BASE_VALUE(Local444));
	MaterialFloat2 Local446 = WSApplyAddressMode(DERIV_BASE_VALUE(Local445), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local447 = MaterialStoreTexCoordScale(Parameters, Local446, 1);
	MaterialFloat4 Local448 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,Local446,View.MaterialTextureMipBias));
	MaterialFloat Local449 = MaterialStoreTexSample(Parameters, Local448, 1);
	MaterialFloat3 Local450 = lerp(Local442,Local448.rgb,Local35.r.r);
	MaterialFloat3 Local451 = lerp(Local427,Local450,0.50000000);
	MaterialFloat3 Local452 = lerp(Local404,Local451,0.50000000);
	MaterialFloat Local453 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local158), 1);
	MaterialFloat4 Local454 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,DERIV_BASE_VALUE(Local158),View.MaterialTextureMipBias));
	MaterialFloat Local455 = MaterialStoreTexSample(Parameters, Local454, 1);
	MaterialFloat Local456 = lerp(Local452.b,Local454.b,Local169.g);
	MaterialFloat Local457 = lerp(Local452.g,Local454.g,Local169.g);
	MaterialFloat Local458 = (Local302 * Material.PreshaderBuffer[14].w);
	MaterialFloat Local459 = saturate(Local458);
	MaterialFloat Local460 = (Material.PreshaderBuffer[15].x * Local459);
	MaterialFloat Local461 = PositiveClampedPow(Local177,Material.PreshaderBuffer[15].y);
	MaterialFloat Local462 = (Local461 * Material.PreshaderBuffer[15].z);
	MaterialFloat Local463 = (Local462 + Material.PreshaderBuffer[15].w);
	MaterialFloat Local464 = lerp(Local463,1.00000000,0.25000000);
	MaterialFloat Local465 = (Local460 * Local464);
	MaterialFloat Local466 = (Local302 * Material.PreshaderBuffer[16].x);
	MaterialFloat Local467 = saturate(Local466);
	MaterialFloat Local468 = (1.10000002 * Local467);
	MaterialFloat Local469 = lerp(Local465,Local468,Local169.r);
	MaterialFloat Local470 = lerp(Local452.r,Local454.r,Local169.g);
	MaterialFloat Local471 = lerp(Material.PreshaderBuffer[16].z,Material.PreshaderBuffer[16].y,Local169.r);

	PixelMaterialInputs.EmissiveColor = Local172;
	PixelMaterialInputs.Opacity = Local469;
	PixelMaterialInputs.OpacityMask = 1.00000000;
	PixelMaterialInputs.BaseColor = Local357;
	PixelMaterialInputs.Metallic = Local456;
	PixelMaterialInputs.Specular = 0.50000000;
	PixelMaterialInputs.Roughness = Local457;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Normal = Local171;
	PixelMaterialInputs.Tangent = Parameters.TangentToWorld[0];
	PixelMaterialInputs.Subsurface = 0;
	PixelMaterialInputs.AmbientOcclusion = Local470;
	PixelMaterialInputs.Refraction = MaterialFloat3(MaterialFloat2(Local471,0.0f),Material.PreshaderBuffer[16].w);
	PixelMaterialInputs.PixelDepthOffset = 0.00000000;
	PixelMaterialInputs.ShadingModel = 1;
	PixelMaterialInputs.FrontMaterial = GetInitialisedSubstrateData();
	PixelMaterialInputs.SurfaceThickness = 0.01000000;
	PixelMaterialInputs.Displacement = 0.50000000;


#if MATERIAL_USES_ANISOTROPY
	Parameters.WorldTangent = CalculateAnisotropyTangent(Parameters, PixelMaterialInputs);
#else
	Parameters.WorldTangent = 0;
#endif
}

#define UnityObjectToWorldDir TransformObjectToWorld

void SetupCommonData( int Parameters_PrimitiveId )
{
	View_MaterialTextureBilinearWrapedSampler = SamplerState_Linear_Repeat;
	View_MaterialTextureBilinearClampedSampler = SamplerState_Linear_Clamp;

	Material_Wrap_WorldGroupSettings = SamplerState_Linear_Repeat;
	Material_Clamp_WorldGroupSettings = SamplerState_Linear_Clamp;

	View.GameTime = View.RealTime = _Time.y;// _Time is (t/20, t, t*2, t*3)
	View.PrevFrameGameTime = View.GameTime - unity_DeltaTime.x;//(dt, 1/dt, smoothDt, 1/smoothDt)
	View.PrevFrameRealTime = View.RealTime;
	View.DeltaTime = unity_DeltaTime.x;
	View.MaterialTextureMipBias = 0.0;
	View.TemporalAAParams = float4( 0, 0, 0, 0 );
	View.ViewRectMin = float2( 0, 0 );
	View.ViewSizeAndInvSize = View_BufferSizeAndInvSize;
	View.MaterialTextureDerivativeMultiply = 1.0f;
	View.StateFrameIndexMod8 = 0;
	View.FrameNumber = (int)_Time.y;
	View.FieldOfViewWideAngles = float2( PI * 0.42f, PI * 0.42f );//75degrees, default unity
	View.RuntimeVirtualTextureMipLevel = float4( 0, 0, 0, 0 );
	View.PreExposure = 0;
	View.BufferBilinearUVMinMax = float4(
		View_BufferSizeAndInvSize.z * ( 0 + 0.5 ),//EffectiveViewRect.Min.X
		View_BufferSizeAndInvSize.w * ( 0 + 0.5 ),//EffectiveViewRect.Min.Y
		View_BufferSizeAndInvSize.z * ( View_BufferSizeAndInvSize.x - 0.5 ),//EffectiveViewRect.Max.X
		View_BufferSizeAndInvSize.w * ( View_BufferSizeAndInvSize.y - 0.5 ) );//EffectiveViewRect.Max.Y

	for( int i2 = 0; i2 < 40; i2++ )
		View.PrimitiveSceneData[ i2 ] = float4( 0, 0, 0, 0 );

	float4x4 LocalToWorld = transpose( UNITY_MATRIX_M );
    LocalToWorld[3] = float4(ToUnrealPos(LocalToWorld[3]), LocalToWorld[3].w);
	float4x4 WorldToLocal = transpose( UNITY_MATRIX_I_M );
	float4x4 ViewMatrix = transpose( UNITY_MATRIX_V );
	float4x4 InverseViewMatrix = transpose( UNITY_MATRIX_I_V );
	float4x4 ViewProjectionMatrix = transpose( UNITY_MATRIX_VP );
	uint PrimitiveBaseOffset = Parameters_PrimitiveId * PRIMITIVE_SCENE_DATA_STRIDE;
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 0 ] = LocalToWorld[ 0 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 1 ] = LocalToWorld[ 1 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 2 ] = LocalToWorld[ 2 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 3 ] = LocalToWorld[ 3 ];//LocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 5 ] = float4( ToUnrealPos( SHADERGRAPH_OBJECT_POSITION ), 100.0 );//ObjectWorldPosition
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 6 ] = WorldToLocal[ 0 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 7 ] = WorldToLocal[ 1 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 8 ] = WorldToLocal[ 2 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 9 ] = WorldToLocal[ 3 ];//WorldToLocal
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 10 ] = LocalToWorld[ 0 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 11 ] = LocalToWorld[ 1 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 12 ] = LocalToWorld[ 2 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 13 ] = LocalToWorld[ 3 ];//PreviousLocalToWorld
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 18 ] = float4( ToUnrealPos( SHADERGRAPH_OBJECT_POSITION ), 0 );//ActorWorldPosition
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 19 ] = LocalObjectBoundsMax - LocalObjectBoundsMin;//ObjectBounds
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 21 ] = mul( LocalToWorld, float3( 1, 0, 0 ) );
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 23 ] = LocalObjectBoundsMin;//LocalObjectBoundsMin 
	View.PrimitiveSceneData[ PrimitiveBaseOffset + 24 ] = LocalObjectBoundsMax;//LocalObjectBoundsMax

#ifdef UE5
	ResolvedView.WorldCameraOrigin = LWCPromote( ToUnrealPos( _WorldSpaceCameraPos.xyz ) );
	ResolvedView.PreViewTranslation = LWCPromote( float3( 0, 0, 0 ) );
	ResolvedView.WorldViewOrigin = LWCPromote( float3( 0, 0, 0 ) );
#else
	ResolvedView.WorldCameraOrigin = ToUnrealPos( _WorldSpaceCameraPos.xyz );
	ResolvedView.PreViewTranslation = float3( 0, 0, 0 );
	ResolvedView.WorldViewOrigin = float3( 0, 0, 0 );
#endif
	ResolvedView.PrevWorldCameraOrigin = ResolvedView.WorldCameraOrigin;
	ResolvedView.ScreenPositionScaleBias = float4( 1, 1, 0, 0 );
	ResolvedView.TranslatedWorldToView		 = ViewMatrix;
	ResolvedView.TranslatedWorldToCameraView = ViewMatrix;
	ResolvedView.TranslatedWorldToClip		 = ViewProjectionMatrix;
	ResolvedView.ViewToTranslatedWorld		 = InverseViewMatrix;
	ResolvedView.PrevViewToTranslatedWorld = ResolvedView.ViewToTranslatedWorld;
	ResolvedView.CameraViewToTranslatedWorld = InverseViewMatrix;
	ResolvedView.BufferBilinearUVMinMax = View.BufferBilinearUVMinMax;
	Primitive.WorldToLocal = WorldToLocal;
	Primitive.LocalToWorld = LocalToWorld;
}
#define VS_USES_UNREAL_SPACE 1
float3 PrepareAndGetWPO( float4 VertexColor, float3 UnrealWorldPos, float3 UnrealNormal, float4 InTangent,
						 float4 UV0, float4 UV1 )
{
	InitializeExpressions();
	FMaterialVertexParameters Parameters = (FMaterialVertexParameters)0;

	float3 InWorldNormal = UnrealNormal;
	float4 tangentWorld = InTangent;
	tangentWorld.xyz = normalize( tangentWorld.xyz );
	//float3x3 tangentToWorld = CreateTangentToWorldPerVertex( InWorldNormal, tangentWorld.xyz, tangentWorld.w );
	Parameters.TangentToWorld = float3x3( normalize( cross( InWorldNormal, tangentWorld.xyz ) * tangentWorld.w ), tangentWorld.xyz, InWorldNormal );

	
	#ifdef VS_USES_UNREAL_SPACE
		UnrealWorldPos = ToUnrealPos( UnrealWorldPos );
	#endif
	Parameters.WorldPosition = UnrealWorldPos;
	#ifdef VS_USES_UNREAL_SPACE
		Parameters.TangentToWorld[ 0 ] = Parameters.TangentToWorld[ 0 ].xzy;
		Parameters.TangentToWorld[ 1 ] = Parameters.TangentToWorld[ 1 ].xzy;
		Parameters.TangentToWorld[ 2 ] = Parameters.TangentToWorld[ 2 ].xzy;//WorldAligned texturing uses normals that think Z is up
	#endif

	Parameters.VertexColor = VertexColor;

#if NUM_MATERIAL_TEXCOORDS_VERTEX > 0			
	Parameters.TexCoords[ 0 ] = float2( UV0.x, UV0.y );
#endif
#if NUM_MATERIAL_TEXCOORDS_VERTEX > 1
	Parameters.TexCoords[ 1 ] = float2( UV1.x, UV1.y );
#endif
#if NUM_MATERIAL_TEXCOORDS_VERTEX > 2
	for( int i = 2; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
	{
		Parameters.TexCoords[ i ] = float2( UV0.x, UV0.y );
	}
#endif

	Parameters.PrimitiveId = 0;

	SetupCommonData( Parameters.PrimitiveId );

#ifdef UE5
	Parameters.PrevFrameLocalToWorld = MakeLWCMatrix( float3( 0, 0, 0 ), Primitive.LocalToWorld );
#else
	Parameters.PrevFrameLocalToWorld = Primitive.LocalToWorld;
#endif
	
	float3 Offset = float3( 0, 0, 0 );
	Offset = GetMaterialWorldPositionOffset( Parameters );
	#ifdef VS_USES_UNREAL_SPACE
		//Convert from unreal units to unity
		Offset /= float3( 100, 100, 100 );
		Offset = Offset.xzy;
	#endif
	return Offset;
}

void SurfaceReplacement( Input In, out SurfaceOutputStandard o )
{
	InitializeExpressions();

	float3 Z3 = float3( 0, 0, 0 );
	float4 Z4 = float4( 0, 0, 0, 0 );

	float3 UnrealWorldPos = float3( In.worldPos.x, In.worldPos.y, In.worldPos.z );

	float3 UnrealNormal = In.normal2;	

	FMaterialPixelParameters Parameters = (FMaterialPixelParameters)0;
#if NUM_TEX_COORD_INTERPOLATORS > 0			
	Parameters.TexCoords[ 0 ] = float2( In.uv_MainTex.x, 1.0 - In.uv_MainTex.y );
#endif
#if NUM_TEX_COORD_INTERPOLATORS > 1
	Parameters.TexCoords[ 1 ] = float2( In.uv2_Material_Texture2D_0.x, 1.0 - In.uv2_Material_Texture2D_0.y );
#endif
#if NUM_TEX_COORD_INTERPOLATORS > 2
	for( int i = 2; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
	{
		Parameters.TexCoords[ i ] = float2( In.uv_MainTex.x, 1.0 - In.uv_MainTex.y );
	}
#endif
	Parameters.PostProcessUV = In.uv_MainTex;
	Parameters.VertexColor = In.color;
	Parameters.WorldNormal = UnrealNormal;
	Parameters.ReflectionVector = half3( 0, 0, 1 );
	//Parameters.CameraVector = normalize( _WorldSpaceCameraPos.xyz - UnrealWorldPos.xyz );
	//Parameters.CameraVector = mul( ( float3x3 )unity_CameraToWorld, float3( 0, 0, 1 ) ) * -1;	
	float3 CameraDirection = (-1 * mul((float3x3)UNITY_MATRIX_M, transpose(mul(UNITY_MATRIX_I_M, UNITY_MATRIX_I_V)) [2].xyz));//From ShaderGraph
	Parameters.CameraVector = CameraDirection;
	Parameters.LightVector = half3( 0, 0, 0 );
	//float4 screenpos = In.screenPos;
	//screenpos /= screenpos.w;
	Parameters.SvPosition = In.screenPos;
	Parameters.ScreenPosition = Parameters.SvPosition;

	Parameters.UnMirrored = 1;

	Parameters.TwoSidedSign = 1;


	float3 InWorldNormal = UnrealNormal;	
	float4 tangentWorld = In.tangent;
	tangentWorld.xyz = normalize( tangentWorld.xyz );
	//float3x3 tangentToWorld = CreateTangentToWorldPerVertex( InWorldNormal, tangentWorld.xyz, tangentWorld.w );
	Parameters.TangentToWorld = float3x3( normalize( cross( InWorldNormal, tangentWorld.xyz ) * tangentWorld.w ), tangentWorld.xyz, InWorldNormal );

	//WorldAlignedTexturing in UE relies on the fact that coords there are 100x larger, prepare values for that
	//but watch out for any computation that might get skewed as a side effect
	UnrealWorldPos = ToUnrealPos( UnrealWorldPos );
	
	Parameters.AbsoluteWorldPosition = UnrealWorldPos;
	Parameters.WorldPosition_CamRelative = UnrealWorldPos;
	Parameters.WorldPosition_NoOffsets = UnrealWorldPos;

	Parameters.WorldPosition_NoOffsets_CamRelative = Parameters.WorldPosition_CamRelative;
	Parameters.LightingPositionOffset = float3( 0, 0, 0 );

	Parameters.AOMaterialMask = 0;

	Parameters.Particle.RelativeTime = 0;
	Parameters.Particle.MotionBlurFade;
	Parameters.Particle.Random = 0;
	Parameters.Particle.Velocity = half4( 1, 1, 1, 1 );
	Parameters.Particle.Color = half4( 1, 1, 1, 1 );
	Parameters.Particle.TranslatedWorldPositionAndSize = float4( UnrealWorldPos, 0 );
	Parameters.Particle.MacroUV = half4( 0, 0, 1, 1 );
	Parameters.Particle.DynamicParameter = half4( 0, 0, 0, 0 );
	Parameters.Particle.LocalToWorld = float4x4( Z4, Z4, Z4, Z4 );
	Parameters.Particle.Size = float2( 1, 1 );
	Parameters.Particle.SubUVCoords[ 0 ] = Parameters.Particle.SubUVCoords[ 1 ] = float2( 0, 0 );
	Parameters.Particle.SubUVLerp = 0.0;
	Parameters.TexCoordScalesParams = float2( 0, 0 );
	Parameters.PrimitiveId = 0;
	Parameters.VirtualTextureFeedback = 0;

	FPixelMaterialInputs PixelMaterialInputs = (FPixelMaterialInputs)0;
	PixelMaterialInputs.Normal = float3( 0, 0, 1 );
	PixelMaterialInputs.ShadingModel = 0;
	//PixelMaterialInputs.FrontMaterial = GetStrataUnlitBSDF( float3( 0, 0, 0 ), float3( 0, 0, 0 ) );

	SetupCommonData( Parameters.PrimitiveId );
	//CustomizedUVs
	#if NUM_TEX_COORD_INTERPOLATORS > 0 && HAS_CUSTOMIZED_UVS
		float2 OutTexCoords[ NUM_TEX_COORD_INTERPOLATORS ];
		//Prevent uninitialized reads
		for( int i = 0; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
		{
			OutTexCoords[ i ] = float2( 0, 0 );
		}
		GetMaterialCustomizedUVs( Parameters, OutTexCoords );
		for( int i = 0; i < NUM_TEX_COORD_INTERPOLATORS; i++ )
		{
			Parameters.TexCoords[ i ] = OutTexCoords[ i ];
		}
	#endif
	//<-
	CalcPixelMaterialInputs( Parameters, PixelMaterialInputs );

	#define HAS_WORLDSPACE_NORMAL 1
	#if HAS_WORLDSPACE_NORMAL
		PixelMaterialInputs.Normal = mul( PixelMaterialInputs.Normal, (MaterialFloat3x3)( transpose( Parameters.TangentToWorld ) ) );
	#endif

	o.Albedo = PixelMaterialInputs.BaseColor.rgb;
	o.Alpha = PixelMaterialInputs.Opacity;
	//if( PixelMaterialInputs.OpacityMask < 0.333 ) discard;

	o.Metallic = PixelMaterialInputs.Metallic;
	o.Smoothness = 1.0 - PixelMaterialInputs.Roughness;
	o.Normal = normalize( PixelMaterialInputs.Normal );
	o.Emission = PixelMaterialInputs.EmissiveColor.rgb;
	o.Occlusion = PixelMaterialInputs.AmbientOcclusion;

	//BLEND_ADDITIVE o.Alpha = ( o.Emission.r + o.Emission.g + o.Emission.b ) / 3;
}