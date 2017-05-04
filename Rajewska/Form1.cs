using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rajewska
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //String alfabetMale = "aąbcćdeęfghijklłmnńoópqrsśtuvwxyzźż";//35 znaków
        String alfabetMale = "abcdefghijklmnopqrstuvwxyz";//35 znaków
        String alfabetDuze;
        private void Form1_Load(object sender, EventArgs e)
        {
            alfabetDuze = alfabetMale.ToUpper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cezar.Text= SzyfrCezar(Jawny.Text,-1);
            Rot.Text= SzyfrRot13(Jawny.Text);
            Polibiusz.Text = SzyfrPolibiusza(Jawny.Text);
            Gaderypoluki.Text = SzyfrGaderypoluki(Jawny.Text, "GA - DE - RY - PO - LU - KI");
            Przestawieniowy.Text = SzyfrPrzestawieniowy(Jawny.Text,3);
            At.Text = AtBash(Jawny.Text);
            Bacon.Text = SzyfrBacona(Jawny.Text);
            Vigener.Text= SzyfrVigener(Jawny.Text, "TAJNE");
        }
        private string SzyfrCezar(string value, int shift)
        {
            char[] buffer = value.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (letter != ' ')
                {
                    int index = -1;
                    index = alfabetMale.IndexOf(letter);
                    if (index!=-1)
                    {
                        index+=shift;
                        if (index > 0) index = (index >= alfabetMale.Count()) ? (alfabetMale.Length - 1 + shift) - alfabetMale.Length : index;
                        else index = (alfabetMale.Length - 1) - alfabetMale.IndexOf(letter);
                        letter = alfabetMale[index];
                    }

                    index = alfabetDuze.IndexOf(letter);
                    if (index != -1)
                    {
                        index+=shift;
                        if (index>0) index = (index >= alfabetDuze.Count()) ? (alfabetDuze.Length - 1 + shift) - alfabetDuze.Length : index;
                        else index = (alfabetDuze.Length - 1) - alfabetDuze.IndexOf(letter);
                        letter = alfabetDuze[index];
                    }
                    buffer[i] = letter;
                }
            }
            return new string(buffer);
        }
        private string SzyfrRot13(string value)
        {
            char[] buffer = value.ToCharArray();
            for (int i = 0; i < buffer.Length; i++)
            {
                char letter = buffer[i];
                if (letter != ' ')
                {
                    int index = -1;
                    index = alfabetMale.IndexOf(letter);
                    if (index != -1)
                    {
                        index += 13;
                        index = (index >= alfabetMale.Count()) ? (alfabetMale.Length - 1 + 13) - alfabetMale.Length : index;
                        letter = alfabetMale[index];
                    }

                    index = alfabetDuze.IndexOf(letter);
                    if (index != -1)
                    {
                        index += 13;
                        index = (index >= alfabetDuze.Count()) ? (alfabetDuze.Length - 1 + 13) - alfabetDuze.Length : index;
                        letter = alfabetDuze[index];
                    }
                    buffer[i] = letter;
                }
            }
            return new string(buffer);
        }
        private string SzyfrPolibiusza(string text)
        {
            text=text.ToLower();
            text = text.Replace('j', 'i');
            string result = "";
            char[,] szachownica = new char[,]
            {
                {'a','b','c','d','e' },
                {'f','g','h','i','k' },
                {'l','m','n','o','p' },
                {'q','r','s','t','u' },
                {'v','w','x','y','z' }
            };

            for (int i = 0; i < text.Length; i++)
                if (text[i]== ' ')
                    result += " ";
                else
                    for (int j = 0; j < 5; j++)
                        for (int k = 0; k < 5; k++)
                            if (text[i] == szachownica[j, k])
                                result += j.ToString() + k.ToString()+" ";
            return result;
        }
        private string SzyfrGaderypoluki(string text, string haslo)
        {
            StringBuilder bufor = new StringBuilder(text.ToUpper());
            haslo = haslo.ToUpper();
            haslo=haslo.Replace("-", "");
            haslo = haslo.Replace(" ", "");

            for (int i = 0; i < bufor.Length; i ++)
            {
                for (int i2 = 0; i2 < haslo.Length; i2+=2)
                {
                    char a = haslo[i2];
                    char b = haslo[i2 + 1];
                    if (bufor[i] == a)
                    {
                        bufor[i] = b;
                        break;
                    }
                    if (bufor[i] == b)
                    {
                        bufor[i] = a;
                        break;
                    }
                }
                
            }
            return bufor.ToString();
        }
        private string SzyfrPrzestawieniowy(string text, int wGrupie)
        {
            StringBuilder temp = new StringBuilder(text);
            for (int i = 0; i < temp.Length; i+=wGrupie)
                for (int i2 = i; i2 <(i+wGrupie/2) && (i2+wGrupie<=temp.Length); i2++)
                {
                    int id = i + wGrupie-1 ;
                    char x = temp[id];
                    temp[id] = temp[i2];
                    temp[i2] = x;
                }
            return temp.ToString();
        }
        private string SzyfrBacona(string text)
        {
            string alfabet = alfabetMale.Replace("j", "").Replace("v", "");
            text = text.ToLower();
            StringBuilder temp = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
                if(text[i]!=' ')
                    temp.Append(Convert.ToString(alfabet.IndexOf(text[i]), 2).PadLeft(5, '0').Replace("0", "A").Replace("1", "B") + " ");
            return temp.ToString();
        }
        private string AtBash(string text)
        {
                text=text.ToLower();
                char[] tablica = text.ToCharArray();
                for (int i = 0; i < text.Length; i++)
                    if (!text[i].Equals(" "))
                        if (alfabetMale.IndexOf(text[i])!=-1)
                            tablica[i] = alfabetMale[ alfabetMale.Length-1-alfabetMale.IndexOf(text[i])];
                        else if (alfabetDuze.IndexOf(text[i]) != -1)
                            tablica[i] = alfabetDuze[alfabetDuze.Length - 1 - alfabetDuze.IndexOf(text[i])];
            return new string(tablica);
        }
        private string SzyfrVigener(string text,string haslo)
        {
            haslo = haslo.ToUpper();
            text = text.ToUpper();
            List<string> tablica = new List<string>();
            for (int i = 0; i < alfabetDuze.Length; i++)
            {
                string alfabet = "";
                for (int i2 = 0; i2 < alfabetDuze.Length; i2++)
                {
                    int index = i2 + i;
                    index = (index >= alfabetDuze.Length) ? index - alfabetDuze.Length : index;
                    alfabet += alfabetDuze[index];
                }
                tablica.Add(alfabet);
            }
            int x=0;
            StringBuilder temp = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != ' ')
                {
                    char literaHasla = haslo[x % haslo.Length];
                    char literaTekstu = text[i];
                    int indexTekstu = alfabetDuze.IndexOf(literaTekstu);
                    int indexHasla = tablica[0].IndexOf(literaHasla);
                    temp.Append(tablica[indexTekstu][indexHasla]);
                    x++;
                }
                else temp.Append(" ");
            }
            return temp.ToString();
        }
    }
}
