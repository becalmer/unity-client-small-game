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
			ZTest Off
			ZWrite Off
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
			//int _Width;
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
				/*
				float dis = sqrt(i.opos.x * i.opos.x + i.opos.z * i.opos.z);
				float max_dis = 5;
				if (dis > max_dis)
				{
					discard;
				}
				else
				{
					if (i.opos.x < 0)
					{
						//discard;
					}
					float ring_world_range = _Object2World[0][0];
					float min_dis = (ring_world_range * max_dis - _Width) / ring_world_range;
					if (dis < min_dis)
					{
						discard;
					}
					_Color.a = (dis - min_dis) / (max_dis - min_dis);
				}
				*/
				float m = 5.3;
				float x = i.opos.x;
				float z = i.opos.z + m;
				float q = x * x + z * z;

				/*
				if ((i.opos.z > -5.0) && (i.opos.z < -4.9))
				{
					return float4(0, 0, 0, 1);
				}

				if ((i.opos.z > 4.9) && (i.opos.z < 5.0))
				{
					return float4(0, 0, 0, 1);
				}

				if ((i.opos.x > -5.0) && (i.opos.x < -4.9))
				{
					return float4(0, 0, 0, 1);
				}

				if ((i.opos.x > 4.9) && (i.opos.x < 5.0))
				{
					return float4(0, 0, 0, 1);
				}
				*/

				if (q > 64)
				{
					discard;
				}
				/*
				if ((i.opos.z > -0.1) && (i.opos.z < 0))
				{
					return float4(0, 0, 0, 1);
				}
				if ((i.opos.z > -5.0) && (i.opos.z < -4.9))
				{
					return float4(0, 0, 0, 1);
				}
				if ((i.opos.x > -0.3) && (i.opos.x < -0.2))
				{
					return float4(0, 0, 0, 1);
				}
				if ((i.opos.z > -1.2) && (i.opos.z < -1.1))
				{
					return float4(0, 0, 0, 1);
				}
				*/

				float k = 1.8;
				float d1 = -1.0 * k * i.opos.x - m - i.opos.z;
				if (d1 > 0)
				{
					discard;
				}
				float d2 = k * i.opos.x - m - i.opos.z;
				if (d2 > 0)
				{
					discard;
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
				/*
				else if (q >= 2.0 && q < 2.1)
				{
					a = 0.7;
					fix = true;
				}
				*/

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
				/*
				else if (d >= 2.0 && d < 2.1)
				{
					if (a > 1.0)
					{
						a = 0.7;
					}
					else
					{
						a = 0.4 * (q / 2 - 1);
					}
				}
				*/
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
				
				_Color = float4(1.0, 1.0, 1.0, clamp(a, 0.1, 1.0));

				return _Color;
			}

			ENDCG
		}		
	}
	FallBack "Diffuse"
}
