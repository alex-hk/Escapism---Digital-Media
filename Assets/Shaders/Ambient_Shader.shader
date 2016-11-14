// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Folder1/Testing Shaders/Ambient" 
{
	Properties
	{
		_color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}

	SubShader
	{
		Pass
		{
			Tags
			{
				"LightMode" = "ForwardBase" //Use forward rendering, whatever that means
			}
			CGPROGRAM

			#pragma vertex vertFunction
			#pragma fragment fragFunction

			//user defined variables
			uniform float4 _color;

			//Unity defined variables
			uniform float4 _LightColor0;
			//_Object2World (4x4 transpose. Multiply by vertex position to go from object to world space)
			//_World2Object (4x4 transpose. Multiply by vertex position to go from world to object space)

			//structs
			struct vertexInput
			{
				float4 vertexPos : POSITION;
				float3 normal : NORMAL; //All vertexes and faces have normals (in local space), shows which way it's facing
			};

			struct vertexOutput
			{
				float4 position : SV_POSITION;
				float4 col : COLOR; //vertex color, made for overwriting vetex color with out own color
			};

			//vertex function
			vertexOutput vertFunction (vertexInput v)
			{
				vertexOutput o;
				float3 normalDirection = normalize(mul(unity_ObjectToWorld,float4(v.normal , 0.0)).xyz);  //One you want to transform is on the right, converts object to world space
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				float atten = 1.0; //Distance between light and source

				float3 diffuseReflect = atten * _LightColor0.xyz * max(0.0, dot(normalDirection, lightDirection)); //For lighting and color
				float3 finalLight = diffuseReflect + UNITY_LIGHTMODEL_AMBIENT.xyz; //Calculates all light first

				o.col = float4(finalLight * _color.rgb, 1.0); //Adds color in later
				o.position = mul(UNITY_MATRIX_MVP, v.vertexPos);
				return o;
			}

			//fragment function
			float4 fragFunction (vertexOutput i) : COLOR
			{
				return i.col;
			}
			ENDCG
		}
	}

	//Fallback "Diffuse"
}