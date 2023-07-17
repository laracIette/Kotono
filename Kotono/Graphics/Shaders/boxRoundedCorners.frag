#version 430 core

struct Rect {
    float x;
    float y;
    float w;
    float h;
};

uniform vec4 color;
uniform vec4 dest;
uniform float fallOff;

out vec4 FragColor;

void main()
{
    Rect newDest;
    newDest.x = dest.x;
    newDest.y = dest.y;
    newDest.w = dest.z;
    newDest.h = dest.w;

    vec4 result = color;

    float dist = 0;

    float left = newDest.x - newDest.w / 2;
    float right = newDest.x + newDest.w / 2;
    float top = newDest.y + newDest.h / 2;
    float bottom = newDest.y - newDest.h / 2;

    bool isLeft = gl_FragCoord.x < left;
    bool isRight = gl_FragCoord.x > right;
    bool isTop = gl_FragCoord.y > top;
    bool isBottom = gl_FragCoord.y < bottom;

    if (isLeft && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(left, top));
    }
    else if (isRight && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(right, top));
    }
    else if (isLeft && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(left, bottom));
    }
    else if (isRight && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(right, bottom));
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
    
    float ratio = dist / fallOff;
    result.a -= ratio;

    FragColor = result;
}