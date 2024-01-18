#version 430 core

uniform sampler2D tex;

uniform vec4 color;

out vec4 FragColor;

void main()
{
    FragColor = texture(tex, gl_FragCoord.xy) * color;
}