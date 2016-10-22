using UnityEngine;
public class Stream : MonoBehaviour {
	
	private float secondPerLevel = 45.0F;
	
	private float currentTime = 0.0F;
	private int currentLevel = 1;
	
	private bool mustStream = true;
	
	public delegate void StepHasIncreasedHandler();
	public event StepHasIncreasedHandler StepHasIncreased;

	void Update () {
		if (mustStream) {
			currentTime += Time.deltaTime;
			if (currentTime > secondPerLevel) {
				currentTime = 0.0F;
				currentLevel += 1;
				RaiseStepHasIncreasedEvent();
			}
		}
	}
	
	private void RaiseStepHasIncreasedEvent() {
		if (StepHasIncreased != null) {
			StepHasIncreased();
		}
	}
	
	public void Start() {
		mustStream = true;
	}
	
	public void End() {
		mustStream = false;
		currentTime = 0.0F;
		currentLevel = 1;
	}
	
	public int GetCurrentLevel() {
		return currentLevel;
	}
	
}
