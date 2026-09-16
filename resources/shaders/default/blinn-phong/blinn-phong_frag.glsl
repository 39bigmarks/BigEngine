#version 330 core
#define MAX_LIGHTS 8

in vec3 position;
in vec3 normal;

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D texture0;
uniform vec4 colDiffuse;

uniform vec3 viewPos;
uniform vec4 ambientColor; // r, g, b, strength

out vec4 finalColor;

struct Material{
    vec3 specular;
    float shininess;
};
struct Light{
    vec3 color;
    vec3 position;
    float strength;
};

uniform Material material;
uniform Light lights[MAX_LIGHTS];

void main()
{
    vec4 texelColor = texture(texture0, fragTexCoord);
    vec3 result;

    for(int i = 0; i < lights.length; i++){
        vec3 lightColor = lights[i].color * lights[i].strength;

        // ambient
        vec3 ambient = lightColor * (ambientColor.xyz * ambientColor.w);

        // diffuse
        vec3 norm = normalize(normal);
        vec3 lightDir = normalize(lights[i].position - position);
        float diff = max(dot(norm, lightDir), 0.0);
        vec3 diffuse = diff * lightColor;

        // specular
        float specularStrength = 0.5;
        vec3 viewDir = normalize(viewPos - position);
        vec3 halfwayDir = normalize(lightDir + viewDir);  
        float spec = pow(max(dot(normal, halfwayDir), 0.0), material.shininess);
        vec3 specular = lightColor * (spec * material.specular);

        result += (ambient + diffuse + specular) * (texelColor*colDiffuse*fragColor).xyz;
    }

    finalColor = vec4(result, 1.0);
}