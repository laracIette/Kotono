#version 430 core

layout(location = 0) in vec3 aPos;
layout(location = 1) in vec3 aNormal;
layout(location = 2) in vec3 aTangent;
layout(location = 3) in vec2 aTexCoords;

out vec3 FragPos;
out vec3 Normal;
out vec2 TexCoords;
out vec3 Tangent;
out vec3 Bitangent;
out mat3 TBN;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = vec4(aPos, 1.0) * model * view * projection;
    FragPos = vec3(vec4(aPos, 1.0) * model);
    Normal = normalize(mat3(transpose(inverse(model))) * aNormal);
    Tangent = normalize(mat3(model) * aTangent);

    vec3 T = normalize(Tangent);
    vec3 N = normalize(Normal);
    vec3 B = normalize(cross(N, T));
    TBN = mat3(T, B, N);

    float handedness = (dot(cross(N, T), B) < 0.0) ? -1.0 : 1.0;
    Bitangent = handedness * B;

    TexCoords = aTexCoords;
}
