#if UNITY_EDITOR

using UnityEngine;

public class GridDrawer : MonoBehaviour {

    [Range(0, 20)]
    public int numberOfRow = 0;
    [Range(0, 20)]
    public int numberOfColumn = 0;
    
    void OnDrawGizmos() {
        for (int i = 0; i < numberOfRow; i++) {
            for (int j = 0; j < numberOfColumn; j++) {
                // Rows
                Debug.DrawLine(new Vector2(0 - j / 2, 0 - i / 2), new Vector2(j / 2, 0 - i / 2), Color.blue);
                Debug.DrawLine(new Vector2(0 - j / 2, i / 2), new Vector2(j / 2, i / 2), Color.blue);
                // Columns
                Debug.DrawLine(new Vector2(j / 2, 0 - i / 2), new Vector2(j / 2, i / 2), Color.blue);
                Debug.DrawLine(new Vector2(0 - j / 2, 0 - i / 2), new Vector2(0 - j / 2, i / 2), Color.blue);
            }
        }
    }

}

#endif
