Shader "BirdGame/BackgroundGradient" {
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ColorTop ("Top Color", Color) = (1,1,1,1)
		_ColorMid ("Mid Color", Color) = (1,1,1,1)
		_ColorBot ("Bot Color", Color) = (1,1,1,1)
		_Height ("Height", Float) = 10.0
		_Middle ("Middle", Range(0.001, 0.999)) = 1
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjectors"="True" "RenderType"="Transparent"  }
		LOD 100
		ZWrite Off
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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _ColorTop;
			float4 _ColorMid;
			float4 _ColorBot;
			float _Height;
			float _Middle;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			half4 frag (v2f i) : COLOR
			{
			
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				
				fixed4 gradient = lerp(_ColorBot, _ColorMid, i.worldPos.y / _Middle) * step(i.worldPos.y, _Middle);
				gradient += lerp(_ColorMid, _ColorTop, (i.worldPos.y - _Middle) / (1 - _Middle)) * (1 - step(i.worldPos.y, _Middle));
				
				col = col * gradient;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
				
				
				/*
				 fixed4 c = lerp(_ColorBot, _ColorMid, i.texcoord.y / _Middle) * step(i.texcoord.y, _Middle);
                 c += lerp(_ColorMid, _ColorTop, (i.texcoord.y - _Middle) / (1 - _Middle)) * (1 - step(i.texcoord.y, _Middle));
                 c.a = 1;
                return c;
                */
			}
			ENDCG
		}
	}
}
