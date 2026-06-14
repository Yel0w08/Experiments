#version 330 core
out vec4 FragColor;
uniform float uMass;

void main() {
    vec3 color = mix(vec3(0.2, 0.5, 1.0), vec3(1.0, 0.5, 0.1), uMass);
    FragColor = vec4(color, 1.0);
}