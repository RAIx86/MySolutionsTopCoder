// TopCoder - Problem Name: ATaleOfThreeCities(500 points) [MEDIUM]
// Class Name : ATaleOfThreeCities
// Method Name : connect
// Return Type : double
// Arg Types : (int[], int[], int[], int[], int[], int[])

// Status on TopCoder: Compiled Successfully; Cleared Initial/Sample Tests; Cleared Wider System Tests
// TopCoder Link(see link for Problem Statement): https://arena.topcoder.com/#/u/practiceCode/1499/3281/3543/2/1499

using System;

namespace TC_ATaleOfThreeCities_500pts
{
    class TC_GlobalConfig
    {
        static public bool b_verbose_TDS = true;
    }
    class TwoCityMinDistance
    {
        private string c1_name, c2_name;
        private int c1_index, c2_index;
        private double c1c2_mindist;

        public TwoCityMinDistance(string c1_name, string c2_name)
        { setParams(c1_name, c2_name); }

        public void setParams(string c1_name, string c2_name)
        { this.c1_name = c1_name; this.c2_name = c2_name; }

        public void setParams(int c1_index, int c2_index, double c1c2_mindist)
        {
            this.c1_index = c1_index; this.c2_index = c2_index;
            this.c1c2_mindist = c1c2_mindist;
        }

        public override string ToString()
        { return "[" + c1_name + " | " + c2_name + " | " + c1_index + " | " + c2_index + " | " + c1c2_mindist + "]"; }
    }

    class ATaleOfThreeCities
    {
        public double connect(int[] ax, int[] ay, int[] bx, int[] by, int[] cx, int[] cy)
        {
            //double xdiffsqd = 0, ydiffsqd = 0;
            double cur_pyth_dist = 0, minimal_added_distance = 0;
            double AB_mindist, BC_mindist, AC_mindist;
            AB_mindist = BC_mindist = AC_mindist = double.MaxValue;
            TwoCityMinDistance AB_mindist_obj = new TwoCityMinDistance("A", "B");
            TwoCityMinDistance BC_mindist_obj = new TwoCityMinDistance("B", "C");
            TwoCityMinDistance AC_mindist_obj = new TwoCityMinDistance("A", "C");

            for (int i = 0; i < ax.Length && i < ay.Length; i++)
            {
                if (TC_GlobalConfig.b_verbose_TDS) Console.WriteLine("ATaleOfThreeCities.connect: {ax[" + i + "] | ay[" + i + "]} = {" + ax[i] + " | " + ay[i] + "}");
                for (int j = 0; j < bx.Length && j < by.Length; j++)
                {
                    if (TC_GlobalConfig.b_verbose_TDS) Console.WriteLine("ATaleOfThreeCities.connect: {bx[" + j + "] | by[" + j + "]} = {" + bx[j] + " | " + by[j] + "}");
                    //xdiffsqd = ((ax[i] - bx[j]) * (ax[i] - bx[j])); ydiffsqd = ((ay[i] - by[j]) * (ay[i] - by[j])); cur_pyth_dist = Math.Sqrt(xdiffsqd + ydiffsqd);
                    cur_pyth_dist = Math.Sqrt((ax[i] - bx[j]) * (ax[i] - bx[j]) + (ay[i] - by[j]) * (ay[i] - by[j]));
                    if (cur_pyth_dist < AB_mindist)
                    {
                        if (TC_GlobalConfig.b_verbose_TDS) Console.WriteLine("ATaleOfThreeCities.connect: AB_mindist updated for [a_index(i) | b_index(j)] = [" + i + " | " + j + "] ... [Prev_Value | New_Value] = [" + AB_mindist + " | " + cur_pyth_dist + "]");
                        AB_mindist = cur_pyth_dist;
                        AB_mindist_obj.setParams(i, j, AB_mindist);
                    }

                    if (i == 0)
                    {
                        for (int k = 0; k < cx.Length && k < cy.Length; k++)
                        {
                            if (j==0 && TC_GlobalConfig.b_verbose_TDS) Console.WriteLine("ATaleOfThreeCities.connect: {cx[" + k + "] | cy[" + k + "]} = {" + cx[k] + " | " + cy[k] + "}");
                            //xdiffsqd = ((bx[j] - cx[k]) * (bx[j] - cx[k])); ydiffsqd = ((by[j] - cy[k]) * (by[j] - cy[k])); cur_pyth_dist = Math.Sqrt(xdiffsqd + ydiffsqd);
                            cur_pyth_dist = Math.Sqrt((bx[j] - cx[k]) * (bx[j] - cx[k]) + (by[j] - cy[k]) * (by[j] - cy[k]));
                            if (cur_pyth_dist < BC_mindist)
                            {
                                if (TC_GlobalConfig.b_verbose_TDS) Console.WriteLine("ATaleOfThreeCities.connect: BC_mindist updated for [b_index(j) | c_index(k)] = [" + j + " | " + k + "] ... [Prev_Value | New_Value] = [" + BC_mindist + " | " + cur_pyth_dist + "]");
                                BC_mindist = cur_pyth_dist;
                                BC_mindist_obj.setParams(j, k, BC_mindist);
                            }
                        }
                    }
                }

                for (int j = 0; j < cx.Length && j < cy.Length; j++)
                {
                    //if (TC_GlobalConfig.b_verbose_TDS) Console.WriteLine("ATaleOfThreeCities.connect: {cx[" + j + "] | cy[" + j + "]} = {" + cx[j] + " | " + cy[j] + "}");
                    //xdiffsqd = ((ax[i] - cx[j]) * (ax[i] - cx[j])); ydiffsqd = ((ay[i] - cy[j]) * (ay[i] - cy[j])); cur_pyth_dist = Math.Sqrt(xdiffsqd + ydiffsqd);
                    cur_pyth_dist = Math.Sqrt((ax[i] - cx[j]) * (ax[i] - cx[j]) + (ay[i] - cy[j]) * (ay[i] - cy[j]));
                    if (cur_pyth_dist < AC_mindist)
                    {
                        if (TC_GlobalConfig.b_verbose_TDS) Console.WriteLine("ATaleOfThreeCities.connect: AC_mindist updated for [a_index(i) | c_index(j)] = [" + i + " | " + j + "] ... [Prev_Value | New_Value] = [" + AC_mindist + " | " + cur_pyth_dist + "]");
                        AC_mindist = cur_pyth_dist;
                        AC_mindist_obj.setParams(i, j, AC_mindist);
                    }
                }
            }

            minimal_added_distance = Math.Min(AB_mindist + BC_mindist, Math.Min(BC_mindist + AC_mindist, AB_mindist + AC_mindist));

            if (TC_GlobalConfig.b_verbose_TDS)
            {
                Console.WriteLine("");
                Console.WriteLine("ATaleOfThreeCities.connect: [AB_mindist | BC_mindist | AC_mindist] = [" + AB_mindist + " | " + BC_mindist + " | " + AC_mindist + "]");
                Console.WriteLine("ATaleOfThreeCities.connect: [AB_mindist_obj | BC_mindist_obj | AC_mindist_obj] = [" + AB_mindist_obj.ToString() + " | " + BC_mindist_obj.ToString() + " | " + AC_mindist_obj.ToString() + "]");
                Console.WriteLine("");
                Console.WriteLine("ATaleOfThreeCities.connect: minimal_added_distance = " + minimal_added_distance);
            }

            return minimal_added_distance;
        }
    }

