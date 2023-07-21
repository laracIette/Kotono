#version 430 core

in vec2 aPos;

uniform mat4 model;

void main()
{
    gl_Position = vec4(aPos, 0.0, 1.0) * model;
}