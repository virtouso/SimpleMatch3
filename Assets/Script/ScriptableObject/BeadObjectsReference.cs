using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BeadObjectsReference", menuName = "ScriptableObjects/BeadObjectsReference", order = 1)]
public class BeadObjectsReference : ScriptableObject {
    [SerializeField] private List<BeadEnumObjectPair> beadsList;

    private Dictionary<BeadTypeEnum, Bead> beadsDictionary;

    public Dictionary<BeadTypeEnum, Bead> beads
    {
        get
        {
            if (beadsDictionary == null) {
                beadsDictionary = new Dictionary<BeadTypeEnum, Bead>(beadsList.Count);
                for (int i = 0; i < beadsList.Count; i++) {
                    beadsDictionary.Add(beadsList[i].beadType, beadsList[i].beadObject);
                }
            }
            return beadsDictionary;
        }
    }

}


[System.Serializable]
public struct BeadEnumObjectPair {
    public BeadTypeEnum beadType;
    public Bead beadObject;
}
