using UnityEngine;

public class Crystal : MonoBehaviour {
    
    private enum State {
        Disabled,
        Idle,
        Matched,
        Spawning,
        MovingLeft,
        MovingRight,
        MovingDownFast,
        ColorChanging
    }
    
    [SerializeField]
    private Rigidbody2D rigidbody2d;
    [SerializeField]
    private SpriteRenderer sprite;
    
    private Vector2 spawnPosition = new Vector2((LevelBoundaries.Left + LevelBoundaries.Right) / 2, LevelBoundaries.Top);
    private float spawningDuration = 0.5F;
    private float movingDuration = 0.1F;
    private float colorChangingDuration = 0.1F;
    private float baseMoveYSpeed = 1.5F;
    private float moveYSpeedIncrementFactor = 1.3F;
    private float moveDownFastYSpeed = 30.0F;
    
    private State currentState = State.Disabled;
    private State previousState = State.Disabled;
    private GameColor.Type currentColorType = GameColor.Type.None;
    private float currentMoveYSpeed;
    private float currentstateTimer = 0.0F;
    
    private float stateChangeSavedXPosition;
    private Color stateChangeSavedColor;
    
    void Awake() {
        currentMoveYSpeed = baseMoveYSpeed;
    }
    
    void FixedUpdate() {
        currentstateTimer += Time.fixedDeltaTime;
        if (previousState != currentState) {
            currentstateTimer = 0.0F;
            stateChangeSavedXPosition = transform.position.x;
            stateChangeSavedColor = sprite.material.color;
            previousState = currentState;
        }

        switch (currentState) {
            case State.Idle:
                MovePositionY(currentMoveYSpeed);
                break;
            case State.Spawning:
                if (currentstateTimer < spawningDuration) {
                    LerpScale(currentstateTimer / spawningDuration);
                } 
                else {
                    // The lerp isn't precise enough so make sure the value changes correctly
                    transform.localScale = new Vector2(1.0F, 1.0F);
                    currentState = State.Idle;
                }
                break;
            case State.MovingLeft:
                MovePositionY(currentMoveYSpeed);
                if (currentstateTimer < movingDuration) {
                    LerpMoveX(GameXDirection.RightToLeft, currentstateTimer / movingDuration);
                }
                else {
                    currentState = State.Idle;
                }
                break;
            case State.MovingRight:
                MovePositionY(currentMoveYSpeed);
                if (currentstateTimer < movingDuration) {
                    LerpMoveX(GameXDirection.LeftToRight, currentstateTimer / movingDuration);
                }
                else {
                    currentState = State.Idle;
                }
                break;
            case State.MovingDownFast:
                MovePositionY(moveDownFastYSpeed);
                break;
            case State.ColorChanging:
                MovePositionY(currentMoveYSpeed);
                if (currentstateTimer < colorChangingDuration) {
                    LerpMaterialColor(currentColorType, currentstateTimer / colorChangingDuration);
                }
                else {
                    currentState = State.Idle;  
                }
                break;
            default: break;
        }
    }
    
    private void MovePositionY(float speed) {
        Vector2 velocity = new Vector2(0.0F, speed * -1.0F);
        rigidbody2d.MovePosition(rigidbody2d.position + velocity * Time.fixedDeltaTime); 
    }
    
    private void LerpScale(float t) {
        float newScale = Mathf.Lerp(0.0F, 1.0F, t);
        transform.localScale = new Vector2(newScale, newScale);
    }
    
    private void LerpMoveX(GameXDirection xDirection, float t) {
        float newXPos = Mathf.Lerp(stateChangeSavedXPosition, stateChangeSavedXPosition + (int)xDirection, t);
        transform.position = new Vector3(newXPos, this.transform.position.y);
    }
    
    private void LerpMaterialColor(GameColor.Type type, float t) {
        Color newColor = Color.Lerp(stateChangeSavedColor, GameColor.ColorByType[type], t);
        sprite.material.color = newColor;
    }
    
    #region Public controls
    
    public GameColor.Type GetCurrentColorType() {
        return currentColorType;
    }
    
    public void Enable() {
        Reset();
        gameObject.SetActive(true);
    }
    
    public void Reset() {
        transform.localScale = Vector2.zero;
        transform.position = spawnPosition;
        currentColorType = GameColor.Type.None;
        sprite.material.color = GameColor.ColorByType[currentColorType];
        currentState = State.Spawning;
    }
    
    public void Disable() {
        currentState = State.Disabled;
        gameObject.SetActive(false);
    }
    
    public void TryMoveLeft() {
        if (currentState == State.Idle && transform.position.x > LevelBoundaries.Left) {
            currentState = State.MovingLeft;
        }
    }
    
    public void TryMoveRight() {
        if (currentState == State.Idle && transform.position.x < LevelBoundaries.Right) {
            currentState = State.MovingRight;
        }
    }
    
    public void TryMoveDown() {
        if (currentState == State.Idle) {
            currentState = State.MovingDownFast;
        }
    }
    
    public void TryChangeColor(GameColor.Type newColorType) {
        if (currentState == State.Idle || currentState == State.MovingDownFast) {
            currentColorType = newColorType;
            currentState = State.ColorChanging;   
        }
    }
    
    public void Match() {
        currentState = State.Matched;   
    }
    
    public bool IsMatched() {
        return currentState == State.Matched;
    }
    
    public bool IsMovingDownFast() {
        return currentState == State.MovingDownFast;
    }
    
    public void IncrementMoveSpeed() {
        currentMoveYSpeed *= moveYSpeedIncrementFactor;
    }
    
    public void ResetMoveSpeed() {
        currentMoveYSpeed = baseMoveYSpeed;
    }
    
    #endregion
	
}
