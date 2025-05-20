BEFORE YOU START:
- you need Unity 2022.3+

Step 1 - You can improve FPS amount by 30% if you change rendering path from forward to deferred at rendering setting. 
Find File "UniversalRenderer" and change Rendering path from forward to deferred. Forward render is ok too but it's slower for big open scenes.

Step 2   Remove "shaders" folder at:
         \NatureManufacture Assets\L.V.E- Lava and Volcano Environment\Shaders 
Step 3
         Import RP support files: URP 14.0 Unity 2022.3 Lava and Volcano pack it should work also for higher versions

Step 4 - Setup Shadows and other render setups. Find File "URP-HighQuality" 
    - Change shadow distance to 150
	- Turn on "SRP Bacther" it will improve fps by 200% 
	- Turn on "Opaque Texture" this will distortion at particles.
	- Optionaly use 1k or 2k shadow resolution. We used 2k.
	- Turn on HDR if its turned off

Step 5 Go to project settings: 
    - Player and set:  Color Space to Linear
    - Quality settings: Go to quality settings and: 
	     * use ultra level 
	     * turn turn off vsync
		 * lod bias should be = 2 and 1 for low end devices.
                        

Step 6 Find our demo scenes and open it, Lava Demo 3, 2, or 1.

Step 7 - HIT PLAY!:)

	About scene construction:
		- There is post process profile: Manage post process by scene post process object.

Play with it give us feedback and lern about URP power.

