using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmProject
{
    public class Invest
    {
        private List<decimal> Price = new List<decimal>
        { 6012037, 15011070, 3066057, 5090040, 2500306, 17736990, 1030063, 11800530, 19040100, 4000520, 23061070, 14700090 };
        private List<decimal> Profit = new List<decimal>
            { 560000, 240000, 95000, 41000, 114900, 1700000, 750000, 578000, 635680, 137000, 890000, 117000 };
        private const decimal Money = 100000000;
        private const decimal P = Money / 1000;
        public List<int> Indexs = new List<int>();

        public decimal[] BackTracking()
        {
            decimal[] Result = new decimal[13];
            List<decimal> temp = new List<decimal>();
            for (int j = 0; j < 13; j++)
                temp.Add(-2);
            decimal w = 0;
            decimal p = 0;
            decimal MaxP = 0;
            int i = 0;
            while (true)
            {
                if (i == temp.Count-1)
                {
                    bool end = true;
                    if (temp[i - 1] == 1)
                    {
                        i--;
                        w -= Price[i];
                        p -= Profit[i];
                        temp[i] = 0;
                    }
                    for (int j = temp.Count - 2; j >= 0; j--)
                    {
                        if (temp[j] != 0)
                        {
                            end = false;
                            w -= Price[j];
                            p -= Profit[j];
                            temp[j] = 0;
                            i = j + 1;
                            break;
                        }
                    }
                    if (end)
                        break;
                }
                else if (w + Price[i] <= Money || temp[i + 1] == -1)
                {
                    if (temp[i + 1] == -1)
                    {
                        i++;
                        temp[i] = 0;
                    }
                    else
                    {
                        w += Price[i];
                        p += Profit[i];
                        temp[i] = 1;
                        if (p > MaxP)
                        {
                            MaxP = p;
                            temp.CopyTo(Result);
                        }
                    }
                    i++;
                }
                else
                {
                    temp[i] = -1;
                    if (p > MaxP)
                    {
                        MaxP = p;
                        temp.CopyTo(Result);
                    }
                    i--;
                }
            }
            Result[Result.Length-1] = MaxP;
            return Result;
        }
        public decimal DynamicProgramming(decimal Capacity, decimal[] price, decimal[] profit, int number)
        {
            decimal[,] result = new decimal[13, 1001];
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 1001; j++)
                {
                    if (i == 0 || j == 0)
                        result[i, j] = 0;
                    else
                    {
                        if (price[i - 1] > (j * P))
                        {
                            result[i, j] = result[i - 1, j];
                        }
                        else
                        {
                            int t = (int)((j * P - Price[i - 1]) / P);
                            result[i, j] = Max(result[i - 1, j], result[i - 1, t] + profit[i - 1], i - 1);
                        }

                    }
                }
            }

            int x = 12;
            int y = 1000;
            while (x > 0)
            {
                if ((result[x, y] != result[x - 1, y]) && Capacity >= Price[x])
                {
                    Indexs.Add(x);
                    Capacity -= Price[x];
                    x--;
                    y = (int)(Capacity / P);
                }
                else
                {
                    x--;
                }
            }

            return result[12, 1000];

            //if (number == 0 || Capacity == 0)
            //    return 0;

            //if (price[number - 1] > Capacity)
            //    return DynamicProgramming(Capacity, price, profit, number - 1);

            //else 
            //    return Max(profit[number - 1] +
            //    DynamicProgramming(Capacity - price[number - 1], price, profit, number - 1),
            //           DynamicProgramming(Capacity, price, profit, number - 1),number-1);
        }
        public List<int> PriceGreedy()
        {
            decimal[] arr = AscendingSort(Price.ToArray());
            List<int> AnswerIndexs = new List<int>();
            decimal capacity = Money;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] <= capacity)
                {
                    capacity -= arr[i];
                    AnswerIndexs.Add(Price.FindIndex(x => x == arr[i]));
                }
            }
            return AnswerIndexs;
        }
        public List<int> ProfitGreedy()
        {
            decimal[] arr = DescendingSort(Profit.ToArray());
            List<int> AnswerIndexs = new List<int>();
            decimal capacity = Money;
            for (int i = 0; i < arr.Length; i++)
            {
                int index = Profit.FindIndex(x => x == arr[i]);
                if (Price[index] <= capacity)
                {
                    capacity -= Price[index];
                    AnswerIndexs.Add(index);
                }
            }
            return AnswerIndexs;
        }
        decimal Max(decimal a, decimal b, int index)
        {
            if (b > a)
            {
                //if (!Indexs.Contains(index))
                //    Indexs.Add(index);
                return b;
            }
            return a;
        }
        public decimal[] AscendingSort(decimal[] value)
        {
            //decimal[] arr = value;
            Array.Sort(value);
            return value;
        }
        public decimal[] DescendingSort(decimal[] value)
        {
            //decimal[] arr = value;
            Array.Sort(value);
            Array.Reverse(value);
            return value;
        }
    }
}
