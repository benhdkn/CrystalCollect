public class Life {
	
	private int baseLife = 3;
	
	private int currentLife;

	public delegate void LifeIsZeroHandler();
	public event LifeIsZeroHandler LifeIsZero;
	
	public Life() {
		currentLife = baseLife;
	}
	
	public int GetCurrentLife() {
		return currentLife;
	}
	
	public void AddOne() {
		currentLife += 1;
	}
	
	public void RemoveOne() {
		currentLife -= 1;
		if (currentLife == 0) {
			RaiseLifeIsZeroEvent();
		}
	}
	
	public void Reset() {
		currentLife = baseLife;
	}
	
	private void RaiseLifeIsZeroEvent() {
		if (LifeIsZero != null) {
			LifeIsZero();
		}
	}
	
}
