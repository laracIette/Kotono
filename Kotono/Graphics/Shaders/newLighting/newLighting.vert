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

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    FragPos = vec3(model * vec4(aPos, 1.0));
    Normal = mat3(transpose(inverse(model))) * aNormal;
    Tangent = mat3(model) * aTangent;
    TexCoords = aTexCoords;

    vec3 T = normalize(Tangent);
    vec3 N = normalize(Normal);
    float handedness = (dot(cross(N, T), Bitangent) < 0.0) ? -1.0 : 1.0;
    Bitangent = normalize(cross(N, T)) * handedness;

    gl_Position = projection * view * vec4(FragPos, 1.0);
}
