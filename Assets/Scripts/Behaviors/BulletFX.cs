using UnityEngine;

public class BulletFX : MonoBehaviour {

	private new ParticleSystem particleSystem;

	void Awake() {
        particleSystem = this.GetComponent<ParticleSystem>();
    }

    void Update() {
        if (!particleSystem.IsAlive()) {
            gameObject.SetActive(false);
        }
    }

	#region Public controls
		
	public void Enable(Vector3 position, GameColor.Type gameColorType) {
		transform.position = position;
		particleSystem.startColor = GameColor.ColorByType[gameColorType];
		gameObject.SetActive(true);
	}

	#endregion

}
