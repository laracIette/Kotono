#version 430 core

uniform vec4 viewportDest;

uniform vec4 color;
uniform vec4 dest;
uniform float fallOff;
uniform float cornerSize;

out vec4 FragColor;

vec4 ToScreenSpace(vec4 dest);

void main()
{
    vec4 dest = ToScreenSpace(dest);

    float left =   dest.x - dest.z / 2;
    float right =  dest.x + dest.z / 2;
    float top =    dest.y + dest.w / 2;
    float bottom = dest.y - dest.w / 2;

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

vec4 ToScreenSpace(vec4 v)
{
    return vec4(
        viewportDest.x + (v.x + 1) / 2 * viewportDest.z,
        (viewportDest.w - viewportDest.y) + (v.y + 1) / 2 * viewportDest.w,
        v.z * viewportDest.z,
        v.w * viewportDest.w
    );
}