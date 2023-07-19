#version 430 core

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
};
#define MAX_POINT_LIGHTS 100

uniform PointLight pointLights[MAX_POINT_LIGHTS];

uniform int numPointLights;

struct SpotLight{
    vec3  location;
    vec3  direction;
    float cutOff;
    float outerCutOff;

    vec4 ambient;
    vec4 diffuse;
    vec4 specular;

    float constant;
    float linear;
    float quadratic;
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

void main()
{
    //properties
    vec3 norm = normalize(Normal);
    vec3 viewDir = normalize(viewPos - FragPos);

    //phase 1: Directional lighting
    vec4 result = CalcDirLight(dirLight, norm, viewDir);
    
    //phase 2: Point lights
    for (int i = 0; i < numPointLights; i++)
    {
        result += CalcPointLight(pointLights[i], norm, FragPos, viewDir);
    }
    
    //phase 3: Spot light
    for (int i = 0; i < numSpotLights; i++)
    {
        result += CalcSpotLight(spotLights[i], norm, FragPos, viewDir);
    }

    FragColor = result * color;
}

vec4 CalcDirLight(DirLight light, vec3 normal, vec3 viewDir)
{
    vec3 lightDir = normalize(-light.direction);
    
    //diffuse shading
    float diff = max(dot(normal, lightDir), 0.0);
    
    //specular shading
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    
    //combine results
    vec4 ambient  = light.ambient  * vec4(texture(material.diffuse, TexCoords));
    vec4 diffuse  = light.diffuse  * diff * vec4(texture(material.diffuse, TexCoords));
    vec4 specular = light.specular * spec * vec4(texture(material.specular, TexCoords));
    
    return (ambient + diffuse + specular);
}

vec4 CalcPointLight(PointLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{
    vec3 lightDir = normalize(light.location - fragPos);
    
    //diffuse shading
    float diff = max(dot(normal, lightDir), 0.0);
    
    //specular shading
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    
    //attenuation
    float distance    = length(light.location - fragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * (distance * distance));
    
    //combine results
    vec4 ambient  = light.ambient  * vec4(texture(material.diffuse, TexCoords));
    vec4 diffuse  = light.diffuse  * diff * vec4(texture(material.diffuse, TexCoords));
    vec4 specular = light.specular * spec * vec4(texture(material.specular, TexCoords));
    
    ambient  *= attenuation;
    diffuse  *= attenuation;
    specular *= attenuation;
    
    return (ambient + diffuse + specular);
} 
vec4 CalcSpotLight(SpotLight light, vec3 normal, vec3 fragPos, vec3 viewDir)
{

    //diffuse shading
    vec3 lightDir = normalize(light.location - FragPos);
    float diff = max(dot(normal, lightDir), 0.0);

    //specular shading
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);

    //attenuation
    float distance    = length(light.location - FragPos);
    float attenuation = 1.0 / (light.constant + light.linear * distance +
    light.quadratic * (distance * distance));

    //spotlight intensity
    float theta     = dot(lightDir, normalize(-light.direction));
    float epsilon   = light.cutOff - light.outerCutOff;
    float intensity = clamp((theta - light.outerCutOff) / epsilon, 0.0, 1.0);

    //combine results
    vec4 ambient = light.ambient * vec4(texture(material.diffuse, TexCoords));
    vec4 diffuse = light.diffuse * diff * vec4(texture(material.diffuse, TexCoords));
    vec4 specular = light.specular * spec * vec4(texture(material.specular, TexCoords));
    
    ambient  *= attenuation;
    diffuse  *= attenuation * intensity;
    specular *= attenuation * intensity;
    
    return (ambient + diffuse + specular);
}