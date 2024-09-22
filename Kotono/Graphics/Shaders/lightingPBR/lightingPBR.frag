#version 430 core

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;
in vec3 Tangent;
in vec3 Bitangent;
in mat3 TBN;

out vec4 FragColor;

struct Material {
    sampler2D albedo;
    sampler2D normal;
    sampler2D metallic;
    sampler2D roughness;
    sampler2D ambientOcclusion;
    sampler2D emissive;
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

uniform Material material;
uniform PointLight pointLights[100];
uniform int numPointLights;
uniform vec3 camLoc;
uniform vec4 baseColor;

const float PI = 3.14159265359;
  
float DistributionGGX(vec3 N, vec3 H, float roughness);
float GeometrySchlickGGX(float NdotV, float roughness);
float GeometrySmith(vec3 N, vec3 V, vec3 L, float roughness);
vec3 fresnelSchlick(float cosTheta, vec3 F0);
vec3 getNormalFromNormalMap();

void main()
{		 
    vec3 albedo = pow(texture(material.albedo, TexCoords).rgb, vec3(2.2));
    vec3 normal = getNormalFromNormalMap();
    float metallic = texture(material.metallic, TexCoords).r;
    float roughness = texture(material.roughness, TexCoords).r;
    float ao = texture(material.ambientOcclusion, TexCoords).r;

    vec3 N = normalize(normal);
    vec3 V = normalize(camLoc - FragPos);

    vec3 F0 = vec3(0.04); 
    F0 = mix(F0, albedo, metallic);
	           
    // reflectance equation
    vec3 Lo = vec3(0.0);
    for (int i = 0; i < numPointLights; ++i)
    {
        // calculate per-light radiance
        vec3 L = normalize(pointLights[i].location - FragPos);
        vec3 H = normalize(V + L);
        float distance    = length(pointLights[i].location - FragPos);
        float attenuation = 1.0 / (distance * distance);
        vec3 radiance     = pointLights[i].diffuse.rgb * attenuation;        
        
        // cook-torrance brdf
        float NDF = DistributionGGX(N, H, roughness);        
        float G   = GeometrySmith(N, V, L, roughness);      
        vec3 F    = fresnelSchlick(max(dot(H, V), 0.0), F0);       
        
        vec3 kS = F;
        vec3 kD = vec3(1.0) - kS;
        kD *= 1.0 - metallic;	  
        
        vec3 numerator    = NDF * G * F;
        float denominator = max(4.0 * max(dot(N, V), 0.0) * max(dot(N, L), 0.0), 0.0001);
        vec3 specular     = numerator / denominator;  
            
        // add to outgoing radiance Lo
        float NdotL = max(dot(N, L), 0.0);                
        Lo += (kD * albedo / PI + specular) * radiance * NdotL; 
    }   
  
    vec3 ambient = vec3(0.03) * albedo * ao;
    vec3 color = ambient + Lo;
	
    color = color / (color + vec3(1.0));
    color = pow(color, vec3(1.0 / 2.2));  

    FragColor = vec4(color, 1.0) * baseColor;
}  

vec3 fresnelSchlick(float cosTheta, vec3 F0)
{
    return F0 + (1.0 - F0) * pow(clamp(1.0 - cosTheta, 0.0, 1.0), 5.0);
}  

float DistributionGGX(vec3 N, vec3 H, float roughness)
{
    float a      = roughness * roughness;
    float a2     = a * a;
    float NdotH  = max(dot(N, H), 0.0);
    float NdotH2 = NdotH * NdotH;
	
    float num   = a2;
    float denom = (NdotH2 * (a2 - 1.0) + 1.0);
    denom = PI * denom * denom;
	
    return num / denom;
}

float GeometrySchlickGGX(float NdotV, float roughness)
{
    float r = (roughness + 1.0);
    float k = (r * r) / 8.0;

    float num   = NdotV;
    float denom = NdotV * (1.0 - k) + k;
	
    return num / denom;
}

float GeometrySmith(vec3 N, vec3 V, vec3 L, float roughness)
{
    float NdotV = max(dot(N, V), 0.0);
    float NdotL = max(dot(N, L), 0.0);
    float ggx2  = GeometrySchlickGGX(NdotV, roughness);
    float ggx1  = GeometrySchlickGGX(NdotL, roughness);
	
    return ggx1 * ggx2;
}

vec3 getNormalFromNormalMap()
{
    // Sample the normal from the normal map (stored in tangent space)
    vec3 normal = texture(material.normal, TexCoords).rgb;
    
    // Convert from [0, 1] range to [-1, 1] range
    normal = normal * 2.0 - 1.0;
    
    // Transform the normal from tangent space to world space using TBN matrix
    vec3 worldNormal = normalize(TBN * normal);

    return worldNormal;
}