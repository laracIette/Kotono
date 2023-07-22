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
float getDist(float initialDist, float left, float right, float top, float bottom, float cornerSize, float halfThick);

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
    
    float dist = getDist(0, newLeft, newRight, newTop, newBottom, newCornerSize, -halfThick * 2);
    if (dist > thickness + fallOff)
    {
        dist -= thickness + fallOff;
        
        vec4 result = color;
        float ratio = dist / fallOff;
        result.a -= ratio;

        return result;
    }
    else
    {
        vec4 result = color;
        float ratio = dist / fallOff;
        result.a -= 1 - ratio;

        return result;
    }
}

vec4 getOutside()
{
    float dist = getDist(INFINITY, left, right, top, bottom, cornerSize, halfThick);
    
    vec4 result = color;
    float ratio = dist / fallOff;
    result.a -= ratio;

    return result;
}

float getDist(float initialDist, float left, float right, float top, float bottom, float cornerSize, float halfThick)
{
    bool isLeft =   gl_FragCoord.x < left + cornerSize;
    bool isRight =  gl_FragCoord.x > right - cornerSize;
    bool isTop =    gl_FragCoord.y > top - cornerSize;
    bool isBottom = gl_FragCoord.y < bottom + cornerSize;
    
    float dist = initialDist;

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

    return dist;
}