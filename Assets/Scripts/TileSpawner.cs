using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TileSpawner : MonoBehaviour {

	public Text scoreText, highScoreText;
	public GameObject EndUI;
	public static int score = 0;
	public static bool gameOver = false;
	private bool justEnded = true;
	public GameObject[] Panels;
	public float speed;
	public float minSpeed, maxSpeed, speedChangeRate;
	private float countdown = 0;
	private int side = 0;

	void Awake()
	{
		EndUI.SetActive (false);
		gameOver = false;
		score = 0;
		SetupButtons ();
	}

	void SetupButtons()
	{
		Button[] bottomButtons = Panels [Panels.Length - 1].GetComponentsInChildren<Button> ();

		foreach (Button button in bottomButtons) 
		{
			UnDangerize (button);
		}
	}

	void Update()
	{
		if (Input.GetKeyDown ("r")) 
		{
			Restart ();
		}
		if (gameOver) 
		{
			if (justEnded) 
			{
				End ();
			}
			return;
		}
		countdown -= Time.deltaTime;

		if (countdown <= 0)
		{
			Spawn ();
		}

		if (side == 1) 
		{
			if (speed < maxSpeed) 
			{
				speed += Time.deltaTime;
			} else
				side = 0;
		} else 
		{
			if (speed > minSpeed) 
			{
				speed -= Time.deltaTime;
			} else
				side = 1;
		}
	}

	void End()
	{
		justEnded = false;
		EndUI.SetActive (true);
		if(score > PlayerPrefs.GetInt("HighScore", 0))
		{
			PlayerPrefs.SetInt ("HighScore", score);
		}
		scoreText.text = "Score : " + score;
		highScoreText.text = "HighScore : " + PlayerPrefs.GetInt ("HighScore", 0);
	}

	void Spawn()
	{
		countdown = speed;
		minSpeed /= speedChangeRate;
		maxSpeed /= speedChangeRate;

		Button[] bottoms = Panels [Panels.Length - 1].GetComponentsInChildren<Button> ();

		foreach (Button button in bottoms) 
		{
			if (button.image.color == Color.black)
			{
				gameOver = true;
			}
		}

		for (int i = Panels.Length - 1; i > 0; i--)
		{
			SetFromPrevious (Panels [i - 1].GetComponentsInChildren<Button>(), Panels [i].GetComponentsInChildren<Button>());
		}

		Button[] topButtons = Panels [0].GetComponentsInChildren<Button> ();

		int index = Random.Range (0, topButtons.Length);

		for (int i = 0; i < topButtons.Length; i++) 
		{
			if (i == index)
			{
				Dangerize (topButtons [i]);
			} else 
			{
				UnDangerize (topButtons [i]);
			}
		}
	}

	void Dangerize(Button button)
	{
		button.image.color = Color.black;
	}

	void UnDangerize(Button button)
	{
		button.image.color = Color.white;
	}

	void SetFromPrevious(Button[] Olds, Button[] News)
	{
		for (int i = 0; i < Olds.Length; i++) 
		{
			News [i].image.color = Olds [i].image.color;
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void Exit()
	{
		Application.Quit ();
	}
}
