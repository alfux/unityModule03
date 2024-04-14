using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
	public TMPro.TextMeshProUGUI	Score = null;
	public TMPro.TextMeshProUGUI	Rank = null;
	public TMPro.TextMeshProUGUI	Next = null;

    void Start()
    {
		this.Score.text = UIManager.GetScore().ToString();
		if (UIManager.GetScore() == 0)
		{
			this.Rank.text = "F";
			this.Rank.color = Color.red;
			this.Next.text = "Try Again";
		}
		else if (UIManager.GetScore() <= 20000)
		{
			this.Next.text = "Next";
			this.Rank.color = new Color(1, 0.5f, 0, 1);
			this.Rank.text = "E";
		}
		else if (UIManager.GetScore() <= 40000)
		{
			this.Next.text = "Next";
			this.Rank.color = Color.yellow;
			this.Rank.text = "D";
		}
		else if (UIManager.GetScore() <= 60000)
		{
			this.Next.text = "Next";
			this.Rank.color = Color.green;
			this.Rank.text = "C";
		}
		else if (UIManager.GetScore() <= 80000)
		{
			this.Next.text = "Next";
			this.Rank.color = Color.cyan;
			this.Rank.text = "B";
		}
		else if (UIManager.GetScore() <= 100000)
		{
			this.Next.text = "Next";
			this.Rank.color = Color.blue;
			this.Rank.text = "A";
		}
		else
		{
			this.Next.text = "Next";
			this.Rank.color = Color.white;
			this.Rank.text = "S";
			for (float i = UIManager.GetScore(); i > 100000; i -= 20000)
				this.Rank.text += "S";
		}
    }

	public void NextLevel()
	{
		if (UIManager.GetScore() == 0)
			SceneManager.LoadScene(UIManager.GetLastIndex());
		else
			SceneManager.LoadScene(UIManager.GetLastIndex() + 1);
	}

	public void GetBackToMenu() => SceneManager.LoadScene(0);
}
