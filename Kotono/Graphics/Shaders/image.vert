#version 430 core

in vec2 aPos;

in vec2 aTexCoords;

uniform mat4 model;

out vec2 TexCoords;

void main()
{
    gl_Position = vec4(aPos, 0.0, 1.0) * model;
    TexCoords = aTexCoords;
}