using UnityEngine;

public class MatchFX : MonoBehaviour {

	private float timer = 0.0F;

	[SerializeField]
	private SpriteRenderer baseSprite;

	private Color colorToLerpTo;

	void Update () {
		timer += Time.fixedDeltaTime;
		float t = timer / 0.4F;
		transform.localScale = Vector3.Lerp(new Vector2(1.0F, 1.0F), new Vector2(1.3F, 1.3F), t);
		if (t > 1.0F) {
			this.gameObject.SetActive(false);
		}
		colorToLerpTo.a = Mathf.Lerp(1.0F, 0.0F, t);
		baseSprite.color = colorToLerpTo;
	}

	#region Public controls
		
	public void Enable(Vector3 position, GameColor.Type gameColorType) {
		timer = 0.0F;
		colorToLerpTo = GameColor.ColorByType[gameColorType];
		transform.position = position;
		gameObject.SetActive(true);
	}

	#endregion
	
}
