#version 430 core

uniform sampler2D frame;

out vec4 FragColor;

vec4 CalcOutline();

void main()
{
    FragColor = vec4(texture(frame, vec2(gl_FragCoord))) + CalcOutline();
}

vec4 CalcOutline()
{
    float alpha = 0;

    

    return vec4(0, 0, 0, alpha);
}
