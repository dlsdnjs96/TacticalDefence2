// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/GrayScale" {
	Properties{
		_Color("Color", Color) = (0.5,0.5,0.5,0.5)
		[HideInInspector]_MainTex("Albedo (RGB)", 2D) = "white" {}
		[HideInInspector]_ColorMask("ColorMask", Float) = 0
		[HideInInspector]_Stencil("Stencil ID", Float) = 0
		[HideInInspector]_StencilOp("Stencil Op", Float) = 0
		[HideInInspector]_StencilComp("Stencil Op", Float) = 0
		[HideInInspector]_StencilReadMask("Stencil Op", Float) = 0
		[HideInInspector]_StencilWriteMask("Stencil Op", Float) = 0
		_Alpha("��ü����", Range(0, 1)) = 1
		_MaxLeft("���� Fade", Range(-1, 0)) = 0
		_MaxRight("������ Fade", Range(-1, 0)) = 0
		_MaxUp("���� Fade", Range(-1, 0)) = 0
		_MaxDown("�Ʒ��� Fade", Range(-1, 0)) = 0
		_HorizontalLineMul("���� ��輱 ����", int) = 20
		_VerticalLineMul("���� ��輱 ����", int) = 20
	}

		SubShader{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}

			LOD 200
			Lighting Off
			ZWrite Off
			ZTest Off
			Blend SrcAlpha OneMinusSrcAlpha

			Stencil{
				Ref 1
				Comp equal
				Pass keep
			}

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			//#pragma surface surf NoLighting alpha:fade
			#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 2.0

			sampler2D _MainTex;

			struct Input {
				float2 uv_MainTex;
				fixed4 screenPos;
			};

			fixed4 _Color;
			fixed _Alpha;
			fixed _MaxLeft; //���� ����ũ
			fixed _MaxRight; //������ ����ũ
			fixed _MaxUp; //���� ����ũ
			fixed _MaxDown; //���� ����ũ
			fixed _HorizontalLineMul; //���� ����
			fixed _VerticalLineMul; //���� ����



			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_INSTANCING_BUFFER_END(Props)

			void surf(Input IN, inout SurfaceOutputStandard o) {

				fixed3 ScreenUV = IN.screenPos.xyz / IN.screenPos.a;

				fixed LeftMask = saturate((ScreenUV.x + _MaxLeft) * _HorizontalLineMul);
				fixed RightMask = saturate((1 - ScreenUV.x + _MaxRight) * _HorizontalLineMul);
				fixed UpMask = saturate((1 - ScreenUV.y + _MaxUp) * _VerticalLineMul);
				fixed DownMask = saturate((ScreenUV.y + _MaxDown) * _VerticalLineMul);



				fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

				if (c.a == 0)
					discard;

				o.Albedo = (c.r * 0.3 + c.g * 0.59 + c.b * 0.11) * _Color;
				o.Alpha = c.a * _Alpha * LeftMask * RightMask * DownMask * UpMask;
			}

			fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
			{
				fixed4 c;
				c.rgb = s.Albedo;
				c.a = s.Alpha;
				return c;
			}


			ENDCG
		}
			FallBack "Diffuse"
}