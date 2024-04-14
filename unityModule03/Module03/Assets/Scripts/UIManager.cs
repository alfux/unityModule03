using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	private Canvas					gameUI = null;
	private Canvas					pauseMenu = null;
	private Canvas					confirmMenu = null;
	private TMPro.TextMeshProUGUI	scoreDisplay = null;

	private static float	score = 0;
	private static int		lastIndex = 0;

	void Start()
	{
		this.gameUI = GameObject.Find("GameUI")?.GetComponent<Canvas>();
		this.pauseMenu = GameObject.Find("PauseUI")?.GetComponent<Canvas>();
		this.confirmMenu = GameObject.Find("ConfirmUI")?.GetComponent<Canvas>();
		this.scoreDisplay = GameObject.Find("Score")?.GetComponent<TMPro.TextMeshProUGUI>();
		this.gameUI.enabled = true;
		this.pauseMenu.enabled = false;
		this.confirmMenu.enabled = false;
		UIManager.score = 0;
		UIManager.lastIndex = SceneManager.GetActiveScene().buildIndex;

	}

    void Update()
    {
		if (Input.GetKey(KeyCode.Escape) && this.gameUI.enabled)
			this.Pause();
		this.scoreDisplay.text = UIManager.score.ToString();
    }

	public void Pause()
	{
		Time.timeScale = 0;
		this.gameUI.enabled = false;
		this.pauseMenu.enabled = true;
	}

	public void Resume()
	{
		Time.timeScale = 1;
		this.gameUI.enabled = true;
		this.pauseMenu.enabled = false;
	}

	public void Quit()
	{
		this.pauseMenu.enabled = false;
		this.confirmMenu.enabled = true;
	}

	public void Yes()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
		GameObject.Destroy(this.gameObject);
	}

	public void No()
	{
		this.confirmMenu.enabled = false;
		this.pauseMenu.enabled = true;
	}

	public static void AddScore(float val) => UIManager.score += val;

	public static float GetScore() => (UIManager.score);

	public static int GetLastIndex() => (UIManager.lastIndex);

	public static void GameOver()
	{
		UIManager.score *= GameObject.Find("Health").GetComponent<Jauge>().Amount();
		UIManager.score *= Mathf.Max(1, GameObject.Find("Energy").GetComponent<Jauge>().Amount());
		SceneManager.LoadScene(1);
	}
}
