#version 430 core

in vec2 aPos;

in vec2 aTexCoords;

out vec2 TexCoords;

uniform mat4 model;

void main()
{
    gl_Position = vec4(aPos, 0.0, 1.0) * model;
    TexCoords = aTexCoords;
}