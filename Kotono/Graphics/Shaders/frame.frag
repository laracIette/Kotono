// https://learnopengl.com/Advanced-Lighting/Bloom

#version 430 core

out vec4 FragColor;

in vec2 TexCoords;


uniform sampler2D frameColorTexture;
//uniform sampler2D frameBloomTexture;


const float exposure = 0.5;
const float gamma = 2.2;

void main()
{

    vec3 frameColor = texture(frameColorTexture, TexCoords).rgb;
    //vec3 bloomColor = texture(frameBloomTexture, TexCoords).rgb;
         
    // additive blending
    //frameColor += bloomColor;

    // tone mapping
    vec3 result = vec3(1.0) - exp(-frameColor * exposure);
    
    // gamma correct 
    result = pow(result, vec3(1.0 / gamma));
    
    FragColor = vec4(result, 1.0);
}
