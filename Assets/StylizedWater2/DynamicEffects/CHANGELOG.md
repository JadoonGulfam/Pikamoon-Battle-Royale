1.1.0
Minimum required version of Stylized Water 2 is now v1.6.0

Added:
- Option on render feature to disable high precision rendering. Uses half the memory, but induces banding artefacts.
- Material toggle to remap displacement to allow negative values.

Changed;
- Overall improvement to effects (all prefabs have been adjusted, custom effects may now need to have their Displacement strength toned down).
- Improved rendering and calculations of normals. Now uses half the amount of VRAM
- Particle effects now use a Quad mesh, instead of a Horizontal Billboard. This ensures their scale stays consistent.

Fixed:
- Edges on trails (corner verts)

1.0.1

Added:
- Demo scene, UI sliders to control the render range and resolution

Fixed:
- DemoController script throwing an error on some platforms (likely due to localization).