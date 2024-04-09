using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float		damage = 0;
	private float		lifeSpan = 0;
	private float		t = 0;


    void Start()
    {
		if (this.transform.parent != null)
		{
			this.damage = this.transform.parent.GetComponent<TurretController>().basicDamage;
			this.lifeSpan = this.transform.parent.GetComponent<TurretController>().GetRange() / 10;
		}
		this.t = 0;
    }

	void Update()
	{
		if (this.t >= this.lifeSpan)
			GameObject.Destroy(this.gameObject);
		this.t += Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		EnnemyController	target = other.gameObject.GetComponent<EnnemyController>();

		if (target != null)
			target.TakeDamage(this.damage);
		if (other.gameObject.CompareTag("Ennemy"))
			GameObject.Destroy(this.gameObject);
		else if ((this.transform.position - other.transform.position).magnitude < 0.5)
			if (this.transform.parent != other.transform)
				GameObject.Destroy(this.gameObject);
	}
}