    class ATaleOfThreeCities_Test
    {
        private const int MIN_NUM_TESTS = 10, MAX_NUM_TESTS = 100;
        private System.Random PRGObj = new System.Random();
        private int[] ax, ay, bx, by, cx, cy;

        private void generateRandData()
        {
            int num_elems = 0;
            num_elems = PRGObj.Next(2, 50);
            ax = generateRandArr(num_elems);
            ay = generateRandArr(num_elems);

            num_elems = PRGObj.Next(2, 50);
            bx = generateRandArr(num_elems);
            by = generateRandArr(num_elems);

            num_elems = PRGObj.Next(2, 50);
            cx = generateRandArr(num_elems);
            cy = generateRandArr(num_elems);
        }

        private int[] generateRandArr(int num_elems)
        {
            int[] arr = new int[num_elems];
            for (int i = 0; i < num_elems; i++)
                arr[i] = PRGObj.Next(-100, 100);
            return arr;
        }

        public void autoRegulateSpecificTests()
        {
            ATaleOfThreeCities ATaleOfThreeCities_Obj = new ATaleOfThreeCities();
            double minimal_added_distance = 0;
            // Test=1
            if (TC_GlobalConfig.b_verbose_TDS) Console.WriteLine("ATaleOfThreeCities_Test.autoRegulateSpecificTests: Conducting Specific-Test#1 (see ProbStmt)");
            int[] ax1 = { 0, 0, 0 }; int[] ay1 = { 0, 1, 2 };
            int[] bx1 = { 2, 3 }; int[] by1 = { 1, 1 };
            int[] cx1 = { 1, 5 }; int[] cy1 = { 3, 28 };
            minimal_added_distance = ATaleOfThreeCities_Obj.connect(ax1, ay1, bx1, by1, cx1, cy1);
            if (TC_GlobalConfig.b_verbose_TDS)
            {
                Console.WriteLine("ATaleOfThreeCities_Test.autoRegulateSpecificTests: minimal_added_distance = " + minimal_added_distance);
                Console.WriteLine("");
            }

            // Test=2
            if (TC_GlobalConfig.b_verbose_TDS) Console.WriteLine("ATaleOfThreeCities_Test.autoRegulateSpecificTests: Conducting Specific-Test#2 (see ProbStmt)");
            int[] ax2 = { -2, -1, 0, 1, 2 }; int[] ay2 = { 0, 0, 0, 0, 0 };
            int[] bx2 = { -2, -1, 0, 1, 2 }; int[] by2 = { 1, 1, 1, 1, 1 };
            int[] cx2 = { -2, -1, 0, 1, 2 }; int[] cy2 = { 2, 2, 2, 2, 2 };
            minimal_added_distance = ATaleOfThreeCities_Obj.connect(ax2, ay2, bx2, by2, cx2, cy2);
            if (TC_GlobalConfig.b_verbose_TDS)
            {
                Console.WriteLine("ATaleOfThreeCities_Test.autoRegulateSpecificTests: minimal_added_distance = " + minimal_added_distance);
                Console.WriteLine("");
            }

            // Test=3
            if (TC_GlobalConfig.b_verbose_TDS) Console.WriteLine("ATaleOfThreeCities_Test.autoRegulateSpecificTests: Conducting Specific-Test#3 (see ProbStmt)");
            int[] ax3 = { 4, 5, 11, 21, 8, 10, 3, 9, 5, 6 }; int[] ay3 = { 12, 8, 9, 12, 2, 3, 5, 7, 10, 13 };
            int[] bx3 = { 34, 35, 36, 41, 32, 39, 41, 37, 39, 50 }; int[] by3 = { 51, 33, 41, 45, 48, 22, 33, 51, 41, 40 };
            int[] cx3 = { 86, 75, 78, 81, 89, 77, 83, 88, 99, 77 }; int[] cy3 = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
            minimal_added_distance = ATaleOfThreeCities_Obj.connect(ax3, ay3, bx3, by3, cx3, cy3);
            if (TC_GlobalConfig.b_verbose_TDS)
            {
                Console.WriteLine("ATaleOfThreeCities_Test.autoRegulateSpecificTests: minimal_added_distance = " + minimal_added_distance);
                Console.WriteLine("");
            }
        }

