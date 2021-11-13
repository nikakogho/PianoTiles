using UnityEngine;
using UnityEngine.UI;

public class Checker : MonoBehaviour {

	private Button button;
	private bool black { get { return button.image.color == Color.black; } }

	void Awake()
	{
		button = GetComponent<Button> ();
	}

	public void Neutralize()
	{
		if (black) 
		{
			button.image.color = Color.grey;
			if (!TileSpawner.gameOver)
			{
				TileSpawner.score++;
			}
		} else 
		{
			TileSpawner.gameOver = true;
			button.image.color = Color.red;
		}
	}
}
