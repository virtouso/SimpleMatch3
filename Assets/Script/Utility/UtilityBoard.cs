using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityBoard {

    public static BeadData[,] IntializePanel(BoardCoordinate boardSize) {

        BeadData[,] result = new BeadData[boardSize.x, boardSize.y];
        for (int i = 0; i < boardSize.x; i++) {
            for (int j = 0; j < boardSize.y; j++) {
                result[i, j] = new BeadData(Guid.NewGuid().ToString(), new BoardCoordinate(i, j), (BeadTypeEnum)UnityEngine.Random.Range(0, Enum.GetNames(typeof(BeadTypeEnum)).Length));
            }
        }

        return result;
    }



    public static BeadData[,] MoveBeadsDown(BeadData[,] board) {


        for (int i = 0; i < board.GetLength(0); i++) {
            for (int j = 0; j < board.GetLength(1) - 1; j++) {
                if (board[i, j] == null) {
                    if (j == board.GetLength(1) - 1) continue;
                    MoveBeadsDown(board, new BoardCoordinate(i, j));
                }
            }
        }
        return board;
    }


    private static BeadData[,] MoveBeadsDown(BeadData[,] board, BoardCoordinate beadPos) {

        for (int i = beadPos.y; i < board.GetLength(1) - 1; i++) {
            BeadData temp = board[beadPos.x, i];
            board[beadPos.x, i] = board[beadPos.x, i + 1];
            board[beadPos.x, i + 1] = temp;

            if (board[beadPos.x, i] != null) {
                board[beadPos.x, i].BeadObject.boardCoordinate = new BoardCoordinate(beadPos.x, i);
            }
            if (board[beadPos.x, i + 1] != null) {
                board[beadPos.x, i + 1].BeadObject.boardCoordinate = new BoardCoordinate(beadPos.x, i + 1);
            }

        }

        return board;
    }

    public static List<BoardCoordinate> FindBiggestMatch(BeadData[,] board) {


        List<BoardCoordinate> BiggestMatch = new List<BoardCoordinate>();

        for (int i = 0; i < board.GetLength(0); i++) {
            for (int j = 0; j < board.GetLength(1); j++) {
                if (board[i, j] == null) {
                    continue;
                }
                List<BoardCoordinate> currentBeadMatch = new List<BoardCoordinate>();
                currentBeadMatch.Add(new BoardCoordinate(i, j));


                bool rightSideFailed = false;
                bool leftSideFailed = false;
                for (int k = i + 1; k < board.GetLength(0); k++) {
                    if (rightSideFailed) {
                        continue;
                    }
                    if (board[k, j] == null) {
                        rightSideFailed = true;
                        continue;
                    }

                    if (board[k, j].beadType == board[i, j].beadType) {
                        currentBeadMatch.Add(new BoardCoordinate(k, j));
                    }
                    else {
                        rightSideFailed = true;
                    }

                }
                for (int l = i - 1; l > 0; l--) {
                    if (leftSideFailed) {
                        continue;
                    }
                    if (board[l, j] == null) {
                        leftSideFailed = true;
                        continue;
                    }

                    if (board[l, j].beadType == board[i, j].beadType) {
                        currentBeadMatch.Add(new BoardCoordinate(l, j));
                    }
                    else {
                        leftSideFailed = true;
                    }
                }

                if (currentBeadMatch.Count > BiggestMatch.Count) {
                    BiggestMatch = currentBeadMatch;
                }

            }

        }
        return BiggestMatch;
    }



}
