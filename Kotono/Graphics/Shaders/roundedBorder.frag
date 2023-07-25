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
    float newLeft =   left + halfThick;
    float newRight =  right - halfThick;
    float newTop =    top - halfThick;
    float newBottom = bottom + halfThick;

    float newCornerSize = cornerSize * ((dest.x - newLeft) / (dest.x - left));
    //TODO: THIS AINT RIGHT maybe
    bool isLeft =   gl_FragCoord.x < newLeft + newCornerSize;
    bool isRight =  gl_FragCoord.x > newRight - newCornerSize;
    bool isTop =    gl_FragCoord.y > newTop - newCornerSize;
    bool isBottom = gl_FragCoord.y < newBottom + newCornerSize;
    
    float dist = INFINITY;

    if (isLeft && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newLeft + cornerSize, newTop - cornerSize));
        dist -= cornerSize;
    }
    else if (isRight && isTop)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newRight - cornerSize, newTop - cornerSize));
        dist -= cornerSize;
    }
    else if (isLeft && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newLeft + cornerSize, newBottom + cornerSize));
        dist -= cornerSize;
    }
    else if (isRight && isBottom)
    {
        dist = distance(vec2(gl_FragCoord), vec2(newRight - cornerSize, newBottom + cornerSize));
        dist -= cornerSize;
    }
    else if (isLeft)
    {
        dist = gl_FragCoord.x - newLeft;
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
        dist = gl_FragCoord.y - newBottom;
    }

    dist = abs(dist);
    dist -= halfThick;
    float ratio = clamp(dist / fallOff, 0.0, 1.0);
    
    vec4 result = color;
    result.a -= ratio;

    FragColor = result + vec4(.1);
}