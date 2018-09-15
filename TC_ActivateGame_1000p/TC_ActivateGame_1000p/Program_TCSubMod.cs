// TopCoder - Problem Name: ActivateGame(1000 points) [HARD]

// Class Name : ActivateGame
// Method Name : findMaxScore
// Return Type : int
// Arg Types : (string[])

// Status on TopCoder: Compiled Successfully; Cleared Initial/Sample Tests; Cleared Wider System Tests
// TopCoder Link(see link for Problem Statement): https://arena.topcoder.com/#/u/practiceCode/14302/10796/10750/2/304636

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

//namespace TC_ActivateGame_1000p
//{
    //class GlobConf_ActGame
    //{
    //    public static bool b_TDSVerbose = false;
    //}

    class ActivateGame
    {
        private bool[][] b_activated_map;
        private int i_activated_count = 0;
        private int numrows = 0, numcols = 0;

        private int findNumForChar(char ch)
        {
            int numforchar = 0;
            try
            {
                if (ch >= '0' && ch <= '9')
                {
                    numforchar = ch - '0';
                    Debug.Assert(numforchar >= 0 && numforchar <= 9);
                }
                else if (ch >= 'a' && ch <= 'z')
                {
                    numforchar = (ch - 'a') + 10;
                    Debug.Assert(numforchar >= 10 && numforchar <= 35);
                }
                else if (ch >= 'A' && ch <= 'Z')
                {
                    numforchar = (ch - 'A') + 36;
                    Debug.Assert(numforchar >= 36 && numforchar <= 61);
                }
            }
            catch (Exception excep) { Console.WriteLine("ActivateGame.findNumForChar: Exception occured! excep = >" + excep.ToString() + "< ... Details/Stack-Trace: >" + excep.StackTrace + "<"); Console.WriteLine(""); }
            //if (GlobConf_ActGame.b_TDSVerbose) Console.WriteLine("ActivateGame.findNumForChar: ch = >" + ch + "< ... numforchar = >" + numforchar + "<");
            return numforchar;
        }

        private bool AllActivated()
        {
            //for (int i = 0; i < numrows; i++)
            //    if (b_activated_map[i].Contains(false))
            //        return false;
            //return true;
            if (i_activated_count < numrows * numcols)
                return false;
            return true;
        }

        private class MaxScoreJump
        {
            public int src_row, src_col;
            public int tgt_row, tgt_col;
            public int max_score_value;

            public MaxScoreJump() { }

            public MaxScoreJump(int src_row, int src_col, int tgt_row, int tgt_col, int max_score_value)
            {
                this.src_row = src_row; this.src_col = src_col;
                this.tgt_row = tgt_row; this.tgt_col = tgt_col;
                this.max_score_value = max_score_value;
            }
        }

        private MaxScoreJump findMaxScoreJumpForCurrentIndex(string[] grid, int src_row, int src_col)
        {
            int max_score_tgt_row = 0, max_score_tgt_col = 0;
            int maxScoreForThisGridElem = int.MinValue;
            try
            {
                // Four possibilities (Two Horizontal + Two Vertical); No diagonal movement allowed
                // [src_row + 1 , src_col] or [src_row - 1 , src_col] or [src_row , src_col + 1] or [src_row , src_col - 1]
                if (src_row + 1 < numrows)
                {
                    // First we must check whether the Grid-Element at [src_row + 1 , src_col] is already activated. If so, we can't consider it.
                    if (!b_activated_map[src_row + 1][src_col])
                    {
                        // Calculate Score-Diff b/w elems at [src_row , src_col] & [src_row + 1 , src_col]. Updated Max-Score if applicable
                        int diff_between_gridelems = Math.Abs(findNumForChar(grid[src_row][src_col]) - findNumForChar(grid[src_row + 1][src_col]));
                        if (diff_between_gridelems > maxScoreForThisGridElem)
                        {
                            maxScoreForThisGridElem = diff_between_gridelems;
                            max_score_tgt_row = src_row + 1;
                            max_score_tgt_col = src_col;
                        }
                    }
                }
                if (src_row - 1 >= 0)
                {
                    // First we must check whether the Grid-Element at [src_row + 1 , src_col] is already activated. If so, we can't consider it.
                    if (!b_activated_map[src_row - 1][src_col])
                    {
                        // Calculate Score-Diff b/w elems at [src_row , src_col] & [src_row + 1 , src_col]. Updated Max-Score if applicable
                        int diff_between_gridelems = Math.Abs(findNumForChar(grid[src_row][src_col]) - findNumForChar(grid[src_row - 1][src_col]));
                        if (diff_between_gridelems > maxScoreForThisGridElem)
                        {
                            maxScoreForThisGridElem = diff_between_gridelems;
                            max_score_tgt_row = src_row - 1;
                            max_score_tgt_col = src_col;
                        }
                    }
                }
                if (src_col + 1 < numcols)
                {
                    // First we must check whether the Grid-Element at [src_row + 1 , src_col] is already activated. If so, we can't consider it.
                    if (!b_activated_map[src_row][src_col + 1])
                    {
                        // Calculate Score-Diff b/w elems at [src_row , src_col] & [src_row + 1 , src_col]. Updated Max-Score if applicable
                        int diff_between_gridelems = Math.Abs(findNumForChar(grid[src_row][src_col]) - findNumForChar(grid[src_row][src_col + 1]));
                        if (diff_between_gridelems > maxScoreForThisGridElem)
                        {
                            maxScoreForThisGridElem = diff_between_gridelems;
                            max_score_tgt_row = src_row;
                            max_score_tgt_col = src_col + 1;
                        }
                    }
                }
                if (src_col - 1 >= 0)
                {
                    // First we must check whether the Grid-Element at [src_row + 1 , src_col] is already activated. If so, we can't consider it.
                    if (!b_activated_map[src_row][src_col - 1])
                    {
                        // Calculate Score-Diff b/w elems at [src_row , src_col] & [src_row + 1 , src_col]. Updated Max-Score if applicable
                        int diff_between_gridelems = Math.Abs(findNumForChar(grid[src_row][src_col]) - findNumForChar(grid[src_row][src_col - 1]));
                        if (diff_between_gridelems > maxScoreForThisGridElem)
                        {
                            maxScoreForThisGridElem = diff_between_gridelems;
                            max_score_tgt_row = src_row;
                            max_score_tgt_col = src_col - 1;
                        }
                    }
                }
            }
            catch (Exception excep) { Console.WriteLine("ActivateGame.findMaxScoreJumpForCurrentIndex: Exception occured! excep = >" + excep.ToString() + "< ... Details/Stack-Trace: >" + excep.StackTrace + "<"); Console.WriteLine(""); }
            //if (GlobConf_ActGame.b_TDSVerbose) Console.WriteLine("ActivateGame.findMaxScoreJumpForCurrentIndex: [src_row, src_col, max_score_tgt_row, max_score_tgt_col, maxScoreForThisGridElem] = [" + src_row + ", " + src_col + ", " + max_score_tgt_row + ", " + max_score_tgt_col + ", " + maxScoreForThisGridElem + "]");
            return new MaxScoreJump(src_row, src_col, max_score_tgt_row, max_score_tgt_col, maxScoreForThisGridElem);
        }

        public int findMaxScore(string[] grid)
        {
            int overallMaxScore = 0;
            if (grid.Count() == 0)
            {
                //if (GlobConf_ActGame.b_TDSVerbose) Console.WriteLine("ActivateGame.findMaxScore: Grid is Empty. Terminating ...");
                return 0;
            }

            try
            {
                numrows = grid.Count(); numcols = grid[0].Count();

                // Forming a Bitmap of Activation across Grid-Cells. It's an array of strings, same as Grid
                // with each element being either 'T' or 'F' - representing True or False
                b_activated_map = new bool[numrows][];
                for (int i = 0; i < numrows; i++)
                {
                    b_activated_map[i] = new bool[numcols];
                    for (int j = 0; j < numcols; j++)
                        b_activated_map[i][j] = false;
                }

                // Initially, only the top-left cell (row 0, column 0) is activated, and your score is zero.
                b_activated_map[0][0] = true;
                i_activated_count = 1;
                //if (GlobConf_ActGame.b_TDSVerbose) displayGridBitmap(grid, true);

                while (!AllActivated())
                {
                    // Go through All Currently Activated Cells, and Explore Horizontally & Vertically across for each Activated Cell
                    // Keep calculating Scores, and Max the Score for each activated cell, then Max across all activated cell.
                    // That will be our next move, and hence Next Activated Cell
                    List<MaxScoreJump> List_MaxScoreJump_Obj = new List<MaxScoreJump>();
                    int all_act_max_score = int.MinValue, all_act_max_score_list_index = 0;
                    for (int i = 0; i < numrows; i++)
                    {
                        for (int j = 0; j < numcols; j++)
                        {
                            if (b_activated_map[i][j])
                            {
                                // We have found a Currently Active Cell at Index (i,j) - which is worth exploring.
                                // Now we move Vertically & Horizontally, Calculate Scores & Maximize for this Currently Active Cell.
                                // And we add it into a List of MaxScoreJumps obtained from other possible Currently-Active-Cells
                                MaxScoreJump cur_max_score_jump = findMaxScoreJumpForCurrentIndex(grid, i, j);
                                List_MaxScoreJump_Obj.Add(cur_max_score_jump);
                                if (cur_max_score_jump.max_score_value > all_act_max_score)
                                {
                                    all_act_max_score = cur_max_score_jump.max_score_value;
                                    all_act_max_score_list_index = (List_MaxScoreJump_Obj.Count() - 1);
                                }
                            }
                        }
                    }

                    // Obtain the MaxScoreJump across ALL Currently-Active-Cells - this is the Best move possible.
                    MaxScoreJump MSJ_AcrossAllActiveCells_Obj = List_MaxScoreJump_Obj[all_act_max_score_list_index];
                    Debug.Assert(all_act_max_score == MSJ_AcrossAllActiveCells_Obj.max_score_value);
                    // Indicate that the Grid-Element at Index (tgt_row,tgt_col) is now being activated.
                    b_activated_map[MSJ_AcrossAllActiveCells_Obj.tgt_row][MSJ_AcrossAllActiveCells_Obj.tgt_col] = true;
                    // Increment Activate Count & Updated Overall-Max-Score
                    i_activated_count++; overallMaxScore += MSJ_AcrossAllActiveCells_Obj.max_score_value;
                    //if (GlobConf_ActGame.b_TDSVerbose) displayGridBitmap(grid, true);
                }
            }
            catch (Exception excep) { Console.WriteLine("ActivateGame.findMaxScore: Exception occured! excep = >" + excep.ToString() + "< ... Details/Stack-Trace: >" + excep.StackTrace + "<"); Console.WriteLine(""); }

            return overallMaxScore;
        }

        //public void displayGridBitmap(string[] grid, bool dispbit)
        //{
        //    try
        //    {
        //        if (grid.Count() == 0)
        //        {
        //            if (GlobConf_ActGame.b_TDSVerbose) Console.WriteLine("ActivateGame.findMaxScore: Grid is Empty. Terminating ...");
        //            return;
        //        }
        //        //if (numrows == 0) numrows = grid.Count();
        //        //if (numcols == 0) numcols = grid[0].Count();
        //        for (int i = 0; i < grid.Count(); i++)
        //        {
        //            Console.WriteLine("ActivateGame.displayGridBitmap: grid[" + i + "] = " + grid[i]); Console.WriteLine("");
        //            if (dispbit)
        //            {
        //                for (int j = 0; j < grid[i].Count(); j++)
        //                {
        //                    Console.Write(b_activated_map[i][j]);
        //                    if (j != numcols - 1)
        //                        Console.Write("   ");
        //                }
        //                Console.WriteLine("");
        //            }
        //        }
        //    }
        //    catch (Exception excep) { Console.WriteLine("ActivateGame.findMaxScore: Exception occured! excep = >" + excep.ToString() + "< ... Details/Stack-Trace: >" + excep.StackTrace + "<"); Console.WriteLine(""); }
        //}
    }

    //class AutoTest_ActivateGame
    //{
    //    private static Random PRGObj = new Random((int)System.DateTime.UtcNow.Ticks);
    //    private const int MIN_NUM_TESTS = 10, MAX_NUM_TESTS = 20;
    //    private const int MIN_NUM_ROWCOLS = 5, MAX_NUM_ROWCOLS = 20;
    //    private static ActivateGame ActGameObj = new ActivateGame();

    //    public string[] generateRandomGrid()
    //    {
    //        string[] charset = { "0123456789", "abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSRUVWXYZ" };
    //        int numrows = PRGObj.Next(MIN_NUM_ROWCOLS, MAX_NUM_ROWCOLS);
    //        int numcols = PRGObj.Next(MIN_NUM_ROWCOLS, MAX_NUM_ROWCOLS);
    //        string[] grid = new string[numrows];
    //        for (int i = 0; i < numrows; i++)
    //        {
    //            grid[i] = "";
    //            for (int j = 0; j < numcols; j++)
    //            {
    //                int cs_row = PRGObj.Next(0, 2) % 3;
    //                int cs_col = PRGObj.Next(0, charset[cs_row].Count() - 1) % charset[cs_row].Count();
    //                grid[i] += charset[cs_row][cs_col];
    //            }
    //        }
    //        return grid;
    //    }

    //    public void AutoRandSingleTest()
    //    {
    //        string[] grid = generateRandomGrid();
    //        Console.WriteLine("AutoTest_ActivateGame.AutoRandSingleTest: grid = >>");
    //        ActGameObj.displayGridBitmap(grid, false);
    //        Console.WriteLine("AutoTest_ActivateGame.AutoRandSingleTest: grid = <<");
    //        Console.WriteLine("AutoTest_ActivateGame.AutoRandSingleTest: Result = >" + ActGameObj.findMaxScore(grid) + "<");
    //        Console.WriteLine(""); Console.WriteLine("");
    //    }

    //    public void AutoRandMultiTest()
    //    {
    //        int numtests = PRGObj.Next(MIN_NUM_TESTS, MAX_NUM_TESTS);
    //        for (int i = 0; i < numtests; i++)
    //        {
    //            Console.WriteLine("AutoTest_ActivateGame.AutoRandMultiTest: Conducting Test#" + (i + 1) + " -->");
    //            AutoRandSingleTest();
    //            Console.WriteLine("AutoTest_ActivateGame.AutoRandMultiTest: Test#" + (i + 1) + " <-- END.");
    //            Console.WriteLine(""); Console.WriteLine("");
    //        }
    //    }

    //    public void AutoTest_SpecificCases()
    //    {
    //        string[] grid1 = { "05", "aB" };
    //        string[] grid2 = { "03", "21" };
    //        string[] grid3 = { "John", "Brus", "Gogo" };
    //        string[] grid4 = { "AAA", "AAA", "AAA" };
    //        string[][] allgrids = { grid1, grid2, grid3, grid4 };

    //        for (int i = 0; i < allgrids.Count(); i++)
    //        {
    //            Console.WriteLine("AutoTest_ActivateGame.AutoTest_SpecificCases: i=" + i + " ... Result = >" + ActGameObj.findMaxScore(allgrids[i]) + "<");
    //            Console.WriteLine(""); Console.WriteLine("");
    //        }
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        AutoTest_ActivateGame AutoTest_ActGame_Obj = new AutoTest_ActivateGame();
    //        AutoTest_ActGame_Obj.AutoRandSingleTest();
    //        Console.WriteLine(""); Console.WriteLine("");
    //        AutoTest_ActGame_Obj.AutoRandMultiTest();
    //        Console.WriteLine(""); Console.WriteLine("");
    //        AutoTest_ActGame_Obj.AutoTest_SpecificCases();
    //        Console.WriteLine(""); Console.WriteLine("");
    //    }
    //}
//}   // END_NAMESPACE
