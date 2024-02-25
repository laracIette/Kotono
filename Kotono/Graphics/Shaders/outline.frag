#version 430 core

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D depth;

const vec3 color = vec3(0.0, 0.0, 0.0);

const vec2 windowSize = vec2(1600.0, 900.0);

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
    
const float near = 0.1;
const float far = 100.0;

float sampleDepth[9];

float GetOutline();
float linearizeDepth(float depth);

void main()
{
    //FragColor = vec4(vec3(linearizeDepth(texture(depth, TexCoords.st).r)), 1.0);
    //return;

    for (int i = 0; i < 9; i++)
    {
        sampleDepth[i] = linearizeDepth(texture(depth, TexCoords.st + offsets[i]).r);
    }

    float outline = GetOutline();

    if (outline > 0.1)
    {
        FragColor = vec4(color, outline);
    }
}

float GetOutline()
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
        if (depth > maxDepth)
        {
            maxDepth = depth;
        }
    }

    return maxDepth - minDepth;
}

// Linearize the depth within the range [0, 1].
float linearizeDepth(float depth)
{
    return (2.0 * near * far) / (far + near - (depth * 2.0 - 1.0) * (far - near)) / far;
}
