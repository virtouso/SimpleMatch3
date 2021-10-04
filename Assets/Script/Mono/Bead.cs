using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bead : MonoBehaviour {
    public BeadData beadData;
    public BoardCoordinate boardCoordinate;
    public void Init(Vector3 position, BeadData beadData, BoardCoordinate coordinate, Vector3 scale) {
        transform.position = position;
        this.beadData = beadData;
        this.boardCoordinate = coordinate;
        this.transform.localScale = scale;
    }


}
