﻿using UnityEngine;
using System.Collections;

public class SphericalMovement : SwitchableBehaviour<Player>
{
	Vector3 sp_position; // uses spherical coordinates: radius, azimuth angle phi, and polar angle gamma/inclination. Angles in radians
	Vector3 sp_velocity; // spherical coordinates
	Vector3 sp_acceleration; // spherical coordinates
	Vector3 sp_origin = Vector3.zero;

	Vector3 sp_forward = new Vector3(0, Mathf.Acos(1), Mathf.Atan(0));
	Vector3 sp_up = new Vector3(0, Mathf.Acos(0), Mathf.Atan(1));
	Vector3 sp_right = new Vector3(0, Mathf.Acos(0), Mathf.Atan(0));

	[SerializeField]
	float mass = 1;
	float gravity = 9.81f;

	[SerializeField]
	float movementForce = 10;

	public override void FSM_FixedUpdate(Player owner)
	{
		Vector3 sp_gravity = ToSpherical((sp_origin - transform.position) * gravity) / mass;
		sp_velocity += sp_gravity * Time.deltaTime;
		sp_position += sp_velocity * Time.deltaTime;
		transform.position = ToCartesian(sp_position);
	}

	Vector3 ToSpherical(Vector3 cartesian)
	{
		// avoid division by zero
		if (cartesian.x == 0)
			cartesian.x = 0.0001f;

		return new Vector3(
			cartesian.magnitude,
			Mathf.Acos(cartesian.z / cartesian.magnitude),
			Mathf.Atan2(cartesian.y, cartesian.x)
			);
	}

	Vector3 ToCartesian(Vector3 spherical)
	{
		return new Vector3(
			spherical.x * Mathf.Sin(spherical.y) * Mathf.Cos(spherical.z),
			spherical.x * Mathf.Sin(spherical.y) * Mathf.Sin(spherical.z),
			spherical.x * Mathf.Cos(spherical.y)
			);
	}
}
