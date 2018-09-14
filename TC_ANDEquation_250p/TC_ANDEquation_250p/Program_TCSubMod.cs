// TopCoder - Problem Name: ANDEquation(250 points) [EASY]
// Class Name : ANDEquation
// Method Name : restoreY
// Return Type : int
// Arg Types : (int[])

// Status on TopCoder: Compiled Successfully; Cleared Initial/Sample Tests; Cleared Wider System Tests
// TopCoder Link(see link for Problem Statement): https://arena.topcoder.com/#/u/practiceCode/15191/26460/12029/2/313351

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace TC_ANDEquation_250p
//{
    class ANDEquation
    {
        public int restoreY(int[] A)
        {
            int yResult = -1;
            int numelem = A.Count();
            for (int i = 0; i < numelem; i++)
            {
                int curres = 1048575;
                for (int j = 0; j < numelem; j++)
                    if (j != i)
                        curres = curres & A[j];
                if (curres == A[i])
                {
                    yResult = curres;
                    break;
                }
            }
            return yResult;
        }
    }

    //class Test_ANDEquation
    //{
    //    public System.Random PRGObj = new System.Random();
    //    public const int MIN_NUM_TESTS = 10, MAX_NUM_TESTS = 100;
    //    public int[] Generate_RandomA()
    //    {
    //        int numelem = PRGObj.Next(2, 50);
    //        int[] A = new int[numelem];
    //        for (int i = 0; i < numelem; i++)
    //            A[i] = PRGObj.Next(0, 1048575);
    //        return A;
    //    }

    //    public String Stringify_A(int[] A)
    //    {
    //        String A_string = "{";
    //        int numelem = A.Count();
    //        Console.WriteLine("A[] ... BEGINS -->");
    //        for (int i = 0; i < numelem; i++)
    //        {
    //            A_string += A[i];
    //            if (i < numelem - 1)
    //                A_string += " , ";
    //        }
    //        A_string += "}";
    //        return A_string;
    //    }
    //    public void AutoTest_ANDEquation_Once()
    //    {
    //        int[] A = Generate_RandomA();
    //        ANDEquation ANDEq_Obj = new ANDEquation();
    //        int yResult = ANDEq_Obj.restoreY(A);
    //        Console.WriteLine("Test_ANDEquation.AutoTest_ANDEquation_Once: A = >" + Stringify_A(A) + "<");
    //        Console.WriteLine("Test_ANDEquation.AutoTest_ANDEquation_Once: yResult = >" + yResult + "<");
    //        Console.WriteLine(""); Console.WriteLine("");
    //    }

    //    public void AutoTest_ANDEquation_Multi()
    //    {
    //        int numtests = PRGObj.Next(MIN_NUM_TESTS, MAX_NUM_TESTS);
    //        for (int i = 0; i < numtests; i++)
    //        {
    //            Console.WriteLine("Test_ANDEquation.AutoTest_ANDEquation_Multi ... Test #" + (i + 1) + " BEGINS -->");
    //            AutoTest_ANDEquation_Once();
    //            Console.WriteLine("Test_ANDEquation.AutoTest_ANDEquation_Multi ... Test #" + (i + 1) + " <-- ENDS");
    //            Console.WriteLine(""); Console.WriteLine("");
    //        }
    //    }

    //    public void SelManTest_ANDEquation_Multi()
    //    {
    //        ANDEquation ANDEq_Obj = new ANDEquation();
    //        int[] A1 = { 1, 3, 5 };
    //        int[] A2 = { 31, 7 };
    //        int[] A3 = { 31, 7, 7 };
    //        int[] A4 = { 1, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0, 1 };
    //        int[] A5 = { 191411, 256951, 191411, 191411, 191411, 256951, 195507, 191411, 192435, 191411, 191411, 195511, 191419, 191411, 256947, 191415, 191475, 195579, 191415, 191411, 191483, 191411, 191419, 191475, 256947, 191411, 191411, 191411, 191419, 256947, 191411, 191411, 191411 };
    //        int[] A6 = { 1362, 1066, 1659, 2010, 1912, 1720, 1851, 1593, 1799, 1805, 1139, 1493, 1141, 1163, 1211 };
    //        int[] A7 = { 2, 3, 7, 19 };
    //        int[][] AllAs = { A1, A2, A3, A4, A5, A6, A7 };

    //        for(int i=0; i<AllAs.Count(); i++)
    //        {
    //            int yResult = ANDEq_Obj.restoreY(AllAs[i]);
    //            Console.WriteLine("Test_ANDEquation.SelManTest_ANDEquation_Multi: AllAs[" + i + "] = >" + Stringify_A(AllAs[i]) + "<");
    //            Console.WriteLine("Test_ANDEquation.SelManTest_ANDEquation_Multi: yResult1 = >" + yResult + "<");
    //        }
    //        Console.WriteLine(""); Console.WriteLine("");
    //    }
    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Test_ANDEquation T_ANDEq_Obj = new Test_ANDEquation();
    //        Console.WriteLine("Conducting One Test ... BEGIN -->");
    //        T_ANDEq_Obj.AutoTest_ANDEquation_Once();
    //        Console.WriteLine("Conducting One Random/AutoTest ... <-- END");
    //        Console.WriteLine(""); Console.WriteLine("");

    //        Console.WriteLine("Conducting Multiple Random/Auto Tests ... BEGIN -->");
    //        T_ANDEq_Obj.AutoTest_ANDEquation_Multi();
    //        Console.WriteLine("Conducting Multiple Random/Auto Tests ... <-- END");
    //        Console.WriteLine(""); Console.WriteLine("");

    //        Console.WriteLine("Conducting Multiple Selective Manual Tests ... BEGIN -->");
    //        T_ANDEq_Obj.SelManTest_ANDEquation_Multi();
    //        Console.WriteLine("Conducting Multiple Selective Manual Tests ... <-- END");
    //    }
    //}
//}   // END_NAMESPACE
