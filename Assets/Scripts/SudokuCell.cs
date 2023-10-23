using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuCell : MonoBehaviour
{
    Board board;
    int row;
    int col;
    int value;
    public Image I;

    public void SetValues(int _row, int _col, int value, Board _board,Sprite _sprite)
    {
        row = _row;
        col = _col; 
        board = _board;
        I.sprite = _sprite;
        if (value != 0)GetComponentInParent<Button>().enabled = false;
    }

    public void ButtonClicked()
    {
        InputButton.instance.ActivateInputButton(this);
    }

    public void UpdateValue(int newValue, Sprite _sprite)
    {
        value = newValue;
        I.sprite = _sprite;
        board.UpdatePuzzle(row, col, value);
    }
}
