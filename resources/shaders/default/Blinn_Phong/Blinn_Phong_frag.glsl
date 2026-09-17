#version 330 core
#define MAX_LIGHTS 32

in vec3 position;
in vec3 normal;

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D texture0;
uniform vec4 colDiffuse;

uniform vec3 viewPos;
uniform vec3 ambientColor;

struct Material{
    sampler2D roughness;
    float specular;
};

struct Light{
    vec3 position;
    vec3 color;
    float intensity;
};
uniform Material material;
uniform Light lights[MAX_LIGHTS];

out vec4 finalColor;

void main()
{
    vec4 texelColor = texture(texture0, fragTexCoord);
    vec3 color = (texelColor*colDiffuse*fragColor).xyz;

    vec3 result;
    for(int i = 0; i < lights.length; i++){
        vec3 lightColor = lights[i].color * lights[i].intensity;

        // ambient
        vec3 ambient = ambientColor * lightColor;
        // diffuse
        //TODO: make these work with the roughness map
        vec3 lightDir = normalize(lights[i].position - position);
        vec3 norm = normalize(normal);
        float diff = max(dot(lightDir, norm), 0.0);
        vec3 diffuse = diff * lightColor;
        // specular
        vec3 viewDir = normalize(viewPos - position);
        vec3 reflectDir = reflect(-lightDir, norm);
        vec3 halfwayDir = normalize(lightDir + viewDir);

        // float smoothness = 2/pow(material.roughness, 4) - 2;
        // float specularAdjustment = 1.0 - material.roughness;

        float spec = pow(max(dot(norm, halfwayDir), 0.0), 32.);
        vec3 specular = lightColor * (spec * material.specular);

        result += (ambient+diffuse+specular) * color;
    }

    finalColor = vec4(result, 1.0);
}