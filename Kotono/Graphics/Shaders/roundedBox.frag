#version 430 core

uniform vec4 color;
uniform vec4 sides;
uniform float fallOff;
uniform float cornerSize;

float left =   sides.x;
float right =  sides.y;
float top =    sides.z;
float bottom = sides.w;

out vec4 FragColor;

void main()
{
    bool isLeft =   gl_FragCoord.x < left + cornerSize;
    bool isRight =  gl_FragCoord.x > right - cornerSize;
    bool isTop =    gl_FragCoord.y > top - cornerSize;
    bool isBottom = gl_FragCoord.y < bottom + cornerSize;
    
    float dist = 0;

    if (isLeft && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(left + cornerSize, top - cornerSize));
        dist -= cornerSize;
    }
    else if (isRight && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(right - cornerSize, top - cornerSize));
        dist -= cornerSize;
    }
    else if (isLeft && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(left + cornerSize, bottom + cornerSize));
        dist -= cornerSize;
    }
    else if (isRight && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(right - cornerSize, bottom + cornerSize));
        dist -= cornerSize;
    }
    else if (isLeft)
    {
        dist = left - gl_FragCoord.x;
    }
    else if (isRight)
    {
        dist = gl_FragCoord.x - right;
    }
    else if (isTop)
    {
        dist = gl_FragCoord.y - top;
    }
    else if (isBottom)
    {
        dist = bottom - gl_FragCoord.y;
    }
    else
    {
        FragColor = color;
        return;
    }
    
    float ratio = clamp(dist / fallOff, 0.0, 1.0) * color.a;
    
    vec4 result = color;
    result.a -= ratio;

    FragColor = result;
}