// TopCoder - Problem Name: ABBA(500 points) [MEDIUM]
// Class Name : ABBA
// Method Name : canObtain
// Return Type : string
// Arg Types : (string, string)

// Status on TopCoder: Compiled Successfully; Cleared most of the Initial/Sample Tests
// TopCoder Link(see link for Problem Statement): https://arena.topcoder.com/#/u/practiceCode/16527/48825/13918/2/326683

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TC_ABBA_Rec_500p
{
    class ABBA
    {
        //public static long recursion_count = 0, recursion_depth = 0;
        //public static long recursion_memory_count = 0, recursion_memory_depth = 0;
        public static Random PRGObj = new Random((int)(DateTime.Now.Ticks));

        public string canObtain(string src, string tgt)
        {
            //Console.WriteLine("canObtain(" + src + ", " + tgt + ")");
            src = src.ToUpper(); tgt = tgt.ToUpper();
            string s_canTransRes = (canTransform(src, tgt)) ? "Possible" : "Impossible";
            //Console.WriteLine("canObtain(" + src + ", " + tgt + ") --> " + s_canTransRes);
            //Console.WriteLine("canObtain(" + src + ", " + tgt + ") ... recursion_[count|depth] = [" + recursion_count + "|" + recursion_depth + "]");
            //if (ABBAConfig.DetailedProfilingEnable) Console.WriteLine("canObtain(" + src + ", " + tgt + ") ... recursion_memory_[count|depth] = [" + recursion_memory_count + "|" + recursion_memory_depth + "]");
            //recursion_count = 0; recursion_depth = 0;
            //recursion_memory_count = 0; recursion_memory_depth = 0;
            return s_canTransRes;
        }

        public static bool canTransform(string src, string tgt)
        {
            //recursion_count++; recursion_depth++;
            //if (ABBAConfig.DetailedProfilingEnable)
            //{
            //    try
            //    {
            //        recursion_memory_count += (src.Length * sizeof(Char) + tgt.Length * sizeof(Char));
            //        recursion_memory_depth += (src.Length * sizeof(Char) + tgt.Length * sizeof(Char));
            //    }
            //    catch (Exception excep) { Console.WriteLine("Exception Occured ... Details: >" + excep.ToString() + "<"); Console.WriteLine(""); }
            //}
            //if (ABBAConfig.TDSPrintEnable)
            //{
            //    Console.WriteLine("canTransform(" + src + ", " + tgt + ") ... recursion_[count|depth] = [" + recursion_count + "|" + recursion_depth + "]");
            //    if (ABBAConfig.DetailedProfilingEnable) Console.WriteLine("canTransform(" + src + ", " + tgt + ") ... recursion_memory_[count|depth] = [" + recursion_memory_count + "|" + recursion_memory_depth + "]");
            //}
            //if (ABBAConfig.TDSPrintEnable && ABBAConfig.UserKPWaitEnable) Console.WriteLine("Press Any Key To Continue ...");
            //if (ABBAConfig.UserKPWaitEnable) { Console.Read(); }

            if (src == tgt)
            {
                //recursion_depth--;
                //if (ABBAConfig.DetailedProfilingEnable)
                //{
                //    try { recursion_memory_depth -= (src.Length * sizeof(Char) + tgt.Length * sizeof(Char)); recursion_memory_depth_2 -= (System.Text.ASCIIEncoding.Unicode.GetByteCount(src) + System.Text.ASCIIEncoding.Unicode.GetByteCount(tgt)); }
                //    catch (Exception excep) { Console.WriteLine("Exception Occured ... Details: >" + excep.ToString() + "<"); Console.WriteLine(""); }
                //}
                //if (ABBAConfig.TDSPrintEnable) Console.WriteLine("canTransform(" + src + ", " + tgt + ") --> true --> [src == tgt]");
                return true;
            }

            if ((src == "" || tgt == "") || (src.Count() >= tgt.Count()))
            {
                //recursion_depth--;
                //if (ABBAConfig.DetailedProfilingEnable)
                //{
                //    try { recursion_memory_depth -= (src.Length * sizeof(Char) + tgt.Length * sizeof(Char)); recursion_memory_depth_2 -= (System.Text.ASCIIEncoding.Unicode.GetByteCount(src) + System.Text.ASCIIEncoding.Unicode.GetByteCount(tgt)); }
                //    catch (Exception excep) { Console.WriteLine("Exception Occured ... Details: >" + excep.ToString() + "<"); Console.WriteLine(""); }
                //}
                //if (ABBAConfig.TDSPrintEnable) Console.WriteLine("canTransform(" + src + ", " + tgt + ") --> false --> [src.Count() >= tgt.Count()]");
                return false;
            }

            ////if ((tgt.Count() - src.Count()) <= 10)
            ////{

            if (PRGObj.Next(0, 100) % 2 == 0)
            {
                // Add "A" to end of string and try ...
                //if (ABBAConfig.TDSPrintEnable) Console.WriteLine("canTransform: Add 'A' to end of string and try ...");
                bool b_AddA_Res = canTransform(src + "A", tgt);
                if (b_AddA_Res)
                {
                    //recursion_depth--;
                    //if (ABBAConfig.DetailedProfilingEnable)
                    //{
                    //    try { recursion_memory_depth -= (src.Length * sizeof(Char) + tgt.Length * sizeof(Char)); }
                    //    catch (Exception excep) { Console.WriteLine("Exception Occured ... Details: >" + excep.ToString() + "<"); Console.WriteLine(""); }
                    //}
                    //if (ABBAConfig.TDSPrintEnable) Console.WriteLine("canTransform(" + src + ", " + tgt + ") --> true --> (b_AddA_Res)");
                    return true;
                }

                // Reverse the string, and add "B" to end of reversed string, and try ...
                string revsrc = "";
                for (int i = 0, j = src.Count() - 1; i < src.Count(); i++, j--)
                    revsrc += src[j];
                revsrc += "B";
                //if (ABBAConfig.TDSPrintEnable) Console.WriteLine("canTransform: Reverse the string, and add 'B' to end of reversed string, and try ... i.e. revsrc = >" + revsrc + "<");
                bool b_RevAddB_Res = canTransform(revsrc, tgt);
                if (b_RevAddB_Res)
                {
                    //recursion_depth--;
                    //if (ABBAConfig.DetailedProfilingEnable)
                    //{
                    //    try { recursion_memory_depth -= (src.Length * sizeof(Char) + tgt.Length * sizeof(Char)); }
                    //    catch (Exception excep) { Console.WriteLine("Exception Occured ... Details: >" + excep.ToString() + "<"); Console.WriteLine(""); }
                    //}
                    //if (ABBAConfig.TDSPrintEnable) Console.WriteLine("canTransform(" + src + ", " + tgt + ") --> true --> (b_RevAddB_Res)");
                    return true;
                }
            }
            else
            {
                // Reverse the string, and add "B" to end of reversed string, and try ...
                string revsrc = "";
                for (int i = 0, j = src.Count() - 1; i < src.Count(); i++, j--)
                    revsrc += src[j];
                revsrc += "B";
                //if (ABBAConfig.TDSPrintEnable) Console.WriteLine("canTransform: Reverse the string, and add 'B' to end of reversed string, and try ... i.e. revsrc = >" + revsrc + "<");
                bool b_RevAddB_Res = canTransform(revsrc, tgt);
                if (b_RevAddB_Res)
                {
                    //recursion_depth--;
                    //if (ABBAConfig.DetailedProfilingEnable)
                    //{
                    //    try { recursion_memory_depth -= (src.Length * sizeof(Char) + tgt.Length * sizeof(Char)); }
                    //    catch (Exception excep) { Console.WriteLine("Exception Occured ... Details: >" + excep.ToString() + "<"); Console.WriteLine(""); }
                    //}
                    //if (ABBAConfig.TDSPrintEnable) Console.WriteLine("canTransform(" + src + ", " + tgt + ") --> true --> (b_RevAddB_Res)");
                    return true;
                }

                // Add "A" to end of string and try ...
                //if (ABBAConfig.TDSPrintEnable) Console.WriteLine("canTransform: Add 'A' to end of string and try ...");
                bool b_AddA_Res = canTransform(src + "A", tgt);
                if (b_AddA_Res)
                {
                    //recursion_depth--;
                    //if (ABBAConfig.DetailedProfilingEnable)
                    //{
                    //    try { recursion_memory_depth -= (src.Length * sizeof(Char) + tgt.Length * sizeof(Char)); }
                    //    catch (Exception excep) { Console.WriteLine("Exception Occured ... Details: >" + excep.ToString() + "<"); Console.WriteLine(""); }
                    //}
                    //if (ABBAConfig.TDSPrintEnable) Console.WriteLine("canTransform(" + src + ", " + tgt + ") --> true --> (b_AddA_Res)");
                    return true;
                }
            }
            ////}

            //recursion_depth--;
            //if (ABBAConfig.DetailedProfilingEnable)
            //{
            //    try { recursion_memory_depth -= (src.Length * sizeof(Char) + tgt.Length * sizeof(Char)); }
            //    catch (Exception excep) { Console.WriteLine("Exception Occured ... Details: >" + excep.ToString() + "<"); Console.WriteLine(""); }
            //}
            //if (ABBAConfig.TDSPrintEnable) Console.WriteLine("canTransform(" + src + ", " + tgt + ") --> false --> END");
            return false;
        }
    }

    class AutoTest_ABBA
    {
        private static Random PRGObj = new Random((int)(DateTime.Now.Ticks));
        private const int MIN_NUM_TEST = 10, MAX_NUM_TESTS = 20;
        public void autoTest_SpecificCases()
        {
            ABBA ABBAObj = new ABBA();
            string[] srcs = { "B", "AB", "BBAB", "BBBBABABBBBBBA", "A" };
            string[] tgts = { "ABBA", "ABB", "ABABABABB", "BBBBABABBABBBBBBABABBBBBBBBABAABBBAA", "BB" };

            for (int i = 0; i < srcs.Length && i < tgts.Length; i++)
            {
                Console.WriteLine("AutoTest_ABBA.autoTest_SpecificCases: [srcs[" + i + "] | tgts[" + i + "]] = [" + srcs[i] + " | " + tgts[i] + "] ... Result = >" + ABBAObj.canObtain(srcs[i], tgts[i]) + "<");
                Console.WriteLine(""); Console.WriteLine("");
            }
        }

        public string generateRandABString(int stringlength)
        {
            string ABstring = "";
            for (int i = 0; i < stringlength; i++)
            {
                if (PRGObj.Next(0, 10) % 2 == 0)
                    ABstring += "A";
                else
                    ABstring += "B";
            }
            return ABstring;
        }

        public void autoRandSingleTest()
        {
            ABBA ABBA_Obj = new ABBA();
            int srcsize, tgtsize;
            do
            {
                //srcsize = PRGObj.Next(1, 999);
                //tgtsize = PRGObj.Next(2, 1000);
                srcsize = PRGObj.Next(1, 10);
                tgtsize = PRGObj.Next(2, 20);
            } while (srcsize >= tgtsize);
            string src = generateRandABString(srcsize);
            string tgt = generateRandABString(tgtsize);
            Console.WriteLine("AutoTest_ABBA.autoRandSingleTest: [src | tgt ] = [" + src + " | " + tgt + " ] ==> " + ABBA_Obj.canObtain(src, tgt));
        }

        public void autoRandMultipleTests()
        {
            int numtests = PRGObj.Next(MIN_NUM_TEST, MAX_NUM_TESTS);
            for (int i = 0; i < numtests; i++)
            {
                Console.WriteLine("AutoTest_ABBA.autoRandMultipleTests: Conducting Test #" + (i + 1) + " -->");
                autoRandSingleTest();
                Console.WriteLine("AutoTest_ABBA.autoRandMultipleTests: <-- Test #" + (i + 1) + " Ended");
                Console.WriteLine(""); Console.WriteLine("");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AutoTest_ABBA AutoTest_ABBA_Obj = new AutoTest_ABBA();
            AutoTest_ABBA_Obj.autoRandSingleTest();
            Console.WriteLine(""); Console.WriteLine("");
            AutoTest_ABBA_Obj.autoRandMultipleTests();
            Console.WriteLine(""); Console.WriteLine("");
            AutoTest_ABBA_Obj.autoTest_SpecificCases();
        }
    }
}
