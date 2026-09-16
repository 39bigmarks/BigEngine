// Thank you Raylib :)
#version 330

in vec2 fragTexCoord;

out vec4 fragColor;

uniform sampler2D texture0;
uniform vec4 colDiffuse;

uniform float offsetX = 0.5;
uniform float offsetY = 0.5;
uniform float fac = 1;

const float PI = 3.1415926535;

void main()
{
    float aperture = 175.0;
    float apertureHalf = 0.5*aperture*(PI/180.0);
    float maxFactor = sin(apertureHalf) * fac;

    vec2 uv = vec2(0);
    vec2 xy = 2.0*fragTexCoord.xy - 1.0;
    float d = length(xy);

    if (d < (2.0 - maxFactor))
    {
        d = length(xy*maxFactor);
        float z = sqrt(1.0 - d*d);
        float r = atan(d, z)/PI;
        float phi = atan(xy.y, xy.x);

        uv.x = r*cos(phi) + offsetX;
        uv.y = r*sin(phi) + offsetY;

        fragColor = texture(texture0, uv);
    }
    else fragColor = vec4(0, 0, 0, 1);

}