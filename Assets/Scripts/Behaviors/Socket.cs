using UnityEngine;

public class Socket : MonoBehaviour {
    
    private enum State {
        Disabled,
        Idle,
        Hidden,
        Spawning,
        Hidding,
        HiddingWithCrystal
    }
    
    [SerializeField]
    private Crystal crystal;
    [SerializeField]
    private SpriteRenderer baseSprite;
    
    private float spawningDuration = 0.5F;
    private float hiddingDuration = 0.5F;
    
    private State currentState = State.Disabled;
    private State previousState = State.Disabled;
    private GameColor.Type currentColorType = GameColor.Type.None;
    private float currentStateTimer = 0.0F;
    
    void FixedUpdate() {
        if (previousState != currentState) {
            currentStateTimer = 0.0F;
            previousState = currentState;
        }
        
        switch (currentState) {
            case State.Spawning:
                if (currentStateTimer < spawningDuration) {
                    currentStateTimer += Time.deltaTime;
                    transform.position = LerpYPosition(LevelBoundaries.Bottom - 2, LevelBoundaries.Bottom, currentStateTimer / spawningDuration);
                }
                else {
                    currentState = State.Idle;
                }
                break;
            case State.Hidding:
                if (currentStateTimer < hiddingDuration) {
                    currentStateTimer += Time.deltaTime;
                    transform.position = LerpYPosition(LevelBoundaries.Bottom, LevelBoundaries.Bottom - 2, currentStateTimer / hiddingDuration);
                }
                else {
                    currentState = State.Hidden;
                }
                break;
            case State.HiddingWithCrystal:
                if (currentStateTimer < hiddingDuration) {
                    currentStateTimer += Time.deltaTime;
                    transform.position = LerpYPosition(LevelBoundaries.Bottom, LevelBoundaries.Bottom - 2, currentStateTimer / hiddingDuration);
                    crystal.transform.position = LerpYPosition(LevelBoundaries.Bottom, LevelBoundaries.Bottom - 2, currentStateTimer / hiddingDuration);
                }
                else {
                    currentState = State.Hidden;
                }
                break;
            default: break;
        }
    }
    
    private Vector2 LerpYPosition(float startPosition, float endPosition, float t) {
        float newYPos = Mathf.Lerp(startPosition, endPosition, t);
        return new Vector2(transform.position.x, newYPos);
    }
    
    private void SetRandomSpawnPosition() {
        int randomXPos = Random.Range((int)LevelBoundaries.Left, (int)LevelBoundaries.Right);
        transform.position = new Vector2(randomXPos, LevelBoundaries.Bottom - 2);
    }
    
    private void SetRandomColorType() {
        currentColorType = (GameColor.Type)Random.Range(1, 3);
    }
    
    #region Public controls
    
    public GameColor.Type GetCurrentColorType() {
        return currentColorType;
    }
    
    public void Enable() {
        Reset();
        gameObject.SetActive(true);
    }
    
    public void Reset() {
        SetRandomSpawnPosition();
        SetRandomColorType();
        baseSprite.material.color = GameColor.ColorByType[currentColorType];
        currentState = State.Spawning;
    }
    
    public void Disable() {
        currentState = State.Disabled;
        gameObject.SetActive(false);
    }
    
    public void Hide() {
        currentState = State.Hidding;   
    }
    
    public void HideWithCrystal() {
        currentState = State.HiddingWithCrystal;
    }
    
    public bool IsHidden() {
        return currentState == State.Hidden;
    }
    
    #endregion
    
}
