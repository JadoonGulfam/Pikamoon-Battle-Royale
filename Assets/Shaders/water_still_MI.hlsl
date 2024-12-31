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
	float4 PreshaderBuffer[11];
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
	Material.PreshaderBuffer[3] = float4(0.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[4] = float4(0.500000,3.000000,-2.000000,2.000000);//(Unknown)
	Material.PreshaderBuffer[5] = float4(-1.000000,0.007843,0.035294,0.035294);//(Unknown)
	Material.PreshaderBuffer[6] = float4(0.015000,0.138750,0.150000,-0.000651);//(Unknown)
	Material.PreshaderBuffer[7] = float4(-0.000488,-0.000391,-0.000326,0.750000);//(Unknown)
	Material.PreshaderBuffer[8] = float4(0.000651,2.578125,4.289887,5.000000);//(Unknown)
	Material.PreshaderBuffer[9] = float4(0.020000,1.150000,0.080000,2.000000);//(Unknown)
	Material.PreshaderBuffer[10] = float4(-1.000000,0.750000,0.000000,0.000000);//(Unknown)
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

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = Local141;


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
	MaterialFloat3 Local142 = lerp(MaterialFloat3(0.00000000,0.00000000,0.00000000),Material.PreshaderBuffer[3].yzw,Material.PreshaderBuffer[3].x);
	MaterialFloat Local143 = dot(WorldNormalCopy,Parameters.CameraVector);
	MaterialFloat Local144 = max(0.00000000,Local143);
	MaterialFloat Local145 = (1.00000000 - Local144);
	MaterialFloat Local146 = abs(Local145);
	MaterialFloat Local147 = max(Local146,0.00010000);
	MaterialFloat Local148 = PositiveClampedPow(Local147,Material.PreshaderBuffer[4].x);
	MaterialFloat Local149 = (Local148 * Material.PreshaderBuffer[4].y);
	MaterialFloat Local150 = (Local149 + Material.PreshaderBuffer[4].z);
	MaterialFloat Local151 = lerp(Material.PreshaderBuffer[5].x,Material.PreshaderBuffer[4].w,Local150);
	MaterialFloat Local152 = saturate(Local151);
	MaterialFloat3 Local153 = lerp(Material.PreshaderBuffer[6].xyz,Material.PreshaderBuffer[5].yzw,Local152.r);
	MaterialFloat Local154 = (View.GameTime * 0.01000000);
	FWSVector3 Local155 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[6].w));
	FWSVector2 Local156 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local155)), WSGetZ(DERIV_BASE_VALUE(Local155)));
	FWSVector2 Local157 = WSAdd(MaterialFloat2(Local154,0.00000000), DERIV_BASE_VALUE(Local156));
	MaterialFloat Local158 = (View.GameTime * 0.04000000);
	FWSVector2 Local159 = WSAdd(MaterialFloat2(0.00000000,Local158), DERIV_BASE_VALUE(Local156));
	FWSVector2 Local160 = WSAdd(DERIV_BASE_VALUE(Local157), DERIV_BASE_VALUE(Local159));
	MaterialFloat2 Local161 = WSApplyAddressMode(DERIV_BASE_VALUE(Local160), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local162 = MaterialStoreTexCoordScale(Parameters, Local161, 4);
	MaterialFloat4 Local163 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local161,View.MaterialTextureMipBias));
	MaterialFloat Local164 = MaterialStoreTexSample(Parameters, Local163, 4);
	FWSVector2 Local165 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local155)), WSGetZ(DERIV_BASE_VALUE(Local155)));
	FWSVector2 Local166 = WSAdd(MaterialFloat2(Local154,0.00000000), DERIV_BASE_VALUE(Local165));
	FWSVector2 Local167 = WSAdd(MaterialFloat2(0.00000000,Local158), DERIV_BASE_VALUE(Local165));
	FWSVector2 Local168 = WSAdd(DERIV_BASE_VALUE(Local166), DERIV_BASE_VALUE(Local167));
	MaterialFloat2 Local169 = WSApplyAddressMode(DERIV_BASE_VALUE(Local168), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local170 = MaterialStoreTexCoordScale(Parameters, Local169, 4);
	MaterialFloat4 Local171 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local169,View.MaterialTextureMipBias));
	MaterialFloat Local172 = MaterialStoreTexSample(Parameters, Local171, 4);
	MaterialFloat3 Local173 = lerp(Local163.rgb,Local171.rgb,Local23.r.r);
	FWSVector2 Local174 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local155)), WSGetY(DERIV_BASE_VALUE(Local155)));
	FWSVector2 Local175 = WSAdd(MaterialFloat2(Local154,0.00000000), DERIV_BASE_VALUE(Local174));
	FWSVector2 Local176 = WSAdd(MaterialFloat2(0.00000000,Local158), DERIV_BASE_VALUE(Local174));
	FWSVector2 Local177 = WSAdd(DERIV_BASE_VALUE(Local175), DERIV_BASE_VALUE(Local176));
	MaterialFloat2 Local178 = WSApplyAddressMode(DERIV_BASE_VALUE(Local177), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local179 = MaterialStoreTexCoordScale(Parameters, Local178, 4);
	MaterialFloat4 Local180 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local178,View.MaterialTextureMipBias));
	MaterialFloat Local181 = MaterialStoreTexSample(Parameters, Local180, 4);
	MaterialFloat3 Local182 = lerp(Local173,Local180.rgb,Local35.r.r);
	MaterialFloat Local183 = (View.GameTime * -0.01000000);
	FWSVector3 Local184 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[7].x));
	FWSVector2 Local185 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local184)), WSGetZ(DERIV_BASE_VALUE(Local184)));
	FWSVector2 Local186 = WSAdd(MaterialFloat2(Local183,0.00000000), DERIV_BASE_VALUE(Local185));
	MaterialFloat Local187 = (View.GameTime * -0.04000000);
	FWSVector2 Local188 = WSAdd(MaterialFloat2(0.00000000,Local187), DERIV_BASE_VALUE(Local185));
	FWSVector2 Local189 = WSAdd(DERIV_BASE_VALUE(Local186), DERIV_BASE_VALUE(Local188));
	MaterialFloat2 Local190 = WSApplyAddressMode(DERIV_BASE_VALUE(Local189), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local191 = MaterialStoreTexCoordScale(Parameters, Local190, 4);
	MaterialFloat4 Local192 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local190,View.MaterialTextureMipBias));
	MaterialFloat Local193 = MaterialStoreTexSample(Parameters, Local192, 4);
	FWSVector2 Local194 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local184)), WSGetZ(DERIV_BASE_VALUE(Local184)));
	FWSVector2 Local195 = WSAdd(MaterialFloat2(Local183,0.00000000), DERIV_BASE_VALUE(Local194));
	FWSVector2 Local196 = WSAdd(MaterialFloat2(0.00000000,Local187), DERIV_BASE_VALUE(Local194));
	FWSVector2 Local197 = WSAdd(DERIV_BASE_VALUE(Local195), DERIV_BASE_VALUE(Local196));
	MaterialFloat2 Local198 = WSApplyAddressMode(DERIV_BASE_VALUE(Local197), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local199 = MaterialStoreTexCoordScale(Parameters, Local198, 4);
	MaterialFloat4 Local200 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local198,View.MaterialTextureMipBias));
	MaterialFloat Local201 = MaterialStoreTexSample(Parameters, Local200, 4);
	MaterialFloat3 Local202 = lerp(Local192.rgb,Local200.rgb,Local23.r.r);
	FWSVector2 Local203 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local184)), WSGetY(DERIV_BASE_VALUE(Local184)));
	FWSVector2 Local204 = WSAdd(MaterialFloat2(Local183,0.00000000), DERIV_BASE_VALUE(Local203));
	FWSVector2 Local205 = WSAdd(MaterialFloat2(0.00000000,Local187), DERIV_BASE_VALUE(Local203));
	FWSVector2 Local206 = WSAdd(DERIV_BASE_VALUE(Local204), DERIV_BASE_VALUE(Local205));
	MaterialFloat2 Local207 = WSApplyAddressMode(DERIV_BASE_VALUE(Local206), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local208 = MaterialStoreTexCoordScale(Parameters, Local207, 4);
	MaterialFloat4 Local209 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local207,View.MaterialTextureMipBias));
	MaterialFloat Local210 = MaterialStoreTexSample(Parameters, Local209, 4);
	MaterialFloat3 Local211 = lerp(Local202,Local209.rgb,Local35.r.r);
	MaterialFloat3 Local212 = lerp(Local182,Local211,0.50000000);
	FWSVector3 Local213 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[7].y));
	FWSVector2 Local214 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local213)), WSGetZ(DERIV_BASE_VALUE(Local213)));
	FWSVector2 Local215 = WSAdd(MaterialFloat2(Local158,0.00000000), DERIV_BASE_VALUE(Local214));
	FWSVector2 Local216 = WSAdd(MaterialFloat2(0.00000000,Local154), DERIV_BASE_VALUE(Local214));
	FWSVector2 Local217 = WSAdd(DERIV_BASE_VALUE(Local215), DERIV_BASE_VALUE(Local216));
	MaterialFloat2 Local218 = WSApplyAddressMode(DERIV_BASE_VALUE(Local217), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local219 = MaterialStoreTexCoordScale(Parameters, Local218, 4);
	MaterialFloat4 Local220 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local218,View.MaterialTextureMipBias));
	MaterialFloat Local221 = MaterialStoreTexSample(Parameters, Local220, 4);
	FWSVector2 Local222 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local213)), WSGetZ(DERIV_BASE_VALUE(Local213)));
	FWSVector2 Local223 = WSAdd(MaterialFloat2(Local158,0.00000000), DERIV_BASE_VALUE(Local222));
	FWSVector2 Local224 = WSAdd(MaterialFloat2(0.00000000,Local154), DERIV_BASE_VALUE(Local222));
	FWSVector2 Local225 = WSAdd(DERIV_BASE_VALUE(Local223), DERIV_BASE_VALUE(Local224));
	MaterialFloat2 Local226 = WSApplyAddressMode(DERIV_BASE_VALUE(Local225), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local227 = MaterialStoreTexCoordScale(Parameters, Local226, 4);
	MaterialFloat4 Local228 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local226,View.MaterialTextureMipBias));
	MaterialFloat Local229 = MaterialStoreTexSample(Parameters, Local228, 4);
	MaterialFloat3 Local230 = lerp(Local220.rgb,Local228.rgb,Local23.r.r);
	FWSVector2 Local231 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local213)), WSGetY(DERIV_BASE_VALUE(Local213)));
	FWSVector2 Local232 = WSAdd(MaterialFloat2(Local158,0.00000000), DERIV_BASE_VALUE(Local231));
	FWSVector2 Local233 = WSAdd(MaterialFloat2(0.00000000,Local154), DERIV_BASE_VALUE(Local231));
	FWSVector2 Local234 = WSAdd(DERIV_BASE_VALUE(Local232), DERIV_BASE_VALUE(Local233));
	MaterialFloat2 Local235 = WSApplyAddressMode(DERIV_BASE_VALUE(Local234), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local236 = MaterialStoreTexCoordScale(Parameters, Local235, 4);
	MaterialFloat4 Local237 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local235,View.MaterialTextureMipBias));
	MaterialFloat Local238 = MaterialStoreTexSample(Parameters, Local237, 4);
	MaterialFloat3 Local239 = lerp(Local230,Local237.rgb,Local35.r.r);
	FWSVector3 Local240 = WSMultiply(DERIV_BASE_VALUE(Local2), ((MaterialFloat3)Material.PreshaderBuffer[7].z));
	FWSVector2 Local241 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local240)), WSGetZ(DERIV_BASE_VALUE(Local240)));
	FWSVector2 Local242 = WSAdd(MaterialFloat2(Local187,0.00000000), DERIV_BASE_VALUE(Local241));
	FWSVector2 Local243 = WSAdd(MaterialFloat2(0.00000000,Local183), DERIV_BASE_VALUE(Local241));
	FWSVector2 Local244 = WSAdd(DERIV_BASE_VALUE(Local242), DERIV_BASE_VALUE(Local243));
	MaterialFloat2 Local245 = WSApplyAddressMode(DERIV_BASE_VALUE(Local244), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local246 = MaterialStoreTexCoordScale(Parameters, Local245, 4);
	MaterialFloat4 Local247 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local245,View.MaterialTextureMipBias));
	MaterialFloat Local248 = MaterialStoreTexSample(Parameters, Local247, 4);
	FWSVector2 Local249 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local240)), WSGetZ(DERIV_BASE_VALUE(Local240)));
	FWSVector2 Local250 = WSAdd(MaterialFloat2(Local187,0.00000000), DERIV_BASE_VALUE(Local249));
	FWSVector2 Local251 = WSAdd(MaterialFloat2(0.00000000,Local183), DERIV_BASE_VALUE(Local249));
	FWSVector2 Local252 = WSAdd(DERIV_BASE_VALUE(Local250), DERIV_BASE_VALUE(Local251));
	MaterialFloat2 Local253 = WSApplyAddressMode(DERIV_BASE_VALUE(Local252), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local254 = MaterialStoreTexCoordScale(Parameters, Local253, 4);
	MaterialFloat4 Local255 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local253,View.MaterialTextureMipBias));
	MaterialFloat Local256 = MaterialStoreTexSample(Parameters, Local255, 4);
	MaterialFloat3 Local257 = lerp(Local247.rgb,Local255.rgb,Local23.r.r);
	FWSVector2 Local258 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local240)), WSGetY(DERIV_BASE_VALUE(Local240)));
	FWSVector2 Local259 = WSAdd(MaterialFloat2(Local187,0.00000000), DERIV_BASE_VALUE(Local258));
	FWSVector2 Local260 = WSAdd(MaterialFloat2(0.00000000,Local183), DERIV_BASE_VALUE(Local258));
	FWSVector2 Local261 = WSAdd(DERIV_BASE_VALUE(Local259), DERIV_BASE_VALUE(Local260));
	MaterialFloat2 Local262 = WSApplyAddressMode(DERIV_BASE_VALUE(Local261), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local263 = MaterialStoreTexCoordScale(Parameters, Local262, 4);
	MaterialFloat4 Local264 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local262,View.MaterialTextureMipBias));
	MaterialFloat Local265 = MaterialStoreTexSample(Parameters, Local264, 4);
	MaterialFloat3 Local266 = lerp(Local257,Local264.rgb,Local35.r.r);
	MaterialFloat3 Local267 = lerp(Local239,Local266,0.50000000);
	MaterialFloat3 Local268 = lerp(Local212,Local267,0.50000000);
	MaterialFloat Local269 = PositiveClampedPow(Local268.r,Material.PreshaderBuffer[7].w);
	MaterialFloat Local270 = GetPixelDepth(Parameters);
	MaterialFloat Local271 = CalcSceneDepth(ScreenAlignedPosition(GetScreenPosition(Parameters)));
	MaterialFloat Local272 = (Local271 - DERIV_BASE_VALUE(Local270));
	MaterialFloat Local273 = (Local272 * Material.PreshaderBuffer[8].x);
	MaterialFloat Local274 = saturate(Local273);
	MaterialFloat Local275 = PositiveClampedPow(Local274,0.25000000);
	MaterialFloat Local276 = lerp((0.00000000 - 2.00000000),(2.00000000 + 1.00000000),Local275);
	MaterialFloat Local277 = saturate(Local276);
	MaterialFloat Local278 = (1.00000000 - Local277.r);
	MaterialFloat Local279 = (View.GameTime * -0.25000000);
	MaterialFloat Local280 = (Local278 * Local278);
	MaterialFloat Local281 = (Local280.r + Local280.r);
	MaterialFloat Local282 = sqrt(Local281);
	MaterialFloat2 Local283 = (MaterialFloat2(0.00000000,Local279) + ((MaterialFloat2)Local282));
	MaterialFloat2 Local284 = (Local283 * ((MaterialFloat2)0.50000000));
	MaterialFloat Local285 = MaterialStoreTexCoordScale(Parameters, Local284, 2);
	MaterialFloat4 Local286 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_2,samplerMaterial_Texture2D_2,Local284,View.MaterialTextureMipBias));
	MaterialFloat Local287 = MaterialStoreTexSample(Parameters, Local286, 2);
	MaterialFloat Local288 = saturate(Local286.g);
	MaterialFloat Local289 = (Local278 * Local288);
	MaterialFloat Local290 = (Local269 * Local289);
	MaterialFloat3 Local291 = (((MaterialFloat3)Local290) * Material.PreshaderBuffer[8].yzw);
	MaterialFloat3 Local292 = (Local153 + Local291);
	FWSVector2 Local293 = WSAdd(MaterialFloat2(Local154,0.00000000), DERIV_BASE_VALUE(Local40));
	FWSVector2 Local294 = WSAdd(MaterialFloat2(0.00000000,Local158), DERIV_BASE_VALUE(Local40));
	FWSVector2 Local295 = WSAdd(DERIV_BASE_VALUE(Local293), DERIV_BASE_VALUE(Local294));
	MaterialFloat2 Local296 = WSApplyAddressMode(DERIV_BASE_VALUE(Local295), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local297 = MaterialStoreTexCoordScale(Parameters, Local296, 1);
	MaterialFloat4 Local298 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local296,View.MaterialTextureMipBias));
	MaterialFloat Local299 = MaterialStoreTexSample(Parameters, Local298, 1);
	FWSVector2 Local300 = WSAdd(MaterialFloat2(Local154,0.00000000), DERIV_BASE_VALUE(Local49));
	FWSVector2 Local301 = WSAdd(MaterialFloat2(0.00000000,Local158), DERIV_BASE_VALUE(Local49));
	FWSVector2 Local302 = WSAdd(DERIV_BASE_VALUE(Local300), DERIV_BASE_VALUE(Local301));
	MaterialFloat2 Local303 = WSApplyAddressMode(DERIV_BASE_VALUE(Local302), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local304 = MaterialStoreTexCoordScale(Parameters, Local303, 1);
	MaterialFloat4 Local305 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local303,View.MaterialTextureMipBias));
	MaterialFloat Local306 = MaterialStoreTexSample(Parameters, Local305, 1);
	MaterialFloat3 Local307 = lerp(Local298.rgb,Local305.rgb,Local23.r.r);
	FWSVector2 Local308 = WSAdd(MaterialFloat2(Local154,0.00000000), DERIV_BASE_VALUE(Local58));
	FWSVector2 Local309 = WSAdd(MaterialFloat2(0.00000000,Local158), DERIV_BASE_VALUE(Local58));
	FWSVector2 Local310 = WSAdd(DERIV_BASE_VALUE(Local308), DERIV_BASE_VALUE(Local309));
	MaterialFloat2 Local311 = WSApplyAddressMode(DERIV_BASE_VALUE(Local310), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local312 = MaterialStoreTexCoordScale(Parameters, Local311, 1);
	MaterialFloat4 Local313 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local311,View.MaterialTextureMipBias));
	MaterialFloat Local314 = MaterialStoreTexSample(Parameters, Local313, 1);
	MaterialFloat3 Local315 = lerp(Local307,Local313.rgb,Local35.r.r);
	FWSVector2 Local316 = WSAdd(MaterialFloat2(Local183,0.00000000), DERIV_BASE_VALUE(Local4));
	FWSVector2 Local317 = WSAdd(MaterialFloat2(0.00000000,Local187), DERIV_BASE_VALUE(Local4));
	FWSVector2 Local318 = WSAdd(DERIV_BASE_VALUE(Local316), DERIV_BASE_VALUE(Local317));
	MaterialFloat2 Local319 = WSApplyAddressMode(DERIV_BASE_VALUE(Local318), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local320 = MaterialStoreTexCoordScale(Parameters, Local319, 1);
	MaterialFloat4 Local321 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local319,View.MaterialTextureMipBias));
	MaterialFloat Local322 = MaterialStoreTexSample(Parameters, Local321, 1);
	FWSVector2 Local323 = WSAdd(MaterialFloat2(Local183,0.00000000), DERIV_BASE_VALUE(Local13));
	FWSVector2 Local324 = WSAdd(MaterialFloat2(0.00000000,Local187), DERIV_BASE_VALUE(Local13));
	FWSVector2 Local325 = WSAdd(DERIV_BASE_VALUE(Local323), DERIV_BASE_VALUE(Local324));
	MaterialFloat2 Local326 = WSApplyAddressMode(DERIV_BASE_VALUE(Local325), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local327 = MaterialStoreTexCoordScale(Parameters, Local326, 1);
	MaterialFloat4 Local328 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local326,View.MaterialTextureMipBias));
	MaterialFloat Local329 = MaterialStoreTexSample(Parameters, Local328, 1);
	MaterialFloat3 Local330 = lerp(Local321.rgb,Local328.rgb,Local23.r.r);
	FWSVector2 Local331 = WSAdd(MaterialFloat2(Local183,0.00000000), DERIV_BASE_VALUE(Local25));
	FWSVector2 Local332 = WSAdd(MaterialFloat2(0.00000000,Local187), DERIV_BASE_VALUE(Local25));
	FWSVector2 Local333 = WSAdd(DERIV_BASE_VALUE(Local331), DERIV_BASE_VALUE(Local332));
	MaterialFloat2 Local334 = WSApplyAddressMode(DERIV_BASE_VALUE(Local333), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local335 = MaterialStoreTexCoordScale(Parameters, Local334, 1);
	MaterialFloat4 Local336 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local334,View.MaterialTextureMipBias));
	MaterialFloat Local337 = MaterialStoreTexSample(Parameters, Local336, 1);
	MaterialFloat3 Local338 = lerp(Local330,Local336.rgb,Local35.r.r);
	MaterialFloat3 Local339 = lerp(Local315,Local338,0.50000000);
	FWSVector2 Local340 = WSAdd(MaterialFloat2(Local158,0.00000000), DERIV_BASE_VALUE(Local75));
	FWSVector2 Local341 = WSAdd(MaterialFloat2(0.00000000,Local154), DERIV_BASE_VALUE(Local75));
	FWSVector2 Local342 = WSAdd(DERIV_BASE_VALUE(Local340), DERIV_BASE_VALUE(Local341));
	MaterialFloat2 Local343 = WSApplyAddressMode(DERIV_BASE_VALUE(Local342), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local344 = MaterialStoreTexCoordScale(Parameters, Local343, 1);
	MaterialFloat4 Local345 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local343,View.MaterialTextureMipBias));
	MaterialFloat Local346 = MaterialStoreTexSample(Parameters, Local345, 1);
	FWSVector2 Local347 = WSAdd(MaterialFloat2(Local158,0.00000000), DERIV_BASE_VALUE(Local84));
	FWSVector2 Local348 = WSAdd(MaterialFloat2(0.00000000,Local154), DERIV_BASE_VALUE(Local84));
	FWSVector2 Local349 = WSAdd(DERIV_BASE_VALUE(Local347), DERIV_BASE_VALUE(Local348));
	MaterialFloat2 Local350 = WSApplyAddressMode(DERIV_BASE_VALUE(Local349), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local351 = MaterialStoreTexCoordScale(Parameters, Local350, 1);
	MaterialFloat4 Local352 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local350,View.MaterialTextureMipBias));
	MaterialFloat Local353 = MaterialStoreTexSample(Parameters, Local352, 1);
	MaterialFloat3 Local354 = lerp(Local345.rgb,Local352.rgb,Local23.r.r);
	FWSVector2 Local355 = WSAdd(MaterialFloat2(Local158,0.00000000), DERIV_BASE_VALUE(Local93));
	FWSVector2 Local356 = WSAdd(MaterialFloat2(0.00000000,Local154), DERIV_BASE_VALUE(Local93));
	FWSVector2 Local357 = WSAdd(DERIV_BASE_VALUE(Local355), DERIV_BASE_VALUE(Local356));
	MaterialFloat2 Local358 = WSApplyAddressMode(DERIV_BASE_VALUE(Local357), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local359 = MaterialStoreTexCoordScale(Parameters, Local358, 1);
	MaterialFloat4 Local360 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local358,View.MaterialTextureMipBias));
	MaterialFloat Local361 = MaterialStoreTexSample(Parameters, Local360, 1);
	MaterialFloat3 Local362 = lerp(Local354,Local360.rgb,Local35.r.r);
	FWSVector2 Local363 = WSAdd(MaterialFloat2(Local187,0.00000000), DERIV_BASE_VALUE(Local105));
	FWSVector2 Local364 = WSAdd(MaterialFloat2(0.00000000,Local183), DERIV_BASE_VALUE(Local105));
	FWSVector2 Local365 = WSAdd(DERIV_BASE_VALUE(Local363), DERIV_BASE_VALUE(Local364));
	MaterialFloat2 Local366 = WSApplyAddressMode(DERIV_BASE_VALUE(Local365), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local367 = MaterialStoreTexCoordScale(Parameters, Local366, 1);
	MaterialFloat4 Local368 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local366,View.MaterialTextureMipBias));
	MaterialFloat Local369 = MaterialStoreTexSample(Parameters, Local368, 1);
	FWSVector2 Local370 = WSAdd(MaterialFloat2(Local187,0.00000000), DERIV_BASE_VALUE(Local114));
	FWSVector2 Local371 = WSAdd(MaterialFloat2(0.00000000,Local183), DERIV_BASE_VALUE(Local114));
	FWSVector2 Local372 = WSAdd(DERIV_BASE_VALUE(Local370), DERIV_BASE_VALUE(Local371));
	MaterialFloat2 Local373 = WSApplyAddressMode(DERIV_BASE_VALUE(Local372), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local374 = MaterialStoreTexCoordScale(Parameters, Local373, 1);
	MaterialFloat4 Local375 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local373,View.MaterialTextureMipBias));
	MaterialFloat Local376 = MaterialStoreTexSample(Parameters, Local375, 1);
	MaterialFloat3 Local377 = lerp(Local368.rgb,Local375.rgb,Local23.r.r);
	FWSVector2 Local378 = WSAdd(MaterialFloat2(Local187,0.00000000), DERIV_BASE_VALUE(Local123));
	FWSVector2 Local379 = WSAdd(MaterialFloat2(0.00000000,Local183), DERIV_BASE_VALUE(Local123));
	FWSVector2 Local380 = WSAdd(DERIV_BASE_VALUE(Local378), DERIV_BASE_VALUE(Local379));
	MaterialFloat2 Local381 = WSApplyAddressMode(DERIV_BASE_VALUE(Local380), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local382 = MaterialStoreTexCoordScale(Parameters, Local381, 1);
	MaterialFloat4 Local383 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local381,View.MaterialTextureMipBias));
	MaterialFloat Local384 = MaterialStoreTexSample(Parameters, Local383, 1);
	MaterialFloat3 Local385 = lerp(Local377,Local383.rgb,Local35.r.r);
	MaterialFloat3 Local386 = lerp(Local362,Local385,0.50000000);
	MaterialFloat3 Local387 = lerp(Local339,Local386,0.50000000);
	MaterialFloat Local388 = (Local272 * Material.PreshaderBuffer[9].x);
	MaterialFloat Local389 = saturate(Local388);
	MaterialFloat Local390 = (Material.PreshaderBuffer[9].y * Local389);
	MaterialFloat Local391 = PositiveClampedPow(Local147,Material.PreshaderBuffer[9].z);
	MaterialFloat Local392 = (Local391 * Material.PreshaderBuffer[9].w);
	MaterialFloat Local393 = (Local392 + Material.PreshaderBuffer[10].x);
	MaterialFloat Local394 = lerp(Local393,1.00000000,0.25000000);
	MaterialFloat Local395 = (Local390 * Local394);

	PixelMaterialInputs.EmissiveColor = Local142;
	PixelMaterialInputs.Opacity = Local395;
	PixelMaterialInputs.OpacityMask = 1.00000000;
	PixelMaterialInputs.BaseColor = Local292;
	PixelMaterialInputs.Metallic = Local387.b;
	PixelMaterialInputs.Specular = 0.50000000;
	PixelMaterialInputs.Roughness = Local387.g;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Normal = Local141;
	PixelMaterialInputs.Tangent = Parameters.TangentToWorld[0];
	PixelMaterialInputs.Subsurface = 0;
	PixelMaterialInputs.AmbientOcclusion = Local387.r;
	PixelMaterialInputs.Refraction = MaterialFloat3(MaterialFloat2(Material.PreshaderBuffer[10].y,0.0f),Material.PreshaderBuffer[10].z);
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