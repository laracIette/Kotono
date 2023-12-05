#version 430 core

out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D color;
uniform sampler2D depth;

vec4 CalcOutline();

void main()
{
    FragColor = vec4(texture(color, TexCoords)) + CalcOutline();
}

vec4 CalcOutline()
{
    float alpha = texture(depth, TexCoords).r;

    return vec4(0, 0, 0, alpha);
}
