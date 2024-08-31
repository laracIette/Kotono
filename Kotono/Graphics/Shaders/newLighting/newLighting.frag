#version 430 core

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;
in vec3 Tangent;
in vec3 Bitangent;

out vec4 FragColor;

struct MaterialTexture 
{
    sampler2D handle;
    float strength;
};

struct Material {
    MaterialTexture albedo;
    MaterialTexture normal;
    MaterialTexture metallic;
    MaterialTexture roughness;
    MaterialTexture ambientOcclusion;
    MaterialTexture emissive;
};

uniform Material material;

struct DirectionalLight {
    vec3 direction;

    //vec4 ambient;
    vec4 diffuse;
    //vec4 specular;

    float intensity;
};

struct PointLight {
    vec3 location;

    vec4 ambient;
    vec4 diffuse;
    vec4 specular;

    float constant;
    float linear;
    float quadratic;

    float intensity;
};

uniform PointLight pointLights[100];
uniform int numPointLights;

uniform DirectionalLight dirLight;

uniform vec3 viewPos;

uniform vec4 baseColor;

bool isTextureValid(sampler2D texture) 
{
    vec2 size = textureSize(texture, 0);
    return size.x > 0 && size.y > 0;
}

vec3 CalculateNormal(vec2 texCoords)
{
    vec3 tangentNormal = texture(material.normal.handle, texCoords).xyz * material.normal.strength * 2.0 - 1.0;

    vec3 Q1 = dFdx(FragPos);
    vec3 Q2 = dFdy(FragPos);
    vec2 st1 = dFdx(TexCoords);
    vec2 st2 = dFdy(TexCoords);

    vec3 N = normalize(Normal);
    vec3 T = normalize(Tangent - dot(Tangent, N) * N);
    vec3 B = cross(N, T);

    mat3 TBN = mat3(T, B, N);
    return normalize(TBN * tangentNormal);
}

vec3 FresnelSchlick(float cosTheta, vec3 F0)
{
    return F0 + (1.0 - F0) * pow(1.0 - cosTheta, 5.0);
}

float DistributionGGX(vec3 N, vec3 H, float roughness)
{
    float a = roughness * roughness;
    float a2 = a * a;
    float NdotH = max(dot(N, H), 0.0);
    float NdotH2 = NdotH * NdotH;

    float num = a2;
    float denom = (NdotH2 * (a2 - 1.0) + 1.0);
    denom = 3.14159265359 * denom * denom;

    return num / denom;
}

float GeometrySchlickGGX(float NdotV, float roughness)
{
    float r = (roughness + 1.0);
    float k = (r * r) / 8.0;

    float num = NdotV;
    float denom = NdotV * (1.0 - k) + k;

    return num / denom;
}

float GeometrySmith(vec3 N, vec3 V, vec3 L, float roughness)
{
    float NdotV = max(dot(N, V), 0.0);
    float NdotL = max(dot(N, L), 0.0);
    float ggx2 = GeometrySchlickGGX(NdotV, roughness);
    float ggx1 = GeometrySchlickGGX(NdotL, roughness);

    return ggx1 * ggx2;
}

vec3 CalculateLighting(DirectionalLight dirLight, vec3 N, vec3 V, vec3 albedo, float roughness, float metallic)
{
    vec3 L = normalize(-dirLight.direction);
    vec3 H = normalize(V + L);

    vec3 F0 = mix(vec3(0.04), albedo, metallic);
    vec3 radiance = dirLight.diffuse.rgb * dirLight.intensity;

    float NDF = DistributionGGX(N, H, roughness);
    float G = GeometrySmith(N, V, L, roughness);
    vec3 F = FresnelSchlick(max(dot(H, V), 0.0), F0);

    vec3 numerator = NDF * G * F;
    float denominator = 4.0 * max(dot(N, V), 0.0) * max(dot(N, L), 0.0);
    vec3 specular = numerator / max(denominator, 0.001);

    vec3 kS = F;
    vec3 kD = vec3(1.0) - kS;
    kD *= 1.0 - metallic;

    float NdotL = max(dot(N, L), 0.0);
    vec3 diffuse = kD * albedo / 3.14159265359;

    return (diffuse + specular) * radiance * NdotL;
}

vec3 CalculateLighting(PointLight pointLight, vec3 N, vec3 V, vec3 FragPos, vec3 albedo, float roughness, float metallic)
{
    vec3 L = normalize(pointLight.location - FragPos);
    vec3 H = normalize(V + L);

    vec3 F0 = mix(vec3(0.04), albedo, metallic);
    vec3 radiance = pointLight.diffuse.rgb * pointLight.intensity;

    float distance = length(pointLight.location - FragPos);
    float attenuation = 1.0 / (pointLight.constant + pointLight.linear * distance + pointLight.quadratic * (distance * distance));

    float NDF = DistributionGGX(N, H, roughness);
    float G = GeometrySmith(N, V, L, roughness);
    vec3 F = FresnelSchlick(max(dot(H, V), 0.0), F0);

    vec3 numerator = NDF * G * F;
    float denominator = 4.0 * max(dot(N, V), 0.0) * max(dot(N, L), 0.0);
    vec3 specular = numerator / max(denominator, 0.001);

    vec3 kS = F;
    vec3 kD = vec3(1.0) - kS;
    kD *= 1.0 - metallic;

    float NdotL = max(dot(N, L), 0.0);
    vec3 diffuse = kD * albedo / 3.14159265359;

    vec3 ambient = pointLight.ambient.rgb;
    vec3 diffuseColor = pointLight.diffuse.rgb * diffuse * NdotL;
    vec3 specularColor = pointLight.specular.rgb * specular;
    vec3 color = ambient + attenuation * (diffuseColor + specularColor);

    return color;
}

void main()
{
    vec3 albedo = baseColor.rgb;
    if (isTextureValid(material.albedo.handle)) 
    {
        albedo *= texture(material.albedo.handle, TexCoords).rgb * material.albedo.strength;
    }

    float metallic = 0.0;
    if (isTextureValid(material.metallic.handle)) 
    {
        metallic = texture(material.metallic.handle, TexCoords).r * material.metallic.strength;
    }

    float roughness = 1.0; // Default roughness to avoid shiny surfaces when no texture is provided
    if (isTextureValid(material.roughness.handle)) 
    {
        roughness = texture(material.roughness.handle, TexCoords).r * material.roughness.strength;
    }

    float ambientOcclusion = 1.0; // Default AO to 1 for no ambient occlusion effect if texture is missing
    if (isTextureValid(material.ambientOcclusion.handle)) 
    {
        ambientOcclusion = texture(material.ambientOcclusion.handle, TexCoords).r * material.ambientOcclusion.strength;
    }

    vec3 N = CalculateNormal(TexCoords);
    vec3 V = normalize(viewPos - FragPos);

    vec3 Lo = vec3(0.0);

    // Directional light
    Lo += CalculateLighting(dirLight, N, V, albedo, roughness, metallic);

    // Point lights
    for (int i = 0; i < numPointLights; ++i) 
    {
        Lo += CalculateLighting(pointLights[i], N, V, FragPos, albedo, roughness, metallic);
    }

    vec3 ambient = vec3(0.03) * albedo * ambientOcclusion;

    vec3 emissive = vec3(0.0);
    if (isTextureValid(material.emissive.handle)) 
    {
        emissive = texture(material.emissive.handle, TexCoords).rgb * material.emissive.strength;
    }

    vec3 color = ambient + Lo + emissive;

    FragColor = vec4(color, baseColor.a);
}
