using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainGameManager : MonoBehaviour {
    [SerializeField] private int minimumPermitedMatch;
    [SerializeField] private BoardCoordinate boardSize;
    [SerializeField] private MeshRenderer boardMeshRenderer;
    [SerializeField] private float movingTime;
    [Inject] private PoolManager poolManager;
    [Inject] private InputController inputController;
    [SerializeField] private float beadsMoveSpeed;


    private BeadData[,] boardReference;
    private Vector3 boardStartPosition;

    [SerializeField] private Vector2 boardCellSize;



    private bool inputEnabled = true;
    private TurnEnum turn = TurnEnum.Player;

    #region Utility

    private void SetBoardStartSize() {
        boardStartPosition = boardMeshRenderer.bounds.min;
    }

    private void SetBoardCellSize() {
        Vector3 boardObjectSize = boardMeshRenderer.bounds.max - boardMeshRenderer.bounds.min;

        float cellXSize = boardObjectSize.x / (float)boardSize.x;
        float cellYSize = boardObjectSize.x / (float)boardSize.y;
        boardCellSize = new Vector2(cellXSize, cellYSize);
    }



    private void InitBoard() {
        boardReference = UtilityBoard.IntializePanel(boardSize);


        for (int i = 0; i < boardSize.x; i++) {
            for (int j = 0; j < boardSize.y; j++) {
                Vector3 goalPosition = boardStartPosition + new Vector3(i * boardCellSize.x, j * boardCellSize.y, 0) + new Vector3(boardCellSize.x / 2, boardCellSize.y / 2);
                boardReference[i, j].BeadObject = poolManager.GetBeadFromPool(boardReference[i, j], goalPosition, new BoardCoordinate(i, j), new Vector3(boardCellSize.x, boardCellSize.y, boardCellSize.x));
            }
        }


    }



    private void CheckPlayerInput() {
        if (!inputEnabled) return;

        Bead bead = inputController.SelectBead();
        if (bead == null) return;
        inputEnabled = false;

        boardReference[bead.boardCoordinate.x, bead.boardCoordinate.y] = null;
        bead.gameObject.SetActive(false);
        StartCoroutine(MoveBeadsDown());
    }


    private void MoveBeadsToTheirGoal() {
        for (int i = 0; i < boardSize.x; i++) {
            for (int j = 0; j < boardSize.y; j++) {
                Debug.Assert(boardReference != null, "board is null");
                BeadData beadData = boardReference[i, j];
                if (beadData == null) continue;

                Vector3 goalPosition = boardStartPosition + new Vector3(i * boardCellSize.x, j * boardCellSize.y, 0) + new Vector3(boardCellSize.x / 2, boardCellSize.y / 2);
                beadData.BeadObject.transform.position = Vector3.MoveTowards(beadData.BeadObject.transform.position, goalPosition, beadsMoveSpeed);
            }
        }
    }


    private IEnumerator MoveBeadsDown() {
        boardReference = UtilityBoard.MoveBeadsDown(boardReference);
        turn = ~turn;

        yield return new WaitForSeconds(movingTime);

        if (turn == TurnEnum.Player)
            inputEnabled = true;
        else
            FindAndDestroyBiggestMatch();



    }

    private void FindAndDestroyBiggestMatch() {
        List<BoardCoordinate> matchingBeads = UtilityBoard.FindBiggestMatch(boardReference);
        Debug.Log("number of matching:" + matchingBeads.Count);

        if (matchingBeads.Count >= minimumPermitedMatch) {

            for (int i = 0; i < matchingBeads.Count; i++) {

                Debug.Assert(boardReference[matchingBeads[i].x, matchingBeads[i].y] != null);

                boardReference[matchingBeads[i].x, matchingBeads[i].y].BeadObject.gameObject.SetActive(false);
                boardReference[matchingBeads[i].x, matchingBeads[i].y] = null;

            }
        }

        StartCoroutine(MoveBeadsDown());
    }
    #endregion






    #region Unity Callbacks

    private void Awake() {

    }


    private void Start() {
        SetBoardStartSize();
        SetBoardCellSize();
        InitBoard();
    }

    private void Update() {
        CheckPlayerInput();
        MoveBeadsToTheirGoal();

    }

    private void OnDrawGizmos() {
        Gizmos.DrawIcon(boardStartPosition, "boardStartPosition");
    }
    #endregion
}
