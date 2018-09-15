// TopCoder - Problem Name: AcademicJournal(1000 points) [HARD]
// Class Name : AcademicJournal
// Method Name : rankByImpact
// Return Type : string[]
// Arg Types : (string[])

// Status on TopCoder: Compiled Successfully; Cleared Initial/Sample Tests
// TopCoder Link(see link for Problem Statement): https://arena.topcoder.com/#/u/practiceCode/1489/3716/4020/2/1489

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

//namespace TC_AcademicJournal_1000p
//{
    //static class GlobConf_AcadJour
    //{
    //    public static bool b_TDSVerbose = false;
    //}

    class AcademicJournal
    {
        private class PaperJournalIndices
        {
            public string journame;
            public List<int> out_indices = new List<int>();
            public int in_citations;
            //public override string ToString() { return "[" + journame + " | " + out_indices.ToString() + " | " + in_citations + "]"; }
        }

        private static PaperJournalIndices[] paperJournalIndices;
        private static Dictionary<string, List<int>> JourIndicesMap = new Dictionary<string, List<int>>();
        private static Dictionary<string, double> JournImpFactMap = new Dictionary<string, double>();

        private void Init_clear_all()
        { JourIndicesMap.Clear(); JournImpFactMap.Clear(); }

        private void Convert_Papers_PJI_DS(string[] papers)
        {
            try
            {
                Init_clear_all();
                paperJournalIndices = new PaperJournalIndices[papers.Count()];
                for (int i = 0; i < papers.Count(); i++)
                {
                    paperJournalIndices[i] = new PaperJournalIndices();
                    // Setting the Incoming-Citation-Count for each Paper to Zero
                    paperJournalIndices[i].in_citations = 0;

                    // Splitting string on Dot, and taking LHS-string to be Journal-Name
                    string[] curpaper_dotsplit = papers[i].Split('.');
                    Debug.Assert(curpaper_dotsplit.Count() == 2);
                    paperJournalIndices[i].journame = curpaper_dotsplit[0];
                    //if (GlobConf_AcadJour.b_TDSVerbose) { Console.WriteLine("AcademicJournal.convert_Papers_PJI_DS: paperJournalIndices[" + i + "].journame: " + paperJournalIndices[i].journame); }

                    // RHS-string is further split on Spaces, and used to form out_indices array
                    curpaper_dotsplit[1] = curpaper_dotsplit[1].Trim(' ');
                    string[] curpaper_spacesplit = curpaper_dotsplit[1].Split(' ');
                    //if (GlobConf_AcadJour.b_TDSVerbose) { Console.WriteLine("AcademicJournal.convert_Papers_PJI_DS: curpaper_dotsplit = [" + curpaper_dotsplit[0] + "|" + curpaper_dotsplit[1] + "] ... curpaper_spacesplit.Count() = >" + curpaper_spacesplit.Count() + "<"); }
                    //if (GlobConf_AcadJour.b_TDSVerbose) Console.Write("AcademicJournal.convert_Papers_PJI_DS: paperJournalIndices[" + i + "].out_indices: >>");
                    for (int j = 0; j < curpaper_spacesplit.Count(); j++)
                    {
                        // Remove leading & trailing white-space, so as to prevent adding spurious/zero elements. And try to Parse an Integer.
                        curpaper_spacesplit[j] = curpaper_spacesplit[j].Trim(' ');
                        if (curpaper_spacesplit[j] == "") continue;
                        int.TryParse(curpaper_spacesplit[j], out int cur_parsed_int);

                        // Check whether the Parsed-Int (representing a tentative outgoing-index) has occured earlier in the List. If so, do NOT add.
                        // This is for the "sloppy author citing multiple times" & "count citations from one paper to another only once" criteria.
                        if (!paperJournalIndices[i].out_indices.Contains(cur_parsed_int))
                            paperJournalIndices[i].out_indices.Add(cur_parsed_int);

                        //if (GlobConf_AcadJour.b_TDSVerbose)
                        //{
                        //    Console.Write(paperJournalIndices[i].out_indices.Last());
                        //    if (j != curpaper_spacesplit.Count() - 1)
                        //        Console.Write(", ");
                        //}
                    }
                    //if (GlobConf_AcadJour.b_TDSVerbose) { Console.Write("<<"); Console.WriteLine(""); Console.WriteLine(""); }

                    // Construct the Journal-Indices-Map Dictionary
                    // (Key=Journal-Name; Value=List of Indices where Journal-Name occurs)
                    if (!JourIndicesMap.ContainsKey(paperJournalIndices[i].journame))
                        JourIndicesMap[paperJournalIndices[i].journame] = new List<int>();
                    JourIndicesMap[paperJournalIndices[i].journame].Add(i);
                }
            }
            catch(Exception excep) { Console.WriteLine("Exception occured! excep = >" + excep + "< ... Details = >" + excep.StackTrace + "<"); }
        }

        public string[] rankByImpact(string[] papers)
        {
            //if (GlobConf_AcadJour.b_TDSVerbose) DisplayPapersRankedJournals("papers", papers);
            Convert_Papers_PJI_DS(papers);

            try
            {
                for (int i = 0; i < paperJournalIndices.Count(); i++)
                {
                    //if (GlobConf_AcadJour.b_TDSVerbose) { Console.WriteLine("AcademicJournal.rankByImpact: i=" + i + " ... paperJournalIndices[" + i + "] = >" + paperJournalIndices[i].ToString() + "< ... paperJournalIndices[" + i + "].out_indices.Count() = >" + paperJournalIndices[i].out_indices.Count() + "<"); }
                    for (int j = 0; j < paperJournalIndices[i].out_indices.Count(); j++)
                    {
                        if (paperJournalIndices[i].out_indices[j] < 0 || paperJournalIndices[i].out_indices[j] >= paperJournalIndices.Count())
                            // For some reason, the jth Out-Index (which will be used to index into paperJournalIndices) is beyond bounds.
                            // This should theoretically not happen, given proper data-sets. But might arise due to Randomly-Generated Data-Sets.
                            // So we guard against this condition, and simply skip processing for this Index. By continuing the Loop.
                            continue;

                        //if (GlobConf_AcadJour.b_TDSVerbose) { Console.WriteLine("AcademicJournal.rankByImpact: j=" + j + " ... paperJournalIndices[" + i + "].out_indices[" + j + "] = >" + paperJournalIndices[i].out_indices[j].ToString() + "< ... paperJournalIndices[paperJournalIndices[" + i + "].out_indices[" + j + "]] = >" + paperJournalIndices[paperJournalIndices[i].out_indices[j]].ToString() + "<"); }
                        if (paperJournalIndices[paperJournalIndices[i].out_indices[j]].journame == paperJournalIndices[i].journame)
                        {
                            // Journal-Name of papers at Index=paperJournalIndices[i].out_indices[j] & current-Index=i are the same.
                            // Outgoing-Index of Current-Element 'a' (at index=i) points to some other Element 'b' (at index=outgoing-index)
                            // If the Journal-Names at these two places match, then the Outgoing-Citation from 'a' to 'b' is Null & Void.
                            // Since citations among Papers published in Same Journal is to be disregarded (as per rules).
                            // Hence, don't count this - i.e. take no action. Do nothing.
                            // Note, outgoing-citation from 'a' to 'b' = more to be interpreted as incoming-citation for 'b'
                            //if (GlobConf_AcadJour.b_TDSVerbose) Console.WriteLine("AcademicJournal.rankByImpact: Journal-Names match b/w Indices i=" + i + " & paperJournalIndices[" + i + "].out_indices[" + j + "] = " + paperJournalIndices[i].out_indices[j] + " ... Hence, taking no action! Not adding citation-number/impact-factor!");
                        }
                        else
                        {
                            // Journal-Names of paper at the two indices do NOT match. Hence, outgoing-citation from 'a' to 'b' can be considered.
                            // Put another way, incoming-citation for 'b' (i.e. at paperJournalIndices[i].out_indices[j] index) can be incremented.
                            paperJournalIndices[paperJournalIndices[i].out_indices[j]].in_citations++;
                            //if (GlobConf_AcadJour.b_TDSVerbose) Console.WriteLine("AcademicJournal.rankByImpact: Journal-Names do NOT match ... Hence, taking action! By adding citation-number/impact-factor! New-Value (of paperJournalIndices[" + paperJournalIndices[i].out_indices[j] + "].in_citations): >" + paperJournalIndices[paperJournalIndices[i].out_indices[j]].in_citations + "<");
                        }
                    }
                }

                // Calculating Impact-Factor for Each Journal (and constructing a Dictionary)
                //if (GlobConf_AcadJour.b_TDSVerbose) Console.WriteLine("");
                foreach (KeyValuePair<string, List<int>> cur_jim_kvpair in JourIndicesMap)
                {
                    int total_in_cit_allpap_curjour = 0;
                    foreach (int cur_index in cur_jim_kvpair.Value)
                        total_in_cit_allpap_curjour += paperJournalIndices[cur_index].in_citations;

                    if (!JournImpFactMap.ContainsKey(cur_jim_kvpair.Key))
                        JournImpFactMap[cur_jim_kvpair.Key] = (total_in_cit_allpap_curjour / cur_jim_kvpair.Value.Count);

                    //if (GlobConf_AcadJour.b_TDSVerbose) Console.WriteLine("AcademicJournal.rankByImpact: cur_jim_kvpair.[Key|Value|Value.Count] = [" + cur_jim_kvpair.Key + "|" + cur_jim_kvpair.Value + "|" + cur_jim_kvpair.Value.Count() + "] ... total_in_cit_allpap_curjour = >" + total_in_cit_allpap_curjour + "< ... JournImpFactMap[" + cur_jim_kvpair.Key + "] = >" + JournImpFactMap[cur_jim_kvpair.Key] + "<");
                }

                //if (GlobConf_AcadJour.b_TDSVerbose) { Console.WriteLine(""); DisplayDicts(); Console.WriteLine(""); }
                string[] rankedjournals = new string[JournImpFactMap.Count()];
                // Ordering the "JournImpFactMap" Dictionary by Descending Order based on Values
                IEnumerable<KeyValuePair<string, double>> DescByVal_Dict_JIFM = JournImpFactMap.OrderByDescending(x => x.Value);
                for (int i = 0; i < DescByVal_Dict_JIFM.Count();)
                {
                    // Obtaining Current-Impact-Factor-Value, and checking for whether this Value is repeated many times.
                    double cur_impfac_ival = DescByVal_Dict_JIFM.ElementAt(i).Value;
                    // Forming a List of all Repeated Impact-Factor-Values (which we will later lexicographically sort).
                    List<KeyValuePair<string, double>> List_SameImpFac = new List<KeyValuePair<string, double>>();
                    List_SameImpFac.Add(new KeyValuePair<string, double>(DescByVal_Dict_JIFM.ElementAt(i).Key, DescByVal_Dict_JIFM.ElementAt(i).Value));
                    for (int j = i + 1; j < DescByVal_Dict_JIFM.Count(); j++)
                    {
                        if (cur_impfac_ival != DescByVal_Dict_JIFM.ElementAt(j).Value)
                            // Since Dictionary is already sorted in Descending Order by Value, if Repeated values occur, they'll be a continuous chain.
                            // Hence, if we encounter a situation where values don't match, then that's the end of the Road w.r.t. checking for Repetitions.
                            break;
                        // As long as above condition isn't met, the Values are repeating. Keep adding into List.
                        List_SameImpFac.Add(new KeyValuePair<string, double>(DescByVal_Dict_JIFM.ElementAt(j).Key, DescByVal_Dict_JIFM.ElementAt(j).Value));
                    }

                    if (List_SameImpFac.Count() == 1)
                    {
                        // Only 1-element (Current-Impact-Factor-Value under consideration) in List - i.e. no subsequent repetitions found.
                        // Safe to add the Dictionary-Element as-is directly to the final RankedJournals List.
                        //if (GlobConf_AcadJour.b_TDSVerbose) Console.WriteLine("AcademicJournal.rankByImpact: i=" + i + ": NO repetitions found for Current-Impact-Factor-Value under consideration. Safe to add the Dictionary-Element as-is directly to the final RankedJournals.");
                        rankedjournals[i] = DescByVal_Dict_JIFM.ElementAt(i).Key; i++;
                    }
                    else
                    {
                        // Lexicographically sort the List in Ascending Order (as per Key-Name), and then assign in-sequence to RankedJournals
                        //if (GlobConf_AcadJour.b_TDSVerbose) Console.WriteLine("AcademicJournal.rankByImpact: i=" + i + ": Repetitions found for Current-Impact-Factor-Value under consideration. Lexicographically sort the List in Ascending Order (as per Key-Name), and then assign in-sequence to RankedJournals.");
                        IEnumerable<KeyValuePair<string, double>> AscByKey_List_SIF = List_SameImpFac.OrderBy(x => x.Key);
                        for (int j = 0; j < AscByKey_List_SIF.Count(); j++)
                        {
                            rankedjournals[i] = AscByKey_List_SIF.ElementAt(j).Key; i++;
                        }
                    }
                }
                //if (GlobConf_AcadJour.b_TDSVerbose) DisplayPapersRankedJournals("rankedjournals", rankedjournals);
                return rankedjournals;
            }
            catch (Exception excep)
            {
                Console.WriteLine("Exception occured! excep = >" + excep + "< ... Details = >" + excep.StackTrace + "<");
                string[] rankedjournals = { }; return rankedjournals;
            }
        }

        //public void DisplayDicts()
        //{
        //    Debug.Assert(JourIndicesMap.Count() == JournImpFactMap.Count());
        //    for(int i=0; i<JourIndicesMap.Count(); i++)
        //    {
        //        Console.WriteLine("AcademicJournal.displayDicts: JourIndicesMap.ElementAt(" + i + ") = " + JourIndicesMap.ElementAt(i).ToString());
        //        Console.WriteLine("AcademicJournal.displayDicts: JournImpFactMap.ElementAt(" + i + ") = " + JournImpFactMap.ElementAt(i));
        //    }
        //}

        //public void DisplayPapersRankedJournals(string str_arr_type, string[] pap_rnkd_jrnl)
        //{
        //    for (int i = 0; i < pap_rnkd_jrnl.Count(); i++)
        //        Console.WriteLine("AcademicJournal.displayRankedJournals:" + str_arr_type + "[" + i + "] = >" + pap_rnkd_jrnl[i] + "<");
        //}
    }

    //class AutoTest_AcademicJournal
    //{
    //    //private const int MIN_NUM_PAPERS = 10, MAX_NUM_PAPERS = 20;
    //    private const int MIN_NUM_PAPERS = 1, MAX_NUM_PAPERS = 50;
    //    private const int MIN_NUM_OUT_INDICES = 0, MAX_NUM_OUT_INDICES = 10;
    //    private const int MIN_NUM_TESTS = 10, MAX_NUM_TESTS = 20;
    //    private Random PRGObj = new Random((int)(DateTime.Now.Ticks));

    //    public string[] GenerateRandData()
    //    {
    //        int numpapers = PRGObj.Next(MIN_NUM_PAPERS, MAX_NUM_PAPERS);
    //        string[] papers = new string[numpapers];
    //        string journames = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    //        for(int i=0; i<numpapers; i++)
    //        {
    //            papers[i] = journames[PRGObj.Next(0, journames.Length - 1) % journames.Length] + ".";
    //            int num_cur_out_indices = PRGObj.Next(MIN_NUM_OUT_INDICES, numpapers);
    //            for(int j=0; j<num_cur_out_indices; j++)
    //            {
    //                if (j != num_cur_out_indices - 1)
    //                    papers[i] += " ";
    //                papers[i] += (PRGObj.Next(0, numpapers-1) % numpapers);
    //            }
    //        }
    //        return papers;
    //    }

    //    public void AutoRandSingleTest()
    //    {
    //        string[] papers = GenerateRandData();
    //        AcademicJournal AcadJourObj = new AcademicJournal();
    //        AcadJourObj.DisplayPapersRankedJournals("papers", papers);
    //        AcadJourObj.DisplayPapersRankedJournals("rankedjournals", AcadJourObj.rankByImpact(papers));
    //        Console.WriteLine(""); Console.WriteLine("");
    //    }
    //    public void AutoRandMultiTest()
    //    {
    //        int numtests = PRGObj.Next(MIN_NUM_TESTS, MAX_NUM_TESTS);
    //        for (int i=0; i<numtests; i++)
    //        {
    //            Console.WriteLine("AutoTest_AcademicJournal.AutoRandMultiTest: Conducting Test#" + (i+1) + " -->");
    //            AutoRandSingleTest();
    //            Console.WriteLine("AutoTest_AcademicJournal.AutoRandMultiTest: Test#" + (i + 1) + " <-- END.");
    //            Console.WriteLine(""); Console.WriteLine("");
    //        }
    //    }

    //    public void AutoTest_SpecificCases()
    //    {
    //        AcademicJournal AcadJourObj = new AcademicJournal();
    //        string[] papers1 = { "A.", "B. 0", "C. 1 0 3", "C. 2" };
    //        string[] papers2 = { "RESPECTED JOURNAL.", "MEDIOCRE JOURNAL. 0", "LOUSY JOURNAL. 0 1", "RESPECTED JOURNAL.", "MEDIOCRE JOURNAL. 3", "LOUSY JOURNAL. 4 3 3 4", "RESPECTED SPECIFIC JOURNAL.", "MEDIOCRE SPECIFIC JOURNAL. 6", "LOUSY SPECIFIC JOURNAL. 6 7" };
    //        string[] papers3 = { "NO CITATIONS.", "COMPLETELY ORIGINAL." };
    //        string[] papers4 = { "CONTEMPORARY PHYSICS. 5 4 6 8 7 1 9", "EUROPHYSICS LETTERS. 9", "J PHYS CHEM REF D. 5 4 6 8 7 1 9", "J PHYS SOC JAPAN. 5 4 6 8 7 1 9", "PHYSICAL REVIEW LETTERS. 5 6 8 7 1 9", "PHYSICS LETTERS B. 6 8 7 1 9", "PHYSICS REPORTS. 8 7 1 9", "PHYSICS TODAY. 1 9", "REP PROGRESS PHYSICS. 7 1 9", "REV MODERN PHYSICS." };
    //        string[][] allpapers = { papers1, papers2, papers3, papers4 };

    //        for(int i=0; i<allpapers.Count(); i++)
    //        {
    //            string[] rankedjournals = AcadJourObj.rankByImpact(allpapers[i]);
    //            AcadJourObj.DisplayPapersRankedJournals("allpapers[" + i + "]", allpapers[i]);
    //            AcadJourObj.DisplayPapersRankedJournals("rankedjournals", rankedjournals);
    //            Console.WriteLine(""); Console.WriteLine("");
    //        }
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        AutoTest_AcademicJournal AutoTest_AcadmicJournal_Obj = new AutoTest_AcademicJournal();
    //        AutoTest_AcadmicJournal_Obj.AutoRandSingleTest();
    //        Console.WriteLine(""); Console.WriteLine("");
    //        AutoTest_AcadmicJournal_Obj.AutoRandMultiTest();
    //        Console.WriteLine(""); Console.WriteLine("");
    //        AutoTest_AcadmicJournal_Obj.AutoTest_SpecificCases();
    //        Console.WriteLine(""); Console.WriteLine("");
    //    }
    //}
//}   // END_NAMESPACE
