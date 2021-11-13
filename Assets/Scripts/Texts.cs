using UnityEngine;
using UnityEngine.UI;

public class Texts : MonoBehaviour {

	private TileSpawner spawner;
	public Text score, spawnTime, minTime, maxTime;

	void Awake()
	{
		spawner = FindObjectOfType<TileSpawner> ();
	}

	void Update()
	{
		score.text = "Score : " + TileSpawner.score;
		spawnTime.text = "Spawn Time : " + string.Format("{00:00.00}", spawner.speed);
		minTime.text = "Min Spawn Time : " + string.Format("{00:00.00}", spawner.minSpeed);
		maxTime.text = "Max Spawn Time : " + string.Format("{00:00.00}", spawner.maxSpeed);
	}
}
