using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadData {

    public string guid;
    public BoardCoordinate coordinate;
    public BeadTypeEnum beadType;
    public Bead BeadObject;
    public BeadData(string guid, BoardCoordinate coordinate, BeadTypeEnum beadType) {
        this.guid = guid;
        this.coordinate = coordinate;
        this.beadType = beadType;

    }
}
