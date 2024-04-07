#version 430 core

in vec2 TexCoords;

uniform sampler2D tex;

out vec4 FragColor;

void main()
{    
    FragColor = vec4(texture(tex, TexCoords));
}