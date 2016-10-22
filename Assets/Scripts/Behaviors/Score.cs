public class Score {
	
	private int pointPerCrystal = 100;
	private float bonusFactor = 1.5F;

	private int currentScore;
	
	public int GetCurrentScore() {
		return currentScore;
	}
	
	public void ScoreCrystalPoint() {
		currentScore += pointPerCrystal;
	}
	
	public void ScoreCrystalAndBonusPoint() {
		currentScore += (int)(pointPerCrystal * bonusFactor);
	}
	
	public void Reset() {
		currentScore = 0;
	}
	
}
