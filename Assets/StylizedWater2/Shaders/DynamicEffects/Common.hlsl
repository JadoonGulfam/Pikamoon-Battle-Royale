#ifndef DYNAMIC_EFFECTS_COMMON_INCLUDED
#define DYNAMIC_EFFECTS_COMMON_INCLUDED

bool _WaterDynamicEffectsHighPrecision;
#define MAX_HEIGHT 32.0f

void OutputChannel(inout float a)
{
	if(_WaterDynamicEffectsHighPrecision == false)
	{
		a *= rcp(MAX_HEIGHT);
	}
}

float UnpackChannel(in float a)
{
	if(_WaterDynamicEffectsHighPrecision == false)
	{
		a *= MAX_HEIGHT;
	}

	return a;
}
#endif