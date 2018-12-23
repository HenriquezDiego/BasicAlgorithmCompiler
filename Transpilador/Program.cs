using System;
using CompiladorLISP;


namespace Transpilador
{
   public class Program
    {
        public static void Main(string[] args)
        {
            //var input = "(asignar ( / (+ (+  (- y x) a)  c)3  )  )";
            //var inputEasy = "(/(+ a b)c)";
            //var inputHeavy = "(= m((/(+ a b)(-a b))))";
            var parente = "(asignar m((-a b)(+a b)(+ a c)))";
            parente = parente.Replace("asignar m", "=•");

            var conver = new Conversorcs();
            var result = conver.Compile(parente);

            Console.WriteLine(result);
            Console.ReadLine();


        }



    }
}
