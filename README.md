![Screenshot 2024-12-09 143048](https://github.com/user-attachments/assets/5cb35b4c-40f3-4b4f-9169-5d9a362b1e4d)

Outlining: Used to highlight the player car. The texture was rendered first. Then, in a pass, the texture and mesh was overlaid with a offset solid black shape. Cull Front makes it so that the part fo the outline covering the mesh is not rendered. The offset variable in the vertex shader makes that the outline is always facing the viewer camera(by normalizing the normal vectors of the mesh) and o.color allows the dev to change the outline color as necessary. Again, to restate, this was done to make the player car more visible.

Glass Shader: Used for the MK Cubes that provide the drops. First, the Transparent render tag was added to make it see through as glass should be. Then the normal map and texture was set up in the properties. In the fragment shader, i then applied bump mapping, by transforming the normals outwards from the mesh. This lets me simulate the distortion that occurs from looking through glass, aka refraction. The bump mapping is possible due to manipulating and sampling the textures beforehand using _GrabTexture, allowing for transforming the vertexes in the vertex shader.

Scrolling Texture: Used to simulate the space background in Rainbow Road. I used the default particle for the miniature stars, though they are a bit numerous as I scaled the plane holding the shader, as well as the texture scale as well(to abt 50 on both x and y). As covered in class, i used the _Time property built in unity and used *= to add to itself in order to simulate the scrolling speed on both x and y, and then to get the texture to move, i inputted the uv data, and since uv's are kept as 2D data, added the float 2 made up of the scrolling vector to provide motion. 

Color correction: Done using a UI image and tweaking Alpha values, just for fun.

Vertex coloring: Used for the track itself. Because its Rainbow Road, I used a formula in the vertex shader to determine color based on world space. These exact numbers allow R,G, and B values to be on the screen all at once to a degree. UnityobjectToCLip pos also ensures that vertex data is shown on screen.
The formula used:  o.color.r = (v.vertex.x + 0.2);
                   o.color.g = (v.vertex.y + 0.3) / 2;
                   o.color.b = (v.vertex.z + 0.4) / 5;
                   This was followed up with a second shader pass to outline the Track itself using the outline shader from before. This is to simulate the grid texture of the road combined with the black bars.
                  
Diagram and Showcase:
![Screenshot 2024-12-09 142827](https://github.com/user-attachments/assets/72c4a08d-a6c1-4476-8fb9-033d373ba5a9)
![Screenshot 2024-12-09 144023](https://github.com/user-attachments/assets/83263f65-2084-4fbd-ab45-88c62e30fe98)


Also left click to lock camera onto player.
