#version 430 core

in vec2 TexCoords;

uniform sampler2D texSampler;

uniform vec3 color;

out vec4 FragColor;

void main()
{
    FragColor = texture(texSampler, TexCoords) * vec4(color, 1.0);
}