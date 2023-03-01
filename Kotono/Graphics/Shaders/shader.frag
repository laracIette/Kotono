#version 330

out vec4 outputColor;

in vec2 texCoord;

#define NR_OF_TEXTURES 2

uniform sampler2D textures[NR_OF_TEXTURES];

void main()
{
    for (int i = 0; i < NR_OF_TEXTURES; i++)
        outputColor += texture(textures[i], texCoord);

    outputColor /= NR_OF_TEXTURES;
}