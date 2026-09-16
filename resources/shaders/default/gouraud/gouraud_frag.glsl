#version 330 core

in vec2 fragTexCoord;
in vec4 fragColor;
in vec3 light;

uniform sampler2D texture0;
uniform vec4 colDiffuse;

out vec4 finalColor;

void main()
{
    vec4 texelColor = texture(texture0, fragTexCoord);
    
    finalColor = vec4(light * (texelColor*colDiffuse*fragColor).xyz, 1.0);
}