using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
	public Jauge	health = null;
	public float	healthPoints = 5;

    void Start()
    {
		Debug.Log("The base has " + this.healthPoints + "HPs.");
    }

    void Update()
    {
        if (Mathf.Round(this.health.PreciseAmount()) <= 0)
		{
			Debug.Log("Game Over");
			Spawner.Deactivate();
			GameObject.Destroy(this.gameObject);
			UIManager.GameOver();
		}
    }
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Ennemy"))
		{
			this.healthPoints -= 1;
			this.health.AddJauge(-20);
			Debug.Log(
				"Base took damage: "
				+ (this.healthPoints + 1)
				+ " - 1 = "
				+ this.healthPoints
			);
		}
	}
}
