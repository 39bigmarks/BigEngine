#version 330 core

in vec3 position;
in vec3 normal;

in vec2 fragTexCoord;
in vec4 fragColor;

uniform sampler2D texture0;
uniform vec4 colDiffuse;

uniform vec3 viewPos;
uniform float ambientStrength;
uniform vec4 lightColor; // r, g, b, strength
uniform vec3 lightPos;

out vec4 finalColor;

void main()
{
    vec4 texelColor = texture(texture0, fragTexCoord);
    
    // ambient
    vec3 ambient = ambientStrength * (lightColor.xyz * lightColor.w);

    // diffuse
    vec3 norm = normalize(normal);
    vec3 lightDir = normalize(lightPos - position);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * (lightColor.xyz * lightColor.w);

    // specular
    float specularStrength = 0.5;
    vec3 viewDir = normalize(viewPos - position);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32); // shininess expo
    vec3 specular = specularStrength * spec * (lightColor.xyz * lightColor.w);

    vec3 result = (ambient + diffuse + specular) * (texelColor*colDiffuse*fragColor).xyz;

    finalColor = vec4(result, 1.0);
}