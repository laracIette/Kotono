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

    bool isLeft = gl_FragCoord.x < (newDest.x - newDest.w / 2);
    bool isRight = gl_FragCoord.x > (newDest.x + newDest.w / 2);
    bool isTop = gl_FragCoord.y > (newDest.y + newDest.h / 2);
    bool isBottom = gl_FragCoord.y < (newDest.y - newDest.h / 2);

    if (isLeft && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newDest.x - newDest.w / 2, newDest.y + newDest.h / 2));
    }
    else if (isRight && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newDest.x + newDest.w / 2, newDest.y + newDest.h / 2));
    }
    else if (isLeft && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newDest.x - newDest.w / 2, newDest.y - newDest.h / 2));
    }
    else if (isRight && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newDest.x + newDest.w / 2, newDest.y - newDest.h / 2));
    }
    else if (isLeft)
    {
        dist = (newDest.x - newDest.w / 2) - gl_FragCoord.x;
    }
    else if (isRight)
    {
        dist = gl_FragCoord.x - (newDest.x + newDest.w / 2);
    }
    else if (isTop)
    {
        dist = gl_FragCoord.y - (newDest.y + newDest.h / 2);
    }
    else if (isBottom)
    {
        dist = (newDest.y - newDest.h / 2) - gl_FragCoord.y;
    }
    
    float ratio = dist / fallOff;
    result.a -= ratio;

    FragColor = result;
}