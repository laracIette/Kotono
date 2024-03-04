#version 430 core

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D color;

const vec2 windowSize = vec2(1600.0, 800.0);

const vec2 offset = 1.0 / windowSize;

const vec2 offsets[9] = vec2[] (
    vec2(-offset.x,  offset.y), // top-left
    vec2( 0.0,       offset.y), // top-center
    vec2( offset.x,  offset.y), // top-right
    vec2(-offset.x,  0.0),      // center-left
    vec2( 0.0,       0.0),      // center-center
    vec2( offset.x,  0.0),      // center-right
    vec2(-offset.x, -offset.y), // bottom-left
    vec2( 0.0,      -offset.y), // bottom-center
    vec2( offset.x, -offset.y)  // bottom-right    
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