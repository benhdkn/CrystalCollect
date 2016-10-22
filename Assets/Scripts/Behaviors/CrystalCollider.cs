using UnityEngine;

public class CrystalCollider : MonoBehaviour {
    
    public delegate void BulletCollisionHandler(Bullet colorUnit);
    public event BulletCollisionHandler BulletCollision;
    
    public delegate void SocketCollisionHandler();
    public event SocketCollisionHandler SocketCollision;
    
    public delegate void BottomBoundaryCollisionHandler();
    public event BottomBoundaryCollisionHandler BottomBoundaryCollision;
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("BulletCollider")) {
            Bullet bullet = other.GetComponentInParent<Bullet>();
            RaiseBulletCollisionEvent(bullet);
        }
        if (other.CompareTag("SocketCollider")) {
            RaiseSocketCollisionEvent();
        }
        if (other.CompareTag("BottomBoundaryCollider")) {
            RaiseBottomBoundaryCollisionEvent();
        }
    }
    
    private void RaiseBulletCollisionEvent(Bullet bullet) {
        if (BulletCollision != null) {
            BulletCollision(bullet);
        }
    }
    
    private void RaiseSocketCollisionEvent() {
        if (SocketCollision != null) {
            SocketCollision();
        }
    }
    
    private void RaiseBottomBoundaryCollisionEvent() {
        if (BottomBoundaryCollision != null) {
            BottomBoundaryCollision();
        }
    }
    
}