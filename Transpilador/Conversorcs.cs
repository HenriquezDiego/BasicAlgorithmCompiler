using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;

namespace CompiladorLISP
{
    public class Conversorcs
    {
        // char specialChar = '•';
        private ArrayList _list;
        private ArrayList _listAux;
        private string _finalResult;
        public Conversorcs()
        {

            _finalResult = "";
            ListAux = new ArrayList();
            _list = new ArrayList();
        }

        private static string CustomTrim(string parm)
        {
            var value = parm.ToCharArray();
          
            return value.Where(i => !i.Equals(' '))
                    .Aggregate("", (current, i) => current + i);
        }

        //Fragmentar expresión
        private void Extraer(string parm)
        {

            const string pattern = @"\(([^()]*[^()]*)\)";
            var regex = new Regex(pattern);

            if (!regex.IsMatch(parm)) return;
            var i = regex.Match(parm).ToString();

            _list.Add(i);
            parm = parm.Replace(i, " ");


            Extraer(parm);

        }

        private void InvertirSignos()
        {
            const string pattern = @"[+\-\*\/=]";
            var regex = new Regex(pattern);
            var result = "";
            foreach (var item in _list)
            {
                var expresion = item.ToString();
                var flag = true;

                for (var i = 0; i < expresion.Length; i++)
                {
                    if (regex.IsMatch(expresion[i].ToString()) && flag)
                    {
                        result += expresion[(i + 1)].ToString();
                        result += expresion[i].ToString();
                        flag = false;
                        i++;
                    }
                    else
                    {
                        result += expresion[i].ToString();
                    }

                }
                ListAux.Add(result);
                result = "";
            }

            ListAux.Reverse();
            _list = (ArrayList)ListAux.Clone();
            Verify(ListAux);

            var parm = ListAux[0].ToString();
            Remplazar(parm, 0);

        }

        private int _space = 1;
        public ArrayList ListAux
        {
            get
            {
                var arrayList = _listAux;
                return arrayList;
            }
            set => _listAux = value;
        }

        private void Remplazar(string parm, int index)
        {
            if (index >= ListAux.Count - 1) return;
            foreach (var item in parm)
            {

                if (item.Equals(' ') && (_space < ListAux.Count))
                {

                    var aux = ListAux[_space].ToString();
                    _finalResult += aux;
                    _space++;
                }
                else
                {
                    _finalResult += item;
                }
            }

            index++;

            _list[index] = _finalResult;
            _finalResult = "";
            Remplazar(_list[index].ToString(), index);

        }
        public void VerifyOrder(IList list) {
            var count = 0;
            var listOrdenada = new ArrayList();
            
            foreach (var item in list)
            {
                if (item.ToString().Contains(" ")) continue;
                listOrdenada.Add(item);
                count++;
            }
            listOrdenada.Reverse();
            
            if (count > 1)
            {
                var index = 0;
                var j = 0;
                foreach (var item in _list)
                {
                    if (!item.ToString().Contains(" "))
                    {
                        ListAux[index] = listOrdenada[j];
                        j++;
                    }
                    index++;
                }
            }

        }
        public  string Compile(string line)
        {
            var newLine = CustomTrim(line);
            Extraer(newLine);
            InvertirSignos();
            _finalResult = _list[(ListAux.Count - 1)].ToString();
            _finalResult = _finalResult.Replace("•", "m");
            _finalResult = _finalResult.Remove(0, 1);
            var index = _finalResult.Length - 1;
            _finalResult = _finalResult.Remove(index,1);
            return _finalResult;
        }

        public void Verify(ArrayList list)
        {
            var index = 0;
            var count = 0;
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var item in list)
            {
                if (item.ToString().Contains(" "))
                {
                    index=list.IndexOf(item);
                }
                else
                {
                    count++;
                }
                
                
            }

            list.Reverse(index+1, count);

        }
    }

}
