#version 430 core

in vec2 TexCoords;

uniform sampler2D texSampler;

out vec4 FragColor;

void main()
{
    FragColor = texture(texSampler, TexCoords);
}