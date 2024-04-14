using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
	public float	healthPoints = 1;
	public float	moveSpeed = 1;
	public float	reward = 5;
	public float	baseScore = 1;
	public float	timeMultiplier = 1;
	public Jauge	energyRef = null;
	public Vector3	direction = Vector3.down;

    void FixedUpdate()
    {
		if (this.healthPoints <= 0 || !Spawner.State())
			Object.Destroy(this.gameObject);
		this.transform.Translate(this.moveSpeed * this.direction.normalized / 100);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Finish"))
			Object.Destroy(this.gameObject);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Map"))
			Object.Destroy(this.gameObject);
	}

	void OnDestroy()
	{
		if (healthPoints <= 0)
		{
			this.energyRef.AddJauge(this.reward);
			UIManager.AddScore(this.baseScore * this.timeMultiplier / Time.timeSinceLevelLoad);
			Spawner.SetDeathTime();
		}
	}

	public void TakeDamage(float val)
	{
		this.healthPoints -= val;
	}
}
