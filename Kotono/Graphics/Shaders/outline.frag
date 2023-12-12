#version 430 core

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D depth;

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
    
float sampleDepth[9];

bool IsOutline();
float linearizeDepth(float depth);

void main()
{
    for(int i = 0; i < 9; i++)
    {
        sampleDepth[i] = linearizeDepth(texture(depth, TexCoords.st + offsets[i]).r);
    }

    if (IsOutline())
    {
        FragColor = vec4(0, 0, 0, 1);
    }
    else
    {
        discard;
    }
}

bool IsOutline()
{
    float minDepth = 1.0;
    float maxDepth = 0.0;

    for (int i = 0; i < 9; i++)
    {
        float depth = sampleDepth[i];
        
        if (depth < minDepth)
        {
            minDepth = depth;
        }
        else if (depth > maxDepth)
        {
            maxDepth = depth;
        }
    }

    return (maxDepth - minDepth) > 0.1 ? true : false;
}

float linearizeDepth(float depth)
{
    float near = 0.1;
    float far = 100.0;

    return (2.0 * near * far) / (far + near - (depth * 2.0 - 1.0) * (far - near)) / far;
}