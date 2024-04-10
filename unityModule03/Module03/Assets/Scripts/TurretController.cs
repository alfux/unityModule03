using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
	public float		fireRate = 1;
	public float		basicDamage = 1;
	public float		bulletSpeed = 1;
	public Vector3		bulletSize = Vector3.one;
	public GameObject	bulletType = null;
	
	private EnnemyController	mainTarget = null;
	private float				t = 0;
	private float				range = 1;
	private bool				active = false;

    void Start()
    {
		this.t = 0;
		this.range = this.GetComponent<CircleCollider2D>().radius;
    }

    void Update()
    {
		if (this.active)
		{
			if (this.t > 1 / this.fireRate && this.mainTarget != null)
			{
				GameObject	bullet = Object.Instantiate(this.bulletType, this.transform);

				bullet.transform.localScale = this.bulletSize;
				bullet.GetComponent<Rigidbody2D>().AddForce((this.mainTarget.transform.position - this.transform.position) * this.bulletSpeed, ForceMode2D.Impulse);
				this.t = 0;
			}
			this.t += Time.deltaTime;
		}
    }

	void OnTriggerStay2D(Collider2D other)
	{
		EnnemyController	target = other.gameObject.GetComponent<EnnemyController>();

		if (target != null)
		{
			if (this.mainTarget == null)
				this.mainTarget = target;
			else
			{
				float	newDistance = (target.transform.position - this.transform.position).magnitude;
				float	preDistance = (this.mainTarget.transform.position - this.transform.position).magnitude;

				if (newDistance < preDistance)
					this.mainTarget = target;
			}
		}
	}

	public float GetRange()
	{
		return (this.range);
	}

	public void SetActive(bool val)
	{
		this.active = val;
	}
}
