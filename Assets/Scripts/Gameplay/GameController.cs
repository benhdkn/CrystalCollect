using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour {

    GameGUI gameGUI;
    
    InputRaycastHandler inputRaycastHandler;
    Stream stream;
    
    private Crystal crystal;
    private CrystalCollider crystalCollider;
    private Socket socket;
    private List<Bullet> bullets = new List<Bullet>();
    private List<Bullet> activeBullets = new List<Bullet>();
    private List<BulletFX> bulletFXs = new List<BulletFX>();
    private MatchFX matchFX;
    
    private Score score;
    private Life life;
    
    void Awake() {
        Application.targetFrameRate = 60;

        gameGUI = FindObjectOfType<GameGUI>();
        
        inputRaycastHandler = FindObjectOfType<InputRaycastHandler>();
        stream  = FindObjectOfType<Stream>();
        
        crystal = FindObjectOfType<Crystal>();
        crystalCollider = FindObjectOfType<CrystalCollider>();
        socket = FindObjectOfType<Socket>();
        bullets = FindObjectsOfType<Bullet>().ToList();
        bulletFXs = FindObjectsOfType<BulletFX>().ToList();
        matchFX = FindObjectOfType<MatchFX>();
        
        score = new Score();
        life = new Life();
    }
    
    void Start() {
        crystal.Disable();
        socket.Disable();
        // Crappy
        for (int i = 0; i < bullets.Count; i++) {
            bullets[i].Disable();
        }
        // Crappy too
        for (int i = 0; i < bulletFXs.Count; i++) {
            bulletFXs[i].gameObject.SetActive(false);
        }
        matchFX.gameObject.SetActive(false);
        
        inputRaycastHandler.SwipeLeft += crystal.TryMoveLeft;
        inputRaycastHandler.SwipeRight += crystal.TryMoveRight;
        inputRaycastHandler.SwipeDown += crystal.TryMoveDown;
        inputRaycastHandler.RaycastedCanon += TrySpawnBullet;
        
        stream.StepHasIncreased += crystal.IncrementMoveSpeed;
        
        crystalCollider.BulletCollision += TryChangeCrystalColor;
        crystalCollider.SocketCollision += TryMatchAndEndWave;
        crystalCollider.BottomBoundaryCollision += EndWave;
        
        life.LifeIsZero += EndGame;
        
        // StartCoroutine("FadeInAndCountDownAndStartGame");

        // GameGUI
        stream.StepHasIncreased += UpdateLevelText;
    }

	void Update () { 
        if (activeBullets.Count > 0) {
            for (int i = 0; i < activeBullets.Count; i++) {
                if (activeBullets[i].transform.position.x > LevelBoundaries.Right * 2 ||
                    activeBullets[i].transform.position.x < LevelBoundaries.Left * 2 ) {
                    activeBullets[i].Disable();
                    activeBullets.Remove(activeBullets[i]);
                }
            }
        }
    }
    
    #region Game controls
    
    private IEnumerator FadeInAndCountDownAndStartGame() {
        Debug.Log("Fade out");
        yield return new WaitForSeconds(1.0F);
        Debug.Log("3");
        yield return new WaitForSeconds(1.0F);
        Debug.Log("2");
        yield return new WaitForSeconds(1.0F);
        Debug.Log("1");
        yield return new WaitForSeconds(1.0F);
        Debug.Log("Go!");
        StartGame();
    }
    
    // Is public so the canvas can call it
    public void StartGame() {
        stream.Start();
        gameGUI.SetLevelText(stream.GetCurrentLevel().ToString());
        crystal.ResetMoveSpeed();
        crystal.Enable();
        socket.Enable();
        score.Reset();
        gameGUI.SetScoreText(score.GetCurrentScore().ToString());
        life.Reset();
        gameGUI.SetLifeText(life.GetCurrentLife().ToString());
    }
    
    private void EndGame() {
        Debug.Log("Game over");
        stream.End();
        crystal.Disable();
        socket.Disable();
    }
    
    #endregion
    
    #region Painting system controls
    
    private void TrySpawnBullet(Canon canon) {
        if (canon.IsReadyToSpawnBullet()) {
            Bullet colorUnit = GetFirstDisabledBullet();
            if (colorUnit != null) {
                canon.DecreaseQuantity();
                colorUnit.Enable(canon.transform.position, canon.GetXDirection(), canon.GetColorType());
                activeBullets.Add(colorUnit);
            }
        }
    }
    
    private Bullet GetFirstDisabledBullet() {
        Bullet ret = null;
        for (int i = 0; i < bullets.Count; i++) {
            if (!bullets[i].isActiveAndEnabled) {
                ret = bullets[i];
                break;
            }
        }
        return ret;
    }

    private BulletFX GetFirstDisabledBulletFX() {
        BulletFX ret = null;
        for (int i = 0; i < bulletFXs.Count; i++) {
            if (!bulletFXs[i].isActiveAndEnabled) {
                ret = bulletFXs[i];
                break;
            }
        }
        return ret;
    }
    
    private void TryChangeCrystalColor(Bullet colorUnit) {
        crystal.TryChangeColor(colorUnit.GetCurrentColorType());
        GetFirstDisabledBulletFX().Enable(colorUnit.transform.position, colorUnit.GetCurrentColorType());
        colorUnit.Disable();
        activeBullets.Remove(colorUnit);
    }
    
    #endregion
    
    #region Wave controls
    
    private void TryMatchAndEndWave() {
        if (crystal.GetCurrentColorType() == socket.GetCurrentColorType()) {
            if (crystal.IsMovingDownFast()) {
                score.ScoreCrystalAndBonusPoint();
                gameGUI.SetScoreText(score.GetCurrentScore().ToString());
            }
            else {
                score.ScoreCrystalPoint();
                gameGUI.SetScoreText(score.GetCurrentScore().ToString());
            }
            matchFX.Enable(socket.transform.position, crystal.GetCurrentColorType());
            crystal.Match();
            socket.HideWithCrystal();
            StartCoroutine("WaitForSocketToBeHiddenAndResetSocketAndCrystal");
        }
    }
    
    private void EndWave() {
        if (!crystal.IsMatched()) {
            life.RemoveOne();
            gameGUI.SetLifeText(life.GetCurrentLife().ToString());
            socket.Hide();
            StartCoroutine("WaitForSocketToBeHiddenAndResetSocketAndCrystal");   
        }
    }
    
    private IEnumerator WaitForSocketToBeHiddenAndResetSocketAndCrystal() {
        while (!socket.IsHidden()) {
            yield return null;    
        }
        crystal.Reset();
        socket.Reset();
        yield return null;
    }
    
    #endregion

    // #region Game GUI

    private void UpdateLevelText() {
        gameGUI.SetLevelText(stream.GetCurrentLevel().ToString());
    }

    // #endregion
    
}
