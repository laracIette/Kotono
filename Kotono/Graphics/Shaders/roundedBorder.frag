#version 430 core

uniform vec4 color;
uniform vec4 dest;
uniform float fallOff;
uniform float cornerSize;
uniform float thickness;

const float INFINITY = 1.0 / 0.0;

const float halfThick = thickness / 2;

const float left =   dest.x - dest.z / 2;
const float right =  dest.x + dest.z / 2;
const float top =    dest.y + dest.w / 2;
const float bottom = dest.y - dest.w / 2;

out vec4 FragColor;

void main()
{
    float leftDist =   abs(gl_FragCoord.x - left);
    float rightDist =  abs(gl_FragCoord.x - right);
    float topDist =    abs(gl_FragCoord.y - top);
    float bottomDist = abs(gl_FragCoord.y - bottom);

    float value = halfThick + fallOff + cornerSize;
    
    bool isLeft =   leftDist   < value;
    bool isRight =  rightDist  < value;
    bool isTop =    topDist    < value;
    bool isBottom = bottomDist < value;

    float dist = INFINITY;

    if (isLeft && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(left + value, top - value));
        dist -= value;
    }
    else if (isRight && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(right - value, top - value));
        dist -= value;
    }
    else if (isLeft && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(left + value, bottom + value));
        dist -= value;
    }
    else if (isRight && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(right - value, bottom + value));
        dist -= value;
    }
    else if (isLeft)
    {
        dist = leftDist;
    }
    else if (isRight)
    {
        dist = rightDist;
    }
    else if (isTop)
    {
        dist = topDist;
    }
    else if (isBottom)
    {
        dist = bottomDist;
    }

    dist = abs(dist);
    dist -= halfThick;
    float ratio = clamp(dist / fallOff, 0.0, 1.0);
    
    vec4 result = color;
    result.a -= ratio;

    if (gl_FragCoord.x < dest.x)
    {
        //result += vec4(.1);
    }

    FragColor = result;
}