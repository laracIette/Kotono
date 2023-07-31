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
    
    bool isLeft =   (leftDist <= rightDist) && (leftDist   < cornerSize);
    bool isRight =  !isLeft                 && (rightDist  < cornerSize);
    bool isTop =    (topDist <= bottomDist) && (topDist    < cornerSize);
    bool isBottom = !isTop                  && (bottomDist < cornerSize);

    float dist = INFINITY;

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

    FragColor = result;
}