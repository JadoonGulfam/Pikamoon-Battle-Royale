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
	Material.PreshaderBuffer[0] = float4(0.000000,0.000000,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[1] = float4(1.200000,0.000200,0.000000,0.000000);//(Unknown)
	Material.PreshaderBuffer[2] = float4(0.502592,0.697917,0.000000,0.000050);//(Unknown)
	Material.PreshaderBuffer[3] = float4(0.700000,10.000000,1.000000,0.400000);//(Unknown)
	Material.PreshaderBuffer[4] = float4(0.000000,0.000000,0.000000,0.000000);//(Unknown)
}struct MaterialCollection0Type
{
	float4 Vectors[2];
};
//WindProperties
MaterialCollection0Type MaterialCollection0;
void Initialize_MaterialCollection0()
{
	MaterialCollection0.Vectors[0] = float4(0.010000,0.000000,0.000000,0.000000);//WindStrength,,,
	MaterialCollection0.Vectors[1] = float4(-1.000000,1.000000,0.000000,0.000000);//WindDirection
}

MaterialFloat3 CustomExpression1(FMaterialVertexParameters Parameters,MaterialFloat a)
{
return acos(a);
}

MaterialFloat3 CustomExpression0(FMaterialPixelParameters Parameters,MaterialFloat a)
{
return acos(a);
}
float3 GetMaterialWorldPositionOffset(FMaterialVertexParameters Parameters)
{
SHADER_PUSH_WARNINGS_STATE
SHADER_DISABLE_WARNINGS
	MaterialFloat Local58 = (View.GameTime * Material.PreshaderBuffer[3].x);
	MaterialFloat Local59 = (Local58 * -0.50000000);
	MaterialFloat3 Local60 = (normalize(MaterialFloat4(0.00000000,1.00000000,0.00000000,1.00000000).rgba.rgb) * ((MaterialFloat3)Local59));
	FWSVector3 Local61 = GetWorldPosition(Parameters);
	FWSVector3 Local62 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local61)), WSGetY(DERIV_BASE_VALUE(Local61)), WSGetZ(DERIV_BASE_VALUE(Local61)));
	FWSVector3 Local63 = WSDivideByPow2(DERIV_BASE_VALUE(Local62), ((MaterialFloat3)1024.00000000));
	FWSVector3 Local64 = WSAdd(Local60, DERIV_BASE_VALUE(Local63));
	FWSVector3 Local65 = WSAdd(DERIV_BASE_VALUE(Local64), ((MaterialFloat3)0.50000000));
	MaterialFloat3 Local66 = WSFracDemote(DERIV_BASE_VALUE(Local65));
	MaterialFloat3 Local67 = (DERIV_BASE_VALUE(Local66) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local68 = (DERIV_BASE_VALUE(Local67) + ((MaterialFloat3)-1.00000000));
	MaterialFloat3 Local69 = abs(DERIV_BASE_VALUE(Local68));
	MaterialFloat3 Local70 = (DERIV_BASE_VALUE(Local69) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local71 = (((MaterialFloat3)3.00000000) - DERIV_BASE_VALUE(Local70));
	MaterialFloat3 Local72 = (DERIV_BASE_VALUE(Local71) * DERIV_BASE_VALUE(Local69));
	MaterialFloat3 Local73 = (DERIV_BASE_VALUE(Local72) * DERIV_BASE_VALUE(Local69));
	MaterialFloat Local74 = dot(normalize(MaterialFloat4(0.00000000,1.00000000,0.00000000,1.00000000).rgba.rgb),DERIV_BASE_VALUE(Local73));
	FWSVector3 Local75 = WSDivide(DERIV_BASE_VALUE(Local62), ((MaterialFloat3)200.00000000));
	FWSVector3 Local76 = WSAdd(((MaterialFloat3)Local59), DERIV_BASE_VALUE(Local75));
	FWSVector3 Local77 = WSAdd(DERIV_BASE_VALUE(Local76), ((MaterialFloat3)0.50000000));
	MaterialFloat3 Local78 = WSFracDemote(DERIV_BASE_VALUE(Local77));
	MaterialFloat3 Local79 = (DERIV_BASE_VALUE(Local78) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local80 = (DERIV_BASE_VALUE(Local79) + ((MaterialFloat3)-1.00000000));
	MaterialFloat3 Local81 = abs(DERIV_BASE_VALUE(Local80));
	MaterialFloat3 Local82 = (DERIV_BASE_VALUE(Local81) * ((MaterialFloat3)2.00000000));
	MaterialFloat3 Local83 = (((MaterialFloat3)3.00000000) - DERIV_BASE_VALUE(Local82));
	MaterialFloat3 Local84 = (DERIV_BASE_VALUE(Local83) * DERIV_BASE_VALUE(Local81));
	MaterialFloat3 Local85 = (DERIV_BASE_VALUE(Local84) * DERIV_BASE_VALUE(Local81));
	MaterialFloat3 Local86 = (DERIV_BASE_VALUE(Local85) - ((MaterialFloat3)0.00000000));
	MaterialFloat Local87 = length(DERIV_BASE_VALUE(Local86));
	MaterialFloat Local88 = (DERIV_BASE_VALUE(Local74) + DERIV_BASE_VALUE(Local87));
	MaterialFloat Local89 = (DERIV_BASE_VALUE(Local88) * 6.28318548);
	MaterialFloat4 Local90 = MaterialFloat4(cross(normalize(MaterialFloat4(0.00000000,1.00000000,0.00000000,1.00000000).rgba.rgb),MaterialFloat3(0.00000000,0.00000000,1.00000000).rgb),DERIV_BASE_VALUE(Local89));
	MaterialFloat4 Local91 = MaterialCollection0.Vectors[0];
	MaterialFloat3 Local92 = CustomExpression1(Parameters,dot(MaterialFloat3(0.00000000,0.00000000,1.00000000),normalize(MaterialFloat3(-0.00952400,0.28917101,1.00000000).rgb)));
	MaterialFloat3 Local93 = (Local92 * ((MaterialFloat3)57.29000092));
	MaterialFloat3 Local94 = (Local93 / ((MaterialFloat3)360.00000000));
	MaterialFloat3 Local95 = (Local94 * ((MaterialFloat3)6.28318548));
	MaterialFloat4 Local96 = MaterialCollection0.Vectors[1];
	MaterialFloat4 Local97 = normalize(Local96.rgba);
	MaterialFloat3 Local98 = DERIV_BASE_VALUE(Local97).xyz;
	MaterialFloat3 Local99 = RotateAboutAxis(MaterialFloat4(cross(MaterialFloat3(0.00000000,0.00000000,1.00000000),normalize(MaterialFloat3(-0.00952400,0.28917101,1.00000000).rgb)),Local95.x),MaterialFloat3(0.00000000,0.00000000,0.00000000).rgb,DERIV_BASE_VALUE(Local98));
	MaterialFloat3 Local100 = (Local99 + DERIV_BASE_VALUE(Local98));
	MaterialFloat Local101 = (Local91.r * Local100.r);
	MaterialFloat Local102 = (Local91.r * Local100.g);
	MaterialFloat Local103 = (View.GameTime * MaterialFloat2(Local101,Local102).r);
	MaterialFloat Local104 = (View.GameTime * MaterialFloat2(Local101,Local102).g);
	FWSVector2 Local105 = MakeWSVector(WSGetComponent(DERIV_BASE_VALUE(Local62), 0),WSGetComponent(DERIV_BASE_VALUE(Local62), 1));
	FWSVector2 Local106 = WSAdd(MaterialFloat2(-0.50000000,-0.50000000), DERIV_BASE_VALUE(Local105));
	MaterialFloat2 Local107 = (MaterialFloat2(1.00000000,0.00000000).rg - Local96.rgba.rg);
	MaterialFloat2 Local108 = normalize(Local107);
	MaterialFloat2 Local109 = atan(DERIV_BASE_VALUE(Local108));
	MaterialFloat2 Local110 = (DERIV_BASE_VALUE(Local109) - ((MaterialFloat2)-90.00000000));
	MaterialFloat2 Local111 = normalize(DERIV_BASE_VALUE(Local110));
	MaterialFloat Local112 = DERIV_BASE_VALUE(Local111).x;
	MaterialFloat Local113 = (DERIV_BASE_VALUE(Local112) * 6.28318548);
	MaterialFloat Local114 = cos(DERIV_BASE_VALUE(Local113));
	MaterialFloat Local115 = sin(DERIV_BASE_VALUE(Local113));
	MaterialFloat Local116 = (DERIV_BASE_VALUE(Local115) * -1.00000000);
	MaterialFloat2 Local117 = MaterialFloat2(DERIV_BASE_VALUE(Local114),DERIV_BASE_VALUE(Local116));
	FWSScalar Local118 = WSDot(DERIV_BASE_VALUE(Local106), WSPromote(DERIV_BASE_VALUE(Local117)));
	MaterialFloat2 Local119 = MaterialFloat2(DERIV_BASE_VALUE(Local115),DERIV_BASE_VALUE(Local114));
	FWSScalar Local120 = WSDot(DERIV_BASE_VALUE(Local106), WSPromote(DERIV_BASE_VALUE(Local119)));
	FWSVector2 Local121 = MakeWSVector(WSPromote(DERIV_BASE_VALUE(Local118)),WSPromote(DERIV_BASE_VALUE(Local120)));
	FWSVector2 Local122 = WSAdd(MaterialFloat2(0.50000000,0.50000000).rg, DERIV_BASE_VALUE(Local121));
	FWSVector2 Local123 = WSAdd(DERIV_BASE_VALUE(Local122), ((MaterialFloat2)1.00000000));
	FWSVector2 Local124 = WSMultiply(DERIV_BASE_VALUE(Local123), ((MaterialFloat2)Material.PreshaderBuffer[2].w));
	FWSVector2 Local125 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local124)), WSGetY(DERIV_BASE_VALUE(Local124)));
	FWSVector2 Local126 = WSAdd(MaterialFloat2(Local103,Local104), DERIV_BASE_VALUE(Local125));
	MaterialFloat2 Local127 = WSApplyAddressMode(Local126, LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat4 Local128 = ProcessMaterialColorTextureLookup(Texture2DSampleLevel(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local127,-1.00000000));
	MaterialFloat3 Local129 = (Local128.rgb * ((MaterialFloat3)Material.PreshaderBuffer[3].y));
	MaterialFloat3 Local130 = (Local129 + MaterialFloat3(0.00000000,0.00000000,-10.00000000).rgb);
	MaterialFloat3 Local131 = RotateAboutAxis(DERIV_BASE_VALUE(Local90),Local130,Local129);
	MaterialFloat3 Local132 = (Local131 * ((MaterialFloat3)Material.PreshaderBuffer[3].z));
	MaterialFloat3 Local133 = (Local132 * ((MaterialFloat3)Material.PreshaderBuffer[3].w));
	MaterialFloat3 Local134 = (Local133 + Local129);
	MaterialFloat4 Local135 = Parameters.VertexColor;
	MaterialFloat3 Local136 = DERIV_BASE_VALUE(Local135).rgb;
	MaterialFloat3 Local137 = lerp(Local134,((MaterialFloat3)0.00000000),DERIV_BASE_VALUE(Local136));
	return Local137;;
SHADER_POP_WARNINGS_STATE
}
void CalcPixelMaterialInputs(in out FMaterialPixelParameters Parameters, in out FPixelMaterialInputs PixelMaterialInputs)
{
	//WorldAligned texturing & others use normals & stuff that think Z is up
	Parameters.TangentToWorld[0] = Parameters.TangentToWorld[0].xzy;
	Parameters.TangentToWorld[1] = Parameters.TangentToWorld[1].xzy;
	Parameters.TangentToWorld[2] = Parameters.TangentToWorld[2].xzy;

	float3 WorldNormalCopy = Parameters.WorldNormal;

SHADER_PUSH_WARNINGS_STATE
SHADER_DISABLE_WARNINGS
	// Initial calculations (required for Normal)

	// The Normal is a special case as it might have its own expressions and also be used to calculate other inputs, so perform the assignment here
	PixelMaterialInputs.Normal = MaterialFloat3(0.00000000,0.00000000,1.00000000);

SHADER_POP_WARNINGS_STATE

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

SHADER_PUSH_WARNINGS_STATE
SHADER_DISABLE_WARNINGS
	// Now the rest of the inputs
	MaterialFloat3 Local0 = lerp(MaterialFloat3(0.00000000,0.00000000,0.00000000),Material.PreshaderBuffer[0].yzw,Material.PreshaderBuffer[0].x);
	MaterialFloat2 Local1 = Parameters.TexCoords[0].xy;
	MaterialFloat Local2 = DERIV_BASE_VALUE(Local1).g;
	MaterialFloat Local3 = (DERIV_BASE_VALUE(Local2) * Material.PreshaderBuffer[1].x);
	MaterialFloat Local4 = saturate(DERIV_BASE_VALUE(Local3));
	MaterialFloat3 Local5 = lerp(MaterialFloat3(0.33245152,0.70110190,0.07036009).rgb,MaterialFloat3(0.06124605,0.26635560,0.03688945).rgb,DERIV_BASE_VALUE(Local4));
	MaterialFloat3 Local6 = lerp(MaterialFloat3(0.06124605,0.26635560,0.03688945).rgb,MaterialFloat3(0.35416666,0.35192871,0.00000000).rgb,DERIV_BASE_VALUE(Local4));
	FWSVector3 Local7 = GetWorldPosition(Parameters);
	FWSVector3 Local8 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local7)), WSGetY(DERIV_BASE_VALUE(Local7)), WSGetZ(DERIV_BASE_VALUE(Local7)));
	FWSVector3 Local9 = WSMultiply(DERIV_BASE_VALUE(Local8), ((MaterialFloat3)Material.PreshaderBuffer[1].y));
	FWSVector2 Local10 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local9)), WSGetY(DERIV_BASE_VALUE(Local9)));
	MaterialFloat2 Local11 = WSApplyAddressMode(DERIV_BASE_VALUE(Local10), LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local12 = MaterialStoreTexCoordScale(Parameters, Local11, 0);
	MaterialFloat4 Local13 = ProcessMaterialLinearColorTextureLookup(Texture2DSampleBias(Material_Texture2D_0,samplerMaterial_Texture2D_0,Local11,View.MaterialTextureMipBias));
	MaterialFloat Local14 = MaterialStoreTexSample(Parameters, Local13, 0);
	MaterialFloat3 Local15 = lerp(DERIV_BASE_VALUE(Local5),DERIV_BASE_VALUE(Local6),Local13.rgb);
	MaterialFloat3 Local16 = lerp(Material.PreshaderBuffer[2].xyz,MaterialFloat3(0.06124605,0.26635560,0.03688945).rgb,DERIV_BASE_VALUE(Local4));
	MaterialFloat4 Local17 = MaterialCollection0.Vectors[0];
	MaterialFloat3 Local18 = CustomExpression0(Parameters,dot(MaterialFloat3(0.00000000,0.00000000,1.00000000),normalize(MaterialFloat3(-0.00952400,0.28917101,1.00000000).rgb)));
	MaterialFloat3 Local19 = (Local18 * ((MaterialFloat3)57.29000092));
	MaterialFloat3 Local20 = (Local19 / ((MaterialFloat3)360.00000000));
	MaterialFloat3 Local21 = (Local20 * ((MaterialFloat3)6.28318548));
	MaterialFloat4 Local22 = MaterialCollection0.Vectors[1];
	MaterialFloat4 Local23 = normalize(Local22.rgba);
	MaterialFloat3 Local24 = DERIV_BASE_VALUE(Local23).xyz;
	MaterialFloat3 Local25 = RotateAboutAxis(MaterialFloat4(cross(MaterialFloat3(0.00000000,0.00000000,1.00000000),normalize(MaterialFloat3(-0.00952400,0.28917101,1.00000000).rgb)),Local21.x),MaterialFloat3(0.00000000,0.00000000,0.00000000).rgb,DERIV_BASE_VALUE(Local24));
	MaterialFloat3 Local26 = (Local25 + DERIV_BASE_VALUE(Local24));
	MaterialFloat Local27 = (Local17.r * Local26.r);
	MaterialFloat Local28 = (Local17.r * Local26.g);
	MaterialFloat Local29 = (View.GameTime * MaterialFloat2(Local27,Local28).r);
	MaterialFloat Local30 = (View.GameTime * MaterialFloat2(Local27,Local28).g);
	FWSVector2 Local31 = MakeWSVector(WSGetComponent(DERIV_BASE_VALUE(Local8), 0),WSGetComponent(DERIV_BASE_VALUE(Local8), 1));
	FWSVector2 Local32 = WSAdd(MaterialFloat2(-0.50000000,-0.50000000), DERIV_BASE_VALUE(Local31));
	MaterialFloat2 Local33 = (MaterialFloat2(1.00000000,0.00000000).rg - Local22.rgba.rg);
	MaterialFloat2 Local34 = normalize(Local33);
	MaterialFloat2 Local35 = atan(DERIV_BASE_VALUE(Local34));
	MaterialFloat2 Local36 = (DERIV_BASE_VALUE(Local35) - ((MaterialFloat2)-90.00000000));
	MaterialFloat2 Local37 = normalize(DERIV_BASE_VALUE(Local36));
	MaterialFloat Local38 = DERIV_BASE_VALUE(Local37).x;
	MaterialFloat Local39 = (DERIV_BASE_VALUE(Local38) * 6.28318548);
	MaterialFloat Local40 = cos(DERIV_BASE_VALUE(Local39));
	MaterialFloat Local41 = sin(DERIV_BASE_VALUE(Local39));
	MaterialFloat Local42 = (DERIV_BASE_VALUE(Local41) * -1.00000000);
	MaterialFloat2 Local43 = MaterialFloat2(DERIV_BASE_VALUE(Local40),DERIV_BASE_VALUE(Local42));
	FWSScalar Local44 = WSDot(DERIV_BASE_VALUE(Local32), WSPromote(DERIV_BASE_VALUE(Local43)));
	MaterialFloat2 Local45 = MaterialFloat2(DERIV_BASE_VALUE(Local41),DERIV_BASE_VALUE(Local40));
	FWSScalar Local46 = WSDot(DERIV_BASE_VALUE(Local32), WSPromote(DERIV_BASE_VALUE(Local45)));
	FWSVector2 Local47 = MakeWSVector(WSPromote(DERIV_BASE_VALUE(Local44)),WSPromote(DERIV_BASE_VALUE(Local46)));
	FWSVector2 Local48 = WSAdd(MaterialFloat2(0.50000000,0.50000000).rg, DERIV_BASE_VALUE(Local47));
	FWSVector2 Local49 = WSAdd(DERIV_BASE_VALUE(Local48), ((MaterialFloat2)1.00000000));
	FWSVector2 Local50 = WSMultiply(DERIV_BASE_VALUE(Local49), ((MaterialFloat2)Material.PreshaderBuffer[2].w));
	FWSVector2 Local51 = MakeWSVector(WSGetX(DERIV_BASE_VALUE(Local50)), WSGetY(DERIV_BASE_VALUE(Local50)));
	FWSVector2 Local52 = WSAdd(MaterialFloat2(Local29,Local30), DERIV_BASE_VALUE(Local51));
	MaterialFloat2 Local53 = WSApplyAddressMode(Local52, LWCADDRESSMODE_WRAP, LWCADDRESSMODE_WRAP);
	MaterialFloat Local54 = MaterialStoreTexCoordScale(Parameters, Local53, 1);
	MaterialFloat4 Local55 = ProcessMaterialColorTextureLookup(Texture2DSampleBias(Material_Texture2D_1,samplerMaterial_Texture2D_1,Local53,View.MaterialTextureMipBias));
	MaterialFloat Local56 = MaterialStoreTexSample(Parameters, Local55, 1);
	MaterialFloat3 Local57 = lerp(Local15,DERIV_BASE_VALUE(Local16),Local55.rgb);

	PixelMaterialInputs.EmissiveColor = Local0;
	PixelMaterialInputs.Opacity = 1.00000000;
	PixelMaterialInputs.OpacityMask = 1.00000000;
	PixelMaterialInputs.BaseColor = Local57;
	PixelMaterialInputs.Metallic = 0.00000000;
	PixelMaterialInputs.Specular = 0.00000000;
	PixelMaterialInputs.Roughness = 1.00000000;
	PixelMaterialInputs.Anisotropy = 0.00000000;
	PixelMaterialInputs.Normal = MaterialFloat3(0.00000000,0.00000000,1.00000000);
	PixelMaterialInputs.Tangent = MaterialFloat3(1.00000000,0.00000000,0.00000000);
	PixelMaterialInputs.Subsurface = MaterialFloat4(Local57,Material.PreshaderBuffer[4].x);
	PixelMaterialInputs.AmbientOcclusion = 1.00000000;
	PixelMaterialInputs.Refraction = 0;
	PixelMaterialInputs.PixelDepthOffset = 0.00000000;
	PixelMaterialInputs.ShadingModel = 2;
	PixelMaterialInputs.FrontMaterial = GetInitialisedSubstrateData();
	PixelMaterialInputs.SurfaceThickness = 0.01000000;
	PixelMaterialInputs.Displacement = -1.00000000;

SHADER_POP_WARNINGS_STATE

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
	Initialize_MaterialCollection0();

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
	Initialize_MaterialCollection0();


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