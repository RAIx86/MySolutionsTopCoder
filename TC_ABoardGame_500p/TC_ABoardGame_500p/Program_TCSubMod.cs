// TopCoder - Problem Name: ABoardGame(500 points) [MEDIUM]
// Class Name : ABoardGame
// Method Name : whoWins
// Return Type : string
// Arg Types : (string[])

// Status on TopCoder: Compiled Successfully; Cleared Initial/Sample Tests; Cleared Wider System Tests
// TopCoder Link(see link for Problem Statement): https://arena.topcoder.com/#/u/practiceCode/15895/38490/13033/1/320754

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace TC_ABoardGame_500p
//{
    //class TC_GlobalConfig
    //{ public static bool b_TDSVerbose = false; }

    class ABoardGame
    {
        public string whoWins(string[] board)
        {
            int numelems = board.Count();
            int numregs = numelems / 2;

            //if (TC_GlobalConfig.b_TDSVerbose) Console.WriteLine("ABoardGame.whoWins: BEGINS --> [numelems | numregs] = [" + numelems + " | " + numregs + "]");

            for (int curregnum = 1, TLx_index = (numelems / 2 - 1), TLy_index = (numelems / 2 - 1); TLx_index >= 0 && TLy_index >= 0 && curregnum <= numregs; TLx_index--, TLy_index--, curregnum++)
            {
                string curregres = inspectRegion(board, curregnum, TLx_index, TLy_index);
                if (curregres != "Draw")
                {
                    //if (TC_GlobalConfig.b_TDSVerbose) Console.WriteLine("ABoardGame.whoWins: Definitve-Winner found in Current-Region #" + curregnum + ". Returning ...");
                    return curregres;
                }
            }

            //if (TC_GlobalConfig.b_TDSVerbose) Console.WriteLine("ABoardGame.whoWins: No Definitive Winner found in ANY/ALL Regions. Returning ...");
            return "Draw";
        }

        public string inspectRegion(string[] board, int curregnum, int TLx_index, int TLy_index)
        {
            //if (TC_GlobalConfig.b_TDSVerbose) Console.WriteLine("ABoardGame.inspectRegion: inspectRegion(curregnum=" + curregnum + ", TLx_index=" + TLx_index + ", TLy_index=" + TLy_index + ") ... BEGINS -->");
            int curreg_alicecount = 0, curreg_bobcount = 0;
            for (int i = TLx_index; i < (TLx_index + 2 * curregnum); i++)
            {
                for (int j = TLy_index; j < (TLy_index + 2 * curregnum); j++)
                {
                    if ((i != TLx_index && i != ((TLx_index + 2 * curregnum) - 1)) && (j != TLy_index && j != ((TLy_index + 2 * curregnum) - 1)))
                    {
                        //if (TC_GlobalConfig.b_TDSVerbose) Console.WriteLine("ABoardGame.inspectRegion: [i,j]=[" + i + "," + j + "] index-pair encountered which belongs to Previous/Lower Region-Numbers. Disregarding for Current-Region #" + curregnum + ".");
                        continue;
                    }

                    char cur_ij_boardelem = board[i].ElementAt(j);
                    if (cur_ij_boardelem == 'A')
                        curreg_alicecount++;
                    else if (cur_ij_boardelem == 'B')
                        curreg_bobcount++;
                }
            }

            //if (TC_GlobalConfig.b_TDSVerbose) Console.WriteLine("ABoardGame.inspectRegion: [curreg_alicecount | curreg_bobcount] = [" + curreg_alicecount + " | " + curreg_bobcount + "]");

            if (curreg_alicecount > curreg_bobcount)
                return "Alice";
            if (curreg_bobcount > curreg_alicecount)
                return "Bob";
            return "Draw";
        }
    }   // END_CLASS: "ABoardGame"

    //public class Autotest_ABoardGame
    //{
    //    public System.Random PRGObj = new System.Random();
    //    public const int MIN_NUM_TESTS = 10, MAX_NUM_TESTS = 100;
    //    public string possbrdelemvals = "A.B";
    //    public string[] generateRandomBoard()
    //    {
    //        int numregs = PRGObj.Next(1, 25);
    //        string[] board = new string[2 * numregs];
    //        for (int i = 0; i < (2 * numregs); i++)
    //        {
    //            string currow = "";
    //            for (int j = 0; j < (2 * numregs); j++)
    //                currow += possbrdelemvals[PRGObj.Next() % 3];
    //            board[i] = currow;
    //        }
    //        return board;
    //    }

    //    public void autoRandTestOnce()
    //    {
    //        string[] board = generateRandomBoard();
    //        ABoardGame ABGObj = new ABoardGame();
    //        string ABGResult = ABGObj.whoWins(board);
    //        displayBoard(board);
    //        Console.WriteLine("Autotest_ABoardGame.autoRandTestOnce: ABGResult = >" + ABGResult + "<");
    //        Console.WriteLine();
    //    }

    //    public void autoRandTestMultiple()
    //    {
    //        int numtests = PRGObj.Next(MIN_NUM_TESTS, MAX_NUM_TESTS);
    //        Console.WriteLine("Autotest_ABoardGame.autoRandTestMultiple: Running " + numtests + " tests with random data");
    //        for (int i = 0; i < numtests; i++)
    //        {
    //            Console.WriteLine("TEST #" + (i + 1) + " BEGIN -->");
    //            autoRandTestOnce();
    //            Console.WriteLine("<-- END TEST #" + (i + 1));
    //            Console.WriteLine(); Console.WriteLine();
    //        }
    //    }

    //    public void displayBoard(string[] board)
    //    {
    //        int numelems = board.Count();
    //        Console.WriteLine("Autotest_ABoardGame.displayBoard: board: [numelems | numregs] = [" + numelems + " | " + numelems / 2 + "]");
    //        Console.WriteLine("Autotest_ABoardGame.displayBoard: board BEGINS -->");
    //        for (int i = 0; i < numelems; i++)
    //        {
    //            for (int j = 0; j < numelems; j++)
    //            {
    //                Console.Write(board[i].ElementAt(j));
    //                if (j != (numelems - 1))
    //                    Console.Write("  ");
    //            }
    //            Console.WriteLine("");
    //        }
    //        Console.WriteLine("Autotest_ABoardGame.displayBoard: <-- ENDS board");
    //    }

    //    public void autoSpecificTests()
    //    {
    //        ABoardGame ABGObj = new ABoardGame();
    //        string[] board1 = { ".....A", "......", "..A...", "...B..", "......", "......" };
    //        string[] board2 = { "AAAA", "A.BA", "A..A", "AAAA" };
    //        string[] board3 = { "..", ".." };
    //        string[] board4 = { "BBB..BAB...B.B", ".AAAAAAAAAAAA.", "AA.AA.AB..A.AB", "..........B.AB", ".A..BBAB.A.BAB", ".AB.B.......A.", ".A..A.AB.A..AB", ".ABAA.BA...BA.", "BAAAB.....ABA.", ".A....B..A..B.", "B...B....B..A.", "BA.B..A.ABA.A.", "BAAAA.AAAAA.A.", "B.B.B.BB.B...." };
    //        string[] board5 = { "..A..AAA..AA", "ABABB..AAAAA", "ABBBBBBBBBA.", "AABBBABABBAA", "...BABABABBA", "B.BA..A.BBA.", "AA.A..B.AB.B", "..BA.B.AABAA", "..ABABBBABA.", ".ABB.BBBBBAA", "ABAAA.AA.A.A", "A..AAA.AAA.A" };
    //        string[] board6 = { "B..ABAABBB", "B.........", "A..A.AA..B", "A.BBBAA..A", "B.AAAAB...", "A..BBBBB.A", "B..ABAABBA", "A......B.B", "B......A.A", "BA.AABBB.A" };
    //        string[][] boards = { board1, board2, board3, board4, board5, board6 };
    //        Console.WriteLine("Autotest_ABoardGame.autoSpecificTests: boards.Count() = >" + boards.Count() + "< ... boards.Length = >" + boards.Length + "<");

    //        for (int i = 0; i < boards.Count(); i++)
    //        {
    //            displayBoard(boards[i]);
    //            string ABGResult = ABGObj.whoWins(boards[i]);
    //            Console.WriteLine("Autotest_ABoardGame.autoSpecificTests: ABGResult = >" + ABGResult + "<"); Console.WriteLine("");
    //        }
    //    }
    //}   // END_CLASS: "Autotest_ABoardGame"

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Autotest_ABoardGame AT_ABG_Obj = new Autotest_ABoardGame();
    //        Console.WriteLine("autotestOnce Standalone Test BEGINS -->");
    //        AT_ABG_Obj.autoRandTestOnce();
    //        Console.WriteLine("<-- ENDS Test Standalone autotestOnce");
    //        Console.WriteLine(""); Console.WriteLine(""); Console.WriteLine("");
    //        AT_ABG_Obj.autoRandTestMultiple();
    //        Console.WriteLine(""); Console.WriteLine(""); Console.WriteLine("");
    //        AT_ABG_Obj.autoSpecificTests();
    //    }
    //}
//}   // END_NAMESPACE: "TC_ABoardGame"
