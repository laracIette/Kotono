#version 430 core

struct Material2 {
    sampler2D albedo;
    sampler2D normal;
    sampler2D metalness;
    sampler2D roughness;
    sampler2D ambientOcclusion;
};

struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float     shininess;
};

struct DirLight {
    vec3 direction;

    vec4 ambient;
    vec4 diffuse;
    vec4 specular;
};
uniform DirLight dirLight;

struct PointLight {
    vec3 location;

    float constant;
    float linear;
    float quadratic;

    vec4 ambient;
    vec4 diffuse;
    vec4 specular;

    float power;
};

#define MAX_POINT_LIGHTS 100

uniform PointLight pointLights[MAX_POINT_LIGHTS];

uniform int numPointLights;

struct SpotLight{
    vec3 location;
    vec3 direction;
    float cutOff;
    float outerCutOff;

    vec4 ambient;
    vec4 diffuse;
    vec4 specular;

    float constant;
    float linear;
    float quadratic;

    float power;
};
#define MAX_SPOT_LIGHTS 1

uniform SpotLight spotLights[MAX_SPOT_LIGHTS];

uniform int numSpotLights;

uniform Material material;
uniform vec3 viewPos;

uniform vec4 color;

out vec4 FragColor;

in vec3 Normal;
in vec3 FragPos;
in vec2 TexCoords;

vec4 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir);
vec4 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir);
vec4 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir);
bool IsTextureValid(sampler2D texture);

void main()
{
    // Properties
    vec3 norm = normalize(Normal);
    vec3 viewDir = normalize(viewPos - FragPos);

    // Phase 1: Directional lighting
    vec4 result = CalcDirLight(dirLight, norm, viewDir);
    
    // Phase 2: Point lights
    for (int i = 0; i < numPointLights; i++)
    {
        result += CalcPointLight(pointLights[i], norm, FragPos, viewDir);
    }
    
    // Phase 3: Spot light
    for (int i = 0; i < numSpotLights; i++)
    {
        result += CalcSpotLight(spotLights[i], norm, FragPos, viewDir);
    }

    FragColor = result * color;
}

vec4 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir)
{
    vec3 lightDir = normalize(-light.direction);
    
    // Diffuse shading
    float diff = max(dot(normal, lightDir), 0.0);
    
    // Specular shading
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    
    // Combine results
    vec4 ambient  = light.ambient  * vec4(texture(material.diffuse, TexCoords));
    vec4 diffuse  = light.diffuse  * diff * vec4(texture(material.diffuse, TexCoords));
    vec4 specular = light.specular * spec * vec4(texture(material.specular, TexCoords));
    
    return (ambient + diffuse + specular);
}

vec4 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{
    vec3 lightDir = normalize(light.location - fragPos);
    
    // Diffuse shading
    float diff = max(dot(normal, lightDir), 0.0);
    
    // Specular shading
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    
    // Attenuation
    float distance    = length(light.location - fragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));
    
    // Combine results
    vec4 ambient  = light.ambient  * vec4(texture(material.diffuse, TexCoords));
    vec4 diffuse  = light.diffuse  * diff * vec4(texture(material.diffuse, TexCoords));
    vec4 specular = light.specular * spec * vec4(texture(material.specular, TexCoords));
    
    ambient  *= attenuation;
    diffuse  *= attenuation;
    specular *= attenuation;
    
    return (ambient + diffuse + specular) * light.power;
} 

vec4 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{

    // Diffuse shading
    vec3 lightDir = normalize(light.location - FragPos);
    float diff = max(dot(normal, lightDir), 0.0);

    // Specular shading
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

    // Attenuation
    float distance    = length(light.location - FragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));

    // Spotlight intensity
    float theta     = dot(lightDir, normalize(-light.direction));
    float epsilon   = light.cutOff - light.outerCutOff;
    float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);

    // Combine results
    vec4 ambient = light.ambient * vec4(texture(material.diffuse, TexCoords));
    vec4 diffuse = light.diffuse * diff * vec4(texture(material.diffuse, TexCoords));
    vec4 specular = light.specular * spec * vec4(texture(material.specular, TexCoords));
    
    ambient  *= attenuation;
    diffuse  *= attenuation * intensity;
    specular *= attenuation * intensity;
    
    return (ambient + diffuse + specular) * light.power;
}

bool IsTextureValid(sampler2D texture)
{    
    vec2 size = textureSize(texture, 0);

    return size.x != 0 && size.y != 0;
}