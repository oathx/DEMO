// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/UVOffset"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OffsetScaleX("offset scale",Range(0,0.5)) = 0
		_OffsetScaleY("offset scale",Range(0,0.5)) = 0
		_AlphaScale("alpha scale",Range(0,1)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" 
					"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
			}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha   

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
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
			float _OffsetScaleX;
			float _OffsetScaleY;
			float _AlphaScale;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				v.uv.x += _Time.y * _OffsetScaleX;
				v.uv.y += _Time.y * _OffsetScaleY;

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				return fixed4(col.rgb,_AlphaScale * col.a);
			}
			ENDCG
		}
	}
}
