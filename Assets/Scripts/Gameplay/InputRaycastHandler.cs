using UnityEngine;

public class InputRaycastHandler : MonoBehaviour {
    
    private float touchRaycastDistance = 10.0F;
    private LayerMask touchRaycastLayer;
    
    private bool beganTouch = false;
    private Vector2 beganTouchPosition;
    
    private float swipeStrength = 40.0F;
    
    public delegate void SwipeLeftHandler();
	public event SwipeLeftHandler SwipeLeft;

	public delegate void SwipeRightHandler();
	public event SwipeRightHandler SwipeRight;
    
    public delegate void SwipeDownHandler();
	public event SwipeDownHandler SwipeDown;

    public delegate void RaycastedCanonHandler(Canon canon);
    public event RaycastedCanonHandler RaycastedCanon;

	void Awake () {
		touchRaycastLayer = LayerIndexToLayerMask(LayerMask.NameToLayer("InputRaycastableCollider"));
	}
    
	private int LayerIndexToLayerMask(int layerIndex) {
		int ret = 1;
		ret <<= layerIndex;
		return ret;
	}
    
    void Update() {
        HandleMouseInput();
    }
    
    private void HandleMouseInput() {
        if (Input.GetMouseButtonDown(0) && MousePositionInputRaycastableHit2D()) {
            if (MousePositionInputRaycastableHit2D().collider.CompareTag("CanonCollider")) {
                Canon canon = MousePositionInputRaycastableHit2D().collider.GetComponentInParent<Canon>();
                RaiseRaycastedCanonEvent(canon);
            }
        }
        else if (Input.GetMouseButtonDown(0)) {
            beganTouch = true;
            beganTouchPosition = Input.mousePosition;
        }
        if (beganTouch) {
            if (Input.GetMouseButton(0)) {
                Vector2 moveTouchPosition = Input.mousePosition;
                if (beganTouchPosition.x - moveTouchPosition.x > swipeStrength) {
                    RaiseSwipeLeftEvent();
                    beganTouch = false;
                }  
                else if (beganTouchPosition.x - moveTouchPosition.x < swipeStrength * - 1) {
                    RaiseSwipeRightEvent();
                    beganTouch = false;
                }
                else if (beganTouchPosition.y - moveTouchPosition.y > swipeStrength) {
                    RaiseSwipeDownEvent();
                    beganTouch = false;
                }
            }
            else {
                beganTouch = false;
            }
        }
    }
    
    private RaycastHit2D MousePositionInputRaycastableHit2D() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics2D.Raycast(ray.origin, ray.direction, touchRaycastDistance, touchRaycastLayer);
    }
    
    private void RaiseSwipeLeftEvent() {
		if (SwipeLeft != null) {
			SwipeLeft();
		}
	}
	
	private void RaiseSwipeRightEvent() {
		if (SwipeRight != null) {
			SwipeRight();
		}
	}
    
    private void RaiseSwipeDownEvent() {
        if (SwipeDown != null) {
            SwipeDown();
        }
    }
    
    private void RaiseRaycastedCanonEvent(Canon colorFactory) {
        if (RaycastedCanon != null) {
            RaycastedCanon(colorFactory);
        }
    }
    
}
