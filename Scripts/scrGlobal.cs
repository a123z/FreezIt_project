using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class scrGlobal {
	public static float motionForce = 20f;
	public static float ultraMotionForce = 30f;
	public static float ultraForceTime = 15f;
	public static float maxSpeedSqr = 25f*25f;

	public static float freezeTime = 3f;

	public static float freezerRadius = 10f;
	public static float freezerRadiusSqr = freezerRadius * freezerRadius;
	public static float ultraFreezerRadius = 15f;

	public static float freezerTime = 0.7f;
	public static float ultraFreezerRadiusTime = 2f;
	
	public static float arenaHalfSizeX = 40f;
	public static float arenaHalfSizeZ = 40f;

	public static float radarTimeRepeat = 3f; //3 sec for repeat radar scan
	public static float radarRadius = 20f;

    public static Vector3 CameraOffset = new Vector3(0, 15f,-25f);

	public static string txtScoreGONamePrefix = "txtScore";
}
