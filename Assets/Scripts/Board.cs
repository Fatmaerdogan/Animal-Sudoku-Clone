using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    int[,] grid = new int[9,9];
    int[,] puzzle = new int[9, 9];
    int difficulty = 15;

   /* public Transform square00, square01, square02, 
                     square10, square11, square12, 
                     square20, square21, square22;*/
    public GameObject SudokuCell_Prefab;
    public List<Grid> SquareTransform ;
    public List<Sprite> AnimalImageList=new List<Sprite>();
    public static Board instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        difficulty = PlayerSettings.difficulty;
        CreateGrid();
        CreatePuzzle();

        CreateButtons();
    }
    void ConsoleOutputGrid(int [,] g)
    {
        string output = "";
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                output += g[i, j];
            }
            output += "\n";
        }
    }

    bool ColumnContainsValue(int col, int value)
    {
        for (int i = 0; i < 9; i++)
        {
            if (grid[i, col] == value)
            {
                return true;
            }
        }

        return false;
    }
    bool RowColumnContainsValue(int col, int row, int value)
    {
        for (int i = 0; i < 9; i++)
        {
            if (col == -1 && grid[row, i] == value )
            {
                return true;
            }
            else if (row == -1 && grid[i, col] == value )
            {
                return true;
            }
        }

        return false;
    }
    bool RowContainsValue(int row, int value)
    {
        for (int i = 0; i < 9; i++)
        {
            if (grid[row, i] == value)
            {
                return true;
            }
        }

        return false;
    }

    bool SquareContainsValue(int row, int col, int value)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid[ row / 3 * 3 + i , col / 3 * 3 + j ] == value)
                {
                    return true;
                }
            }
        }

        return false;
    }

    bool CheckAll(int row, int col, int value)
    {
        if (RowColumnContainsValue(col,-1,value))  return false;
        if (RowColumnContainsValue(-1,row, value)) return false;
        if (SquareContainsValue(row, col, value))return false;

        return true;
    }

    bool IsValid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (grid[i,j] == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    void CreateGrid()
    {
        List<int> rowList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> colList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        int value = rowList[Random.Range(0, rowList.Count)];
        grid[0, 0] = value;
        rowList.Remove(value);
        colList.Remove(value);

        for (int i = 1; i < 9; i++)
        {
            value = rowList[Random.Range(0, rowList.Count)];
            grid[i, 0] = value;
            rowList.Remove(value);
        }

        for (int i = 1; i < 9; i++)
        {
            value = colList[Random.Range(0, colList.Count)];
            if (i < 3)
            {
                while(SquareContainsValue(0, 0, value))
                {
                    value = colList[Random.Range(0, colList.Count)]; 
                }
            }
            grid[0, i] = value;
            colList.Remove(value);
        }

        for (int i = 6; i < 9; i++)
        {
            value = Random.Range(1, 10);
            while (SquareContainsValue(0, 8, value) || SquareContainsValue(8, 0, value) || SquareContainsValue(8, 8, value))
            {
                value = Random.Range(1, 10);
            }
            grid[i, i] = value;
        }

        ConsoleOutputGrid(grid);

        SolveSudoku();
    }

    bool SolveSudoku()
    {
        int row = 0;
        int col = 0;

        if (IsValid())
        {
            return true;
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (grid[i, j] == 0)
                {
                    row = i;
                    col = j;
                }
            }
        }

        for (int i = 1; i <=9; i++)
        {
            if (CheckAll(row, col, i)) {
                grid[row, col] = i;
                
                if (SolveSudoku())
                {
                    return true;
                }
                else
                {
                    grid[row, col] = 0;
                }
            }
        }
        return false;
    }

    void CreatePuzzle()
    {
        System.Array.Copy(grid, puzzle, grid.Length);
        for (int i = 0; i < difficulty; i++)
        {
            int row = Random.Range(0, 9);
            int col = Random.Range(0, 9);

            while (puzzle[row,col] == 0)
            {
                row = Random.Range(0, 9);
                col = Random.Range(0, 9);
            }

            puzzle[row, col] = 0;
        }
        List<int> onBoard = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        RandomizeList(onBoard);

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                for (int k = 0; k < onBoard.Count - 1; k++)
                {
                    if (onBoard[k] == puzzle[i,j])
                    {
                        onBoard.RemoveAt(k);
                    }
                }
            }
        }

        while (onBoard.Count - 1 > 1)
        {
            int row = Random.Range(0, 9);
            int col = Random.Range(0, 9);

            if (grid[row,col] == onBoard[0])
            {
                puzzle[row, col] = grid[row, col];
                onBoard.RemoveAt(0);
            }

        }

        ConsoleOutputGrid(puzzle);

    }

    void RandomizeList(List<int> l)
    {
        for (var i = 0; i < l.Count - 1; i++)
        {
            int rand = Random.Range(i, l.Count);
            int temp = l[i];
            l[i] = l[rand];
            l[rand] = temp;
        }
    }

    void CreateButtons()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject newButton = Instantiate(SudokuCell_Prefab);
                SudokuCell sudokuCell = newButton.GetComponent<SudokuCell>();
                sudokuCell.SetValues(i, j, puzzle[i, j], this, AnimalImageList[puzzle[i, j]]);
                newButton.name = i.ToString() + j.ToString();
                newButton.transform.SetParent(SquareTransform[i/3].GridList[j/3], false);
            }
        }
    }

    public void UpdatePuzzle(int row, int col, int value)
    {
        puzzle[row, col] = value;
    }

   

    public bool CheckGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j =  0; j < 9; j++)
            {
                if (puzzle[i,j] != grid[i,j])
                {
                    return false;
                }
            }
        }
        return true;
    }
}
[System.Serializable]
public class Grid
{
    public List<Transform> GridList = new List<Transform>();
}