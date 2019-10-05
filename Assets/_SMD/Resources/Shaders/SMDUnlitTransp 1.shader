//Hey guys! This shader is part of forum.unity3d.com/threads/simple-mesh-decal-script.103489
//I am https://youtube.com/vulgerstal edited this shader to make material double-sided
//What you can do here is make this material receive and cast shadows which I haven't done
//Very big thanks to guy who made this very cool decal system! ДЯКУЮ!

Shader "SMD/Tinted Alpha Blend Cull Off" {

   
Properties {
    _Color ("Color Tint (A = Opacity)", Color) = (1,1,1,1)
    _MainTex ("Texture (A = Transparency)", 2D) = ""
}

SubShader {
    Tags {Queue = Transparent}
    ZWrite Off
    Cull Off
    Blend SrcAlpha OneMinusSrcAlpha
    Pass {
        SetTexture[_MainTex] {Combine texture * constant ConstantColor[_Color]}
    }
    
    
    
    
    // What is coming after this line
    CGPROGRAM
 #pragma surface surf Lambert
 
 sampler2D _MainTex;
 fixed4 _Color;
 
 struct Input {
     float2 uv_MainTex;
     float4 color : COLOR;
 };
 
 void surf (Input IN, inout SurfaceOutput o) {
     fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
     o.Albedo = c.rgb;
     o.Albedo *= IN.color.rgb;
     o.Alpha = c.a;
 }
 ENDCG
  //  and before this line can be safely deleted/commented if You've some issues/don't need this
    
    
    
}
 //Fallback "VertexLit" // Commented out this line because I need something more..
 Fallback "Transparent/VertexLit" // And added this line instead....
}