namespace myStatisticCalc
{
    public class StatisticCalc
    {
        //the use of Auto-implemented property
        public double Sum { get; set; }
        public double Avg { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public double Mode { get; set; }
        public double Med { get; set; }
        public double Var { get; set; }
        public double SD { get; set; }

       
        public static double SumOfData(double[] data)
        {
            double sum = 0;
            for (var i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            return sum;
        }

        public static double MeanOfData(double[] data)
        {
            double mean = 0;
            for (var i = 0; i < data.Length; i++)
            {
                mean += data[i] / data.Length;
            }
            return mean;
        }

        public static double VarianceOfData(double[] data)
        {
            double mean = MeanOfData(data);
            double variance = 0;
            for(var i = 0; i < data.Length; i++)
            {
                variance += Math.Pow(data[i] - mean, 2) / (data.Length - 1); 
            }
            return variance;
        }

        public static double StandardDeviationOfData(double[] data)
        {
            double variance = VarianceOfData(data);
            double standardDeviation = Math.Sqrt(variance);
            return standardDeviation;
        }

        public static double MaxNumber(double[] data)
        {
            double max = data[0];
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                }
            }
            return max;
        }
        public static double MinNumber(double[] data)
        {
            double min = data[0];
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] < min)
                {
                    min = data[i];
                }
            }
            return min;
        }

        public static double[] ModeOfData(double[] data)
        {
            var frequency = new Dictionary<double, int>();
            foreach (var value in data)
            {
                if (frequency.ContainsKey(value))
                {
                    frequency[value]++;
                }
                else
                {
                    frequency[value] = 1;
                }
            }

            int maxCount = frequency.Values.Max();
            var modes = frequency.Where(pair => pair.Value == maxCount)
                .Select(pair => pair.Key)
                .ToArray();
            
            return modes;
        }

        public static double MedianOfData(double[] data)
        {
            if (data ==  null || data.Length == 0)
            {
                throw new ArgumentException("Data Array cannot be null or Empty");
            }
            var orderedData = data.OrderBy(x => x).ToArray();
            int n = orderedData.Length;
            if (n % 2 == 1) //odd numbers of data
            {
                return orderedData[n / 2];
            }
            else //even number of data
            {
                return (orderedData[(n / 2) - 1] + orderedData[n / 2]) / 2.0;
            }
        }
    }


}
