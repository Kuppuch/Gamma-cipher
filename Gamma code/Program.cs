using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamma_code {
    class Program {
        static string[] alphaCodes = new string[] {
            "000001",  "001001",  "001010",  "001011",  "001100",  "000010", "001101",  "001110",  "000011",  "001111",  "010000",  "010001", "010010",  "000100",
            "010011",  "010100",  "010101",  "010110", "000101",  "010111",  "011000",  "011001",  "011010",  "011011", "011100",  "011101",  "011110",  "000110",
            "000111",  "001000",  "100000", "110010"
        };

        static char[] alpha = new char[] {
            'а',  'б',  'в',  'г',  'д',  'е', 'ж',  'з',  'и',  'к',  'л',  'м', 'н',  'о',  'п',  'р',  'с',  'т', 'у',  'ф',  'х',  'ц',  'ч',  'ш', 'щ',  'ы',
            'ь',  'э',  'ю',  'я', 'й', ' '
        };
        static void Main(string[] args) {

            int[] code = new int[] { 2, 3, 10, 4, 1, 5, 6, 7, 8, 11, 15, 14, 12, 13, 9, 0, };
            string input = "Помехоустойчивое кодирование это кодирование с возможностью восстановления потерянных или ошибочно принятых данных";
            input = input.ToLower();

            string cryptText = Crypt(code, input).Trim();
            Console.WriteLine(cryptText);
            Console.WriteLine();

            DeCrypt(code, cryptText);

            Console.ReadKey();
        }

        public static string Crypt(int[] code, string input) {

            string cryptText = "";
            int index;
            for (int i = 0; i < input.Length; i++) {
                index = Array.IndexOf(alpha, input[i]);
                if (index == -1)
                    continue;
                cryptText += Comparison(/*alpha[index],*/ alphaCodes[index], /*code[i % code.Length],*/  ConvertNumber(code[i % code.Length])) + " ";
            }
            
            Console.WriteLine();
            return cryptText;
        }

        public static string ConvertNumber(int num) {
            string convNum = Convert.ToString(num, 2);
            int count = convNum.Length;
            for (int i = 0; i < 6- count; i++) {
                convNum = "0" + convNum;
            }
            //Console.WriteLine(convNum);
            return convNum;
        }

        public static string Comparison(/*char alpha,*/ string alphaCod, /*int numb,*/ string convNumb) {

            string result = "";

            for (int i = alphaCod.Length-1; i >= 0; i--) {
                result = ((int)alphaCod[i] ^ (int)convNumb[i]) + result;
            }

            Console.WriteLine(/*alpha + " = " +*/ alphaCod + "  ^  "/* + numb + " = "*/ + convNumb + "  =  " + result);
            
            return result;
        }

        ///////////////////////////// DeCrypt ///////////////////////////////////
        
        public static void DeCrypt(int[] code, string cryptText) {

            string[] parse = cryptText.Split(' ');
            string[] output = new string[parse.Length];

            for (int i = 0; i < parse.Length; i++) {
                output[i] = Comparison(parse[i], ConvertNumber(code[i % code.Length]));
            }

            int index;
            string trueText = "";
            for (int i = 0; i < output.Length; i++) {
                index = Array.IndexOf(alphaCodes, output[i]);
                if (index == -1)
                    continue;
                trueText += alpha[index];
            }

            Console.WriteLine();
            Console.WriteLine(trueText);
            //return output;
        }

    }
}