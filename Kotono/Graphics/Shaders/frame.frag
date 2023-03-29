#version 430 core

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D frameTexture;


uniform float threshold = 1.0;
uniform float bloomIntensity = 1.0;
uniform float bloomRadius = 0.1;

vec3 CalcBloom();


void main()
{
    vec3 color = texture(frameTexture, TexCoords).rgb;

    if (color.r + color.g + color.b > threshold)
    {
        color += CalcBloom();
    }

    FragColor = vec4(color, 1.0);
}


vec3 CalcBloom()
{
    vec3 bloom = vec3(0.0);
    vec2 texelSize = 1.0 / textureSize(frameTexture, 0);
    
    // Blur the bright areas of the image
    for (int x = -5; x <= 5; x++)
    {
        for (int y = -5; y <= 5; y++)
        {
            vec2 offset = vec2(float(x), float(y)) * texelSize * bloomRadius;
            bloom += texture(frameTexture, TexCoords + offset).rgb;
        }
    }
    
    bloom /= 121.0; // Divide by the number of samples to get the average color
    
    return bloom * bloomIntensity;
}
