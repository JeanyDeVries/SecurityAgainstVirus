Shader "Custom/Flipping Normals" {
	Properties{
		_TintColor("Tint Color", Color) = (0,0.5,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	}
		SubShader{

			Tags { "RenderType" = "Opaque" }

			Cull Front

			CGPROGRAM
			fixed4 _TintColor;

			#pragma surface surf Lambert vertex:vert fragment:frag
			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
				float4 color : COLOR;
			};

			void vert(inout appdata_full v) {
				v.normal.xyz = v.normal * -1;
			}

			fixed4 frag(Input i) : SV_Target
			{
				fixed4 col = _TintColor;

				return col;
			}

			void surf(Input IN, inout SurfaceOutput o) {
			fixed3 result = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = result.rgb;
			o.Alpha = 1;
			}

			ENDCG
	}

	Fallback "Diffuse"
}