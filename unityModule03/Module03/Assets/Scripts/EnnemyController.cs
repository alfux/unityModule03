using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
	public float	healthPoints = 1;
	public float	moveSpeed = 1;
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

	public void TakeDamage(float val)
	{
		this.healthPoints -= val;
	}
}
