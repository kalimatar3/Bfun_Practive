Shader "UI/LogoFlash"
{
  Properties
  {
    _MainTex ("Texture", 2D) = "white" {}
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      ZClip Off
      Blend SrcAlpha OneMinusSrcAlpha
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      //uniform float4 _Time;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 xlv_TEXCOORD0 :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          float4 tmpvar_1;
          tmpvar_1.w = 1;
          tmpvar_1.xyz = in_v.vertex.xyz;
          out_v.vertex = mul(unity_MatrixVP, mul(unity_ObjectToWorld, tmpvar_1));
          out_v.xlv_TEXCOORD0 = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          float4 texCol_1;
          float4 outp_2;
          float4 tmpvar_3;
          tmpvar_3 = tex2D(_MainTex, in_f.xlv_TEXCOORD0);
          texCol_1 = tmpvar_3;
          float brightness_4;
          brightness_4 = 0;
          float tmpvar_5;
          tmpvar_5 = (_Time.y - float((int((_Time.y / 3)) * 3)));
          if((tmpvar_5>2))
          {
              float xProjL_6;
              float x0_7;
              float xPointRightBound_8;
              float xPointLeftBound_9;
              x0_7 = ((tmpvar_5 - 2) / 0.7);
              xProjL_6 = (in_f.xlv_TEXCOORD0.y / 3.722121);
              xPointLeftBound_9 = ((x0_7 - 0.25) - xProjL_6);
              xPointRightBound_8 = (x0_7 - xProjL_6);
              xPointLeftBound_9 = (xPointLeftBound_9 + 0.15);
              xPointRightBound_8 = (xPointRightBound_8 + 0.15);
              if(((in_f.xlv_TEXCOORD0.x>xPointLeftBound_9) && (in_f.xlv_TEXCOORD0.x<xPointRightBound_8)))
              {
                  brightness_4 = ((0.25 - (2 * abs((in_f.xlv_TEXCOORD0.x - ((xPointLeftBound_9 + xPointRightBound_8) / 2))))) / 0.25);
              }
          }
          float tmpvar_10;
          tmpvar_10 = max(brightness_4, 0);
          brightness_4 = tmpvar_10;
          if((texCol_1.w>0.5))
          {
              outp_2 = (texCol_1 + float4(tmpvar_10, tmpvar_10, tmpvar_10, tmpvar_10));
          }
          else
          {
              outp_2 = float4(0, 0, 0, 0);
          }
          out_f.color = outp_2;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
