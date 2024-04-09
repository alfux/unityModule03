using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject	ennemyType = null;
	public float		spawnRate = 1;

	private float	t = 0;

	private static bool		active = true;

	void Start()
	{
		this.t = 0;
	}

    void FixedUpdate()
    {
		if (this.t >= 1 / this.spawnRate)
		{
			this.t = 0;
			if (Spawner.active)
				Object.Instantiate(this.ennemyType, this.transform);
		}
		else
			this.t += Time.deltaTime;
    }

	public static void Activate()
	{
		Spawner.active = true;
	}

	public static void Deactivate()
	{
		Spawner.active = false;
	}

	public static bool State()
	{
		return (Spawner.active);
	}
}
