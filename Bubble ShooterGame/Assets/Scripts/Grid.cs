﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static int gridWidth = 13;
    public static int gridHeight = 19;
    public static Transform[,] grid = new Transform[gridWidth, gridHeight];
    public static Color[,] colorGrid = new Color[gridWidth, gridHeight];
    public static bool[,] checkedGrid = new bool[gridWidth, gridHeight];
    public static bool[,] anchoredGrid = new bool[gridWidth, gridHeight];
    public static int clusterCount;
    public static bool popOk, checkAnchorDone;
    public static ArrayList colorArray = new ArrayList();

    void Update()
    {
        ColorArray();
        if (popOk && clusterCount >= 3)
            PopCluster();

        if (colorArray.Count == 0)
            GameController.gameOver = true;
    }

    /*
	* Removes all bubbles of the bubble matrix
	*/
    public static void ClearGrid()
    {
        for (int i = 0; i < gridWidth; i++)
            for (int j = 0; j < gridHeight; j++)
            {
                grid[i, j] = null;
                colorGrid[i, j] = Color.clear;
                checkedGrid[i, j] = false;
            }
    }

    /*
	* Starts the anchor checker reaction
	*/
    public static void CAStarter()
    {
        checkAnchorDone = false;
        ResetChecked();

        for (int k = 0; k < gridWidth; k++)
            CheckAnchors(k, 17);

        for (int i = 0; i < gridWidth; i++)
            for (int j = 0; j <= 16; j++)
                if (!checkedGrid[i, j])
                    anchoredGrid[i, j] = false;

        checkAnchorDone = true;

    }

    /*
	* Removes all checked flags and resets cluster count
	*/
    public static void ResetChecked()
    {
        for (int i = 0; i < gridWidth; i++)
            for (int j = 0; j < gridHeight; j++)
                checkedGrid[i, j] = false;

        clusterCount = 0;
    }

    /*
	* Flags all bubbles in related cluster for popping
	*/
    public static void PopCluster()
    {
        for (int i = 0; i < gridWidth; i++)
            for (int j = 0; j < gridHeight; j++)
                if (checkedGrid[i, j] == true)
                {
                    colorGrid[i, j] = Color.clear;
                    grid[i, j] = null;
                }
        ResetChecked();
    }

    /*
	 * Generate a list of colors active on current level.
	 */
    public static void ColorArray()
    {
        colorArray.Clear();

        for (int i = 0; i < gridWidth; i++)
            for (int j = 0; j < gridHeight; j++)
                if (colorGrid[i, j] != Color.clear && !colorArray.Contains(colorGrid[i, j]))
                {
                    colorArray.Add(colorGrid[i, j]);
                }
    }

    /*
	* Checks the bubles in the matrix attached to the initial bubble. With every bubble of the same color
	* connected is checked against its neighbors. The chain is then flagged for popping.
	* @param {int, int} location of bubble in the chain
	* @param {color} color of bubble chain
	*/
    public static void ChainReaction(int x, int y, Color color)
    {
        if (x >= 0 || x <= 12)
        {
            checkedGrid[x, y] = true;
            if (checkedGrid[x, y] == true)
            {
                clusterCount += 1;
                popOk = false;
            }
            // if row is even
            if (y % 2 == 0 && x != 12)
            {
                // non edge
                if (x != 0 && x != 11)
                {
                    CRHelper(x, y + 1, color);
                    CRHelper(x + 1, y + 1, color);
                    CRHelper(x - 1, y, color);
                    CRHelper(x + 1, y, color);
                    CRHelper(x, y - 1, color);
                    CRHelper(x + 1, y - 1, color);
                }
                // left edge
                else if (x == 0)
                {
                    CRHelper(x, y + 1, color);
                    CRHelper(x + 1, y + 1, color);
                    CRHelper(x + 1, y, color);
                    CRHelper(x, y - 1, color);
                    CRHelper(x + 1, y - 1, color);
                }
                // right edge
                else if (x == 11)
                {
                    CRHelper(x, y + 1, color);
                    CRHelper(x + 1, y + 1, color);
                    CRHelper(x - 1, y, color);
                    CRHelper(x, y - 1, color);
                    CRHelper(x + 1, y - 1, color);
                }
            }
            else
            {
                // top row and non edge
                if (y == 17 && x != 0 && x != 12)
                {
                    CRHelper(x - 1, y, color);
                    CRHelper(x + 1, y, color);
                    CRHelper(x - 1, y - 1, color);
                    CRHelper(x, y - 1, color);
                }
                // top row and left edge
                else if (y == 17 && x == 0)
                {
                    CRHelper(x + 1, y, color);
                    CRHelper(x, y - 1, color);
                }
                // top row and right edge
                else if (y == 17 && x == 12)
                {
                    CRHelper(x - 1, y, color);
                    CRHelper(x - 1, y - 1, color);
                }
                // not top row or on edge
                else if (y != 17 && x != 0 && x != 12)
                {
                    CRHelper(x - 1, y + 1, color);
                    CRHelper(x, y + 1, color);
                    CRHelper(x - 1, y, color);
                    CRHelper(x + 1, y, color);
                    CRHelper(x - 1, y - 1, color);
                    CRHelper(x, y - 1, color);
                }
                // not top and left edge
                else if (y != 17 && x == 0)
                {
                    CRHelper(x, y + 1, color);
                    CRHelper(x + 1, y, color);
                    CRHelper(x, y - 1, color);
                }
                // not top and right edge
                else if (y != 17 && x == 12)
                {
                    CRHelper(x - 1, y + 1, color);
                    CRHelper(x, y + 1, color);
                    CRHelper(x - 1, y, color);
                    CRHelper(x - 1, y - 1, color);
                    CRHelper(x, y - 1, color);
                }
            }
        }
    }

    /*
	* helper class to handle calls for each chain reaction statement
	* @param {int, int} location of bubble in the chain
	* @param {color} color of bubble chain
	*/
    static void CRHelper(int x, int y, Color color)
    {
        if (colorGrid[x, y] == color && !checkedGrid[x, y])
        {
            // if the following location is not checked continue chain
            ChainReaction(x, y, color);
            Debug.Log("checked " + x + ", " + y + ", " + color);
        }
        popOk = true;
    }

    /*
	* Starting from the top all bubles are checked to see if they are anchored through
	* a chain that links to the top
	* @param {int, int} location of bubble in the chain
	*/
    public static void CheckAnchors(int x, int y)
    {
        if (x >= 0 || x <= 12)
        {
            if (grid[x, y] != null)
            {
                checkedGrid[x, y] = true;
                anchoredGrid[x, y] = true;

                // if row is even
                if (y % 2 == 0 && x != 12)
                {
                    // non edge
                    if (x != 0 && x != 11)
                    {
                        CAHelper(x, y - 1);
                        CAHelper(x + 1, y - 1);
                        CAHelper(x + 1, y);
                        CAHelper(x - 1, y);
                    }
                    // left edge
                    if (x == 0)
                    {
                        CAHelper(x, y - 1);
                        CAHelper(x + 1, y - 1);
                        CAHelper(x + 1, y);
                    }
                    // right edge
                    if (x == 11)
                    {
                        CAHelper(x, y - 1);
                        CAHelper(x + 1, y - 1);
                        CAHelper(x - 1, y);
                    }
                }
                else
                {
                    // top row and non edge
                    if (x != 0 && x != 12)
                    {
                        CAHelper(x - 1, y - 1);
                        CAHelper(x, y - 1);
                        CAHelper(x + 1, y);
                        CAHelper(x - 1, y);
                    }
                    // top row and left edge
                    if (x == 0)
                    {
                        CAHelper(x, y - 1);
                        CAHelper(x + 1, y);
                    }
                    // top row and right edge
                    if (x == 12)
                    {
                        CAHelper(x - 1, y - 1);
                        CAHelper(x - 1, y);
                    }
                }
            }
        }
    }

    /*
	* helper class to handle calls for each statement
	* @param {int, int} location of bubble in the chain
	*/
    static void CAHelper(int x, int y)
    {
        if (grid[x, y] != null && !checkedGrid[x, y])
        {
            // if the following location has a ball and is not checked continue chain
            CheckAnchors(x, y);
        }
    }

}
