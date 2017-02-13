Shader "Custom/attack_fan_range"
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
			"Queue"="Transparent" "RenderType"="Transparent"
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
				float m = 5.3;
				float x = i.opos.x;
				float z = i.opos.z + m;
				float q = x * x + z * z;

				if (q > 64)
				{
					_Color = float4(1.0, 1.0, 1.0, 0.0);
					return _Color;
				}

				float k = 1.8;
				float d1 = -1.0 * k * i.opos.x - m - i.opos.z;
				if (d1 > 0)
				{
					_Color = float4(1.0, 1.0, 1.0, 0.0);
					return _Color;
				}
				float d2 = k * i.opos.x - m - i.opos.z;
				if (d2 > 0)
				{
					_Color = float4(1.0, 1.0, 1.0, 0.0);
					return _Color;
				}

				q = sqrt(q);

				q = 8 - q;

				float a = 2.0;

				bool fix = false;

				if (q < 0.1)
				{
					a = 0.7;
				}
				else if (q >= 0.1 && q < 2.0)
				{
					a = 0.4 * (1 - q / 2);
				}

				float d = d2;
				if (d1 > d2)
				{
					d = d1;
				}

				d *= -1.0;

				if (d < 0.1)
				{
					a = 0.7;
				}
				else if (d >= 0.1 && d < 2.0)
				{
					if ((a > 1) || (fix))
					{
						a = 0.4 * (1 - q / 2);
					}
				}
				else
				{
					if (a > 1)
					{
						a = 0.3 * (q / 10);
					}
				}

				if ((i.opos.z > -5.0) && (i.opos.z < -4.95))
				{
					a = 0.7;
				}
				
				_Color = float4(1.0, 0.0, 0.0, clamp(a, 0.1, 1.0));

				return _Color;
			}

			ENDCG
		}		
	}
	FallBack "Diffuse"
}
