// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ParticlesAni/AdditiveAnimation"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_AddColor("_AddColor",Color) = (1,1,1,1)
		_Ani("Animation:Tiling X,Tiling Y,Offset X,Offset Y",vector) = (1,1,0,0)
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha One
		Cull Off Lighting Off ZWrite Off
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _AddColor;
			float4 _Ani;
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float2 uvOff = TRANSFORM_TEX(v.uv, _MainTex);
				uvOff.x +=_Ani.z *_Time.y;
				uvOff.y +=_Ani.w *_Time.y;
				o.uv = uvOff;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float2  tilingOff = i.uv;
				tilingOff.x *=_Ani.x;
				tilingOff.y *=_Ani.y;
				fixed4 col = tex2D(_MainTex, tilingOff);
				return col*_AddColor;
			}
			ENDCG
		}
	}
}
