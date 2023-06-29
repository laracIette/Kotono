#version 430 core

uniform sampler2D texSampler;

uniform vec4 color;

uniform bool isInFront;

out vec4 FragColor;

in vec2 TexCoords;

void main()
{
    // Compare depth value with stored depth in the depth buffer
    if (isInFront && (gl_FragCoord.z < gl_FragDepth))
    {
        discard;
    }

	FragColor = texture(texSampler, TexCoords) * color;
}