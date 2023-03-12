using System;
using System.Collections.Generic;
using System.Globalization;

namespace NextBiggerTask
{
    public static class NumberExtension
    {
        /// <summary>
        /// Finds the nearest largest integer consisting of the digits of the given positive integer number and null if no such number exists.
        /// </summary>
        /// <param name="number">Source number.</param>
        /// <returns>
        /// The nearest largest integer consisting of the digits  of the given positive integer and null if no such number exists.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when source number is less than 0.</exception>
        public static int? NextBiggerThan(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("mess", nameof(number));
            }
            else if (number <= 10)
            {
                return null;
            }
            else if (number == int.MaxValue)
            {
                return null;
            }

            List<int> list = new List<int>();
            while (number > 0)
            {
                list.Add(number % 10);
                number /= 10;
            }

            bool notExist = false;
            bool enough = false;
            list.Reverse();
            int i = 0;
            while (i < list.Count - 1 && !enough)
            {
                if (list[i] >= list[i + 1])
                {
                    notExist = true;
                }
                else
                {
                    notExist = false;
                    enough = true;
                }

                i++;
            }

            if (notExist)
            {
                return null;
            }

            i = 0;
            int buff;
            bool isDone = false;
            bool sec = false;
            int sizeOfSec = 1;
            int countOfSec = 0;
            int indOfSec = 0;
            List<int> indOfPSec = new List<int>();
            for (; i <= list.Count - 1; i++)
            {
                if (i + 1 == list.Count)
                {
                    if (sec)
                    {
                        for (int k = 1; k <= indOfPSec.Count - 1; k += 2)
                        {
                            if (indOfPSec[k] > 1)
                            {
                                indOfSec = indOfPSec[k - 1];
                                sizeOfSec = indOfPSec[k];
                            }
                        }

                        buff = list[indOfSec + sizeOfSec - 1];
                        list[indOfSec + sizeOfSec - 1] = list[indOfSec + sizeOfSec - 2];
                        list[indOfSec + sizeOfSec - 2] = buff;
                    }
                    else
                    {
                        if (list[list.Count - 1] == list[list.Count - 2])
                        {
                            buff = list[list.Count - 2];
                            list[list.Count - 2] = list[list.Count - 3];
                            list[list.Count - 3] = buff;
                            isDone = true;
                        }
                        else
                        {
                            buff = list[list.Count - 2];
                            list[list.Count - 2] = list[list.Count - 1];
                            list[list.Count - 1] = buff;
                            isDone = true;
                        }
                    }
                }
                else if (list[i] > list[i + 1])
                {
                    countOfSec++;
                    indOfPSec.Add(indOfSec);
                    indOfPSec.Add(sizeOfSec);
                    indOfSec = i + 1;
                    sec = true;
                    sizeOfSec = 1;
                }
                else
                {
                    sizeOfSec++;
                    sec = false;
                }
            }

            if (!isDone)
            {
                List<int> newList = list.GetRange(indOfSec + sizeOfSec - 1, list.Count - sizeOfSec);
                newList.Sort();
                list.RemoveRange(indOfSec + sizeOfSec - 1, list.Count - sizeOfSec);
                list.AddRange(newList);
            }
      
            int[] array = list.ToArray();
            int result = 0;
            for (int m = 0; m < array.Length; m++)
            {
                result += array[m] * Convert.ToInt32(Math.Pow(10, array.Length - m - 1));
            }

            return result;
        }
    }
}
