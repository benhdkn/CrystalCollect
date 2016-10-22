using UnityEngine;
using System.Collections.Generic;

public static class GameColor {
    
    public enum Type {
        None = 0,
        Red = 1,
        Blue = 2
    }
    
    public static readonly Dictionary<Type, Color32> ColorByType = new Dictionary<Type, Color32> {
        { Type.None,    Color.grey },
        { Type.Red,     new Color32(237, 109, 121, 255) },
        { Type.Blue,    new Color32(102, 141, 229, 255) }
    };
    
}
