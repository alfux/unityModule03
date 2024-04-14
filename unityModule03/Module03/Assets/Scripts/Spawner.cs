using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject	ennemyType = null;
	public Jauge		energyRef = null;
	public float		spawnRate = 1;
	public float		ennemyHP = 3;
	public float		ennemyMS = 0.5f;
	public float		ennemyEnergyReward = 5;
	public float		ennemyBaseScore = 1;
	public float		ennemyTimeMultiplier = 60;
	public int			maxEnnemies = 100;

	private float	t = 0;
	private int		ennemyCounter = 0;

	private static bool		active = true;
	private static float	lastDeath = 0;
	private static float	deathTime = 0;
	private static int		ennemyDeaths = 0;

	void Start()
	{
		this.t = 0;
		this.ennemyCounter = 0;
		Spawner.active = true;
		Spawner.lastDeath = 0;
		Spawner.deathTime = 1 / this.spawnRate;
		Spawner.ennemyDeaths = 0;
		if (this.ennemyCounter < this.maxEnnemies)
			this.CreateEnnemy();
	}

    void FixedUpdate()
    {
		if (this.t >= 1 / this.spawnRate)
		{
			this.t = 0;
			if (Spawner.active && this.ennemyCounter < this.maxEnnemies)
				this.CreateEnnemy();
		}
		else
			this.t += Time.deltaTime;
		if (Spawner.deathTime < 1 / this.spawnRate)
			this.spawnRate = 1 / Spawner.deathTime;
		if (Spawner.ennemyDeaths == this.maxEnnemies)
			UIManager.GameOver();
    }

	void CreateEnnemy()
	{
		EnnemyController	ennemy = Object.Instantiate(this.ennemyType, this.transform).GetComponent<EnnemyController>();

		ennemy.healthPoints = this.ennemyHP;
		ennemy.moveSpeed = this.ennemyMS;
		ennemy.energyRef = this.energyRef;
		ennemy.reward = this.ennemyEnergyReward;
		ennemy.baseScore = this.ennemyBaseScore;
		ennemy.timeMultiplier = this.ennemyTimeMultiplier;
		this.ennemyCounter += 1;
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

	public static void SetDeathTime()
	{
		float	time = Time.timeSinceLevelLoad;

		Spawner.deathTime = time - Spawner.lastDeath;
		Spawner.lastDeath = time;
		Spawner.ennemyDeaths += 1;
	}
}
