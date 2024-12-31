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
TEXTURE2D(       Material_Texture2D_5 );
SAMPLER(  samplerMaterial_Texture2D_5 );
float4 Material_Texture2D_5_TexelSize;
float4 Material_Texture2D_5_ST;

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
	float4 PreshaderBuffer[9];
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
	Material.PreshaderBuffer[0] = float4(6.000000,-0.003906,-0.000977,25.000000);//(Unknown)
	Material.PreshaderBuffer[1] = float4(-0.000326,-0.000244,-0.000195,-0.000163);//(Unknown)
	Material.PreshaderBuffer[2] = float4(0.468750,0.954914,1.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[3] = float4(0.500000,0.500000,0.500000,0.000000);//(Unknown)
	Material.PreshaderBuffer[4] = float4(0.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[5] = float4(1.000000,1.000000,1.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[6] = float4(1.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[7] = float4(1.000000,1.000000,1.000000,-0.000977);//(Unknown)
	Material.PreshaderBuffer[8] = float4(0.500000,0.000000,0.000000,0.000000);//(Unknown)
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
	MaterialFloat2 Local0 = Parameters.TexCoords[0].xy;
	MaterialFloat2 Local1 = (DERIV_BASE_VALUE(Local0) * ((MaterialFloat2)Material.PreshaderBuffer[0].x));
	MaterialFloat Local2 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local1), 1);
	MaterialFloat4 Local3 = UnpackNormalMap(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,DERIV_BASE_VALUE(Local1),View.MaterialTextureMipBias));
	MaterialFloat Local4 = MaterialStoreTexSample(Parameters, Local3, 1);
	MaterialFloat Local5 = (Local3.rgb.b + 1.00000000);
	MaterialFloat3 Local6 = normalize(Parameters.TangentToWorld[2]);
	MaterialFloat3 Local7 = cross(Local6,normalize(MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb));
	MaterialFloat Local8 = dot(Local7,Local7);
	MaterialFloat3 Local9 = normalize(Local7);
	MaterialFloat4 Local10 = select((abs(Local8 - 0.00000100) > 0.00001000), select((Local8 >= 0.00000100), MaterialFloat4(Local9,0.00000000), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000)), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000));
	FWSVector3 Local11 = GetWorldPosition_NoMaterialOffsets(Parameters);
	FWSVector3 Local12 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local11)), WSGetY(DERIV_BASE_VALUE(Local11)), WSGetZ(DERIV_BASE_VALUE(Local11)));
	FWSVector3 Local13 = WSMultiply(DERIV_BASE_VALUE(Local12), ((MaterialFloat3)Material.PreshaderBuffer[0].y));
	FWSVector2 Local14 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local13)), WSGetZ(DERIV_BASE_VALUE(Local13)));
	MaterialFloat2 Local15 = WSApplyAddressMode(DERIV_BASE_VALUE(Local14), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local16 = MaterialStoreTexCoordScale(Parameters, Local15, 8);
	MaterialFloat4 Local17 = UnpackNormalMap(Texture2DSample(Material_Texture2D_1,GetMaterialSharedSampler(samplerMaterial_Texture2D_1,View_MaterialTextureBilinearWrapedSampler),Local15));
	MaterialFloat Local18 = MaterialStoreTexSample(Parameters, Local17, 8);
	MaterialFloat Local19 = dot(Parameters.TangentToWorld[2],MaterialFloat3(0.00000000,1.00000000,0.00000000).rgb);
	MaterialFloat Local20 = select((Local19 >= 0.00000000), -1.00000000, 1.00000000);
	MaterialFloat3 Local21 = (Local17.rgb * MaterialFloat3(MaterialFloat2(Local20,-1.00000000),1.00000000));
	FWSVector2 Local22 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local13)), WSGetZ(DERIV_BASE_VALUE(Local13)));
	MaterialFloat2 Local23 = WSApplyAddressMode(DERIV_BASE_VALUE(Local22), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local24 = MaterialStoreTexCoordScale(Parameters, Local23, 8);
	MaterialFloat4 Local25 = UnpackNormalMap(Texture2DSample(Material_Texture2D_1,GetMaterialSharedSampler(samplerMaterial_Texture2D_1,View_MaterialTextureBilinearWrapedSampler),Local23));
	MaterialFloat Local26 = MaterialStoreTexSample(Parameters, Local25, 8);
	MaterialFloat Local27 = dot(Parameters.TangentToWorld[2],MaterialFloat3(1.00000000,0.00000000,0.00000000).rgb);
	MaterialFloat Local28 = select((Local27 >= 0.00000000), 1.00000000, -1.00000000);
	MaterialFloat3 Local29 = (Local25.rgb * MaterialFloat3(MaterialFloat2(Local28,-1.00000000),1.00000000));
	MaterialFloat3 Local30 = mul(MaterialFloat3(0.00000000,0.00000000,1.00000000), Parameters.TangentToWorld);
	MaterialFloat Local31 = abs(Local30.r);
	MaterialFloat Local32 = lerp((0.00000000 - 0.00000000),(0.00000000 + 1.00000000),DERIV_BASE_VALUE(Local31));
	MaterialFloat Local33 = saturate(DERIV_BASE_VALUE(Local32));
	MaterialFloat Local34 = DERIV_BASE_VALUE(Local33).r;
	MaterialFloat3 Local35 = lerp(Local21,Local29,DERIV_BASE_VALUE(Local34));
	MaterialFloat3 Local36 = (Local10.rgb * ((MaterialFloat3)Local35.r));
	MaterialFloat3 Local37 = cross(Local7,Local6);
	MaterialFloat Local38 = dot(Local37,Local37);
	MaterialFloat3 Local39 = normalize(Local37);
	MaterialFloat4 Local40 = select((abs(Local38 - 0.00000100) > 0.00001000), select((Local38 >= 0.00000100), MaterialFloat4(Local39,0.00000000), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000)), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000));
	MaterialFloat3 Local41 = (Local40.rgb * ((MaterialFloat3)Local35.g));
	MaterialFloat3 Local42 = (Local36 + Local41);
	MaterialFloat3 Local43 = (Local6 * ((MaterialFloat3)Local35.b));
	MaterialFloat3 Local44 = (Local43 + MaterialFloat3(0.00000000,0.00000000,0.00000000));
	MaterialFloat3 Local45 = (Local42 + Local44);
	MaterialFloat3 Local46 = cross(Local6,normalize(MaterialFloat3(0.00000000,1.00000000,0.00000000).rgb));
	MaterialFloat Local47 = dot(Local46,Local46);
	MaterialFloat3 Local48 = normalize(Local46);
	MaterialFloat4 Local49 = select((abs(Local47 - 0.00000100) > 0.00001000), select((Local47 >= 0.00000100), MaterialFloat4(Local48,0.00000000), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000)), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000));
	FWSVector2 Local50 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local13)), WSGetY(DERIV_BASE_VALUE(Local13)));
	MaterialFloat2 Local51 = WSApplyAddressMode(DERIV_BASE_VALUE(Local50), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local52 = MaterialStoreTexCoordScale(Parameters, Local51, 8);
	MaterialFloat4 Local53 = UnpackNormalMap(Texture2DSample(Material_Texture2D_1,GetMaterialSharedSampler(samplerMaterial_Texture2D_1,View_MaterialTextureBilinearWrapedSampler),Local51));
	MaterialFloat Local54 = MaterialStoreTexSample(Parameters, Local53, 8);
	MaterialFloat Local55 = dot(Parameters.TangentToWorld[2],MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb);
	MaterialFloat Local56 = select((Local55 >= 0.00000000), 1.00000000, -1.00000000);
	MaterialFloat3 Local57 = (Local53.rgb * MaterialFloat3(MaterialFloat2(Local56,-1.00000000),1.00000000));
	MaterialFloat3 Local58 = (Local49.rgb * ((MaterialFloat3)Local57.r));
	MaterialFloat3 Local59 = cross(Local46,Local6);
	MaterialFloat Local60 = dot(Local59,Local59);
	MaterialFloat3 Local61 = normalize(Local59);
	MaterialFloat4 Local62 = select((abs(Local60 - 0.00000100) > 0.00001000), select((Local60 >= 0.00000100), MaterialFloat4(Local61,0.00000000), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000)), MaterialFloat4(MaterialFloat3(0.00000000,0.00000000,0.00000000),1.00000000));
	MaterialFloat3 Local63 = (Local62.rgb * ((MaterialFloat3)Local57.g));
	MaterialFloat3 Local64 = (Local58 + Local63);
	MaterialFloat3 Local65 = (Local6 * ((MaterialFloat3)Local57.b));
	MaterialFloat3 Local66 = (Local65 + MaterialFloat3(0.00000000,0.00000000,0.00000000));
	MaterialFloat3 Local67 = (Local64 + Local66);
	MaterialFloat Local68 = abs(Local30.b);
	MaterialFloat Local69 = lerp((0.00000000 - 0.00000000),(0.00000000 + 1.00000000),DERIV_BASE_VALUE(Local68));
	MaterialFloat Local70 = saturate(DERIV_BASE_VALUE(Local69));
	MaterialFloat Local71 = DERIV_BASE_VALUE(Local70).r;
	MaterialFloat3 Local72 = lerp(Local45,Local67,DERIV_BASE_VALUE(Local71));
	MaterialFloat3 Local73 = mul((MaterialFloat3x3)(Parameters.TangentToWorld), Local72);
	MaterialFloat2 Local74 = (Local73.rg * ((MaterialFloat2)-1.00000000));
	MaterialFloat Local75 = dot(MaterialFloat3(Local3.rgb.rg,Local5),MaterialFloat3(Local74,Local73.b));
	MaterialFloat3 Local76 = (MaterialFloat3(Local3.rgb.rg,Local5) * ((MaterialFloat3)Local75));
	MaterialFloat3 Local77 = (((MaterialFloat3)Local5) * MaterialFloat3(Local74,Local73.b));
	MaterialFloat3 Local78 = (Local76 - Local77);
	FWSVector3 Local79 = WSMultiply(DERIV_BASE_VALUE(Local12), ((MaterialFloat3)Material.PreshaderBuffer[0].z));
	FWSVector2 Local80 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local79)), WSGetZ(DERIV_BASE_VALUE(Local79)));
	MaterialFloat2 Local81 = WSApplyAddressMode(DERIV_BASE_VALUE(Local80), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local82 = MaterialStoreTexCoordScale(Parameters, Local81, 2);
	MaterialFloat4 Local83 = ProcessMaterialLinearGreyscaleTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local81));
	MaterialFloat Local84 = MaterialStoreTexSample(Parameters, Local83, 2);
	FWSVector2 Local85 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local79)), WSGetZ(DERIV_BASE_VALUE(Local79)));
	MaterialFloat2 Local86 = WSApplyAddressMode(DERIV_BASE_VALUE(Local85), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local87 = MaterialStoreTexCoordScale(Parameters, Local86, 2);
	MaterialFloat4 Local88 = ProcessMaterialLinearGreyscaleTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local86));
	MaterialFloat Local89 = MaterialStoreTexSample(Parameters, Local88, 2);
	MaterialFloat Local90 = abs(Parameters.TangentToWorld[2].r);
	MaterialFloat Local91 = lerp((0.00000000 - 1.00000000),(1.00000000 + 1.00000000),Local90);
	MaterialFloat Local92 = saturate(Local91);
	MaterialFloat3 Local93 = lerp(Local83.rgb,Local88.rgb,Local92.r.r);
	FWSVector2 Local94 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local79)), WSGetY(DERIV_BASE_VALUE(Local79)));
	MaterialFloat2 Local95 = WSApplyAddressMode(DERIV_BASE_VALUE(Local94), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local96 = MaterialStoreTexCoordScale(Parameters, Local95, 2);
	MaterialFloat4 Local97 = ProcessMaterialLinearGreyscaleTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local95));
	MaterialFloat Local98 = MaterialStoreTexSample(Parameters, Local97, 2);
	MaterialFloat Local99 = abs(Parameters.TangentToWorld[2].b);
	MaterialFloat Local100 = lerp((0.00000000 - 1.00000000),(1.00000000 + 1.00000000),Local99);
	MaterialFloat Local101 = saturate(Local100);
	MaterialFloat3 Local102 = lerp(Local93,Local97.rgb,Local101.r.r);
	MaterialFloat3 Local103 = PositiveClampedPow(Local102,((MaterialFloat3)2.00000000));
	MaterialFloat Local104 = lerp((0.00000000 - 0.50000000),(0.50000000 + 1.00000000),Local103.x);
	MaterialFloat Local105 = saturate(Local104);
	MaterialFloat3 Local106 = lerp(Local78,Local3.rgb,Local105.r);

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = Local106;


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
	FWSVector3 Local107 = GetWorldPosition(Parameters);
	FWSVector3 Local108 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local107)), WSGetY(DERIV_BASE_VALUE(Local107)), WSGetZ(DERIV_BASE_VALUE(Local107)));
	FWSScalar Local109 = WSGetX(DERIV_BASE_VALUE(Local108));
	MaterialFloat Local110 = (View.GameTime * -125.00000000);
	FWSVector2 Local111 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local108)), WSGetZ(DERIV_BASE_VALUE(Local108)));
	FWSVector2 Local112 = WSAdd(MaterialFloat2(0.00000000,Local110), DERIV_BASE_VALUE(Local111));
	FWSVector3 Local113 = MakeWSVector(WSPromote(DERIV_BASE_VALUE(Local109)),WSPromote(DERIV_BASE_VALUE(Local112)));
	MaterialFloat Local114 = MaterialExpressionNoise(DERIV_BASE_VALUE(Local113),-0.00800000,1.00000000,2.00000000,1.00000000,1.00000000,0.00000000,1.00000000,0.50000000,0.00000000,0.00000000,512.00000000);
	MaterialFloat Local115 = (1.00000000 - Local114);
	MaterialFloat Local116 = PositiveClampedPow(Local115,Material.PreshaderBuffer[0].w);
	MaterialFloat Local117 = (View.GameTime * 100.00000000);
	FWSVector2 Local118 = WSAdd(MaterialFloat2(0.00000000,Local117), DERIV_BASE_VALUE(Local111));
	FWSVector3 Local119 = MakeWSVector(WSPromote(DERIV_BASE_VALUE(Local109)),WSPromote(DERIV_BASE_VALUE(Local118)));
	MaterialFloat Local120 = MaterialExpressionNoise(DERIV_BASE_VALUE(Local119),-0.01500000,1.00000000,2.00000000,1.00000000,1.00000000,0.00000000,1.00000000,0.50000000,0.00000000,0.00000000,512.00000000);
	MaterialFloat Local121 = (1.00000000 - Local120);
	MaterialFloat Local122 = PositiveClampedPow(Local121,Material.PreshaderBuffer[0].w);
	MaterialFloat Local123 = (Local116 + Local122);
	MaterialFloat Local124 = (View.GameTime * 0.01000000);
	FWSVector3 Local125 = WSMultiply(DERIV_BASE_VALUE(Local108), ((MaterialFloat3)Material.PreshaderBuffer[1].x));
	FWSVector2 Local126 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local125)), WSGetZ(DERIV_BASE_VALUE(Local125)));
	FWSVector2 Local127 = WSAdd(MaterialFloat2(Local124,0.00000000), DERIV_BASE_VALUE(Local126));
	MaterialFloat Local128 = (View.GameTime * 0.04000000);
	FWSVector2 Local129 = WSAdd(MaterialFloat2(0.00000000,Local128), DERIV_BASE_VALUE(Local126));
	FWSVector2 Local130 = WSAdd(DERIV_BASE_VALUE(Local127), DERIV_BASE_VALUE(Local129));
	MaterialFloat2 Local131 = WSApplyAddressMode(DERIV_BASE_VALUE(Local130), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local132 = MaterialStoreTexCoordScale(Parameters, Local131, 4);
	MaterialFloat4 Local133 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local131,View.MaterialTextureMipBias));
	MaterialFloat Local134 = MaterialStoreTexSample(Parameters, Local133, 4);
	FWSVector2 Local135 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local125)), WSGetZ(DERIV_BASE_VALUE(Local125)));
	FWSVector2 Local136 = WSAdd(MaterialFloat2(Local124,0.00000000), DERIV_BASE_VALUE(Local135));
	FWSVector2 Local137 = WSAdd(MaterialFloat2(0.00000000,Local128), DERIV_BASE_VALUE(Local135));
	FWSVector2 Local138 = WSAdd(DERIV_BASE_VALUE(Local136), DERIV_BASE_VALUE(Local137));
	MaterialFloat2 Local139 = WSApplyAddressMode(DERIV_BASE_VALUE(Local138), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local140 = MaterialStoreTexCoordScale(Parameters, Local139, 4);
	MaterialFloat4 Local141 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local139,View.MaterialTextureMipBias));
	MaterialFloat Local142 = MaterialStoreTexSample(Parameters, Local141, 4);
	MaterialFloat Local143 = lerp((0.00000000 - 0.00000000),(0.00000000 + 1.00000000),Local90);
	MaterialFloat Local144 = saturate(Local143);
	MaterialFloat3 Local145 = lerp(Local133.rgb,Local141.rgb,Local144.r.r);
	FWSVector2 Local146 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local125)), WSGetY(DERIV_BASE_VALUE(Local125)));
	FWSVector2 Local147 = WSAdd(MaterialFloat2(Local124,0.00000000), DERIV_BASE_VALUE(Local146));
	FWSVector2 Local148 = WSAdd(MaterialFloat2(0.00000000,Local128), DERIV_BASE_VALUE(Local146));
	FWSVector2 Local149 = WSAdd(DERIV_BASE_VALUE(Local147), DERIV_BASE_VALUE(Local148));
	MaterialFloat2 Local150 = WSApplyAddressMode(DERIV_BASE_VALUE(Local149), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local151 = MaterialStoreTexCoordScale(Parameters, Local150, 4);
	MaterialFloat4 Local152 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local150,View.MaterialTextureMipBias));
	MaterialFloat Local153 = MaterialStoreTexSample(Parameters, Local152, 4);
	MaterialFloat Local154 = lerp((0.00000000 - 0.00000000),(0.00000000 + 1.00000000),Local99);
	MaterialFloat Local155 = saturate(Local154);
	MaterialFloat3 Local156 = lerp(Local145,Local152.rgb,Local155.r.r);
	MaterialFloat Local157 = (View.GameTime * -0.01000000);
	FWSVector3 Local158 = WSMultiply(DERIV_BASE_VALUE(Local108), ((MaterialFloat3)Material.PreshaderBuffer[1].y));
	FWSVector2 Local159 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local158)), WSGetZ(DERIV_BASE_VALUE(Local158)));
	FWSVector2 Local160 = WSAdd(MaterialFloat2(Local157,0.00000000), DERIV_BASE_VALUE(Local159));
	MaterialFloat Local161 = (View.GameTime * -0.04000000);
	FWSVector2 Local162 = WSAdd(MaterialFloat2(0.00000000,Local161), DERIV_BASE_VALUE(Local159));
	FWSVector2 Local163 = WSAdd(DERIV_BASE_VALUE(Local160), DERIV_BASE_VALUE(Local162));
	MaterialFloat2 Local164 = WSApplyAddressMode(DERIV_BASE_VALUE(Local163), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local165 = MaterialStoreTexCoordScale(Parameters, Local164, 4);
	MaterialFloat4 Local166 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local164,View.MaterialTextureMipBias));
	MaterialFloat Local167 = MaterialStoreTexSample(Parameters, Local166, 4);
	FWSVector2 Local168 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local158)), WSGetZ(DERIV_BASE_VALUE(Local158)));
	FWSVector2 Local169 = WSAdd(MaterialFloat2(Local157,0.00000000), DERIV_BASE_VALUE(Local168));
	FWSVector2 Local170 = WSAdd(MaterialFloat2(0.00000000,Local161), DERIV_BASE_VALUE(Local168));
	FWSVector2 Local171 = WSAdd(DERIV_BASE_VALUE(Local169), DERIV_BASE_VALUE(Local170));
	MaterialFloat2 Local172 = WSApplyAddressMode(DERIV_BASE_VALUE(Local171), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local173 = MaterialStoreTexCoordScale(Parameters, Local172, 4);
	MaterialFloat4 Local174 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local172,View.MaterialTextureMipBias));
	MaterialFloat Local175 = MaterialStoreTexSample(Parameters, Local174, 4);
	MaterialFloat3 Local176 = lerp(Local166.rgb,Local174.rgb,Local144.r.r);
	FWSVector2 Local177 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local158)), WSGetY(DERIV_BASE_VALUE(Local158)));
	FWSVector2 Local178 = WSAdd(MaterialFloat2(Local157,0.00000000), DERIV_BASE_VALUE(Local177));
	FWSVector2 Local179 = WSAdd(MaterialFloat2(0.00000000,Local161), DERIV_BASE_VALUE(Local177));
	FWSVector2 Local180 = WSAdd(DERIV_BASE_VALUE(Local178), DERIV_BASE_VALUE(Local179));
	MaterialFloat2 Local181 = WSApplyAddressMode(DERIV_BASE_VALUE(Local180), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local182 = MaterialStoreTexCoordScale(Parameters, Local181, 4);
	MaterialFloat4 Local183 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local181,View.MaterialTextureMipBias));
	MaterialFloat Local184 = MaterialStoreTexSample(Parameters, Local183, 4);
	MaterialFloat3 Local185 = lerp(Local176,Local183.rgb,Local155.r.r);
	MaterialFloat3 Local186 = lerp(Local156,Local185,0.50000000);
	FWSVector3 Local187 = WSMultiply(DERIV_BASE_VALUE(Local108), ((MaterialFloat3)Material.PreshaderBuffer[1].z));
	FWSVector2 Local188 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local187)), WSGetZ(DERIV_BASE_VALUE(Local187)));
	FWSVector2 Local189 = WSAdd(MaterialFloat2(Local128,0.00000000), DERIV_BASE_VALUE(Local188));
	FWSVector2 Local190 = WSAdd(MaterialFloat2(0.00000000,Local124), DERIV_BASE_VALUE(Local188));
	FWSVector2 Local191 = WSAdd(DERIV_BASE_VALUE(Local189), DERIV_BASE_VALUE(Local190));
	MaterialFloat2 Local192 = WSApplyAddressMode(DERIV_BASE_VALUE(Local191), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local193 = MaterialStoreTexCoordScale(Parameters, Local192, 4);
	MaterialFloat4 Local194 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local192,View.MaterialTextureMipBias));
	MaterialFloat Local195 = MaterialStoreTexSample(Parameters, Local194, 4);
	FWSVector2 Local196 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local187)), WSGetZ(DERIV_BASE_VALUE(Local187)));
	FWSVector2 Local197 = WSAdd(MaterialFloat2(Local128,0.00000000), DERIV_BASE_VALUE(Local196));
	FWSVector2 Local198 = WSAdd(MaterialFloat2(0.00000000,Local124), DERIV_BASE_VALUE(Local196));
	FWSVector2 Local199 = WSAdd(DERIV_BASE_VALUE(Local197), DERIV_BASE_VALUE(Local198));
	MaterialFloat2 Local200 = WSApplyAddressMode(DERIV_BASE_VALUE(Local199), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local201 = MaterialStoreTexCoordScale(Parameters, Local200, 4);
	MaterialFloat4 Local202 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local200,View.MaterialTextureMipBias));
	MaterialFloat Local203 = MaterialStoreTexSample(Parameters, Local202, 4);
	MaterialFloat3 Local204 = lerp(Local194.rgb,Local202.rgb,Local144.r.r);
	FWSVector2 Local205 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local187)), WSGetY(DERIV_BASE_VALUE(Local187)));
	FWSVector2 Local206 = WSAdd(MaterialFloat2(Local128,0.00000000), DERIV_BASE_VALUE(Local205));
	FWSVector2 Local207 = WSAdd(MaterialFloat2(0.00000000,Local124), DERIV_BASE_VALUE(Local205));
	FWSVector2 Local208 = WSAdd(DERIV_BASE_VALUE(Local206), DERIV_BASE_VALUE(Local207));
	MaterialFloat2 Local209 = WSApplyAddressMode(DERIV_BASE_VALUE(Local208), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local210 = MaterialStoreTexCoordScale(Parameters, Local209, 4);
	MaterialFloat4 Local211 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local209,View.MaterialTextureMipBias));
	MaterialFloat Local212 = MaterialStoreTexSample(Parameters, Local211, 4);
	MaterialFloat3 Local213 = lerp(Local204,Local211.rgb,Local155.r.r);
	FWSVector3 Local214 = WSMultiply(DERIV_BASE_VALUE(Local108), ((MaterialFloat3)Material.PreshaderBuffer[1].w));
	FWSVector2 Local215 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local214)), WSGetZ(DERIV_BASE_VALUE(Local214)));
	FWSVector2 Local216 = WSAdd(MaterialFloat2(Local161,0.00000000), DERIV_BASE_VALUE(Local215));
	FWSVector2 Local217 = WSAdd(MaterialFloat2(0.00000000,Local157), DERIV_BASE_VALUE(Local215));
	FWSVector2 Local218 = WSAdd(DERIV_BASE_VALUE(Local216), DERIV_BASE_VALUE(Local217));
	MaterialFloat2 Local219 = WSApplyAddressMode(DERIV_BASE_VALUE(Local218), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local220 = MaterialStoreTexCoordScale(Parameters, Local219, 4);
	MaterialFloat4 Local221 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local219,View.MaterialTextureMipBias));
	MaterialFloat Local222 = MaterialStoreTexSample(Parameters, Local221, 4);
	FWSVector2 Local223 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local214)), WSGetZ(DERIV_BASE_VALUE(Local214)));
	FWSVector2 Local224 = WSAdd(MaterialFloat2(Local161,0.00000000), DERIV_BASE_VALUE(Local223));
	FWSVector2 Local225 = WSAdd(MaterialFloat2(0.00000000,Local157), DERIV_BASE_VALUE(Local223));
	FWSVector2 Local226 = WSAdd(DERIV_BASE_VALUE(Local224), DERIV_BASE_VALUE(Local225));
	MaterialFloat2 Local227 = WSApplyAddressMode(DERIV_BASE_VALUE(Local226), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local228 = MaterialStoreTexCoordScale(Parameters, Local227, 4);
	MaterialFloat4 Local229 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local227,View.MaterialTextureMipBias));
	MaterialFloat Local230 = MaterialStoreTexSample(Parameters, Local229, 4);
	MaterialFloat3 Local231 = lerp(Local221.rgb,Local229.rgb,Local144.r.r);
	FWSVector2 Local232 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local214)), WSGetY(DERIV_BASE_VALUE(Local214)));
	FWSVector2 Local233 = WSAdd(MaterialFloat2(Local161,0.00000000), DERIV_BASE_VALUE(Local232));
	FWSVector2 Local234 = WSAdd(MaterialFloat2(0.00000000,Local157), DERIV_BASE_VALUE(Local232));
	FWSVector2 Local235 = WSAdd(DERIV_BASE_VALUE(Local233), DERIV_BASE_VALUE(Local234));
	MaterialFloat2 Local236 = WSApplyAddressMode(DERIV_BASE_VALUE(Local235), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local237 = MaterialStoreTexCoordScale(Parameters, Local236, 4);
	MaterialFloat4 Local238 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_3,samplerMaterial_Texture2D_3,Local236,View.MaterialTextureMipBias));
	MaterialFloat Local239 = MaterialStoreTexSample(Parameters, Local238, 4);
	MaterialFloat3 Local240 = lerp(Local231,Local238.rgb,Local155.r.r);
	MaterialFloat3 Local241 = lerp(Local213,Local240,0.50000000);
	MaterialFloat3 Local242 = lerp(Local186,Local241,0.50000000);
	MaterialFloat Local243 = (Local123 * Local242.b);
	MaterialFloat3 Local244 = (((MaterialFloat3)Local243) * Material.PreshaderBuffer[2].xyz);
	MaterialFloat3 Local245 = (Local244 * ((MaterialFloat3)Material.PreshaderBuffer[2].w));
	MaterialFloat Local246 = dot(WorldNormalCopy,Parameters.CameraVector);
	MaterialFloat Local247 = max(0.00000000,Local246);
	MaterialFloat Local248 = (1.00000000 - Local247);
	MaterialFloat Local249 = abs(Local248);
	MaterialFloat Local250 = max(Local249,0.00010000);
	MaterialFloat Local251 = PositiveClampedPow(Local250,Material.PreshaderBuffer[3].x);
	MaterialFloat Local252 = (Local251 * Material.PreshaderBuffer[3].y);
	MaterialFloat Local253 = (Local252 + Material.PreshaderBuffer[3].z);
	MaterialFloat3 Local254 = lerp(Local245,MaterialFloat3(0.00000000,0.00000000,0.00000000).rgb,Local253);
	MaterialFloat3 Local255 = lerp(Local254,Material.PreshaderBuffer[4].xyz,Material.PreshaderBuffer[3].w);
	MaterialFloat Local256 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local1), 5);
	MaterialFloat4 Local257 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_4,samplerMaterial_Texture2D_4,DERIV_BASE_VALUE(Local1),View.MaterialTextureMipBias));
	MaterialFloat Local258 = MaterialStoreTexSample(Parameters, Local257, 5);
	MaterialFloat3 Local259 = RotateAboutAxis(MaterialFloat4(normalize(MaterialFloat3(1.00000000,1.00000000,1.00000000).rgb),Material.PreshaderBuffer[4].w),((MaterialFloat3)0.00000000),Local257.rgb);
	MaterialFloat3 Local260 = (Local259 + Local257.rgb);
	MaterialFloat3 Local261 = (Material.PreshaderBuffer[5].xyz * Local260);
	MaterialFloat Local262 = MaterialStoreTexCoordScale(Parameters, Local15, 2);
	MaterialFloat4 Local263 = ProcessMaterialLinearGreyscaleTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local15));
	MaterialFloat Local264 = MaterialStoreTexSample(Parameters, Local263, 2);
	MaterialFloat Local265 = MaterialStoreTexCoordScale(Parameters, Local23, 2);
	MaterialFloat4 Local266 = ProcessMaterialLinearGreyscaleTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local23));
	MaterialFloat Local267 = MaterialStoreTexSample(Parameters, Local266, 2);
	MaterialFloat3 Local268 = lerp(Local263.rgb,Local266.rgb,Local92.r.r);
	MaterialFloat Local269 = MaterialStoreTexCoordScale(Parameters, Local51, 2);
	MaterialFloat4 Local270 = ProcessMaterialLinearGreyscaleTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local51));
	MaterialFloat Local271 = MaterialStoreTexSample(Parameters, Local270, 2);
	MaterialFloat3 Local272 = lerp(Local268,Local270.rgb,Local101.r.r);
	MaterialFloat3 Local273 = PositiveClampedPow(Local272,((MaterialFloat3)Material.PreshaderBuffer[5].w));
	MaterialFloat Local274 = lerp(Material.PreshaderBuffer[6].y,Material.PreshaderBuffer[6].x,Local273.x);
	MaterialFloat Local275 = saturate(Local274);
	MaterialFloat3 Local276 = lerp(Local261,Local260,Local275.r);
	MaterialFloat3 Local277 = lerp(Local276,Local260,Local105.r);
	MaterialFloat3 Local278 = (Material.PreshaderBuffer[7].xyz * Local277);
	FWSVector3 Local279 = WSMultiply(DERIV_BASE_VALUE(Local12), ((MaterialFloat3)Material.PreshaderBuffer[7].w));
	FWSVector2 Local280 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local279)), WSGetZ(DERIV_BASE_VALUE(Local279)));
	MaterialFloat2 Local281 = WSApplyAddressMode(DERIV_BASE_VALUE(Local280), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local282 = MaterialStoreTexCoordScale(Parameters, Local281, 2);
	MaterialFloat4 Local283 = ProcessMaterialLinearGreyscaleTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local281));
	MaterialFloat Local284 = MaterialStoreTexSample(Parameters, Local283, 2);
	FWSVector2 Local285 = MakeWSVector(WSGetY(DERIV_BASE_VALUE(Local279)), WSGetZ(DERIV_BASE_VALUE(Local279)));
	MaterialFloat2 Local286 = WSApplyAddressMode(DERIV_BASE_VALUE(Local285), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local287 = MaterialStoreTexCoordScale(Parameters, Local286, 2);
	MaterialFloat4 Local288 = ProcessMaterialLinearGreyscaleTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local286));
	MaterialFloat Local289 = MaterialStoreTexSample(Parameters, Local288, 2);
	MaterialFloat3 Local290 = lerp(Local283.rgb,Local288.rgb,Local92.r.r);
	FWSVector2 Local291 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local279)), WSGetY(DERIV_BASE_VALUE(Local279)));
	MaterialFloat2 Local292 = WSApplyAddressMode(DERIV_BASE_VALUE(Local291), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local293 = MaterialStoreTexCoordScale(Parameters, Local292, 2);
	MaterialFloat4 Local294 = ProcessMaterialLinearGreyscaleTextureLookup(Texture2DSample(Material_Texture2D_2,GetMaterialSharedSampler(samplerMaterial_Texture2D_2,View_MaterialTextureBilinearWrapedSampler),Local292));
	MaterialFloat Local295 = MaterialStoreTexSample(Parameters, Local294, 2);
	MaterialFloat3 Local296 = lerp(Local290,Local294.rgb,Local101.r.r);
	MaterialFloat3 Local297 = PositiveClampedPow(Local296,((MaterialFloat3)Material.PreshaderBuffer[5].w));
	MaterialFloat Local298 = lerp(Material.PreshaderBuffer[6].y,Material.PreshaderBuffer[6].x,Local297.x);
	MaterialFloat Local299 = saturate(Local298);
	MaterialFloat3 Local300 = lerp(Local278,Local276,Local299.r);
	MaterialFloat3 Local301 = (Local300 * ((MaterialFloat3)Material.PreshaderBuffer[8].x));
	MaterialFloat Local302 = MaterialStoreTexCoordScale(Parameters, DERIV_BASE_VALUE(Local1), 0);
	MaterialFloat4 Local303 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_5,samplerMaterial_Texture2D_5,DERIV_BASE_VALUE(Local1),View.MaterialTextureMipBias));
	MaterialFloat Local304 = MaterialStoreTexSample(Parameters, Local303, 0);

	PixelMaterialInputs.EmissiveColor = Local255;
	PixelMaterialInputs.Opacity = 1.00000000;
	PixelMaterialInputs.OpacityMask = 1.00000000;
	PixelMaterialInputs.BaseColor = Local301;
	PixelMaterialInputs.Metallic = Local303.b;
	PixelMaterialInputs.Specular = 0.50000000;
	PixelMaterialInputs.Roughness = Local303.g;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Normal = Local106;
	PixelMaterialInputs.Tangent = MaterialFloat3(1.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Subsurface = 0;
	PixelMaterialInputs.AmbientOcclusion = Local303.r;
	PixelMaterialInputs.Refraction = 0;
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