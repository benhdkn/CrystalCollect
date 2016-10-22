using UnityEngine;

public class Canon : MonoBehaviour {
    
    [SerializeField]
    private GameObject quantityGameObject;
    [SerializeField]
    private GameColor.Type colorType;
    [SerializeField]
    private GameXDirection xDirection;
    
    private float energyMaxQuantity = 0.9F;
    private float energyRegenPerSec = 0.3F;
    private float energyCostPerBullet = 0.3F;
    
    private float currentEnergyQuantity = 0.9F;
    
    void Awake() {
        quantityGameObject.GetComponent<SpriteRenderer>().material.color = GameColor.ColorByType[colorType];
    }
    
    void Update() {
        if (currentEnergyQuantity < energyMaxQuantity) {
            currentEnergyQuantity += energyRegenPerSec * Time.deltaTime; 
            quantityGameObject.transform.localScale = new Vector2(currentEnergyQuantity, currentEnergyQuantity);  
        }
    }
    
    #region Public controls
    
    public GameColor.Type GetColorType() {
        return colorType;
    }
    
    public GameXDirection GetXDirection() {
        return xDirection;
    }
    
    public bool IsReadyToSpawnBullet() {
        return currentEnergyQuantity >= energyCostPerBullet;
    }
    
    public void DecreaseQuantity() {
        currentEnergyQuantity -= energyCostPerBullet;
    }
    
    #endregion
    
}
