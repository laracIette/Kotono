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
uniform float cornerSize;

out vec4 FragColor;

void main()
{
    Rect newDest = Rect(dest.x, dest.y, dest.z, dest.w);

    float left = newDest.x - newDest.w / 2;
    float right = newDest.x + newDest.w / 2;
    float top = newDest.y + newDest.h / 2;
    float bottom = newDest.y - newDest.h / 2;

    bool isLeft = gl_FragCoord.x < left + cornerSize;
    bool isRight = gl_FragCoord.x > right - cornerSize;
    bool isTop = gl_FragCoord.y > top - cornerSize;
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
    
    vec4 result = color;
    float ratio = dist / fallOff;
    result.a -= ratio;

    FragColor = result;
}