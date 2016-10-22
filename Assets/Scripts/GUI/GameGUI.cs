using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour {

	[SerializeField]
	private Text lifeText;
	[SerializeField]
	private Text levelText;
	[SerializeField]
	private Text scoreText;

	public void SetLifeText(string text) {
		lifeText.text = text;
	}

	public void SetLevelText(string text) {
		levelText.text = text;
	}

	public void SetScoreText(string text) {
		scoreText.text = text;
	}

}
