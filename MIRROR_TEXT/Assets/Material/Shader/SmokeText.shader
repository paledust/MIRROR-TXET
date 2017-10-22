Shader "GUI/SmokeText"
{
	Properties
	{
        _MainTex ("Font Texture", 2D) = "white" {} 
        _Color ("Text Color", Color) = (1,1,1,1) 
	}
    SubShader { 
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" } 
        Lighting Off Cull Off ZWrite Off Fog { Mode Off } 
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
			};
			float random2(in fixed2 st){
				st = fixed2(dot(st,fixed2(127.1,311.7)),
						dot(st,fixed2(269.5,183.3)) );
				fixed2 r = frac(sin(st)*43758.5453123);
				return r.x*r.y;
			}

			float noisy (in fixed2 st) {
				fixed2 i = floor(st);
				fixed2 f = frac(st);

				// Four corners in 2D of a tile
				float a = random2(fixed2(i));
				float b = random2(fixed2(i) + fixed2(1.0, 0.0));
				float c = random2(fixed2(i) + fixed2(0.0, 1.0));
				float d = random2(fixed2(i) + fixed2(1.0, 1.0));

				fixed2 u = f * f * (3.0 - 2.0 * f);

				return a*u.x + b*(1-u.x) + 
						(c - a)* u.y * (1.0 - u.x) + 
						(d - b) * u.x * u.y;;
			}
			float fbm(in fixed2 st){
				fixed value = 0.0;
				fixed amplitud = 1.;
				fixed2 shift = fixed2(100.0,100.0);
				fixed frequency = 0.;

				float2x2 rot = float2x2(cos(.5), sin(.5),
								-sin(.5), cos(.5));

				for (int i = 0; i < 6; i++) {
					value += amplitud * noisy(st + frequency * _Time);
					st = mul(rot , st) * 2. + shift;
					amplitud *= .5;
				}

				return value;
			}

			fixed4 main(v2f i) {
				fixed2 st = i.uv;
				fixed3 color = fixed3(0,0,0);
				st *= 3.;

				fixed2 q = fixed2(0,0);
				q.x = fbm(st + 0.00*_Time);
				q.y = fbm(st + fixed2(1,1));

				fixed2 r = fixed2(0,0);
				r.x = fbm(st + .3*q + fixed2(1.7,9.2) + .15*_Time);
				r.y = fbm( st + .3*q + fixed2(8.3,2.8)+ 0.126*_Time);

				float f = fbm(st + r);
				color = lerp(fixed3(0.101961,0.619608,0.666667),
							fixed3(0.666667,0.666667,0.498039),
							clamp((f*f)*4.0,0.0,1.0));

				color = lerp(color,
							fixed3(0,0,0.164706),
							clamp(length(q),0.0,1.0));

				color = lerp(color,
							fixed3(0.666667,1,1),
							clamp(length(r.x * r.y * r.y),0.0,1.0));
				return fixed4(color,1.0);
			}

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				col.rgb = _Color.rgb * fbm(i.uv.xy + 0.1*_Time);
				col.a *= _Color.a;
				return col;
			}
			ENDCG
		}
	}
}
