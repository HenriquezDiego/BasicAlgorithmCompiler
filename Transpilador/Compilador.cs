using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Transpilador
{
    class Compilador
    {
        private IList<string> _list;
        private Dictionary<string, string> _dictionary;
        private IList<string> _listAux;
        private string FinalResult;
        public Compilador()
        {
             FinalResult = "";
            _listAux = new List<string>();
            _dictionary = new Dictionary<string, string>();
            _list = new List<string>();
        }

        private string CustomTrim(string parm)
        {
            var resul = "";
            var value = parm.ToCharArray();
            foreach (var i in value)
            {
                if (!i.Equals(' ')) resul += i;
            }

            return resul;
        }


        public void Fragmentar(string parm)
        {
           
            const string pattern = @"\(([^()]*[^()]*)\)";
            var regex = new Regex(pattern);

            if (regex.IsMatch(parm))
            {
                var i = regex.Match(parm).ToString();
                Console.WriteLine(i);
                _list.Add(i);
                parm = parm.Replace(i, " ");
                Fragmentar(parm);
            }
            
        }

        public void Reverse()
        {
            const string pattern = @"[+\-\*\/=]";
            var regex = new Regex(pattern);
            var result = "";
            foreach (var item in _list)
            {
                var replace = item.ToCharArray();
                bool flag = false;

                for (int i = 0; i < replace.Length; i++)
                {
                    if (regex.IsMatch(replace[i].ToString()) && !flag)
                    {
                        var j = i+1;
                        result += replace[j].ToString();
                        result += replace[i].ToString();
                        flag = true;
                        i++;
                    }
                    else
                    {
                        result += replace[i].ToString();
                    }

                    
                }
                //_dictionary.Add(_list.IndexOf(item), result);
                _listAux.Add(result);
                FinalResult += result;
                result = "";
            }

           
            Console.WriteLine(result);

        }
      
        public void Compile()
        {
            FinalResult = "";
            var aux = "";
            for (int i = 0; i < _list.Count; i++)
            {
                aux = _listAux[i];
                aux = aux.Replace(" ", (i-1).ToString());
                _listAux[i] = aux;
                FinalResult += aux;
                _dictionary.Add(i+"?", aux);

            }

            aux = "";
            for (int i = 0; i < _listAux.Count; i++)
            {
                //_finalResult.Replace(i.ToString(), _dictionary[i]);
                aux = _dictionary[i+"?"];
                if (i <= 0)
                {
                    //other += aux.Replace(i.ToString(), _dictionary[(i)]);
                }
                else
                {
                    var x = i - 1;
                    var other = aux.Replace(x.ToString(), _dictionary[x+"?"]);
                    _dictionary[i+"?"] = other;
                }
            }

           // Console.WriteLine(_dictionary[(_dictionary.Count-1)+"?"]);
            
        }


        public string CompileDone(string parm)
        {

            Fragmentar(CustomTrim(parm));
            Reverse();
            Compile();
            return _dictionary[(_dictionary.Count - 1) + "?"];
        }

      

    }

}
