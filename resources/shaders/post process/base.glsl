#version 330

// Input from the vertex shader
in vec2 fragTexCoord;

// Output color for the screen
out vec4 finalColor;

uniform sampler2D texture0;

void main()
{
    vec3 color = texture(texture0, fragTexCoord).rgb;
    finalColor = vec4(color, 1.0);
}