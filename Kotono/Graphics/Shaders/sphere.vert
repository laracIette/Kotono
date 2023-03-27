#version 430 core

layout (location = 0) in vec3 aPos;

//uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform vec3 cameraRight;
uniform vec3 cameraUp;
uniform vec3 centerPos;
uniform vec2 scale;

void main()
{
    vec3 vertexPosition = 
		centerPos
		+ cameraRight * aPos.x * scale.x
		+ cameraUp    * aPos.y * scale.y;

    gl_Position = vec4(vertexPosition, 1.0) * view * projection;
}