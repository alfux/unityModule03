using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jauge : MonoBehaviour
{
	public float					animationSpeed = 1;
	public float					supLimit = 0;
	public float					infLimit = -6;
	public float					total = 100;
	public TMPro.TextMeshProUGUI	valueDisplay = null;
	public TMPro.TextMeshProUGUI	totalDisplay = null;

	private Transform	mask;
	private float		current;
	private float		step;

    void Start()
    {
		this.mask = this.transform.GetChild(0);
		this.current = this.supLimit;
		this.step = 0;
		this.valueDisplay.text = Mathf.Round(this.Amount()).ToString();
		this.totalDisplay.text = Mathf.Round(this.total).ToString();
    }

    void FixedUpdate()
    {
		if (Mathf.Abs(this.current - this.mask.localPosition.x) > 0.1)
		{
			this.mask.Translate(Vector2.right * this.step * Time.deltaTime, Space.Self);
			if (Mathf.Abs(this.current - this.mask.localPosition.x) < 0.1)
				this.mask.Translate(Vector2.right * (this.current - this.mask.localPosition.x), Space.Self);
			this.valueDisplay.text = Mathf.Round(this.Amount()).ToString();
		}
    }

	public void AddJauge(float percentage)
	{
		this.current = Mathf.Clamp(this.current + (this.supLimit - this.infLimit) * percentage / 100, this.infLimit, this.supLimit);
		this.step = this.current - this.mask.localPosition.x;
	}
	
	public float Amount()
	{
		return (this.total * (this.current - this.infLimit) / (this.supLimit - this.infLimit));
	}

	public float PreciseAmount()
	{
		return (this.total * (this.mask.localPosition.x - this.infLimit) / (this.supLimit - this.infLimit));
	}	
}
