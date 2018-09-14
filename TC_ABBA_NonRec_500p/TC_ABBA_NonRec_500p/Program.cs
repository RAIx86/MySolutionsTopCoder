// TopCoder - Problem Name: ABBA(500 points) [MEDIUM]
// Class Name : ABBA
// Method Name : canObtain
//  Return Type : string
// Arg Types : (string, string)

// Status on TopCoder: Status on TopCoder: Compiled Successfully; Cleared most of the Initial Sample Test(failed one due to exceeding time-limit); Failed Wider System Tests
// TopCoder Link(see link for Problem Statement): https://arena.topcoder.com/#/u/practiceCode/16527/48825/13918/2/326683

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TC_ABBA_NonRec_500p
{
    class ABBAConfig
    {
        public static bool b_verbose_TDS = true;
    }

    class ABBA
    {
        private static Random PRGObj = new Random((int)(DateTime.Now.Ticks));

        public class String_Transform_StackNode
        {
            public char cur_ch;
            public string cur_fwd_action;
            public bool b_AddA_fromthispoint_avlb;
            public bool b_RevStrAddB_fromthispoint_avlb;

            public String_Transform_StackNode(char cur_ch, string cur_fwd_action, bool b_AddA_fromthispoint_avlb, bool b_RevStrAddB_fromthispoint_avlb)
            { setParams(cur_ch, cur_fwd_action, b_AddA_fromthispoint_avlb, b_RevStrAddB_fromthispoint_avlb); }

            public void setParams(char cur_ch, string cur_fwd_action, bool b_AddA_fromthispoint_avlb, bool b_RevStrAddB_fromthispoint_avlb)
            {
                this.cur_ch = cur_ch;
                this.cur_fwd_action = cur_fwd_action;
                this.b_AddA_fromthispoint_avlb = b_AddA_fromthispoint_avlb;
                this.b_RevStrAddB_fromthispoint_avlb = b_RevStrAddB_fromthispoint_avlb;
            }

            public override string ToString()
            { return "[" + cur_ch + " | " + cur_fwd_action + " | " + b_AddA_fromthispoint_avlb + " | " + b_RevStrAddB_fromthispoint_avlb + "]"; }
        }

        private Stack<String_Transform_StackNode> str_trans_stck = new Stack<String_Transform_StackNode>();

        public string obtain_StackTrans_String()
        {
            string result_str = "";
            for (int i = 0; i < str_trans_stck.Count; i++)
                result_str += str_trans_stck.ElementAt(str_trans_stck.Count - 1 - i).cur_ch;
            //if (ABBAConfig.b_verbose_TDS) Console.WriteLine("ABBA.obtain_StackTrans_String: >" + result_str + "<");
            return result_str;
        }

        public string obtain_StackTransObj_String()
        {
            string strresult = ">";
            foreach (String_Transform_StackNode cur_stacknode in str_trans_stck)
            {
                strresult += cur_stacknode.ToString();
                strresult += " / ";
            }
            strresult += "<";
            //if (ABBAConfig.b_verbose_TDS) Console.WriteLine("ABBA.obtain_StackTransObj_String: >" + strresult + "<");
            return strresult;
        }

        public string canObtain(string src, string tgt)
        {
            if (ABBAConfig.b_verbose_TDS) Console.WriteLine("ABBA.canObtain(" + src + ", " + tgt + ")");
            src = src.ToUpper(); tgt = tgt.ToUpper();
            string s_canTransRes = (canTransform(src, tgt)) ? "Possible" : "Impossible";
            if (ABBAConfig.b_verbose_TDS) Console.WriteLine("ABBA.canObtain(" + src + ", " + tgt + ") --> " + s_canTransRes);
            return s_canTransRes;
        }

        public bool canTransform(string src, string tgt)
        {
            str_trans_stck.Clear();
            for (int i = 0; i < src.Length; i++)
            {
                bool b_AddA_RevStrAddB_avlb = (i == src.Length - 1) ? true : false;
                str_trans_stck.Push(new String_Transform_StackNode(src[i], "Cpy_Frm_Src", b_AddA_RevStrAddB_avlb, b_AddA_RevStrAddB_avlb));
            }
            if (ABBAConfig.b_verbose_TDS) { Console.WriteLine("ABBA.canTransform: [src | tgt] = [" + src + " | " + tgt + "] ... str_trans_stck = >" + obtain_StackTransObj_String() + "<"); Console.WriteLine(""); }

            while (!transformDone(tgt))
            {
                if (!shouldWeBacktrack(tgt))
                {
                    // No need to Backtrack. Keep proceeding ahead by taking further actions.
                    // First determine whether we can proceed ahead. Or if no further action is possible.
                    if (str_trans_stck.Peek().b_AddA_fromthispoint_avlb && str_trans_stck.Peek().b_RevStrAddB_fromthispoint_avlb)
                    {
                        // From this point, it's possible to conduct either/both actions:
                        // i.e. (1) Add A, OR (2) Reverse String & Add B, so choosing at random.
                        if (PRGObj.Next(0, 9) % 2 == 0)
                        {
                            // Add 'A' to end of straight string.
                            // Set the "AddA" bool-flag of Previous-Element to "false" to indicate that we have exhausted that option here.
                            // So that when we eventually backtrack, and check, we don't repeat same action again & again.
                            // Set 3rd & 4th param of Current-Stack-Element being pushed as "true" because if we do backtrack
                            // we can still try out both options from on-top of Current-Stack-Element being pushed.
                            str_trans_stck.Peek().b_AddA_fromthispoint_avlb = false;
                            str_trans_stck.Push(new String_Transform_StackNode('A', "AddA", true, true));
                            if (ABBAConfig.b_verbose_TDS) { Console.WriteLine("ABBA.canTransform: AddA ... Updated str_trans_stck = >" + obtain_StackTransObj_String() + "<"); Console.WriteLine(""); }
                        }
                        else
                        {
                            // Reverse string & add 'B' to the end.
                            // Set the "RevStrAddB" bool-flag of Previous-Element to "false" to indicate that we have exhausted that option here.
                            // So that when we eventually backtrack, and check, we don't repeat same action again & again.
                            str_trans_stck.Peek().b_RevStrAddB_fromthispoint_avlb = false;

                            Stack<String_Transform_StackNode> TMP_str_trans_stck = new Stack<String_Transform_StackNode>();
                            for (int i = 0; i < str_trans_stck.Count; i++)
                                TMP_str_trans_stck.Push(str_trans_stck.ElementAt(i));
                            str_trans_stck.Clear(); str_trans_stck = TMP_str_trans_stck;

                            // Set 3rd & 4th param of Current-Stack-Element being pushed as "true" because if we do backtrack
                            // we can still try out both options from on-top of Current-Stack-Element being pushed.
                            str_trans_stck.Push(new String_Transform_StackNode('B', "RevStrAddB", true, true));
                            if (ABBAConfig.b_verbose_TDS) { Console.WriteLine("ABBA.canTransform: RevStrAddB ... Updated str_trans_stck = >" + obtain_StackTransObj_String() + "<"); Console.WriteLine(""); }
                        }
                    }
                    else if (str_trans_stck.Peek().b_AddA_fromthispoint_avlb)
                    {
                        // From this point, it's possible to conduct only action: Add A; So taking that course of action.
                        // Set the "AddA" bool-flag of Previous-Element to "false" to indicate that we have exhausted that option here.
                        // So that when we eventually backtrack, and check, we don't repeat same action again & again.
                        // Set 3rd & 4th param of Current-Stack-Element being pushed as "true" because if we do backtrack
                        // we can still try out both options from on-top of Current-Stack-Element being pushed.
                        str_trans_stck.Peek().b_AddA_fromthispoint_avlb = false;
                        str_trans_stck.Push(new String_Transform_StackNode('A', "AddA", true, true));
                        if (ABBAConfig.b_verbose_TDS) { Console.WriteLine("ABBA.canTransform: AddA ... Updated str_trans_stck = >" + obtain_StackTransObj_String() + "<"); Console.WriteLine(""); }
                    }
                    else if (str_trans_stck.Peek().b_RevStrAddB_fromthispoint_avlb)
                    {
                        // From this point, it's possible to conduct only action: Reverse String & Add B; So taking that course of action.
                        // Set the "RevStrAddB" bool-flag of Previous-Element to "false" to indicate that we have exhausted that option here.
                        // So that when we eventually backtrack, and check, we don't repeat same action again & again.
                        str_trans_stck.Peek().b_RevStrAddB_fromthispoint_avlb = false;

                        Stack<String_Transform_StackNode> TMP_str_trans_stck = new Stack<String_Transform_StackNode>();
                        for (int i = 0; i < str_trans_stck.Count; i++)
                            TMP_str_trans_stck.Push(str_trans_stck.ElementAt(i));
                        str_trans_stck.Clear(); str_trans_stck = TMP_str_trans_stck;

                        // Set 3rd & 4th param of Current-Stack-Element being pushed as "true" because if we do backtrack
                        // we can still try out both options from on-top of Current-Stack-Element being pushed.
                        str_trans_stck.Push(new String_Transform_StackNode('B', "RevStrAddB", true, true));
                        if (ABBAConfig.b_verbose_TDS) { Console.WriteLine("ABBA.canTransform: RevStrAddB ... Updated str_trans_stck = >" + obtain_StackTransObj_String() + "<"); Console.WriteLine(""); }
                    }
                    else
                    {
                        // From this point, it's NOT possible to conduct either of the actions.
                        // We have reached a stalemate situation. Breaking out of the Loop.
                        if (ABBAConfig.b_verbose_TDS) { Console.WriteLine("ABBA.canTransform: From this point, it's NOT possible to conduct either of the actions. We have reached a stalemate situation. Breaking out of the Loop."); Console.WriteLine(""); }
                        break;
                    }
                }
                else
                {
                    // We need to backtrack. First check whether backtracking is even possible
                    if (str_trans_stck.Count <= src.Length && (!str_trans_stck.Peek().b_AddA_fromthispoint_avlb && !str_trans_stck.Peek().b_RevStrAddB_fromthispoint_avlb))
                    {
                        // We have reached (or gone below) source Length. And neither option is available.
                        // No further backtracking possible. Break out of the Loop.
                        if (ABBAConfig.b_verbose_TDS) { Console.WriteLine("ABBA.canTransform: No backtracking possible. Break out of the Loop."); Console.WriteLine(""); }
                        break;
                    }

                    // Ok, we can backtrack. Let's go back 1 step at a time.
                    string most_recent_previous_action = str_trans_stck.Peek().cur_fwd_action;
                    if (most_recent_previous_action == "AddA")
                    {
                        // Most-Recent-Previous-Action = Add A to straight string; Undoing that.
                        String_Transform_StackNode popped_stacknode = str_trans_stck.Pop();
                        char popped_ch = popped_stacknode.cur_ch;
                        if (ABBAConfig.b_verbose_TDS) { Console.WriteLine("ABBA.canTransform: BACKTRACKING (Undoing AddA). most_recent_previous_action = >" + most_recent_previous_action + "< ... popped_ch = >" + popped_ch + "< ... Updated str_trans_stck = >" + obtain_StackTransObj_String() + "<"); Console.WriteLine(""); }
                        if (ABBAConfig.b_verbose_TDS) { Console.WriteLine("ABBA.canTransform: BACKTRACKING (Undoing AddA). str_trans_stck.Peek().b_AddA_fromthispoint_avlb = >" + str_trans_stck.Peek().b_AddA_fromthispoint_avlb + "<"); Console.WriteLine(""); }
                    }
                    else if (most_recent_previous_action == "RevStrAddB")
                    {
                        // Most-Recent-Previous-Action = Reverse-String + Add B; Undoing that.
                        String_Transform_StackNode popped_stacknode = str_trans_stck.Pop();
                        char popped_ch = popped_stacknode.cur_ch;

                        Stack<String_Transform_StackNode> TMP_str_trans_stck = new Stack<String_Transform_StackNode>();
                        for (int i = 0; i < str_trans_stck.Count; i++)
                            TMP_str_trans_stck.Push(str_trans_stck.ElementAt(i));
                        str_trans_stck.Clear(); str_trans_stck = TMP_str_trans_stck;

                        if (ABBAConfig.b_verbose_TDS) { Console.WriteLine("ABBA.canTransform: BACKTRACKING (Undoing RevStrAddB). most_recent_previous_action = >" + most_recent_previous_action + "< ... popped_ch = >" + popped_ch + "< ... Updated str_trans_stck = >" + obtain_StackTransObj_String() + "<"); Console.WriteLine(""); }
                        if (ABBAConfig.b_verbose_TDS) { Console.WriteLine("ABBA.canTransform: BACKTRACKING (Undoing RevStrAddB). str_trans_stck.Peek().b_RevStrAddB_fromthispoint_avlb = >" + str_trans_stck.Peek().b_RevStrAddB_fromthispoint_avlb + "<"); Console.WriteLine("");
                    }
                }
                    else
                    {
                        // Most-Recent-Previous-Action = Cpy_Frm_Src - i.e. we have reached back to Src-size
                        // No need to Pop or take any other action. Let the loop continue.
                    }
                }
            }
            if (transformDone(tgt))
                return true;
            return false;
        }

        public bool transformDone(string tgt)
        {
            //if (ABBAConfig.b_verbose_TDS) Console.WriteLine("ABBA.transformDone: >" + obtain_StackTransObj_String() + "<");
            if (obtain_StackTrans_String() == tgt)
                return true;
            return false;
        }

        public bool shouldWeBacktrack(string tgt)
        {
            //if (ABBAConfig.b_verbose_TDS) Console.WriteLine("ABBA.transformDone: str_trans_stck.Count = >" + str_trans_stck.Count + "< ... tgt.[Count | Length] = [" + tgt.Count() + " | " + tgt.Length + "]");
            if (str_trans_stck.Count >= tgt.Length && !transformDone(tgt))
            {
                // The Intermediate-String-Stack has reached or exceeded the Size/Length of Target-String
                // And even then, has not been transformed into Target. Hence, we should backtrack & try other options.
                return true;
            }
            if (!str_trans_stck.Peek().b_AddA_fromthispoint_avlb && !str_trans_stck.Peek().b_RevStrAddB_fromthispoint_avlb)
            {
                // There's no way to proceed forward from current Stack-Top element. Both AddA & RevStrAddB have been exhausted.
                // Therefore, we must backtrack & see if there's some earlier Stack-Elements where either or both options are still available.
                // If we don't check for this condition, then Loop will terminate prematurely without exhausting all possible options.
                // Since it's possible that the current Stack-Top might have exhausted both options, but some earlier elements still have options Left.
                return true;
            }
            return false;
        }
    }

    class AutoTest_ABBA
    {
        private static Random PRGObj = new Random((int)(DateTime.Now.Ticks));
        private const int MIN_NUM_TEST = 10, MAX_NUM_TESTS = 100;
        public void autoTest_SpecificCases()
        {
            ABBA ABBAObj = new ABBA();
            string[] srcs = { "B", "AB", "BBAB", "BBBBABABBBBBBA", "A" };
            string[] tgts = { "ABBA", "ABB", "ABABABABB", "BBBBABABBABBBBBBABABBBBBBBBABAABBBAA", "BB" };

            for (int i = 0; i < srcs.Length && i < tgts.Length; i++)
            {
                Console.WriteLine("AutoTest_ABBA.autoTest_SpecificCases: [srcs[" + i + "] | tgts[" + i + "]] = [" + srcs[i] + " | " + tgts[i] + "] ... Result = >" + ABBAObj.canObtain(srcs[i], tgts[i]) + "<");
                Console.WriteLine(""); Console.WriteLine(""); Console.WriteLine("");
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
            } while (tgtsize > srcsize);
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
                Console.WriteLine(""); Console.WriteLine(""); Console.WriteLine("");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AutoTest_ABBA AutoTest_ABBA_Obj = new AutoTest_ABBA();
            //AutoTest_ABBA_Obj.autoRandSingleTest();
            //Console.WriteLine(""); Console.WriteLine("");
            //AutoTest_ABBA_Obj.autoRandMultipleTests();
            //Console.WriteLine(""); Console.WriteLine("");
            AutoTest_ABBA_Obj.autoTest_SpecificCases();
        }
    }
}
