using System.Numerics;

namespace plusbigint
{
    public static class Program
    {

        static void Main(string[] args)
        {
            //string a = "1";
            //string a = "99999999999999999999999999";
            string a = "9999999999999999999999999999999999999999" +
                "";
            //string b = "2";
            string b = "44444444443333322222";

            uint[] numA = ConvertToUIntArray(a);
            uint[] numB = ConvertToUIntArray(b);

            uint[] result = UIntArrayAddition(numA, numB);

            string stringReult = ConvertToString(result);
            Console.WriteLine("ผลลัพธ์การบวก: " + stringReult.TrimStart('0'));
        }

        public static uint[] ConvertToUIntArray(string number)
        {
            // แบ่ง string ออกเป็นช่วงๆละ 9 หลักเพื่อให้ง่ายต่อการแปลงเป็น uint[]
            int chunkSize = 9;
            int arraySize = (number.Length + chunkSize - 1) / chunkSize;
            uint[] result = new uint[arraySize];

            int index = 0;
            for (int i = number.Length; i > 0; i -= chunkSize)
            {
                int start = Math.Max(0, i - chunkSize);
                int length = i - start;
                result[index] = uint.Parse(number.Substring(start, length));
                index++;
            }

            return result;
        }

        public static string ConvertToString(uint[] number)
        {
            string result = "";
            //bool leadingZero = true;

            for (int i = number.Length - 1; i >= 0; i--)
            {
                //if (leadingZero && number[i] == 0)
                //    continue;

                //leadingZero = false;
                result += number[i].ToString().PadLeft(9, '0');
            }

            return /*leadingZero ? "0" :*/ result;
        }

        public static uint[] UIntArrayAddition(uint[] numA, uint[] numB)
        {
            int maxLength = Math.Max(numA.Length, numB.Length);
            uint[] result = new uint[maxLength + 1];

            ulong carry = 0;

            for (int i = 0; i < maxLength; i++)
            {
                ulong sum = carry;
                if (i < numA.Length)
                    sum += numA[i];
                if (i < numB.Length)
                    sum += numB[i];

                result[i] = (uint)(sum % 1_000_000_000); // ใช้ modulo 1 พันล้าน
                carry = sum / 1_000_000_000;
            }

            if (carry > 0)
            {
                result[maxLength] = (uint)carry;
            }

            return result;
        }

        //99999999999999999999999999
        //99999999999999999999999999999999999999999999
    }
}
