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
uniform float thickness;

const float INFINITY = 1.0 / 0.0;

const Rect newDest = Rect(dest.x, dest.y, dest.z, dest.w);

const float halfThick = thickness / 2;

const float left =   newDest.x - newDest.w / 2;
const float right =  newDest.x + newDest.w / 2;
const float top =    newDest.y + newDest.h / 2;
const float bottom = newDest.y - newDest.h / 2;

out vec4 FragColor;

vec4 getInside();
vec4 getOutside();

void main()
{
    bool isInside = (gl_FragCoord.x > left) && (gl_FragCoord.x < right) && (gl_FragCoord.y > bottom) && (gl_FragCoord.y < top);

    FragColor = (isInside) ? getInside() : getOutside();
}

vec4 getInside()
{
    float newLeft =   left + halfThick;
    float newRight =  right - halfThick;
    float newTop =    top - halfThick;
    float newBottom = bottom + halfThick;

    float newCornerSize = cornerSize * ((newDest.x - newLeft) / (newDest.x - left));

    bool isLeft =   gl_FragCoord.x < newLeft + newCornerSize;
    bool isRight =  gl_FragCoord.x > newRight - newCornerSize;
    bool isTop =    gl_FragCoord.y > newTop - newCornerSize;
    bool isBottom = gl_FragCoord.y < newBottom + newCornerSize;
    
    float dist = 0;
    
    if (isLeft && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newLeft + newCornerSize, newTop - newCornerSize));
        dist -= newCornerSize;
    }
    else if (isRight && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newRight - newCornerSize, newTop - newCornerSize));
        dist -= newCornerSize;
    }
    else if (isLeft && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newLeft + newCornerSize, newBottom + newCornerSize));
        dist -= newCornerSize;
    }
    else if (isRight && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newRight - newCornerSize, newBottom + newCornerSize));
        dist -= newCornerSize;
    }
    else if (isLeft)
    {
        dist = newLeft - gl_FragCoord.x;
    }
    else if (isRight)
    {
        dist = gl_FragCoord.x - newRight;
    }
    else if (isTop)
    {
        dist = gl_FragCoord.y - newTop;
    }
    else if (isBottom)
    {
        dist = newBottom - gl_FragCoord.y;
    }
    
    vec4 result = color;
    float ratio = dist / fallOff;
    result.a = ratio;

    return result;
}

vec4 getOutside()
{
    bool isLeft =   gl_FragCoord.x < left + cornerSize;
    bool isRight =  gl_FragCoord.x > right - cornerSize;
    bool isTop =    gl_FragCoord.y > top - cornerSize;
    bool isBottom = gl_FragCoord.y < bottom + cornerSize;
    
    float dist = INFINITY;

    if (isLeft && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(left + cornerSize, top - cornerSize));
        dist -= cornerSize + halfThick;
    }
    else if (isRight && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(right - cornerSize, top - cornerSize));
        dist -= cornerSize + halfThick;
    }
    else if (isLeft && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(left + cornerSize, bottom + cornerSize));
        dist -= cornerSize + halfThick;
    }
    else if (isRight && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(right - cornerSize, bottom + cornerSize));
        dist -= cornerSize + halfThick;
    }
    else if (isLeft)
    {
        dist = (left - halfThick) - gl_FragCoord.x;
    }
    else if (isRight)
    {
        dist = gl_FragCoord.x - (right + halfThick);
    }
    else if (isTop)
    {
        dist = gl_FragCoord.y - (top + halfThick);
    }
    else if (isBottom)
    {
        dist = (bottom - halfThick) - gl_FragCoord.y;
    }
    
    vec4 result = color;
    float ratio = dist / fallOff;
    result.a -= ratio;

    return result;
}