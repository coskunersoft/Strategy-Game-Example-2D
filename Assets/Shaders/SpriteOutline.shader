Shader "Outline2D" {
	Properties {
		_Color ("Main Tint", Color) = (1,1,1,1)
		_OutlineColor ("Outline Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Cutoff ("Main Alpha Cutoff", Range(0,1)) = 0.5
		_OutlineCutoff ("Outline Alpha Cutoff", Range(0,1)) = 0.25
		_LineOffset ("Outline Depth Offset", Range(0,-10000)) = -1000
	}
	SubShader {

		//Standard Pass
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Offset -3, [_LineOffset]
		Cull Off

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		half _Cutoff;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 albedo = tex2D (_MainTex, IN.uv_MainTex) * _Color;
  			clip (albedo.a - _Cutoff);

			o.Albedo = albedo.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = albedo.a;
		}
		ENDCG

		
		//Outline Pass
		Tags { "RenderType"="Opaque" }
		LOD 200

		Offset 0, 0
		Cull Off
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard addshadow
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _OutlineColor;
		half _OutlineCutoff;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 albedo = tex2D (_MainTex, IN.uv_MainTex);
  			clip (albedo.a - _OutlineCutoff);

			o.Albedo = _OutlineColor.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = albedo.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}