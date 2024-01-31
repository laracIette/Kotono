#version 430 core

uniform vec4 color;

out vec4 FragColor;

void main()
{
    // Compare depth value with stored depth in the depth buffer
    if (gl_FragCoord.z < gl_FragDepth)
    {
        discard;
    }

	FragColor = color;
}