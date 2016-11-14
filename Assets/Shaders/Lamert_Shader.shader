// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Folder1/Testing Shaders/Lambert Shader" 
{
	Properties
	{
		_color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
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

				float3 diffuseReflection = atten * _LightColor0.xyz * _color.rgb * max(0.0, dot(normalDirection, lightDirection)); //For lighting and color

				o.col = float4(diffuseReflection, 1.0);
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