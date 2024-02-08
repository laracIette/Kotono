#version 430 core

uniform sampler2D texSampler;

uniform vec4 color;

out vec4 FragColor;

const vec2 size = vec2(1600.0, 800.0);

void main()
{
    vec2 pos = gl_FragCoord.xy / size;
    FragColor = texture(texSampler, pos) * color;
}