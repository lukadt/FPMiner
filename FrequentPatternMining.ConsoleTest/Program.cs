using System;
using System.Collections.Generic;
using System.Text;
using FrequentPatternMining.Entities;
using FrequentPatternMining.APrioriOpt;
using System.Diagnostics;


namespace FrequentPatternMining.ConsoleTest
{

    public class Products
    {
        public readonly int a = 1;  public readonly int b = 2;  public readonly int c = 3;
        public readonly int d = 4;  public readonly int e = 5;  public readonly int f = 6;
        public readonly int g = 7;  public readonly int h = 8;  public readonly int i = 9;
        public readonly int j = 10; public readonly int k = 11; public readonly int l = 12;
        public readonly int m = 13; public readonly int n = 14; public readonly int o = 15;
        public readonly int p = 16; public readonly int s = 17;        
    }
    
    public class Program
    {
        static void Main(string[] args)
        {
            List<Transaction> data = LoadSampleData();
            
            Double minsup = 0.6;
            
            Stopwatch timer = new Stopwatch();
            timer.Reset();
            
            AprioriOpt aprioriOptimized = new AprioriOpt();                        
            aprioriOptimized.SetMinSup(minsup);
            
            timer.Start();
            List<ItemSet> frequentAprioriPattern = aprioriOptimized.ExtractFrequentPattern(data);
            timer.Stop();

            Console.WriteLine("*-----Dumping Frequent Pattern Extracted by Apriori -----*\n");
            Console.WriteLine("******  Execution time: {0} ms  *******\n",timer.ElapsedMilliseconds);
            for (int i = 0; i < frequentAprioriPattern.Count; i++)
            {
                Console.WriteLine("\tFrequent Pattern Number {0}:" + frequentAprioriPattern[i].ToString(),i+1);
            }

            
            FrequentPatternMining.FPGrowth.FPGrowth fpGrowth = new FrequentPatternMining.FPGrowth.FPGrowth();
            fpGrowth.SetMinSup(minsup);
            
            timer.Reset();
            timer.Start();
            List<ItemSet> frequentFPGrowthPattern = fpGrowth.ExtractFrequentPattern(data);
            timer.Stop();
            Console.WriteLine("\n*-----Dumping Frequent Pattern Extracted by FPGrowth -----*\n");
            Console.WriteLine("******  Execution time: {0} ms  ******\n", timer.ElapsedMilliseconds);

            for (int i = 0; i < frequentFPGrowthPattern.Count; i++)
            {
                Console.WriteLine("\tFrequent Pattern Number {0}:" + frequentFPGrowthPattern[i].ToString(), i+1);
            }
            Console.ReadLine();            
        }

        private static List<Transaction> LoadSampleData()
        {
            List<Transaction> db = new List<Transaction>();
            Products prodotti = new Products();
            Transaction t1 = new Transaction();
            Transaction t2 = new Transaction();
            Transaction t3 = new Transaction();
            Transaction t4 = new Transaction();
            Transaction t5 = new Transaction();

            #region Populate Transaction 
            t1.Id = 100;
            t1.addItem(prodotti.f);
            t1.addItem(prodotti.a);
            t1.addItem(prodotti.c);
            t1.addItem(prodotti.d);
            t1.addItem(prodotti.g);
            t1.addItem(prodotti.i);
            t1.addItem(prodotti.m);
            t1.addItem(prodotti.p);

            t2.Id = 200;
            t2.addItem(prodotti.a);
            t2.addItem(prodotti.b);
            t2.addItem(prodotti.c);
            t2.addItem(prodotti.f);
            t2.addItem(prodotti.l);
            t2.addItem(prodotti.m);
            t2.addItem(prodotti.o);

            t3.Id = 300;
            t3.addItem(prodotti.b);
            t3.addItem(prodotti.f);
            t3.addItem(prodotti.h);
            t3.addItem(prodotti.j);
            t3.addItem(prodotti.o);

            t4.Id = 400;
            t4.addItem(prodotti.b);
            t4.addItem(prodotti.c);
            t4.addItem(prodotti.k);
            t4.addItem(prodotti.s);
            t4.addItem(prodotti.p);

            t5.Id = 500;
            t5.addItem(prodotti.a);
            t5.addItem(prodotti.f);
            t5.addItem(prodotti.c);
            t5.addItem(prodotti.e);
            t5.addItem(prodotti.l);
            t5.addItem(prodotti.p);
            t5.addItem(prodotti.m);
            t5.addItem(prodotti.n);
            
            #endregion

            db.Add(t1);
            db.Add(t2);
            db.Add(t3);
            db.Add(t4);
            db.Add(t5);

            return db;
        }
    }
}
