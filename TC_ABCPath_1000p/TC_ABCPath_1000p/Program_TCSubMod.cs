// TopCoder - Problem Name: ABCPath(1000 points) [HARD]
// Class Name : ABCPath
// Method Name : length
// Return Type : int
// Arg Types : (string[])

// Status on TopCoder: Compiled Successfully; Cleared Initial/Sample Tests; Didn't clear Wider System Tests
// TopCoder Link(see link for Problem Statement): https://arena.topcoder.com/#/u/practiceCode/1527/4329/4678/2/1527

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace TC_ABCPath_1000p
//{
    //static class GlobalConfig_ABCPath
    //{
    //    public static bool b_TDSVerbose = false;
    //}
    class ABCPath
    {
        public class A_indices
        {
            public int A_xindex, A_yindex;
            public A_indices() { this.A_xindex = 0; this.A_yindex = 0; }
            public A_indices(int A_xindex, int A_yindex) { this.A_xindex = A_xindex; this.A_yindex = A_yindex; }
        }

        private string[] cgrid;
        private static int cur_index_maxpathlen_recbcktrck = 1;
        public int length(string[] grid)
        {
            // Copying over passed argument "grid" into class-member "cgrid" - this is so as to avoid/minimize overhead on Function-Stack during Recursion
            this.cgrid = new string[grid.Count()];
            for (int i = 0; i < grid.Count(); i++)
            {
                this.cgrid[i] = grid[i];
                //if (GlobalConfig_ABCPath.b_TDSVerbose) Console.WriteLine("grid[" + i + "] = >" + grid[i] + "<");
            }

            int global_maxpathlen = 0;
            for (int i = 0; i < grid.Count(); i++)
            {
                for (int j = 0; j < grid[i].Count(); j++)
                {
                    if (grid[i].ElementAt(j) == 'A')
                    {
                        int cur_index_maxpathlen = SeekMaxPathFromIndex(i, j, 1);
                        if (cur_index_maxpathlen > global_maxpathlen)
                        {
                            //if (GlobalConfig_ABCPath.b_TDSVerbose) Console.WriteLine("ABCPath.length: New Global-Max Found! Updating Global-Max-Path-Len. [Prev|New] = [" + global_maxpathlen + "|" + cur_index_maxpathlen + "]");
                            global_maxpathlen = cur_index_maxpathlen;
                        }
                    }
                }
            }
            return global_maxpathlen;
        }

        private int SeekMaxPathFromIndex(int i, int j, int stepdepth)
        {
            // Resetting the Max-Path-Len counter for *this/current* index back to 1
            cur_index_maxpathlen_recbcktrck = 1;
            // Obtaining new value for Max-Path-Len counter for *this/current* index through Recursion/Backtracking
            cur_index_maxpathlen_recbcktrck = SeekPathFromIndex(i, j, stepdepth);
            return cur_index_maxpathlen_recbcktrck;
        }

        private int SeekPathFromIndex(int i, int j, int stepdepth)
        {
            List<A_indices> possnextmoves = PossibleNextMoves(i, j);
            if (possnextmoves.Count == 0)
            {
                // No valid/possible next move - Returning the 'stepdepth' at this point ...
                //if (GlobalConfig_ABCPath.b_TDSVerbose) Console.WriteLine("ABCPath.SeekPathFromIndex(" + i + ", " + j + ", " + stepdepth + "): No valid/possible next move for [i,j]=[" + i + "," + j + "]! Returning 'stepdepth'=" + stepdepth + " at this point");
                return stepdepth;
            }

            for (int k = 0; k < possnextmoves.Count(); k++)
            {
                int cur_index_curpathlen_tmp = SeekPathFromIndex(possnextmoves[k].A_xindex, possnextmoves[k].A_yindex, stepdepth + 1);
                if (cur_index_curpathlen_tmp > cur_index_maxpathlen_recbcktrck)
                {
                    //if (GlobalConfig_ABCPath.b_TDSVerbose) Console.WriteLine("ABCPath.SeekPathFromIndex(" + i + ", " + j + ", " + stepdepth + "): New Index-Max Found for [i,j]=[" + i + "," + j + "]! Updating Index-Max-Path-Len. [Prev|Next] = [" + cur_index_maxpathlen_recbcktrck + "|" + cur_index_curpathlen_tmp + "]");
                    cur_index_maxpathlen_recbcktrck = cur_index_curpathlen_tmp;
                }
            }
            // We've reached the end of this Recursive-Call - meaning we've exhausted all Backtracking Possibilities FROM THIS POINT
            // So we return back to potential/previous recursive calls - and we return the Max-Path-Length determined across all Backtracked Possibilities.
            //if (GlobalConfig_ABCPath.b_TDSVerbose) Console.WriteLine("ABCPath.SeekPathFromIndex(" + i + ", " + j + ", " + stepdepth + "): Reached end of this Rec-Call; Exhausted all Backtracking Possibilities FROM THIS PT; Return Max-Path-Leng across all Backtracked Possibilities");
            return cur_index_maxpathlen_recbcktrck;
        }

        private List<A_indices> PossibleNextMoves(int i, int j)
        {
            List<A_indices> possnextmoves = new List<A_indices>();

            // Given [i,j] - Traverse recursively/back-tracking in following manner:
            // {1}[i-1,j-1]             {2}[i-1,j]          {3}[i-1,j+1]
            // {4}[i,j-1]                   X               {5}[i, j+1]
            // {6}[i+1,j-1]             {7}[i+1,j]          {8}[i+1,j+1]

            if (cgrid[i].ElementAt(j) == 'Z')
            {
                //if (GlobalConfig_ABCPath.b_TDSVerbose) Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): ");
                return possnextmoves;
            }

            // Looking into all "i-1" cases - i.e. [i-1, j-1] [i-1, j] [i-1, j+1]
            if (i - 1 >= 0)
            {
                // Worth considering "i-1" cases (along with others).
                if (j - 1 >= 0)
                {
                    // Worth considering "j-1" cases (along with others).
                    if (cgrid[i - 1].ElementAt(j - 1) == cgrid[i].ElementAt(j) + 1)
                    {
                        // [i-1, j-1] element is immediate next alphabet as compared to [i, j] element - i.e. Valid Next Move - Hence Add.
                        //if (GlobalConfig_ABCPath.b_TDSVerbose)
                        //{
                        //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): grid[" + i + "][" + j + "]=>" + cgrid[i][j] + "< ... grid[" + (i - 1) + "][" + (j - 1) + "]=>" + cgrid[i - 1][j - 1] + "<");
                        //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): Valid Next Move Found! Adding [i-1, j-1]=[" + (i - 1) + ", " + (j - 1) + "]!");
                        //}
                        possnextmoves.Add(new A_indices(i - 1, j - 1));
                    }
                }

                // Considering "j" case.
                if (cgrid[i - 1].ElementAt(j) == cgrid[i].ElementAt(j) + 1)
                {
                    // [i-1, j] element is immediate next alphabet as compared to [i, j] element - i.e. Valid Next Move - Hence Add.
                    //if (GlobalConfig_ABCPath.b_TDSVerbose)
                    //{
                    //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): grid[" + i + "][" + j + "]=>" + cgrid[i][j] + "< ... grid[" + (i - 1) + "][" + j + "]=>" + cgrid[i - 1][j] + "<");
                    //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): Valid Next Move Found! Adding [i-1, j]=[" + (i - 1) + ", " + j + "]!");
                    //}
                    possnextmoves.Add(new A_indices(i - 1, j));
                }

                if (j + 1 < cgrid[i].Count())
                {
                    // Worth considering "j+1" cases (along with others).
                    if (cgrid[i - 1].ElementAt(j + 1) == cgrid[i].ElementAt(j) + 1)
                    {
                        // [i-1, j+1] element is immediate next alphabet as compared to [i, j] element - i.e. Valid Next Move - Hence Add.
                        //if (GlobalConfig_ABCPath.b_TDSVerbose)
                        //{
                        //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): grid[" + i + "][" + j + "]=>" + cgrid[i][j] + "< ... grid[" + (i - 1) + "][" + (j + 1) + "]=>" + cgrid[i - 1][j + 1] + "<");
                        //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): Valid Next Move Found! Adding [i-1, j+1]=[" + (i - 1) + ", " + (j + 1) + "]!");
                        //}
                        possnextmoves.Add(new A_indices(i - 1, j + 1));
                    }
                }
            }

            // Looking into all "i" cases - i.e. [i, j-1] [i, j+1].
            if (j - 1 >= 0)
            {
                // Worth considering "j-1" cases (along with others).
                if (cgrid[i].ElementAt(j - 1) == cgrid[i].ElementAt(j) + 1)
                {
                    // [i, j-1] element is immediate next alphabet as compared to [i, j] element - i.e. Valid Next Move - Hence Add.
                    //if (GlobalConfig_ABCPath.b_TDSVerbose)
                    //{
                    //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): grid[" + i + "][" + j + "]=>" + cgrid[i][j] + "< ... grid[" + i + "][" + (j - 1) + "]=>" + cgrid[i][j - 1] + "<");
                    //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): Valid Next Move Found! Adding [i, j-1]=[" + i + ", " + (j - 1) + "]!");
                    //}
                    possnextmoves.Add(new A_indices(i, j - 1));
                }
            }
            // SKIPPING "j" case ... (since [i,j] is nothing but current element itself)
            // ...
            if (j + 1 < cgrid[i].Count())
            {
                // Worth considering "j+1" cases (along with others).
                if (cgrid[i].ElementAt(j + 1) == cgrid[i].ElementAt(j) + 1)
                {
                    // [i, j+1] element is immediate next alphabet as compared to [i, j] element - i.e. Valid Next Move - Hence Add.
                    //if (GlobalConfig_ABCPath.b_TDSVerbose)
                    //{
                    //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): grid[" + i + "][" + j + "]=>" + cgrid[i][j] + "< ... grid[" + i + "][" + (j + 1) + "]=>" + cgrid[i][j + 1] + "<");
                    //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): Valid Next Move Found! Adding [i, j+1]=[" + i + ", " + (j + 1) + "]!");
                    //}
                    possnextmoves.Add(new A_indices(i, j + 1));
                }
            }

            // Looking into all "i+1" cases - i.e. [i+1, j-1] [i+1, j] [i+1, j+1]
            if (i + 1 < cgrid.Count())
            {
                // Worth considering "i+1" cases (along with others).
                if (j - 1 >= 0)
                {
                    // Worth considering "j-1" cases (along with others).
                    if (cgrid[i + 1].ElementAt(j - 1) == cgrid[i].ElementAt(j) + 1)
                    {
                        // [i+1, j-1] element is immediate next alphabet as compared to [i, j] element - i.e. Valid Next Move - Hence Add.
                        //if (GlobalConfig_ABCPath.b_TDSVerbose)
                        //{
                        //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): grid[" + i + "][" + j + "]=>" + cgrid[i][j] + "< ... grid[" + (i + 1) + "][" + (j - 1) + "]=>" + cgrid[i + 1][j - 1] + "<");
                        //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): Valid Next Move Found! Adding [i+1, j-1]=[" + (i + 1) + ", " + (j - 1) + "]!");
                        //}
                        possnextmoves.Add(new A_indices(i + 1, j - 1));
                    }
                }

                // Considering "j" case.
                if (cgrid[i + 1].ElementAt(j) == cgrid[i].ElementAt(j) + 1)
                {
                    // [i+1, j] element is immediate next alphabet as compared to [i, j] element - i.e. Valid Next Move - Hence Add.
                    //if (GlobalConfig_ABCPath.b_TDSVerbose)
                    //{
                    //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): grid[" + i + "][" + j + "]=>" + cgrid[i][j] + "< ... grid[" + (i + 1) + "][" + j + "]=>" + cgrid[i + 1][j] + "<");
                    //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): Valid Next Move Found! Adding [i+1, j]=[" + (i + 1) + ", " + j + "]!");
                    //}
                    possnextmoves.Add(new A_indices(i + 1, j));
                }

                if (j + 1 < cgrid[i].Count())
                {
                    // Worth considering "j+1" cases (along with others).
                    if (cgrid[i + 1].ElementAt(j + 1) == cgrid[i].ElementAt(j) + 1)
                    {
                        // [i+1, j+1] element is immediate next alphabet as compared to [i, j] element - i.e. Valid Next Move - Hence Add.
                        //if (GlobalConfig_ABCPath.b_TDSVerbose)
                        //{
                        //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): grid[" + i + "][" + j + "]=>" + cgrid[i][j] + "< ... grid[" + (i + 1) + "][" + (j + 1) + "]=>" + cgrid[i + 1][j + 1] + "<");
                        //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): Valid Next Move Found! Adding [i+1, j+1]=[" + (i + 1) + ", " + (j + 1) + "]!");
                        //}
                        possnextmoves.Add(new A_indices(i + 1, j + 1));
                    }
                }
            }

            //if (GlobalConfig_ABCPath.b_TDSVerbose)
            //{
            //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): possnextmoves == >");
            //    for (int k = 0; k < possnextmoves.Count(); k++)
            //        Console.WriteLine("possnextmoves[" + k + "].[A_xindex|A_yindex] = [" + possnextmoves[k].A_xindex + "|" + possnextmoves[k].A_yindex + "]");
            //    Console.WriteLine("ABCPath.PossibleNextMoves(" + i + ", " + j + "): < == possnextmoves");
            //}
            return possnextmoves;
        }
    }

    //class AutoTest_ABCPath
    //{
    //    private System.Random PRGObj1 = new System.Random();
    //    private const int MIN_NUM_TESTS = 10, MAX_NUM_TESTS = 100;
    //    private ABCPath ABCPath_Obj = new ABCPath();
    //    public string[] Generate_Random_Grid()
    //    {
    //        int numrows = PRGObj1.Next(1, 50);
    //        int numcols = PRGObj1.Next(1, 50);
    //        string charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    //        string[] grid = new string[numrows];
    //        for (int i = 0; i < numrows; i++)
    //        {
    //            grid[i] = "";
    //            for (int j = 0; j < numcols; j++)
    //                grid[i] += charset.ElementAt(PRGObj1.Next(0, 25));
    //            Console.WriteLine("AutoTest_ABCPath.Generate_Random_Grid: grid[" + i + "] = >" + grid[i] + "<");
    //        }
    //        return grid;
    //    }
    //    public void AutoRandomTest_ABCPath_Once()
    //    {

    //        Console.WriteLine("AutoTest_ABCPath.AutoRandomTest_ABCPath_Once: length = >" + ABCPath_Obj.length(Generate_Random_Grid()) + "<");
    //        Console.WriteLine(""); Console.WriteLine("");
    //    }

    //    public void AutoRandomTest_ABCPath_Multi()
    //    {
    //        int numtests = PRGObj1.Next(MIN_NUM_TESTS, MAX_NUM_TESTS);
    //        for (int i = 0; i < numtests; i++)
    //        {
    //            Console.WriteLine("AutoTest_ABCPath.AutoRandomTest_ABCPath_Multi: Test #" + (i + 1) + " ... BEGINS -->");
    //            AutoRandomTest_ABCPath_Once();
    //            Console.WriteLine("AutoTest_ABCPath.AutoRandomTest_ABCPath_Multi: Test #" + (i + 1) + " ... <-- ENDS");
    //            Console.WriteLine(""); Console.WriteLine("");
    //        }
    //    }

    //    public void AutoSelManTests_ABCPath_Multi()
    //    {
    //        string[] grid1 = { "ABE", "CFG", "BDH", "ABC" };
    //        string[] grid2 = { "A" };
    //        string[] grid3 = { "BCDEFGHIJKLMNOPQRSTUVWXYZ" };
    //        string[] grid4 = { "C", "D", "B", "A" };
    //        string[] grid5 = { "KCBVNRXSPVEGUEUFCODMOAXZYWEEWNYAAXRBKGACSLKYRVRKIO", "DIMCZDMFLAKUUEPMPGRKXSUUDFYETKYQGQHNFFEXFPXNYEFYEX", "DMFRPZCBOWGGHYAPRMXKZPYCSLMWVGMINAVRYUHJKBBRONQEXX", "ORGCBHXWMTIKYNLFHYBVHLZFYRPOLLAMBOPMNODWZUBLSQSDZQ", "QQXUAIPSCEXZTTINEOFTJDAOBVLXZJLYOQREADUWWSRSSJXDBV", "PEDHBZOVMFQQDUCOWVXZELSEBAMBRIKBTJSVMLCAABHAQGBWRP", "FUSMGCSCDLYQNIXTSTPJGZKDIAZGHXIOVGAZHYTMIWAIKPMHTJ", "QMUEDLXSREWNSMEWWRAUBFANSTOOJGFECBIROYCQTVEYGWPMTU", "FFATSKGRQJRIQXGAPLTSXELIHXOPUXIDWZHWNYUMXQEOJIAJDH", "LPUTCFHYQIWIYCVOEYHGQGAYRBTRZINKBOJULGYCULRMEOAOFP", "YOBMTVIKVJOSGRLKTBHEJPKVYNLJQEWNWARPRMZLDPTAVFIDTE", "OOBFZFOXIOZFWNIMLKOTFHGKQAXFCRZHPMPKGZIDFNBGMEAXIJ", "VQQFYCNJDQGJPYBVGESDIAJOBOLFPAOVXKPOVODGPFIYGEWITS", "AGVBSRLBUYOULWGFOFFYAAONJTLUWRGTYWDIXDXTMDTUYESDPK", "AAJOYGCBYTMXQSYSPTBWCSVUMNPRGPOEAVVBGMNHBXCVIQQINJ", "SPEDOAHYIDYUJXGLWGVEBGQSNKCURWYDPNXBZCDKVNRVEMRRXC", "DVESXKXPJBPSJFSZTGTWGAGCXINUXTICUCWLIBCVYDYUPBUKTS", "LPOWAPFNDRJLBUZTHYVFHVUIPOMMPUZFYTVUVDQREFKVWBPQFS", "QEASCLDOHJFTWMUODRKVCOTMUJUNNUYXZEPRHYOPUIKNGXYGBF", "XQUPBSNYOXBPTLOYUJIHFUICVQNAWFMZAQZLTXKBPIAKXGBHXX" };
    //        string[] grid6 = { "EDCCBA", "EDCCBA" };
    //        string[] grid7 = { "AMNOPA", "ALEFQR", "KDABGS", "AJCHUT", "AAIWVA", "AZYXAA" };
    //        string[][] grids = { grid1 , grid2 , grid3 , grid4 , grid5 , grid6 , grid7 };

    //        for (int i=0; i<grids.Count(); i++)
    //        {
    //            Console.WriteLine("AutoTest_ABCPath.AutoSelManTests_ABCPath_Multi: grids[" + i + "] = >" + grids[i].ToString() + "< ... length#" + i + " = >" + ABCPath_Obj.length(grids[i]) + "<");
    //            Console.WriteLine(""); Console.WriteLine("");
    //        }
    //    }
    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        AutoTest_ABCPath AT_ABCPath_Obj = new AutoTest_ABCPath();
    //        AT_ABCPath_Obj.AutoSelManTests_ABCPath_Multi();
    //        Console.WriteLine(""); Console.WriteLine("");
    //        AT_ABCPath_Obj.AutoRandomTest_ABCPath_Once();
    //        Console.WriteLine(""); Console.WriteLine("");
    //        AT_ABCPath_Obj.AutoRandomTest_ABCPath_Multi();
    //        Console.WriteLine(""); Console.WriteLine("");
    //    }
    //}
//}   // END_NAMESPACE
