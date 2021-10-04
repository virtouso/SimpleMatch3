using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BoardCoordinate {
    public int x;
    public int y;

    public BoardCoordinate(int x, int y) {
        this.x = x;
        this.y = y;
    }
}
