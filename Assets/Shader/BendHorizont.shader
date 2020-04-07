Shader "Unlit/BendHorizont"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Curvature("Curvature", Float) = 0.001
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert vertex:vert addshadow

        uniform fixed4 _Color;
        uniform float _Curvature;

        struct Input
        {
            float2 uv_MainTex;
		};

        void vert(inout appdata_full v)
        {
            float4 worldSpace = mul(unity_ObjectToWorld, v.vertex);
            worldSpace.xyz -= _WorldSpaceCameraPos.xyz;
            worldSpace = float4(0.0f, (worldSpace.x * worldSpace.x) * -_Curvature, 0.0f, 0.0f);

            v.vertex += mul(unity_WorldToObject, worldSpace);
		}

        void surf(Input IN, inout SurfaceOutput o)
        {
            half4 c = _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;
		}

        ENDCG
    }

    FallBack "Mobile/Diffuse"
}
