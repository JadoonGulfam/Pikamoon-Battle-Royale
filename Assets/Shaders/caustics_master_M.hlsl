#define NUM_TEX_COORD_INTERPOLATORS 0
#define NUM_MATERIAL_TEXCOORDS_VERTEX 0
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
	float4 PreshaderBuffer[5];
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
	Material.PreshaderBuffer[0] = float4(5.000000,-0.000326,-0.000244,-0.000195);//(Unknown)
	Material.PreshaderBuffer[1] = float4(-0.000163,1.000000,0.593613,0.513641);//(Unknown)
	Material.PreshaderBuffer[2] = float4(3.300000,1.000000,1.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[3] = float4(-0.050000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[4] = float4(0.000000,0.000000,0.000000,0.000000);//(Unknown)
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

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = MaterialFloat3(0.00000000,0.00000000,1.00000000);


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
	FWSVector3 Local0 = GetWorldPosition(Parameters);
	FWSVector3 Local1 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local0)), WSGetY(DERIV_BASE_VALUE(Local0)), WSGetZ(DERIV_BASE_VALUE(Local0)));
	FWSScalar Local2 = WSGetX(DERIV_BASE_VALUE(Local1));
	MaterialFloat Local3 = (View.GameTime * 125.00000000);
	FWSVector2 Local4 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local1)), WSGetZ(DERIV_BASE_VALUE(Local1)));
	FWSVector2 Local5 = WSAdd(MaterialFloat2(0.00000000,Local3), DERIV_BASE_VALUE(Local4));
	FWSVector3 Local6 = MakeWSVector(WSPromote(DERIV_BASE_VALUE(Local2)),WSPromote(DERIV_BASE_VALUE(Local5)));
	MaterialFloat Local7 = MaterialExpressionNoise(DERIV_BASE_VALUE(Local6),-0.00800000,1.00000000,2.00000000,1.00000000,1.00000000,0.00000000,1.00000000,0.50000000,0.00000000,0.00000000,512.00000000);
	MaterialFloat Local8 = (1.00000000 - Local7);
	MaterialFloat Local9 = PositiveClampedPow(Local8,Material.PreshaderBuffer[0].x);
	MaterialFloat Local10 = (View.GameTime * 100.00000000);
	FWSVector2 Local11 = WSAdd(MaterialFloat2(0.00000000,Local10), DERIV_BASE_VALUE(Local4));
	FWSVector3 Local12 = MakeWSVector(WSPromote(DERIV_BASE_VALUE(Local2)),WSPromote(DERIV_BASE_VALUE(Local11)));
	MaterialFloat Local13 = MaterialExpressionNoise(DERIV_BASE_VALUE(Local12),-0.01500000,1.00000000,2.00000000,1.00000000,1.00000000,0.00000000,1.00000000,0.50000000,0.00000000,0.00000000,512.00000000);
	MaterialFloat Local14 = (1.00000000 - Local13);
	MaterialFloat Local15 = PositiveClampedPow(Local14,Material.PreshaderBuffer[0].x);
	MaterialFloat Local16 = (Local9 + Local15);
	MaterialFloat Local17 = (View.GameTime * 0.01000000);
	FWSVector3 Local18 = WSMultiply(DERIV_BASE_VALUE(Local1), ((MaterialFloat3)Material.PreshaderBuffer[0].y));
	FWSVector2 Local19 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local18)), WSGetZ(DERIV_BASE_VALUE(Local18)));
	FWSVector2 Local20 = WSAdd(MaterialFloat2(Local17,0.00000000), DERIV_BASE_VALUE(Local19));
	MaterialFloat Local21 = (View.GameTime * 0.04000000);
	FWSVector2 Local22 = WSAdd(MaterialFloat2(0.00000000,Local21), DERIV_BASE_VALUE(Local19));
	FWSVector2 Local23 = WSAdd(DERIV_BASE_VALUE(Local20), DERIV_BASE_VALUE(Local22));
	MaterialFloat2 Local24 = WSApplyAddressMode(DERIV_BASE_VALUE(Local23), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local25 = MaterialStoreTexCoordScale(Parameters, Local24, 1);
	MaterialFloat4 Local26 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local24,View.MaterialTextureMipBias));
	MaterialFloat Local27 = MaterialStoreTexSample(Parameters, Local26, 1);
	FWSVector2 Local28 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local18)), WSGetZ(DERIV_BASE_VALUE(Local18)));
	FWSVector2 Local29 = WSAdd(MaterialFloat2(Local17,0.00000000), DERIV_BASE_VALUE(Local28));
	FWSVector2 Local30 = WSAdd(MaterialFloat2(0.00000000,Local21), DERIV_BASE_VALUE(Local28));
	FWSVector2 Local31 = WSAdd(DERIV_BASE_VALUE(Local29), DERIV_BASE_VALUE(Local30));
	MaterialFloat2 Local32 = WSApplyAddressMode(DERIV_BASE_VALUE(Local31), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local33 = MaterialStoreTexCoordScale(Parameters, Local32, 1);
	MaterialFloat4 Local34 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local32,View.MaterialTextureMipBias));
	MaterialFloat Local35 = MaterialStoreTexSample(Parameters, Local34, 1);
	MaterialFloat Local36 = abs(Parameters.TangentToWorld[2].r);
	MaterialFloat Local37 = lerp((0.00000000 - 0.00000000),(0.00000000 + 1.00000000),Local36);
	MaterialFloat Local38 = saturate(Local37);
	MaterialFloat3 Local39 = lerp(Local26.rgb,Local34.rgb,Local38.r.r);
	FWSVector2 Local40 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local18)), WSGetY(DERIV_BASE_VALUE(Local18)));
	FWSVector2 Local41 = WSAdd(MaterialFloat2(Local17,0.00000000), DERIV_BASE_VALUE(Local40));
	FWSVector2 Local42 = WSAdd(MaterialFloat2(0.00000000,Local21), DERIV_BASE_VALUE(Local40));
	FWSVector2 Local43 = WSAdd(DERIV_BASE_VALUE(Local41), DERIV_BASE_VALUE(Local42));
	MaterialFloat2 Local44 = WSApplyAddressMode(DERIV_BASE_VALUE(Local43), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local45 = MaterialStoreTexCoordScale(Parameters, Local44, 1);
	MaterialFloat4 Local46 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local44,View.MaterialTextureMipBias));
	MaterialFloat Local47 = MaterialStoreTexSample(Parameters, Local46, 1);
	MaterialFloat Local48 = abs(Parameters.TangentToWorld[2].b);
	MaterialFloat Local49 = lerp((0.00000000 - 0.00000000),(0.00000000 + 1.00000000),Local48);
	MaterialFloat Local50 = saturate(Local49);
	MaterialFloat3 Local51 = lerp(Local39,Local46.rgb,Local50.r.r);
	MaterialFloat Local52 = (View.GameTime * -0.01000000);
	FWSVector3 Local53 = WSMultiply(DERIV_BASE_VALUE(Local1), ((MaterialFloat3)Material.PreshaderBuffer[0].z));
	FWSVector2 Local54 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local53)), WSGetZ(DERIV_BASE_VALUE(Local53)));
	FWSVector2 Local55 = WSAdd(MaterialFloat2(Local52,0.00000000), DERIV_BASE_VALUE(Local54));
	MaterialFloat Local56 = (View.GameTime * -0.04000000);
	FWSVector2 Local57 = WSAdd(MaterialFloat2(0.00000000,Local56), DERIV_BASE_VALUE(Local54));
	FWSVector2 Local58 = WSAdd(DERIV_BASE_VALUE(Local55), DERIV_BASE_VALUE(Local57));
	MaterialFloat2 Local59 = WSApplyAddressMode(DERIV_BASE_VALUE(Local58), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local60 = MaterialStoreTexCoordScale(Parameters, Local59, 1);
	MaterialFloat4 Local61 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local59,View.MaterialTextureMipBias));
	MaterialFloat Local62 = MaterialStoreTexSample(Parameters, Local61, 1);
	FWSVector2 Local63 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local53)), WSGetZ(DERIV_BASE_VALUE(Local53)));
	FWSVector2 Local64 = WSAdd(MaterialFloat2(Local52,0.00000000), DERIV_BASE_VALUE(Local63));
	FWSVector2 Local65 = WSAdd(MaterialFloat2(0.00000000,Local56), DERIV_BASE_VALUE(Local63));
	FWSVector2 Local66 = WSAdd(DERIV_BASE_VALUE(Local64), DERIV_BASE_VALUE(Local65));
	MaterialFloat2 Local67 = WSApplyAddressMode(DERIV_BASE_VALUE(Local66), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local68 = MaterialStoreTexCoordScale(Parameters, Local67, 1);
	MaterialFloat4 Local69 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local67,View.MaterialTextureMipBias));
	MaterialFloat Local70 = MaterialStoreTexSample(Parameters, Local69, 1);
	MaterialFloat3 Local71 = lerp(Local61.rgb,Local69.rgb,Local38.r.r);
	FWSVector2 Local72 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local53)), WSGetY(DERIV_BASE_VALUE(Local53)));
	FWSVector2 Local73 = WSAdd(MaterialFloat2(Local52,0.00000000), DERIV_BASE_VALUE(Local72));
	FWSVector2 Local74 = WSAdd(MaterialFloat2(0.00000000,Local56), DERIV_BASE_VALUE(Local72));
	FWSVector2 Local75 = WSAdd(DERIV_BASE_VALUE(Local73), DERIV_BASE_VALUE(Local74));
	MaterialFloat2 Local76 = WSApplyAddressMode(DERIV_BASE_VALUE(Local75), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local77 = MaterialStoreTexCoordScale(Parameters, Local76, 1);
	MaterialFloat4 Local78 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local76,View.MaterialTextureMipBias));
	MaterialFloat Local79 = MaterialStoreTexSample(Parameters, Local78, 1);
	MaterialFloat3 Local80 = lerp(Local71,Local78.rgb,Local50.r.r);
	MaterialFloat3 Local81 = lerp(Local51,Local80,0.50000000);
	FWSVector3 Local82 = WSMultiply(DERIV_BASE_VALUE(Local1), ((MaterialFloat3)Material.PreshaderBuffer[0].w));
	FWSVector2 Local83 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local82)), WSGetZ(DERIV_BASE_VALUE(Local82)));
	FWSVector2 Local84 = WSAdd(MaterialFloat2(Local21,0.00000000), DERIV_BASE_VALUE(Local83));
	FWSVector2 Local85 = WSAdd(MaterialFloat2(0.00000000,Local17), DERIV_BASE_VALUE(Local83));
	FWSVector2 Local86 = WSAdd(DERIV_BASE_VALUE(Local84), DERIV_BASE_VALUE(Local85));
	MaterialFloat2 Local87 = WSApplyAddressMode(DERIV_BASE_VALUE(Local86), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local88 = MaterialStoreTexCoordScale(Parameters, Local87, 1);
	MaterialFloat4 Local89 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local87,View.MaterialTextureMipBias));
	MaterialFloat Local90 = MaterialStoreTexSample(Parameters, Local89, 1);
	FWSVector2 Local91 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local82)), WSGetZ(DERIV_BASE_VALUE(Local82)));
	FWSVector2 Local92 = WSAdd(MaterialFloat2(Local21,0.00000000), DERIV_BASE_VALUE(Local91));
	FWSVector2 Local93 = WSAdd(MaterialFloat2(0.00000000,Local17), DERIV_BASE_VALUE(Local91));
	FWSVector2 Local94 = WSAdd(DERIV_BASE_VALUE(Local92), DERIV_BASE_VALUE(Local93));
	MaterialFloat2 Local95 = WSApplyAddressMode(DERIV_BASE_VALUE(Local94), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local96 = MaterialStoreTexCoordScale(Parameters, Local95, 1);
	MaterialFloat4 Local97 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local95,View.MaterialTextureMipBias));
	MaterialFloat Local98 = MaterialStoreTexSample(Parameters, Local97, 1);
	MaterialFloat3 Local99 = lerp(Local89.rgb,Local97.rgb,Local38.r.r);
	FWSVector2 Local100 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local82)), WSGetY(DERIV_BASE_VALUE(Local82)));
	FWSVector2 Local101 = WSAdd(MaterialFloat2(Local21,0.00000000), DERIV_BASE_VALUE(Local100));
	FWSVector2 Local102 = WSAdd(MaterialFloat2(0.00000000,Local17), DERIV_BASE_VALUE(Local100));
	FWSVector2 Local103 = WSAdd(DERIV_BASE_VALUE(Local101), DERIV_BASE_VALUE(Local102));
	MaterialFloat2 Local104 = WSApplyAddressMode(DERIV_BASE_VALUE(Local103), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local105 = MaterialStoreTexCoordScale(Parameters, Local104, 1);
	MaterialFloat4 Local106 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local104,View.MaterialTextureMipBias));
	MaterialFloat Local107 = MaterialStoreTexSample(Parameters, Local106, 1);
	MaterialFloat3 Local108 = lerp(Local99,Local106.rgb,Local50.r.r);
	FWSVector3 Local109 = WSMultiply(DERIV_BASE_VALUE(Local1), ((MaterialFloat3)Material.PreshaderBuffer[1].x));
	FWSVector2 Local110 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local109)), WSGetZ(DERIV_BASE_VALUE(Local109)));
	FWSVector2 Local111 = WSAdd(MaterialFloat2(Local56,0.00000000), DERIV_BASE_VALUE(Local110));
	FWSVector2 Local112 = WSAdd(MaterialFloat2(0.00000000,Local52), DERIV_BASE_VALUE(Local110));
	FWSVector2 Local113 = WSAdd(DERIV_BASE_VALUE(Local111), DERIV_BASE_VALUE(Local112));
	MaterialFloat2 Local114 = WSApplyAddressMode(DERIV_BASE_VALUE(Local113), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local115 = MaterialStoreTexCoordScale(Parameters, Local114, 1);
	MaterialFloat4 Local116 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local114,View.MaterialTextureMipBias));
	MaterialFloat Local117 = MaterialStoreTexSample(Parameters, Local116, 1);
	FWSVector2 Local118 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local109)), WSGetZ(DERIV_BASE_VALUE(Local109)));
	FWSVector2 Local119 = WSAdd(MaterialFloat2(Local56,0.00000000), DERIV_BASE_VALUE(Local118));
	FWSVector2 Local120 = WSAdd(MaterialFloat2(0.00000000,Local52), DERIV_BASE_VALUE(Local118));
	FWSVector2 Local121 = WSAdd(DERIV_BASE_VALUE(Local119), DERIV_BASE_VALUE(Local120));
	MaterialFloat2 Local122 = WSApplyAddressMode(DERIV_BASE_VALUE(Local121), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local123 = MaterialStoreTexCoordScale(Parameters, Local122, 1);
	MaterialFloat4 Local124 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local122,View.MaterialTextureMipBias));
	MaterialFloat Local125 = MaterialStoreTexSample(Parameters, Local124, 1);
	MaterialFloat3 Local126 = lerp(Local116.rgb,Local124.rgb,Local38.r.r);
	FWSVector2 Local127 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local109)), WSGetY(DERIV_BASE_VALUE(Local109)));
	FWSVector2 Local128 = WSAdd(MaterialFloat2(Local56,0.00000000), DERIV_BASE_VALUE(Local127));
	FWSVector2 Local129 = WSAdd(MaterialFloat2(0.00000000,Local52), DERIV_BASE_VALUE(Local127));
	FWSVector2 Local130 = WSAdd(DERIV_BASE_VALUE(Local128), DERIV_BASE_VALUE(Local129));
	MaterialFloat2 Local131 = WSApplyAddressMode(DERIV_BASE_VALUE(Local130), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local132 = MaterialStoreTexCoordScale(Parameters, Local131, 1);
	MaterialFloat4 Local133 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local131,View.MaterialTextureMipBias));
	MaterialFloat Local134 = MaterialStoreTexSample(Parameters, Local133, 1);
	MaterialFloat3 Local135 = lerp(Local126,Local133.rgb,Local50.r.r);
	MaterialFloat3 Local136 = lerp(Local108,Local135,0.50000000);
	MaterialFloat3 Local137 = lerp(Local81,Local136,0.50000000);
	MaterialFloat Local138 = (Local16 * Local137.b);
	MaterialFloat3 Local139 = (((MaterialFloat3)Local138) * Material.PreshaderBuffer[1].yzw);
	MaterialFloat3 Local140 = (Local139 * ((MaterialFloat3)Material.PreshaderBuffer[2].x));
	MaterialFloat Local141 = dot(WorldNormalCopy,Parameters.CameraVector);
	MaterialFloat Local142 = max(0.00000000,Local141);
	MaterialFloat Local143 = (1.00000000 - Local142);
	MaterialFloat Local144 = abs(Local143);
	MaterialFloat Local145 = max(Local144,0.00010000);
	MaterialFloat Local146 = PositiveClampedPow(Local145,Material.PreshaderBuffer[2].y);
	MaterialFloat Local147 = (Local146 * Material.PreshaderBuffer[2].z);
	MaterialFloat Local148 = (Local147 + Material.PreshaderBuffer[2].w);
	MaterialFloat Local149 = (Local148 + Material.PreshaderBuffer[3].x);
	MaterialFloat3 Local150 = lerp(Local140,MaterialFloat3(0.00000000,0.00000000,0.00000000).rgb,Local149);
	MaterialFloat3 Local151 = lerp(Local150,Material.PreshaderBuffer[4].xyz,Material.PreshaderBuffer[3].y);

	PixelMaterialInputs.EmissiveColor = Local151;
	PixelMaterialInputs.Opacity = 1.00000000;
	PixelMaterialInputs.OpacityMask = 1.00000000;
	PixelMaterialInputs.BaseColor = MaterialFloat3(0.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Metallic = 0.00000000;
	PixelMaterialInputs.Specular = 0.50000000;
	PixelMaterialInputs.Roughness = 0.50000000;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Normal = MaterialFloat3(0.00000000,0.00000000,1.00000000);
	PixelMaterialInputs.Tangent = MaterialFloat3(1.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Subsurface = 0;
	PixelMaterialInputs.AmbientOcclusion = 1.00000000;
	PixelMaterialInputs.Refraction = MaterialFloat3(MaterialFloat3(1.00000000,0.00000000,0.00000000).xy,Material.PreshaderBuffer[4].w);
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

	#define HAS_WORLDSPACE_NORMAL 0
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