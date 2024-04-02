#version 430 core

in vec2 TexCoords;

uniform sampler2D array;

out vec4 FragColor;

void main()
{    
    FragColor = vec4(vec3(texture(array, TexCoords).x), 1.0);
}