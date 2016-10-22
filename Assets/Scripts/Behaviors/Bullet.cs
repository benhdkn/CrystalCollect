using UnityEngine;

public class Bullet : MonoBehaviour {
    
    [SerializeField]
    private Rigidbody2D rigidbody2d;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Transform pivot;
    
    private GameColor.Type currentColorType = GameColor.Type.None;
    private GameXDirection currentXDirection = GameXDirection.LeftToRight;
    
    private float moveSpeed = 8.0F;
    private float rotateSpeed = 300.0F;
    
    void FixedUpdate() {
        Vector2 velocity = new Vector2(moveSpeed * (float)currentXDirection, 0.0F);
        rigidbody2d.MovePosition(rigidbody2d.position + velocity * Time.fixedDeltaTime);
        pivot.Rotate(0.0F, 0.0F, rotateSpeed * Time.fixedDeltaTime, Space.Self);
    }
    
    #region Public controls
    
    public GameColor.Type GetCurrentColorType() {
        return currentColorType;
    }
    
    public void Enable(Vector2 spawnPosition, GameXDirection xDirection, GameColor.Type colorType) {
        transform.position = spawnPosition;
        currentXDirection = xDirection;
        currentColorType = colorType;
        sprite.material.color = GameColor.ColorByType[currentColorType];
        gameObject.SetActive(true);
    }
    
    public void Disable() {
        gameObject.SetActive(false);
    }
    
    #endregion

}
