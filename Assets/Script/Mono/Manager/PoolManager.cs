using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {


    [SerializeField] private BeadObjectsReference beadObjects;

    [SerializeField] private int poolSize;



    private Queue<Bead> bead0Pool;
    private Queue<Bead> bead1Pool;
    private Queue<Bead> bead2Pool;
    private Queue<Bead> bead3Pool;



    public Bead GetBeadFromPool(BeadData beadData, Vector3 position, BoardCoordinate coordinate, Vector3 scale) {
        switch (beadData.beadType) {
            case BeadTypeEnum.Tile0:
                return GetBead0FromPool(beadData, position, coordinate, scale);

            case BeadTypeEnum.Tile1:
                return GetBead1FromPool(beadData, position, coordinate, scale);

            case BeadTypeEnum.Tile2:
                return GetBead2FromPool(beadData, position, coordinate, new Vector3(scale.x, scale.y / 2, scale.z));

            case BeadTypeEnum.Tile3:
                return GetBead3FromPool(beadData, position, coordinate, new Vector3(scale.x, scale.y / 2, scale.z));

            default:
                throw new System.Exception();
        }
    }


    private Bead GetBead0FromPool(BeadData beadData, Vector3 position, BoardCoordinate coordinate, Vector3 scale) {
        Bead bead = bead0Pool.Dequeue();
        bead.gameObject.SetActive(true);
        bead.Init(position, beadData, coordinate, scale);
        bead0Pool.Enqueue(bead);
        return bead;

    }

    private Bead GetBead1FromPool(BeadData beadData, Vector3 position, BoardCoordinate coordinate, Vector3 scale) {
        Bead bead = bead1Pool.Dequeue();
        bead.gameObject.SetActive(true);
        bead.Init(position, beadData, coordinate, scale);
        bead1Pool.Enqueue(bead);
        return bead;
    }

    private Bead GetBead2FromPool(BeadData beadData, Vector3 position, BoardCoordinate coordinate, Vector3 scale) {
        Bead bead = bead2Pool.Dequeue();
        bead.gameObject.SetActive(true);
        bead.Init(position, beadData, coordinate, scale);
        bead2Pool.Enqueue(bead);
        return bead;
    }
    private Bead GetBead3FromPool(BeadData beadData, Vector3 position, BoardCoordinate coordinate, Vector3 scale) {
        Bead bead = bead3Pool.Dequeue();
        bead.gameObject.SetActive(true);
        bead.Init(position, beadData, coordinate, scale);
        bead3Pool.Enqueue(bead);
        return bead;
    }




    private void InitPool() {
        bead0Pool = new Queue<Bead>(poolSize);
        bead1Pool = new Queue<Bead>(poolSize);
        bead2Pool = new Queue<Bead>(poolSize);
        bead3Pool = new Queue<Bead>(poolSize);

        for (int i = 0; i < poolSize; i++) {
            Bead bead0 = Instantiate(beadObjects.beads[BeadTypeEnum.Tile0]);
            bead0.gameObject.SetActive(false);
            bead0Pool.Enqueue(bead0);

            Bead bead1 = Instantiate(beadObjects.beads[BeadTypeEnum.Tile1]);
            bead1.gameObject.SetActive(false);
            bead1Pool.Enqueue(bead1);

            Bead bead2 = Instantiate(beadObjects.beads[BeadTypeEnum.Tile2]);
            bead2.gameObject.SetActive(false);
            bead2Pool.Enqueue(bead2);

            Bead bead3 = Instantiate(beadObjects.beads[BeadTypeEnum.Tile3]);
            bead3.gameObject.SetActive(false);
            bead3Pool.Enqueue(bead3);
        }
    }




    private void Awake() {
        InitPool();

    }



}
