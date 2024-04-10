using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	public GameObject	fastTurret = null;
	public GameObject	mediumTurret = null;
	public GameObject	heavyTurret = null;
	public Jauge		energy = null;
	public Button		fastButton = null;
	public Button		mediumButton = null;
	public Button		heavyButton = null;

	private GameObject	current = null;
	private float		currentPrice = 0;
    
	void Start()
    {
    }

    void FixedUpdate()
    {
		if (this.energy.Amount() < 10)
		{
			this.fastButton.interactable = false;
			this.mediumButton.interactable = false;
			this.heavyButton.interactable = false;
		}
		else if (this.energy.Amount() < 20)
		{
			this.fastButton.interactable = true;
			this.mediumButton.interactable = false;
			this.heavyButton.interactable = false;
		}
		else if (this.energy.Amount() < 30)
		{
			this.fastButton.interactable = true;
			this.mediumButton.interactable = true;
			this.heavyButton.interactable = false;
		}
		else
		{
			this.fastButton.interactable = true;
			this.mediumButton.interactable = true;
			this.heavyButton.interactable = true;
		}
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
					if (this.fastButton.interactable)
					{
						this.currentPrice = 10;
						this.current = Object.Instantiate(this.fastTurret);
						this.current.GetComponent<TurretController>().SetActive(false);
					}
					break ;
				case "MediumTurret":
					if (this.mediumButton.interactable)
					{
						this.currentPrice = 20;
						this.current = Object.Instantiate(this.mediumTurret);
						this.current.GetComponent<TurretController>().SetActive(false);
					}
					break ;
				case "HeavyTurret":
					if (this.heavyButton.interactable)
					{
						this.currentPrice = 30;
						this.current = Object.Instantiate(this.heavyTurret);
						this.current.GetComponent<TurretController>().SetActive(false);
					}
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
					this.energy.AddJauge(-this.currentPrice);
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