        public void autoRegulateSingleRandomTest()
        {
            this.generateRandData();
            ATaleOfThreeCities ATaleOfThreeCities_Obj = new ATaleOfThreeCities();
            double minimal_added_distance = ATaleOfThreeCities_Obj.connect(ax, ay, bx, by, cx, cy);
            if (TC_GlobalConfig.b_verbose_TDS)
            {
                Console.WriteLine("ATaleOfThreeCities_Test.autoRegulateSingleRandomTest: minimal_added_distance = " + minimal_added_distance);
                Console.WriteLine("");
            }
        }

        public void autoRegulateMultiRandomTests()
        {
            int num_tests = PRGObj.Next(MIN_NUM_TESTS, MAX_NUM_TESTS);
            if (TC_GlobalConfig.b_verbose_TDS) Console.WriteLine("ATaleOfThreeCities_Test.autoRegulateMultiRandomTests: Conducting " + num_tests + " tests with random data");
            for (int i = 0; i < num_tests; i++)
            {
                if (TC_GlobalConfig.b_verbose_TDS)
                {
                    Console.WriteLine("");
                    Console.WriteLine("ATaleOfThreeCities_Test.autoRegulateMultiRandomTests: Test #" + i + " --> BEGINS");
                }
                autoRegulateSingleRandomTest();
                if (TC_GlobalConfig.b_verbose_TDS)
                {
                    Console.WriteLine("ATaleOfThreeCities_Test.autoRegulateMultiRandomTests: Test #" + i + " <-- ENDS");
                    Console.WriteLine("");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ATaleOfThreeCities_Test ATaleOfThreeCities_Test_Obj = new ATaleOfThreeCities_Test();
            ATaleOfThreeCities_Test_Obj.autoRegulateSpecificTests();
            ATaleOfThreeCities_Test_Obj.autoRegulateMultiRandomTests();
        }
    }
}
