BEFORE YOU START:
Step 1   Remove "shaders" folder at:
         \NatureManufacture Assets\L.V.E- Lava and Volcano Environment\Shaders 

Step 2
         Import RP support files: HD RP 14.0 Unity 2022.3.10 Lava and Volcano pack  it should work also for higher versions
		 

Step 3 - Setup Shadows and other render setups. Find File "HDRenderPipelineAsset" 
    - Change shadow atlas width and height to 2048 or 4096, Rather this first one.
	- Optionaly turn on "increase resolution of volumetrics (a bit expensive but I didn't notice big drop so..) 
	- Check if volumetric light is turned on.
	- Check if you got Deferred only in Lit Shader mode.
	- Check if contact shadows are turned on
	- Turn On SRP Batcher 

Step 4 Go to project settings and quality and set:
	- Set VSync to don't sync
	- LOD Bias to 2. We setup whole asset to this value which is default in LW and Standard.

Step 5 Find our demo scenes and open it, Lava Demo 3, 2, or 1 .

Step 6 - HIT PLAY!:)

Play with it give us feedback and lern about hd srp power.

