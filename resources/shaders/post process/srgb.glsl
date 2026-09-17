#version 330

in vec2 fragTexCoord;

uniform sampler2D texture0;
uniform vec4 colDiffuse;

out vec4 finalColor;

vec3 linearToSrgb(vec3 c) {
    return mix(1.055 * pow(c, vec3(1.0/2.4)) - 0.055, c * 12.92, step(c, vec3(0.0031308)));
}

void main() {
    vec3 color = texture(texture0, fragTexCoord).rgb;
    finalColor = vec4(linearToSrgb(color), 1.0);
}