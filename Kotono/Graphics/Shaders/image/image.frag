#version 430 core

in vec2 TexCoords;
out vec4 FragColor;

uniform sampler2D texSampler;
uniform vec4 color;

void main()
{
    FragColor = texture(texSampler, TexCoords) * color;
}