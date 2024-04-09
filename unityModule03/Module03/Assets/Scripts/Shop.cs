using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
	public GameObject	fastTurret = null;
	public GameObject	mediumTurret = null;
	public GameObject	heavyTurret = null;
	
	private GameObject	current = null;
    
	void Start()
    {
    }

    void Update()
    {
    }

	public void BeginDrag()
	{
		Ray				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D	hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << 5);

		if (hit.collider != null)
		{
			switch (hit.collider.gameObject.name)
			{
				case "FastTurret":
					this.current = Object.Instantiate(this.fastTurret);
					this.current.GetComponent<TurretController>().SetActive(false);
					break ;
				case "MediumTurret":
					this.current = Object.Instantiate(this.mediumTurret);
					this.current.GetComponent<TurretController>().SetActive(false);
					break ;
				case "HeavyTurret":
					this.current = Object.Instantiate(this.heavyTurret);
					this.current.GetComponent<TurretController>().SetActive(false);
					break ;
				default :
					break ;
			}
		}
	}

	public void Drag()
	{
		Ray				ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (this.current != null)
			this.current.transform.position = new Vector2(ray.origin.x, ray.origin.y);
	}

	public void EndDrag()
	{
		Ray				ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D	hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << 5);

		if (this.current != null)
		{
			if (hit.collider != null && hit.collider.CompareTag("Slot"))
			{
				Slot	slot = hit.transform.GetComponent<Slot>();

				if (slot != null && slot.empty)
				{
					slot.empty = false;
					this.current.transform.position = hit.transform.position + new Vector3(-0.1f, 0.2f, 0);
					this.current.GetComponent<TurretController>().SetActive(true);
				}
				else
					Object.Destroy(this.current);
			}
			else
				Object.Destroy(this.current);
			this.current = null;
		}
	}
}
