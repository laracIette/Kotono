#version 430 core

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D color;

const float offset = 1.0 / 1280.0;

const vec2 offsets[9] = vec2[] (
    vec2(-offset,  offset), // top-left
    vec2( 0.0f,    offset), // top-center
    vec2( offset,  offset), // top-right
    vec2(-offset,  0.0f),   // center-left
    vec2( 0.0f,    0.0f),   // center-center
    vec2( offset,  0.0f),   // center-right
    vec2(-offset, -offset), // bottom-left
    vec2( 0.0f,   -offset), // bottom-center
    vec2( offset, -offset)  // bottom-right    
);
    
vec3 sampleColor[9];

vec4 CalcBlur();

void main()
{
    for(int i = 0; i < 9; i++)
    {
        sampleColor[i] = vec3(texture(color, TexCoords.st + offsets[i]));
    }

    FragColor = CalcBlur();
}

vec4 CalcBlur()
{
    vec3 result = vec3(0);

    for (int i = 0; i < 9; i++)
    {
        result += sampleColor[i] / 9;
    }

    return vec4(result, 1);
}