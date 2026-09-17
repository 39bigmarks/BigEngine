#version 330

in vec2 fragTexCoord;

out vec4 fragColor;

uniform sampler2D texture0;
uniform vec4 colDiffuse;
uniform float time;

// NOTE: Add your custom variables here

const float PI = 3.1415926535;
const int iResolutionX = 1280;
const int iResolutionY = 720;

void main()
{
    vec2 uv = fragTexCoord;

    // Scanline settings
    float blendFactor = 0.1; // Use lower values for subtle scanline effect.
    float scanlineHeight = 4.0;
    float scanlineIntensity = 0.25;
    float scrollSpeed = 16.0; // Use +/- values for up/down movement.
    vec3 color = vec3(1.0, 1.0, 1.0);
    
    // Grain settings
    float grainIntensity = 2.0;
    vec2 grainSeed = vec2(12.9898, 78.233) + time * 0.1; // Varying the seed over time
    
    // Glitch settings
    // Use higher values in this section for extreme effect.
    float glitchProbability = 0.4;
    float glitchIntensityX = 0.001; // Intensity of the horizontal jitter
    float glitchIntensityY = .003; // Intensity of the vertical jitter
    
    // Vertical Band settings
    float bandSpeed = 0.2;
    float bandHeight = 0.01;
    float bandIntensity = 0.2;
    float bandChoppiness = 0.2;
    float staticAmount = 0.02;
    float warpFactor = .005;
    float chromaAmount = .2;
    
    // Moving VHS effect
    float scanline = sin((uv.y * iResolutionY - time * scrollSpeed) * (1.0/scanlineHeight));
    vec3 vhsColor = color * scanline * scanlineIntensity;

    // Grain effect
    float grain = fract(sin(dot(fragTexCoord * uv, grainSeed)) * 43758.5453);
    vhsColor += grain * grainIntensity;

    // Glitch Effect
    if (fract(time) < glitchProbability) {
        float glitchOffsetX = (fract(sin(time * 12.9898) * 43758.5453) - 0.5) * glitchIntensityX;
        float glitchOffsetY = (fract(cos(time * 78.233) * 43758.5453) - 0.5) * glitchIntensityY;
        uv += vec2(glitchOffsetX, glitchOffsetY);
    }

    // VHS Band
    float bandPos = fract(time * bandSpeed);
    float bandNoise = fract(sin(dot(uv * vec2(iResolutionX, iResolutionY), vec2(12.9898, 78.233))) * 43758.5453);

    if (abs(uv.y - bandPos) < bandHeight) {
        // Add static with choppiness
        float randomStatic = bandNoise * bandChoppiness;
        vhsColor += vec3(randomStatic) * staticAmount;

        // Add warp effect with choppiness
        uv.x += sin(uv.y * iResolutionY * 10.0 + randomStatic) * warpFactor;

        // Chromatic aberration with choppiness
        vec3 chromaColor = vec3(
            texture(texture0, uv + vec2(chromaAmount * randomStatic, 0.0)).r,
            texture(texture0, uv).g,
            texture(texture0, uv - vec2(chromaAmount * randomStatic, 0.0)).b
        );

        // Mix chromatic aberration with reduced intensity
        float adjustedIntensity = bandIntensity * (1.0 - randomStatic);
        vhsColor = mix(vhsColor, chromaColor, adjustedIntensity);
    }

    // Original color before applying scanlines (but after the glitch, noise, and vertical band)
    // If you have an input texture, fetch the color from the texture here
    vec3 originalColor = texture(texture0, uv).rgb;

    // Blend the original color with the VHS effect
    vec3 finalColor = mix(originalColor, vhsColor, blendFactor);

    fragColor = vec4(finalColor, 1.0);
}