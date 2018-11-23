using System;


namespace Transpilador
{
   public class Program
    {
        static void Main(string[] args)
        {
            string input = "(=m (/(+ (+ (- y x) a)c)3))";
            string inputEasy = "(/(+ a b)c)";

            var compilador = new Compilador();


            Console.WriteLine(CustomTrim(input));
            
            compilador.Fragmentar(CustomTrim(inputEasy));
            compilador.Reverse();
            compilador.Compile();
            Console.ReadLine();


        }

        static string CustomTrim(string parm)
        {
            var resul = "";
            var value = parm.ToCharArray();
            foreach (var i in value)
            {
                if(!i.Equals(' ')) resul += i;
            }
            
            return resul;
        }


    }
}
