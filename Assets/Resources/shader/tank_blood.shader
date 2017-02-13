Shader "Custom/tank_blood"
{
	Properties
	{
		_Rate("BloodRate", Range(0, 100)) = 60
	}
	SubShader
	{		
		Tags
		{
			"Queue"="Overlay"
		}
		Pass
		{
			ZTest Off
			//ZWrite Off
			ColorMask 0
		}

		Pass
		{
			//Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			//#pragma surface surf Standard fullforwardshadows
			#pragma vertex vert
			#pragma fragment frag

			// Use shader model 3.0 target, to get nicer looking lighting
			//#pragma target 3.0
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 opos : TEXCOORD1;
			};

			float4 _Color;
			int _Rate;
			//float4 _MainTex_ST;

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.opos = v.vertex;
				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				float x = _Rate / 100.0 * 10 - 5.0;
				if (i.opos.x > x)
				{
					return float4(0.56, 0.56, 0.56, 1.0);
				}

				return float4(1, 0, 0, 1);
			}

			ENDCG
		}		
	}
	FallBack "Diffuse"
}
