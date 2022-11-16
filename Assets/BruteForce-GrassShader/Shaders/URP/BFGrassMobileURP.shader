﻿// MADE BY MATTHIEU HOULLIER
// Copyright 2021 BRUTE FORCE, all rights reserved.
// You are authorized to use this work if you have purchased the asset.
// Mail me at bruteforcegamesstudio@gmail.com if you have any questions or improvements you want.
Shader "BruteForceURP/InteractiveGrassMobileURP"
{
	Properties
	{
		[Header(Tint Colors)]
		[Space]
		[MainColor]_Color("ColorTint",Color) = (0.5 ,0.5 ,0.5,1.0)
		_GroundColor("GroundColorTint",Color) = (0.7 ,0.68 ,0.68,1.0)
		_SelfShadowColor("ShadowColor",Color) = (0.41 ,0.41 ,0.36,1.0)
		_ProjectedShadowColor("ProjectedShadowColor",Color) = (0.45 ,0.42 ,0.04,1.0)
		_GrassShading("GrassShading", Range(0.0, 1)) = 0.197
		_GrassSaturation("GrassSaturation", Float) = 2

		[Header(Textures)]
		[Space]
		[MainTexture]_MainTex("Color Grass", 2D) = "white" {}
		[NoScaleOffset]_GroundTex("Ground Texture", 2D) = "white" {}
		[NoScaleOffset]_NoGrassTex("NoGrassTexture", 2D) = "white" {}
		[NoScaleOffset]_GrassTex("Grass Pattern", 2D) = "white" {}
		[NoScaleOffset]_Noise("NoiseColor", 2D) = "white" {}
		[NoScaleOffset]_Distortion("DistortionWind", 2D) = "white" {}

		[Header(Grass Values)]
		[Space]
		_GrassThinness("GrassThinness", Range(0.01, 2.5)) = 0.66
		_GrassThinnessIntersection("GrassThinnessIntersection", Range(0.01, 2)) = 0.13
		_TilingN1("TilingOfGrass", Float) = 6.06
		_WindMovement("WindMovementSpeed", Float) = 0.55
		_WindForce("WindForce", Float) = 0.25
		_TilingN3("WindNoiseTiling", Float) = 1
		_GrassCut("Grass Cut", Range(0, 1)) = 0
		_TilingN2("TilingOfNoise", Float) = 0.05
		_NoisePower("NoisePower", Float) = 1
		_FadeDistanceStart("FadeDistanceStart", Float) = 2
		_FadeDistanceEnd("FadeDistanceEnd", Float) = 20
		[Toggle(USE_RT)] _UseRT("Use RenderTexture Effect", Float) = 1
		[Toggle(USE_S)] _UseShadow("Use Shadows", Float) = 1
		[Toggle(USE_SC)] _UseShadowCast("Use Shadow Casting", Float) = 0
		[Toggle(USE_VR)] _UseVR("Use For VR", Float) = 0
		[Toggle(USE_VP)] _UseVP("Use with Vertex Painting", Float) = 0

		[Header(Procedural Tiling)]
		[Space]
		[Toggle(USE_PR)] _UsePR("Use Procedural Tiling (Reduce performance)", Float) = 0
		_ProceduralDistance("Tile start distance", Float) = 5.5
		_ProceduralStrength("Tile Smoothness", Float) = 1.5
		[Space]
		[Toggle(USE_WC)] _UseWC("Use World Coordinates", Float) = 0
		_WorldScale("World Scale", Float) = 10
		_WorldRotation("World Rotation", Range(0, 360)) = 0
		[Space]
		[Toggle(USE_TP)] _UseTP("Use Triplanar", Float) = 0

		[Header(Lighting Parameters)]
		[Space]
		[Toggle(USE_AL)] _UseAmbientLight("Use Ambient Light", Float) = 0

	}
		SubShader
		{
			Tags{"DisableBatching" = "true" }
			pass
			{
			Tags{"RenderPipeline" = "UniversalPipeline" }
			LOD 100
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			#pragma multi_compile_instancing
			#pragma prefer_hlslcc gles
			#pragma shader_feature USE_RT
			#pragma shader_feature USE_SC
			#pragma shader_feature USE_S
			#pragma shader_feature USE_PR
			#pragma shader_feature USE_WC
			#pragma shader_feature USE_AL
			#pragma shader_feature USE_VR
			#pragma shader_feature USE_VP
			#pragma shader_feature USE_TP

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
			#pragma multi_compile _ _SHADOWS_SOFT
			#pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
			#pragma multi_compile _ LIGHTMAP_ON
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 color : COLOR;
				float3 normal : NORMAL;
#ifdef USE_VR
				UNITY_VERTEX_INPUT_INSTANCE_ID
#endif
#ifdef LIGHTMAP_ON
					half4 texcoord1 : TEXCOORD1;
#endif
			};

			struct v2f
			{
				float3 worldPos : TEXCOORD3;
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
				float fogCoord : TEXCOORD1;
				float2 color : COLOR;
				float3 normal : TEXCOORD2;
#ifdef USE_VR
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
#endif
#ifdef LIGHTMAP_ON
					float2 lmap : TEXCOORD3;
#endif
			};
			// Render Texture Effects //
			uniform Texture2D _GlobalEffectRT;
			uniform float3 _Position;
			uniform float _OrthographicCamSize;
			uniform float _HasRT;

			Texture2D _MainTex;
			Texture2D _NoGrassTex;
			float4 _MainTex_ST;
			Texture2D _Distortion;
			sampler2D _GrassTex;
			Texture2D _Noise;
			Texture2D _GroundTex;
			float _TilingN1;
			float _TilingN2, _WindForce;
			float4 _Color, _SelfShadowColor, _GroundColor, _ProjectedShadowColor;
			float _TilingN3;
			float _WindMovement, _OffsetValue;
			half _GrassThinness, _GrassShading, _GrassThinnessIntersection;
			half _NoisePower, _GrassSaturation, _FadeDistanceStart, _FadeDistanceEnd, _GrassCut, _WorldScale, _WorldRotation;
			float _ProceduralDistance, _ProceduralStrength;

			SamplerState my_linear_repeat_sampler;
			SamplerState my_linear_clamp_sampler;

			float2 hash2D2D(float2 s)
			{
				//magic numbers
				return frac(sin(s)*4.5453);
			}

			//stochastic sampling
			float4 tex2DStochastic(sampler2D tex, float2 UV)
			{
				float4x3 BW_vx;
				float2 skewUV = mul(float2x2 (1.0, 0.0, -0.57735027, 1.15470054), UV * 3.464);

				//vertex IDs and barycentric coords
				float2 vxID = float2 (floor(skewUV));
				float3 barry = float3 (frac(skewUV), 0);
				barry.z = 1.0 - barry.x - barry.y;

				BW_vx = ((barry.z > 0) ?
					float4x3(float3(vxID, 0), float3(vxID + float2(0, 1), 0), float3(vxID + float2(1, 0), 0), barry.zyx) :
					float4x3(float3(vxID + float2 (1, 1), 0), float3(vxID + float2 (1, 0), 0), float3(vxID + float2 (0, 1), 0), float3(-barry.z, 1.0 - barry.y, 1.0 - barry.x)));

				//calculate derivatives to avoid triangular grid artifacts
				float2 dx = ddx(UV);
				float2 dy = ddy(UV);

				//blend samples with calculated weights
				float4 stochasticTex = mul(tex2D(tex, UV + hash2D2D(BW_vx[0].xy), dx, dy), BW_vx[3].x) +
					mul(tex2D(tex, UV + hash2D2D(BW_vx[1].xy), dx, dy), BW_vx[3].y) +
					mul(tex2D(tex, UV + hash2D2D(BW_vx[2].xy), dx, dy), BW_vx[3].z);
				return stochasticTex;
			}

#define UnityObjectToWorld(o) mul(unity_ObjectToWorld, float4(o.xyz,1.0))
			v2f vert(appdata v)
			{
				v2f o;
#ifdef USE_VR
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
#endif
				VertexPositionInputs vertexInput = GetVertexPositionInputs(v.vertex.xyz);

				o.pos = GetVertexPositionInputs(v.vertex).positionCS;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color;
				o.worldPos = UnityObjectToWorld(v.vertex);
				o.fogCoord = ComputeFogFactor(vertexInput.positionCS.z);
				o.normal = v.normal;
#ifdef LIGHTMAP_ON
				o.lmap = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
#endif
				return o;
			}
			half4 frag(v2f i) : SV_Target
			{
				float dist = 1;

			float2 mainUV;
			//Setup Coordinate Space
#ifdef USE_WC
			mainUV = i.worldPos.xz / max(_WorldScale, 0.001);

			float rotationAngle = _WorldRotation * 3.14159265359f / 180.0;
			float sina, cosa;
			sincos(rotationAngle, sina, cosa);
			float2x2 m = float2x2(cosa, -sina, sina, cosa);
			mainUV = mul(m, mainUV.xy);
#else
			mainUV = i.uv;
#endif

#ifdef USE_PR
				dist = clamp(lerp(0, 1, (distance(_WorldSpaceCameraPos, i.worldPos) - _ProceduralDistance) / max(_ProceduralStrength,0.05)), 0, 1);
#endif
				half FadeStacks = 1;
				float distFade = distance(_WorldSpaceCameraPos, i.worldPos);
				if (distFade > 0)
				{
					FadeStacks = lerp(1, 0, (distFade - _FadeDistanceStart)*(1 / max(_FadeDistanceEnd - _FadeDistanceStart, 0.0001)));//Clamp because people will start dividing by 0
				}
				float2 uv = i.worldPos.xz - _Position.xz;
				uv = uv / (_OrthographicCamSize * 2);
				uv += 0.5;

				float bRipple = 1;
#ifdef USE_RT
				if (_HasRT)
				{
					bRipple = 1 - clamp(_GlobalEffectRT.Sample(my_linear_clamp_sampler, uv).b * 5, 0, 2);
				}
#endif
				float2 dis = _Distortion.Sample(my_linear_repeat_sampler, mainUV * _TilingN3 + _Time.xx * 3 * _WindMovement);
				float displacementStrengh = 0.6 * (((sin(_Time.y + dis * 5) + sin(_Time.y * 0.5 + 1.051)) / 5.0) + 0.15 * dis) * bRipple; //hmm math
				dis = dis * displacementStrengh * (i.color.r * 1.3) * _WindForce * bRipple;

				float ripples = 0.25;
				float ripples2 = 0;
				float ripples3 = 0;
				float ripplesG = 0;
#ifdef USE_RT
				if (_HasRT)
				{
#ifdef USE_TP
					float3 rippleMain = _GlobalEffectRT.Sample(my_linear_clamp_sampler, float2((i.uv.x / _MainTex_ST.x) - _MainTex_ST.z, (i.uv.y / _MainTex_ST.y) - _MainTex_ST.w) + dis.xy * 0.04);
#else
					float3 rippleMain = _GlobalEffectRT.Sample(my_linear_clamp_sampler, uv + dis.xy * 0.04);
#endif
					ripples = (0.25 - rippleMain.z);
					ripples2 = (rippleMain.x);
					ripplesG = (0 - rippleMain.y);
					ripples3 = (0 - ripples2) * ripples2;
				}
#endif
#ifdef USE_PR
				float3 grassPattern = lerp(tex2D(_GrassTex, mainUV * _TilingN1 + dis.xy), tex2DStochastic(_GrassTex, mainUV * _TilingN1 + dis.xy), dist);
#else
				float3 grassPattern = tex2D(_GrassTex, mainUV * _TilingN1 + dis.xy);
#endif
				half4 col = _MainTex.Sample(my_linear_repeat_sampler, mainUV + dis.xy * 0.09);
				half4 colGround = _GroundTex.Sample(my_linear_repeat_sampler, mainUV + dis.xy * 0.05);

				float3 noise = _Noise.Sample(my_linear_repeat_sampler, mainUV * _TilingN2 + dis.xy) * _NoisePower;

#ifdef USE_VP
				half3 NoGrass = i.color.g;
				NoGrass.r = saturate(NoGrass.r + ripplesG);
#else
				half3 NoGrass = _NoGrassTex.Sample(my_linear_repeat_sampler, mainUV + dis.xy * 0.05);
				NoGrass.r = saturate(NoGrass.r + ripplesG);
#endif

				half alpha = step(1 - ((col.x + grassPattern.x) * _GrassThinness)*((2 - i.color.r)*NoGrass.r*grassPattern.x), (1 - i.color.r)*(NoGrass.r*grassPattern.x)*_GrassThinness - dis.x * 5);
				alpha = lerp(alpha, alpha + (grassPattern.x*NoGrass.r*(1 - i.color.r))*_GrassThinnessIntersection ,1 - NoGrass.r);

				if (i.color.r >= 0.01 && FadeStacks > 0.1)
				{
					clip(alpha*((ripples3 + 1)+ripples- (i.color.r)) -0.02);
				}
				if (_GrassCut > 0)
				{
					clip(alpha / max(_GrassCut, 0.001) - 0.02);
				}

				_Color *= 2;
				col.xyz = (pow(col, _GrassSaturation) * _GrassSaturation)*float3(_Color.x, _Color.y, _Color.z);
				col.xyz *= saturate(lerp(_SelfShadowColor, 1, pow(i.color.x, 1.1)) + (_GrassShading  * (ripples * 1 + 1) - noise.x*dis.x * 2) + (1 - NoGrass.r) - noise.x*dis.x * 2);
				col.xyz *= _Color * (ripples*-0.1 + 1);
				col.xyz *= 1 - (ripples2*(1 - saturate(i.color.r - 0.7)));

				if (i.color.r <= 0.01)
				{
					colGround.xyz *= ((1 - NoGrass.r)*_GroundColor*_GroundColor * 2);
					col.xyz = lerp(col.xyz, colGround.xyz, 1 - NoGrass.r);
				}
				Light mainLight = GetMainLight(TransformWorldToShadowCoord(i.worldPos));
				float3 normalDir = i.normal;
#ifdef USE_S

				half3 lm = 1;
#ifdef LIGHTMAP_ON
				lm = SampleLightmap(i.lmap, normalDir);
				col.rgb *= saturate(lm + 0.1);
				return col;
#else

				float shadowmap = mainLight.shadowAttenuation;
				half3 shadowmapColor = lerp(_ProjectedShadowColor, 1, shadowmap);
				col.xyz = col.xyz * saturate(shadowmapColor);
				if (mainLight.color.r + mainLight.color.g + mainLight.color.b > 0)
				{
					col.xyz *= mainLight.color;
				}
#endif
#endif	
#ifdef USE_AL
				col.rgb = saturate(col.rgb + (SampleSH(i.normal) - 0.33) * 0.33);
#endif
				col.xyz = MixFog(col.xyz, i.fogCoord);
				return col;
			}
				ENDHLSL
		}
		
		// SHADOW CASTING PASS, this will redraw geometry so keep this pass disabled if you want to save performance
		// Keep it if you want depth for post process or if you're using deferred rendering
		Pass{
				Tags {"LightMode" = "ShadowCaster" "RenderPipeline" = "UniversalPipeline" }
				//Tags{ "LightMode" = "ForwardBase" "DisableBatching" = "true" }
				//Tag for debugging only
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature USE_RT
			#pragma shader_feature USE_SC
			#pragma shader_feature USE_WC
			#pragma shader_feature USE_TP
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

			float3 _LightDirection;
			float3 _LightPosition;

		struct appdata
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
			half1 color : COLOR;
			float4 normal : NORMAL;
		};

		struct v2f
		{
			float3 worldPos : TEXCOORD2;
			float2 uv : TEXCOORD0;
			float4 pos : SV_POSITION;
			half1 color : COLOR;
			float3 normal : TEXCOORD3;
		};

		// Render Texture Effects //
		uniform Texture2D _GlobalEffectRT;
		uniform float3 _Position;
		uniform float _OrthographicCamSize;
		uniform float _HasRT;

		Texture2D _MainTex;
		Texture2D _NoGrassTex;
		float4 _MainTex_ST;
		Texture2D _Distortion;
		Texture2D _GrassTex;
		Texture2D _Noise;
		Texture2D _GroundTex;
		float _TilingN1;
		float _TilingN2, _WindForce;
		float4 _Color, _SelfShadowColor, _GroundColor;
		float _TilingN3;
		float _WindMovement;
		half _GrassThinness, _GrassShading, _GrassThinnessIntersection;
		half _NoisePower, _GrassSaturation, _FadeDistanceStart, _FadeDistanceEnd, _GrassCut, _WorldScale, _WorldRotation;

		SamplerState my_linear_repeat_sampler;
		SamplerState my_linear_clamp_sampler;

#define UnityObjectToWorld(o) mul(unity_ObjectToWorld, float4(o.xyz,1.0))
		v2f vert(appdata v)
		{
			v2f o;
			//o.pos = UnityObjectToClipPos(v.vertex);
			o.pos = 0;
			o.normal = 0;
#ifdef USE_SC
			o.pos = TransformWorldToHClip(ApplyShadowBias(GetVertexPositionInputs(v.vertex).positionWS, GetVertexNormalInputs(v.normal).normalWS, _LightDirection));
			o.normal = v.normal;
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			o.color = v.color;
			o.worldPos = UnityObjectToWorld(v.vertex);
#endif
			return o;
		}

			half4 frag(v2f i) : SV_Target
			{
							float2 mainUV;
			//Setup Coordinate Space
#ifdef USE_WC
			mainUV = i.worldPos.xz / max(_WorldScale, 0.001);

			float rotationAngle = _WorldRotation * 3.14159265359f / 180.0;
			float sina, cosa;
			sincos(rotationAngle, sina, cosa);
			float2x2 m = float2x2(cosa, -sina, sina, cosa);
			mainUV = mul(m, mainUV.xy);
#else
			mainUV = i.uv;
#endif

#ifdef USE_SC
				half FadeStacks = 1;
				float dist = distance(_WorldSpaceCameraPos, i.worldPos);
				if (dist > 0)
				{
					FadeStacks = lerp(1, 0, (dist - _FadeDistanceStart)*(1 / max(_FadeDistanceEnd - _FadeDistanceStart, 0.0001)));//Clamp because people will start dividing by 0
				}
				float2 uv = i.worldPos.xz - _Position.xz;
				uv = uv / (_OrthographicCamSize * 2);
				uv += 0.5;

				float bRipple = 1;
#ifdef USE_RT
				if (_HasRT)
				{
					bRipple = 1 - clamp(_GlobalEffectRT.Sample(my_linear_clamp_sampler, uv).b * 5, 0, 2);
				}
#endif
				float2 dis = _Distortion.Sample(my_linear_repeat_sampler, mainUV * _TilingN3 + _Time.xx * 3 * _WindMovement);
				float displacementStrengh = 0.6 * (((sin(_Time.y + dis * 5) + sin(_Time.y * 0.5 + 1.051)) / 5.0) + 0.15 * dis) * bRipple; //hmm math
				dis = dis * displacementStrengh * (i.color.r * 1.3) * _WindForce * bRipple;

				float ripples = 0.25;
				float ripples2 = 0;
				float ripples3 = 0;
				float ripplesG = 0;
#ifdef USE_RT
				if (_HasRT)
				{
					// .b(lue) = Grass height / .r(ed) = Grass shadow / .g(reen) is unassigned you can put anything you want if you need a new effect
#ifdef USE_TP
					float3 rippleMain = _GlobalEffectRT.Sample(my_linear_clamp_sampler, float2((i.uv.x / _MainTex_ST.x) - _MainTex_ST.z, (i.uv.y / _MainTex_ST.y) - _MainTex_ST.w) + dis.xy * 0.04);
#else
					float3 rippleMain = _GlobalEffectRT.Sample(my_linear_clamp_sampler, uv + dis.xy * 0.04);
#endif
					ripples = (0.25 - rippleMain.z);
					ripples2 = (rippleMain.x);
					ripplesG = (0 - rippleMain.y);
					ripples3 = (0 - ripples2) * ripples2;
				}
#endif
				half4 col = _MainTex.Sample(my_linear_repeat_sampler, mainUV + dis.xy * 0.09);

				float3 noise = _Noise.Sample(my_linear_repeat_sampler, mainUV * _TilingN2 + dis.xy) * _NoisePower;
				float3 grassPattern = _GrassTex.Sample(my_linear_repeat_sampler, mainUV * _TilingN1 + dis.xy);
				half3 NoGrass = _NoGrassTex.Sample(my_linear_repeat_sampler, mainUV + dis.xy * 0.05);
				NoGrass.r = saturate(NoGrass.r + ripplesG);

				half alpha = step(1 - ((col.x + col.y + col.z + grassPattern.x) * _GrassThinness) * ((2 - i.color.r) * NoGrass.r * grassPattern.x) * saturate(ripples + 1) * saturate(ripples + 1), ((1 - i.color.r) * (ripples + 1)) * (NoGrass.r * grassPattern.x) * _GrassThinness - dis.x * 5);
				alpha = lerp(alpha, alpha + (grassPattern.x * NoGrass.r * (1 - i.color.r)) * _GrassThinnessIntersection, 1 - (NoGrass.r) * (ripples * NoGrass.r + 0.75));

				if (i.color.r >= 0.01 && FadeStacks > 0.1)
				{
					clip(alpha*((ripples3 + 1) + ripples - (i.color.r)) - 0.02);
				}
				if (_GrassCut > 0)
				{
					clip(alpha / max(_GrassCut, 0.001) - 0.02);
				}
				//SHADOW_CASTER_FRAGMENT(i)
				return 0;
#endif
				return (0,0,0,0);
			}
				ENDHLSL
		}

		} //Fallback "VertexLit"
}
