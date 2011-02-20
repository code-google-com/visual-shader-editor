float4 Color;

float UseTexture;

texture2D Texture;
SamplerState Texture_Sampler
{
	Filter = MIN_MAG_MIP_LINEAR;
	AddressU = Wrap;
	AddressV = Wrap;
	AddressW = Wrap;
};


struct VS_IN
{
	float4 pos : POSITION;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
};

struct VS_IN_TEXTURE
{
	float4 pos : POSITION;
	float2 TCoord : TEXCOORD0;
	float4 Color : COLOR0;
};

struct PS_IN_TEXTURE
{
	float4 pos : SV_POSITION;
	float2 TCoord : TEXCOORD0;
	float4 Color: COLOR0;
};

PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	
	output.pos = input.pos;
	
	return output;
}

float4 PS( PS_IN input ) : SV_Target
{
	return Color;
}

PS_IN_TEXTURE VSQuad( VS_IN_TEXTURE input )
{
	PS_IN_TEXTURE output = (PS_IN_TEXTURE)0;
	
	output.pos = input.pos;
	output.TCoord = input.TCoord;
	output.Color = input.Color;
	
	return output;
}

float4 PSQuad( PS_IN_TEXTURE input ) : SV_Target
{
	float4 c = float4(0,0,0,0);
	
	if(UseTexture)
		c = Texture.Sample(Texture_Sampler, input.TCoord) * input.Color;
	else
		c = input.Color;
		
		//c.r = c.a;
	return c;
	//return float4(1,1,1,1);
}

BlendState SrcAlphaBlendingAdd
{
	BlendEnable[0] = TRUE;
	SrcBlend = SRC_ALPHA;
	DestBlend = INV_SRC_ALPHA;
	BlendOp = ADD;
	SrcBlendAlpha = ZERO;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
	RenderTargetWriteMask[0] = 0x0F;
};

BlendState NoBlend
{
	BlendEnable[0] = FALSE;
	SrcBlend = SRC_ALPHA;
	DestBlend = INV_SRC_ALPHA;
	BlendOp = ADD;
	SrcBlendAlpha = ZERO;
	DestBlendAlpha = ZERO;
	BlendOpAlpha = ADD;
	RenderTargetWriteMask[0] = 0x0F;
};

technique10 ColorTechnique
{
	pass P0
	{
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, VS() ) );
		SetPixelShader( CompileShader( ps_4_0, PS() ) );
	}
}

technique10 QuadTechnique
{
	pass P0
	{
		SetBlendState( SrcAlphaBlendingAdd, float4( 0.0f, 0.0f, 0.0f, 0.0f ), 0xFFFFFFFF );

		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, VSQuad() ) );
		SetPixelShader( CompileShader( ps_4_0, PSQuad() ) );
	}
}

technique10 QuadTechniqueNoBlend
{
	pass P0
	{
		SetBlendState( NoBlend, float4( 0.0f, 0.0f, 0.0f, 0.0f ), 0xFFFFFFFF );

		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_4_0, VSQuad() ) );
		SetPixelShader( CompileShader( ps_4_0, PSQuad() ) );
	}
}
