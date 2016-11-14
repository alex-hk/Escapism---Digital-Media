Shader "Folder1/Testing Shaders/First Shader" 
{

	Properties
	{
	//Underscored variables are properties we can mess with
		_color ("Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_MainTex ("Sprite Texture", 2D) = "white" {}
		// _propertyName ("Name", Color package? )
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM

			//pragmas (tells shader what functions to expect)
			#pragma vertex vertF
			#pragma fragment fragF

			//user defined variables
			uniform float4 _color;

			//base input structs
			struct vertexInput //name doesn't matter
			{
				float4 vertexPos : POSITION; //made to store position of object
			};

			struct vertexOutput
			{
				float4 position : SV_POSITION; //made so that we can multiply position by unity matrix and send it back
			};

			//vertex function
			vertexOutput vertF(vertexInput v)
			{
				vertexOutput o;
				o.position = mul(UNITY_MATRIX_MVP, v.vertexPos); //mul means multiply, used for multiplying matrixes
				return o;
			}

			//fragments function
			float4 fragF(vertexOutput i) : COLOR
			{
				return _color;
			}
			ENDCG
		}
	}
	//Comment out the fallback during development to test it
	Fallback "Diffuse"
}