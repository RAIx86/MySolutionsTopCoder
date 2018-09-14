// TopCoder - Problem Name: ANewHope(250 points) [EASY]
// Class Name : ANewHope
// Method Name : count
// Return Type : int
// Arg Types : (int[], int[], int)

// Status on TopCoder: Compiled Successfully; Cleared Initial/Sample Tests; Didn't clear Wider System Tests
// TopCoder Link(see link for Problem Statement): https://arena.topcoder.com/#/u/practiceCode/16656/45873/13585/1/327836

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace TC_ANewHope_250p
{
    public static class ANHCommonConfig
    {
        public static int MIN_NUM_DAYS_PER_WEEK = 2;
        //public static int MAX_NUM_DAYS_PER_WEEK = 2500;
        public static int MAX_NUM_DAYS_PER_WEEK = 10;
        public static bool b_TDSVerbose = true;
    }

    public class ANewHope
    {
        ////ANewHope() { }
        private static int NumDaysPerWeek;
        private enum e_TravelDirection { NOTSPEC, FORWARD, BACKWARD };

        private class ShirtIndexRange
        {
            // RFTPIndex --> "RIGHT from This Point" Index --> Min Start  Point --> Circular INCREMENT Index & Go Rightwards 
            // LFTPIndex --> "LEFT  from This Point" Index --> Max Ending Point --> Circular DECREMENT Index & Go Leftwards
            public int RFTPIndex, LFTPIndex;

            public ShirtIndexRange() { this.RFTPIndex = this.LFTPIndex = -1; }

            public ShirtIndexRange(int RFTPIndex, int LFTPIndex)
            {
                this.RFTPIndex = RFTPIndex;
                this.LFTPIndex = LFTPIndex;
            }

            public bool Contains(ShirtIndexRange AnotherObj)
            {
                Random PRGObj = new Random((int)(DateTime.Now.Ticks));
                bool ContainsResult = (PRGObj.Next(0, 99) % 2 == 0) ? true : false;
                return ContainsResult;
            }

            public override String ToString()
            { return (ANHCommonConfig.b_TDSVerbose ? "[RFTPIndex | LFTPIndex] = [" + this.RFTPIndex + " | " + this.LFTPIndex + "]" : ""); }
        }

        private class OverlappedSIRPair
        {
            public int OverlapLevel;
            public ShirtIndexRange FWFWD_SIR, LWBCK_SIR;

            public OverlappedSIRPair() { OverlapLevel = -1; FWFWD_SIR = new ShirtIndexRange(); LWBCK_SIR = new ShirtIndexRange(); }
            public OverlappedSIRPair(int OverlapLevel, ShirtIndexRange FWFWD_SIR, ShirtIndexRange LWBCK_SIR)
            { this.OverlapLevel = OverlapLevel; this.FWFWD_SIR = FWFWD_SIR; this.LWBCK_SIR = LWBCK_SIR; }

            public override String ToString()
            { return (ANHCommonConfig.b_TDSVerbose ? "{OverlapLevel | FWFWD_SIR | LWBCK_SIR } = {" + OverlapLevel + " | " + FWFWD_SIR.ToString() + " | " + LWBCK_SIR.ToString() + "}" : ""); }
        }

        private int AddSubModIndex(int BaseIndex, int OffsetIndex, int ModFactor)
        {
            int ResultIndex = (BaseIndex + OffsetIndex) % ModFactor;
            if (ResultIndex < 0)
                ResultIndex = (ModFactor + ResultIndex) % ModFactor;
            return ResultIndex;
        }

        private ShirtIndexRange Generate_SIRObj_GivenIndex(int GivenIndex, int OffsetIndex, int ModFactor, e_TravelDirection OffsetDir)
        {
            if (OffsetDir == e_TravelDirection.FORWARD)
                //return new ShirtIndexRange(AddSubModIndex(GivenIndex, OffsetIndex, ModFactor), AddSubModIndex(GivenIndex, -1, ModFactor));
                return new ShirtIndexRange(AddSubModIndex(GivenIndex, OffsetIndex, ModFactor), AddSubModIndex(GivenIndex, 0, ModFactor));
            else if (OffsetDir == e_TravelDirection.BACKWARD)
                //return new ShirtIndexRange(AddSubModIndex(GivenIndex, 1, ModFactor), AddSubModIndex(GivenIndex, -1 * OffsetIndex, ModFactor));
                return new ShirtIndexRange(AddSubModIndex(GivenIndex, 0, ModFactor), AddSubModIndex(GivenIndex, -1 * OffsetIndex, ModFactor));
            return new ShirtIndexRange();
        }

        private bool OverlapSIRPairFound(ShirtIndexRange SIRObj1, ShirtIndexRange SIRObj2)
        {
            bool circledone = false;
            for (int i = SIRObj1.RFTPIndex; (SIRObj1.LFTPIndex < SIRObj1.RFTPIndex && (!circledone || i <= SIRObj1.LFTPIndex)) || (SIRObj1.LFTPIndex >= SIRObj1.RFTPIndex && i <= SIRObj1.LFTPIndex); i = (i + 1) % NumDaysPerWeek)
            {
                if (i >= SIRObj2.RFTPIndex && ((SIRObj2.LFTPIndex < SIRObj2.RFTPIndex && (!circledone || i <= SIRObj2.LFTPIndex)) || (SIRObj2.LFTPIndex >= SIRObj2.RFTPIndex && i <= SIRObj2.LFTPIndex)))
                {
                    if (ANHCommonConfig.b_TDSVerbose) Console.WriteLine("OverlapSIRPairFound(" + SIRObj1.ToString() + ", " + SIRObj2.ToString() + ") --> TRUE @ Index=" + i);
                    return true;
                }
                if (i == NumDaysPerWeek - 1)
                    circledone = true;
            }
            return false;
        }

        private bool ConstructCheckOverlapDictForLevel(int levelnum)
        {
            Dict_FWD_BCK_Overlap.Clear();
            if (ANHCommonConfig.b_TDSVerbose) PrintDictsTMP("ConstructCheckOverlapDictForLevel(" + levelnum + ") ... BEFORE CONSTRUCTION ...");

            // Checking for Overlap-Constraint-Satisfaction for all Shirts 1...N
            for (int i = 1; i <= NumDaysPerWeek; i++)
            {
                Debug.Assert(levelnum < DLL_FWFWD_Explorer[i].Count && levelnum < DLL_LWBCK_Explorer[i].Count, "Level-Number exceeds Count! [i|levelnum]=[" + i + "|" + levelnum + "]");
                List<ShirtIndexRange> FWFWD_CurrentSIRPoss = DLL_FWFWD_Explorer[i].ElementAt(levelnum);
                List<ShirtIndexRange> LWBCK_CurrentSIRPoss = DLL_LWBCK_Explorer[i].ElementAt(levelnum);

                for (int j = 0; j < FWFWD_CurrentSIRPoss.Count; j++)
                    for (int k = 0; k < LWBCK_CurrentSIRPoss.Count; k++)
                        if (OverlapSIRPairFound(FWFWD_CurrentSIRPoss.ElementAt(j), LWBCK_CurrentSIRPoss.ElementAt(k)))
                            if (!Dict_FWD_BCK_Overlap.ContainsKey(i))
                                Dict_FWD_BCK_Overlap[i] = new OverlappedSIRPair(levelnum, FWFWD_CurrentSIRPoss.ElementAt(j), LWBCK_CurrentSIRPoss.ElementAt(k));
            }

            if (ANHCommonConfig.b_TDSVerbose) PrintDictsTMP("ConstructCheckOverlapDictForLevel(" + levelnum + ") ... AFTER CONSTRUCTION ...");

            if (Dict_FWD_BCK_Overlap.Count == NumDaysPerWeek)
                return true;
            return false;
        }

        private static int MAX_LEVELNUM_TMP = 25000;
        private static Dictionary<int, OverlappedSIRPair> Dict_FWD_BCK_Overlap = new Dictionary<int, OverlappedSIRPair>();
        private static Dictionary<int, List<List<ShirtIndexRange>>> DLL_FWFWD_Explorer = new Dictionary<int, List<List<ShirtIndexRange>>>();
        private static Dictionary<int, List<List<ShirtIndexRange>>> DLL_LWBCK_Explorer = new Dictionary<int, List<List<ShirtIndexRange>>>();

        private int count_DLL(int[] firstWeek, int[] lastWeek, int D)
        {
            int levelnum;
            if (ANHCommonConfig.b_TDSVerbose) Console.WriteLine("ANewHope.count_DLL ...");

            // Initialization Phase ... (i.e. First "Intermediate" Week Exploration & Discovery Phase)
            DLL_FWFWD_Explorer.Clear(); DLL_LWBCK_Explorer.Clear();
            for (int i = 0; i < firstWeek.Length; i++)
            {
                DLL_FWFWD_Explorer[firstWeek[i]] = new List<List<ShirtIndexRange>>();
                DLL_LWBCK_Explorer[lastWeek[i]] = new List<List<ShirtIndexRange>>();

                List<ShirtIndexRange> FWFWD_CurrentSIRPoss = new List<ShirtIndexRange>();
                List<ShirtIndexRange> LWBCK_CurrentSIRPoss = new List<ShirtIndexRange>();

                FWFWD_CurrentSIRPoss.Add(Generate_SIRObj_GivenIndex(i, D, NumDaysPerWeek, e_TravelDirection.FORWARD));
                LWBCK_CurrentSIRPoss.Add(Generate_SIRObj_GivenIndex(i, D, NumDaysPerWeek, e_TravelDirection.BACKWARD));

                DLL_FWFWD_Explorer[firstWeek[i]].Add(FWFWD_CurrentSIRPoss);
                DLL_LWBCK_Explorer[lastWeek[i]].Add(LWBCK_CurrentSIRPoss);

                Debug.Assert(FWFWD_CurrentSIRPoss.Count == LWBCK_CurrentSIRPoss.Count, "ANewHope.count_DLL - CURRENT [FWFWD|LWBCK]_CurrentSIRPossCounts do NOT Match!");
                Debug.Assert(DLL_FWFWD_Explorer[firstWeek[i]].Count == DLL_LWBCK_Explorer[lastWeek[i]].Count, "ANewHope.count_DLL - DLL_[FWFWD|LWBCK]_Explorer[firstWeek[" + i + "]].Count do NOT Match!");
                Debug.Assert(DLL_FWFWD_Explorer.Count == DLL_LWBCK_Explorer.Count, "ANewHope.count_DLL - DLL_[FWFWD|LWBCK]_Explorer Counts do NOT Match!");
            }

            // All Subsequent "Intermediate" Week Exploration & Discovery Phase ...
            for (levelnum = 0; !ConstructCheckOverlapDictForLevel(levelnum) && levelnum < MAX_LEVELNUM_TMP; levelnum++)
            {
                for (int i = 1; i <= NumDaysPerWeek; i++)
                {
                    List<ShirtIndexRange> FWFWD_PrevSIRPoss = DLL_FWFWD_Explorer[i].ElementAt(levelnum);
                    List<ShirtIndexRange> LWBCK_PrevSIRPoss = DLL_LWBCK_Explorer[i].ElementAt(levelnum);
                    Debug.Assert(FWFWD_PrevSIRPoss.Count == LWBCK_PrevSIRPoss.Count, "ANewHope.count_DLL - PREVIOUS [FWFWD|LWBCK]_PrevSIRPossCounts do NOT Match!");

                    List<ShirtIndexRange> FWFWD_CurrentSIRPoss = new List<ShirtIndexRange>();
                    List<ShirtIndexRange> LWBCK_CurrentSIRPoss = new List<ShirtIndexRange>();

                    for (int j = 0; j < FWFWD_PrevSIRPoss.Count && j < LWBCK_PrevSIRPoss.Count; j++)
                    {
                        FWFWD_CurrentSIRPoss.Add(Generate_SIRObj_GivenIndex(FWFWD_PrevSIRPoss.ElementAt(j).RFTPIndex, D, NumDaysPerWeek, e_TravelDirection.FORWARD));
                        FWFWD_CurrentSIRPoss.Add(Generate_SIRObj_GivenIndex(FWFWD_PrevSIRPoss.ElementAt(j).LFTPIndex, D, NumDaysPerWeek, e_TravelDirection.FORWARD));

                        LWBCK_CurrentSIRPoss.Add(Generate_SIRObj_GivenIndex(LWBCK_PrevSIRPoss.ElementAt(j).LFTPIndex, D, NumDaysPerWeek, e_TravelDirection.BACKWARD));
                        LWBCK_CurrentSIRPoss.Add(Generate_SIRObj_GivenIndex(LWBCK_PrevSIRPoss.ElementAt(j).RFTPIndex, D, NumDaysPerWeek, e_TravelDirection.BACKWARD));
                    }

                    DLL_FWFWD_Explorer[i].Add(FWFWD_CurrentSIRPoss); DLL_LWBCK_Explorer[i].Add(LWBCK_CurrentSIRPoss);

                    Debug.Assert(DLL_FWFWD_Explorer.Count == DLL_LWBCK_Explorer.Count, "ANewHope.count_DLL - DLL_[FWFWD|LWBCK]_Explorer Counts do NOT Match!");
                    Debug.Assert(DLL_FWFWD_Explorer.Count == DLL_LWBCK_Explorer.Count, "ANewHope.count_DLL - DLL_[FWFWD|LWBCK]_Explorer Counts do NOT Match!");
                    Debug.Assert(DLL_FWFWD_Explorer.Count == DLL_LWBCK_Explorer.Count, "ANewHope.count_DLL - DLL_[FWFWD|LWBCK]_Explorer Counts do NOT Match!");
                }
            }

            return (levelnum + 3);
        }

        public int count(int[] firstWeek, int[] lastWeek, int D)
        {
            NumDaysPerWeek = firstWeek.Length;
            Debug.Assert(firstWeek.Length == lastWeek.Length, "ANewHope.count: Input Invalid. First & Last Week Arrays have different Lengths! [firstWeek|lastWeek].length = [" + firstWeek.Length + "|" + lastWeek.Length + "]");
            Debug.Assert(D >= 1 && D < NumDaysPerWeek, "ANewHope.count: Input Invalid. D (number of days to wash/dry shirt) < 0 ... D = >" + D + "<");

            bool b_FWLW_NotEqual = false, b_FWLW_NotReverseEqual = false;
            for (int i = 0; i < firstWeek.Length; i++)
            {
                if (firstWeek[i] != lastWeek[i])
                    b_FWLW_NotEqual = true;

                if (firstWeek[i] != lastWeek[firstWeek.Length - 1 - i])
                    b_FWLW_NotReverseEqual = true;

                if (b_FWLW_NotEqual && b_FWLW_NotReverseEqual)
                    break;
            }

            if (!b_FWLW_NotEqual)
            {
                // First-Week and Last-Week are exactly the same - (i.e. they are the SAME week)
                if (ANHCommonConfig.b_TDSVerbose) Console.WriteLine("ANewHope.count: Returning 1 ... (!b_FWLW_NotEqual)");
                return 1;
            }
            else if ((D == NumDaysPerWeek - 1) && !b_FWLW_NotReverseEqual)
            {
                // First-Week and Last-Week are EXACT RERVESE
                if (ANHCommonConfig.b_TDSVerbose) Console.WriteLine("ANewHope.count: Returning 'NumDaysPerWeek' = " + NumDaysPerWeek + " ... ((D == NumDaysPerWeek - 1) && !b_FWLW_NotReverseEqual)");
                return NumDaysPerWeek;
            }

            return count_DLL(firstWeek, lastWeek, D);
        }

        private void PrintDictsTMP(String CustMsg)
        {
            if (!ANHCommonConfig.b_TDSVerbose) return;
            if (CustMsg == null || CustMsg == "")
                Console.WriteLine("ANewHope.PrintDictsTMP: Printing Dictionaries ... D_BEGIN_>>");
            else
                Console.WriteLine(CustMsg + " ... Printing Dictionaries ... D_BEGIN_>>");

            Console.WriteLine("ANewHope.PrintDictsTMP: Dict_FWD_BCK_Overlap (Count=" + Dict_FWD_BCK_Overlap.Count + ") BEGIN");
            for (int i = 1; i <= NumDaysPerWeek; i++)
            {
                if (!Dict_FWD_BCK_Overlap.ContainsKey(i))
                    continue;
                Console.WriteLine("Dict_FWD_BCK_Overlap[" + i + "] = >" + Dict_FWD_BCK_Overlap[i].ToString() + "<");
            }
            Console.WriteLine("Dict_FWD_BCK_Overlap END"); Console.WriteLine("");

            Console.WriteLine("ANewHope.PrintDictsTMP: DLL_FWFWD_Explorer (Count=" + DLL_FWFWD_Explorer.Count + ") BEGIN");
            for (int i = 1; i <= NumDaysPerWeek; i++)
            {
                if (!DLL_FWFWD_Explorer.ContainsKey(i))
                    continue;
                List<List<ShirtIndexRange>> ListListTMPObj = DLL_FWFWD_Explorer[i];
                Console.WriteLine("DLL_FWFWD_Explorer[" + i + "] = >");
                for (int j = 0; j < ListListTMPObj.Count; j++)
                {
                    Console.WriteLine("[List] @ DLL_FWFWD_Explorer[" + i + "].ElementAt(" + j + ") ... "); //Console.WriteLine("");
                    for (int k = 0; k < ListListTMPObj.ElementAt(j).Count; k++)
                        Console.Write(ListListTMPObj.ElementAt(j).ElementAt(k).ToString() + " // ");
                }
                Console.WriteLine("<");
            }
            Console.WriteLine("DLL_FWFWD_Explorer END"); Console.WriteLine("");

            Console.WriteLine("ANewHope.PrintDictsTMP: DLL_LWBCK_Explorer (Count=" + DLL_LWBCK_Explorer.Count + ") BEGIN");
            for (int i = 1; i <= NumDaysPerWeek; i++)
            {
                if (!DLL_LWBCK_Explorer.ContainsKey(i))
                    continue;
                List<List<ShirtIndexRange>> ListListTMPObj = DLL_LWBCK_Explorer[i];
                Console.WriteLine("DLL_LWBCK_Explorer[" + i + "] = >");
                for (int j = 0; j < ListListTMPObj.Count; j++)
                {
                    Console.WriteLine("[List] @ DLL_LWBCK_Explorer[" + i + "].ElementAt(" + j + ") ... "); //Console.WriteLine("");
                    for (int k = 0; k < ListListTMPObj.ElementAt(j).Count; k++)
                        Console.Write(ListListTMPObj.ElementAt(j).ElementAt(k).ToString() + " // ");
                }
                Console.WriteLine("<"); Console.WriteLine("");
            }
            Console.WriteLine("DLL_LWBCK_Explorer END");

            if (CustMsg == null || CustMsg == "")
                Console.WriteLine("<<_D_END");
            else
                Console.WriteLine(CustMsg + " ... <<_D_END");
            Console.WriteLine("");
        }
    }

    public class AutoTest_ANewHope
    {
        private static int MAX_NUM_TESTS = 10;
        private static Random PRGObj = new Random((int)(DateTime.Now.Ticks));
        private static ANewHope ANewHopeTestObj = new ANewHope();

        public void AutoRandMultiTests()
        {
            int num_tests = PRGObj.Next(1, MAX_NUM_TESTS);
            for (int i = 0; i < num_tests; i++)
            {
                if (ANHCommonConfig.b_TDSVerbose) Console.WriteLine("AutoTest_ANewHope.AutoRandMultiTests: TEST #" + (i + 1) + " --> BEGINS -->");
                AutoRandSingleTest();
                if (ANHCommonConfig.b_TDSVerbose)
                {
                    Console.WriteLine("AutoTest_ANewHope.AutoRandMultiTests <-- END OF TEST #" + (i + 1));
                    Console.WriteLine(""); Console.WriteLine("");
                }
            }
        }

        public void AutoRandSingleTest()
        {
            int NumDaysPerWeek = PRGObj.Next(ANHCommonConfig.MIN_NUM_DAYS_PER_WEEK, ANHCommonConfig.MAX_NUM_DAYS_PER_WEEK);
            int D = PRGObj.Next(1, NumDaysPerWeek - 1);
            int[] firstWeek = new int[NumDaysPerWeek];
            int[] lastWeek = new int[NumDaysPerWeek];
            bool b_FWLW_EqualFlag = (PRGObj.Next(1, ANHCommonConfig.MAX_NUM_DAYS_PER_WEEK) % 2 == 0) ? true : false;
            bool b_FWLW_ReverseEqualFlag = false;   //b_FWLW_EqualFlag ? false : ((PRGObj.Next(1, MAX_NUM_DAYS_PER_WEEK) % 2 == 0) ? true : false);

            if (ANHCommonConfig.b_TDSVerbose) Console.WriteLine("AutoTest_ANewHope.AutoRandSingleTest: {NumDaysPerWeek | D | b_FWLW_EqualFlag | b_FWLW_ReverseEqualFlag} = {" + NumDaysPerWeek + " | " + D + " | " + b_FWLW_EqualFlag + " | " + b_FWLW_ReverseEqualFlag + "}");
            for (int i = 0; i < NumDaysPerWeek; i++)
            {
                int FirstWeek_NextCandidate;
                bool b_FirstWeek_NextCandidate_taken;
                do
                {
                    b_FirstWeek_NextCandidate_taken = false;
                    FirstWeek_NextCandidate = PRGObj.Next(1, NumDaysPerWeek + 1);
                    for (int j = 0; j < i; j++)
                    {
                        if (FirstWeek_NextCandidate == firstWeek[j])
                            b_FirstWeek_NextCandidate_taken = true;
                    }
                } while (b_FirstWeek_NextCandidate_taken);
                firstWeek[i] = FirstWeek_NextCandidate;

                if (b_FWLW_EqualFlag || b_FWLW_ReverseEqualFlag)
                {

                    if (b_FWLW_EqualFlag)
                        lastWeek[i] = firstWeek[i];
                    if (b_FWLW_ReverseEqualFlag)
                        lastWeek[NumDaysPerWeek - 1 - i] = firstWeek[i];
                    Console.WriteLine("AutoTest_ANewHope.AutoRandSingleTest: {i | firstWeek[i] | lastWeek[i]} = {" + i + " | " + firstWeek[i] + " | " + lastWeek[i] + "}");
                    continue;
                }

                int LastWeek_NextCandidate;
                bool b_LastWeek_NextCandidate_taken;
                do
                {
                    b_LastWeek_NextCandidate_taken = false;
                    LastWeek_NextCandidate = PRGObj.Next(1, NumDaysPerWeek + 1);
                    for (int j = 0; j < i; j++)
                    {
                        if (LastWeek_NextCandidate == lastWeek[j])
                            b_LastWeek_NextCandidate_taken = true;
                    }
                } while (b_LastWeek_NextCandidate_taken);
                lastWeek[i] = LastWeek_NextCandidate;

                Console.WriteLine("AutoTest_ANewHope.AutoRandSingleTest: {i | firstWeek[i] | lastWeek[i]} = {" + i + " | " + firstWeek[i] + " | " + lastWeek[i] + "}");
            }

            if (ANHCommonConfig.b_TDSVerbose) Console.WriteLine(""); Console.WriteLine("");
            Console.WriteLine("AutoTest_ANewHope.AutoRandSingleTest: FINAL-RESULT [COUNT] = >" + ANewHopeTestObj.count(firstWeek, lastWeek, D) + "<");
            if (ANHCommonConfig.b_TDSVerbose) Console.WriteLine(""); Console.WriteLine("");
        }

        public void AutoTest_SpecificCases()
        {
            ANewHope ANewHopeTestObj = new ANewHope();
            int[] firstWeek_1 = { 1, 2, 3, 4 };
            int[] lastWeek_1  = { 4, 3, 2, 1 };
            int[] firstWeek_2 = { 8, 5, 4, 1, 7, 6, 3, 2 };
            int[] lastWeek_2  = { 2, 4, 6, 8, 1, 3, 5, 7 };
            int[] firstWeek_3 = { 1, 2, 3, 4 };
            int[] lastWeek_3  = { 1, 2, 3, 4 };
            int[][] firstWeekAll = { firstWeek_1 , firstWeek_2 , firstWeek_3 };
            int[][] lastWeekAll  = { lastWeek_1 , lastWeek_2 , lastWeek_3 };
            int[] d = { 3 , 3 , 2 };

            for(int i=0; i<firstWeekAll.Count() && i<lastWeekAll.Count(); i++)
            {
                Console.WriteLine("[firstWeekAll[" + i + "] | lastWeekAll[ " + i + "] | d["+ i + "]] = [" + firstWeekAll[i] + " | " + lastWeekAll[i] + " | " + d[i] + "]");
                Console.WriteLine("ANewHopeTestObj.count = >" + ANewHopeTestObj.count(firstWeekAll[i], lastWeekAll[i], d[i]) + "<");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AutoTest_ANewHope AutoTest_ANewHope_Obj = new AutoTest_ANewHope();
            AutoTest_ANewHope_Obj.AutoRandSingleTest();
            Console.WriteLine(""); Console.WriteLine("");
            AutoTest_ANewHope_Obj.AutoRandMultiTests();
            Console.WriteLine(""); Console.WriteLine("");
            AutoTest_ANewHope_Obj.AutoTest_SpecificCases();
            Console.WriteLine(""); Console.WriteLine("");
        }
    }
}
