using BigML;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Iris
{
    class TestLocalPredictions
    {
        static void Main()
        {
            MainAsync().Wait();
            Console.ReadLine();
        }

        static async Task MainAsync()
        {
            var client = new Client("rm0", "448715de0d1ee3b7d25029fc66bf4d2f0b9c15f1", vpcDomain:"realmatch.vpc.bigml.io");
            client = new Client("rm1", "c4aef8539db2e8dbbee17ab0d188dec28f1e7933", vpcDomain: "realmatch.vpc.bigml.io");

            Ensemble ensemble;
            string ensembleId = "ensemble/5831e35c4fb51223a30024ea";
            // No push, so we need to busy wait for the source to be processed.
            while ((ensemble = await client.Get<Ensemble>(ensembleId)).StatusMessage.NotSuccessOrFail())
            {
                await Task.Delay(-1);
            }
            var localEnsemble = ensemble.EnsembleStructure;
            // populate each model tree
            Model modelInEnsemble = null;
            modelInEnsemble = new Model();
            string modelId;
            for (int i = 0; i < ensemble.Models.Count; i++)
            {
                modelId = ensemble.Models[i];
                while ((modelInEnsemble = await client.Get<Model>(modelId)).StatusMessage.NotSuccessOrFail())
                {
                    await Task.Delay(-1);
                }
                if (i == 0)
                {
                    localEnsemble.addLocalModel(modelInEnsemble.ModelStructure(null));
                } else
                {
                    localEnsemble.addLocalModel(modelInEnsemble.ModelStructure(localEnsemble._models[0].Fields));
                }
                
                Console.WriteLine((i+1) + "/" + ensemble.Models.Count + " models loaded");

                //modelInEnsemble.Save();
            }
            
            Dictionary<string, dynamic> inputData;
            Dictionary<object, object> results;
            /*
            inputData = new Dictionary<string, dynamic>();
            inputData.Add("ZipsSize", 1);
            inputData.Add("DaysPublished", 29.99973);
            inputData.Add("JobTitleId", "600");
            inputData.Add("SubCategoryId", "4");
            inputData.Add("Location", "Hamburg, NJ");
            inputData.Add("ActivationDate", 9);
            inputData.Add("ExpirationDate", 10);
            inputData.Add("AffiliateId", "2029");
            inputData.Add("HasSalary", "0");
            inputData.Add("HasCompanyLogo", "0");
            inputData.Add("HasCompanyViewInformation", "0");
            inputData.Add("ZipCode", "07419");
            inputData.Add("NumOfSkills", 0);
            inputData.Add("NumOfEducations", 0);
            inputData.Add("Budget", 21);
            inputData.Add("SF_City_Competition", 1);
            inputData.Add("IsPriority", "0");
            inputData.Add("IsFeatured", "0");
            inputData.Add("IsFullTime", "1");
            inputData.Add("IsPartTime", "0");
            inputData.Add("IsCommissionOnly", "0");
            inputData.Add("IsWorkFromHome", "0");
            inputData.Add("IsShiftBased", "0");
            inputData.Add("IsTemporarySeasonal", "0");
            inputData.Add("IsInternship", "0");
            inputData.Add("IsFranchiseOffer", "0");
            inputData.Add("ManagerialLevel", 0);
            inputData.Add("Affiliate_Impressions_Factor", 1712077);
            inputData.Add("Location_Impressions_Factor", 57729);
            inputData.Add("JobTitle_Views_Factor", 35221);
            inputData.Add("JobTitle_Applies_Factor", 3295);

            results = localEnsemble.predict(inputData, combiner: Combiner.Plurality);*/
            /*
            inputData = new Dictionary<string, dynamic>();
            inputData.Add("000001", 1);
            inputData.Add("000002", 29.99973);
            inputData.Add("000003", "600");
            inputData.Add("000004", "4");
            inputData.Add("000005", "Hamburg, NJ");
            inputData.Add("000006", 9);
            inputData.Add("000007", 10);
            inputData.Add("000008", "2029");
            inputData.Add("000009", "0");
            inputData.Add("00000a", "0");
            inputData.Add("00000b", "0");
            inputData.Add("00000c", "07419");
            inputData.Add("00000d", 0);
            inputData.Add("00000e", 0);
            inputData.Add("00000f", 21);
            inputData.Add("000010", 1);
            inputData.Add("000011", "0");
            inputData.Add("000012", "0");
            inputData.Add("000013", "1");
            inputData.Add("000014", "0");
            inputData.Add("000015", "0");
            inputData.Add("000016", "0");
            inputData.Add("000017", "0");
            inputData.Add("000018", "0");
            inputData.Add("000019", "0");
            inputData.Add("00001a", "0");
            inputData.Add("00001b", 0);
            inputData.Add("00001c", 1712077);
            inputData.Add("00001d", 57729);
            inputData.Add("00001e", 35221);
            inputData.Add("00001f", 3295);
            */

            double diff = 0.0;
            string fileName = "Validation_2016-09-29_16-08-45.csv";

            var reader = new System.IO.StreamReader(System.IO.File.OpenRead(@"C:/Users/Jose/Downloads/" + fileName));
            List<string> listFields = new List<string>();
            List<string> listIDs = new List<string>();
            List<dynamic> listValues = new List<dynamic>();
            string line = reader.ReadLine();
            listFields.AddRange(line.Split(';'));
            int index;
            string fieldName;

            for (index = 0; index < listFields.Count; index++)
            {
                fieldName = listFields[index];
                listIDs.Add(modelInEnsemble.ModelStructure().getFieldByName(fieldName).Id);
            }

            string[] values;            
            double totalDiffPow = 0.0;
            int N = 0;
            
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = ",";
            provider.NumberGroupSizes = new int[] { 3 };

            //System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/Users/Jose/Downloads/LocalPreditions.txt");
            DateTime t1 = DateTime.Now;
            if (modelInEnsemble != null)
            {
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    values = line.Split(';');
                    listValues.Clear();
                    listValues.AddRange(values);

                    inputData = new Dictionary<string, dynamic>();
                    for (index = 0; index < listFields.Count; index++)
                    {
                        fieldName = listFields[index];

                        if (modelInEnsemble.ModelStructure().getFieldByName(fieldName).Optype == OpType.Numeric)
                        {
                            listValues[index] = Convert.ToDouble(listValues[index], provider);
                        }

                        inputData.Add(listIDs[index], listValues[index]);
                    }
                    N += 1;

                    results = localEnsemble.predict(inputData, byName:false, combiner: Combiner.Plurality, addDistribution: false);

                    diff = Convert.ToDouble(listValues[0]) - (double)results["prediction"];
                    //file.WriteLine(results["prediction"]);
                    totalDiffPow += (diff * diff);
                }
            }
            DateTime t2 = DateTime.Now;
            TimeSpan t3 = t2.Subtract(t1);

            //file.Close();
            Console.WriteLine(N + " predictions done");
            Console.WriteLine("");

            // sqrt( sum( predicted-actual )^2 / N)
            var RMSE = Math.Sqrt(totalDiffPow / N);
            Console.WriteLine("SE: " + totalDiffPow);
            Console.WriteLine("MSE: " + totalDiffPow / N);
            Console.WriteLine("RMSE: " + RMSE);
            Console.WriteLine("Time elapsed " + t3.TotalSeconds + " s.");
            Console.WriteLine("tpp: " + t3.TotalSeconds * 1000 / N + " ms.");
        }
    }
}
