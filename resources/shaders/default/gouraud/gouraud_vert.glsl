#version 330

in vec3 vertexPosition;
in vec2 vertexTexCoord;
in vec3 vertexNormal;
in vec4 vertexColor;

uniform mat4 matModel;
uniform mat4 mvp;

uniform vec3 viewPos;
uniform float ambientStrength;
uniform vec4 lightColor; // r, g, b, strength
uniform vec3 lightPos;

out vec2 fragTexCoord;
out vec3 light;
out vec4 fragColor;

void main()
{
    fragTexCoord = vertexTexCoord;
    fragColor = vertexColor;

    gl_Position = mvp*vec4(vertexPosition, 1.0);

    vec3 position = vec3(matModel * vec4(vertexPosition, 1.0));
    vec3 normal = mat3(transpose(inverse(matModel))) * vertexNormal;

    // ambient
    vec3 ambient = ambientStrength * (lightColor.xyz * lightColor.w);

    // diffuse
    vec3 norm = normalize(normal);
    vec3 lightDir = normalize(lightPos - position);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * (lightColor.xyz * lightColor.w);

    // specular
    float specularStrength = 1.0;
    vec3 viewDir = normalize(viewPos - position);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32); // 32 is shininess exponent
    vec3 specular = specularStrength * spec * (lightColor.xyz * lightColor.w);
    
    light = ambient + diffuse + specular;
}