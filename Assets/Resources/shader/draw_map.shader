Shader "Custom/draw_map"
{
	/*
	Properties
	{
	_Color ("Color", Color) = (1,1,1,1)
	_Width("RoundWidth", int) = 1
	}
	*/
	SubShader
	{
		Tags
		{
			"Queue" = "Transparent" "RenderType" = "Transparent"
		}

		Pass
		{
			//ZWrite Off
			ZTest Off
			ColorMask 0
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
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

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.opos = v.vertex;
				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				do
				{
					float e = 0.2;
					float s = 0.005;
					int m = i.opos.x / e;
					float f = e * m;

					if ((i.opos.x > (f - s)) && (i.opos.x < (f + s)))
					{
						_Color = float4(0.0, 1.0, 0.0, 0.3);
						break;
					}

					m = i.opos.z / e;
					f = e * m;

					if ((i.opos.z >(f - s)) && (i.opos.z < (f + s)))
					{
						_Color = float4(0.0, 1.0, 0.0, 0.3);
						break;
					}

					_Color = float4(1.0, 1.0, 1.0, 0.3);

				} while (false);

				return _Color;
			}

			ENDCG
		}
	}
	FallBack "Diffuse"
}
