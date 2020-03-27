using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace Proyecto1_Compi1_1S2020
{
    public partial class Form1 : Form
    {

        List<Lexeme> tokens = new List<Lexeme>();
        List<Lexeme> faults = new List<Lexeme>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void analyzeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analyze();
            foreach (Lexeme l in tokens)
            {
                Console.WriteLine(l.Token + " código: " + l.Code + " Tipo--->" + l.Type);
            }
            CreateXML();
        }

        private void Analyze()
        {
            String concat = "";
            int currentState = 0;
            char[] txt = richTextBox1.Text.ToCharArray();

            for (int i = 0; i < txt.Length; i++)
            {
                switch (currentState)
                {
                    case 0: //estado inicial

                        if (char.IsLetter(txt[i])) //id and chars(from macros)
                        {
                            concat += txt[i];
                            currentState = 7;
                        }
                        else if (char.IsNumber(txt[i])) //digits
                        {
                            Lexeme aux = new Lexeme(txt[i].ToString(), 15);
                            tokens.Add(aux);
                        }
                        else
                        {
                            switch (txt[i])
                            {
                                case '/': //single line comments
                                    currentState = 1;
                                    break;
                                case '<': //multi line comments
                                    currentState = 4;
                                    break;
                                case '"': //strings
                                    currentState = 9;
                                    break;
                                case ' ':
                                case '{':
                                case '}':
                                case '-':
                                case '>':
                                    break;
                                default: //simbolos
                                    Lexeme aux = TypeOfSymbol(txt[i]);
                                    if (aux.Code != 0)
                                    {
                                        tokens.Add(aux);
                                    }
                                    else
                                    {
                                        faults.Add(aux);
                                    }
                                    break;
                            }
                        }

                        break;
                    case 1: //single line comment transsicion
                        if (txt[i].Equals('/'))
                        {
                            currentState = 2;
                        }
                        else //fault
                        {
                            concat += txt[i];
                            currentState = 3;
                        }
                        break;
                    case 2: //aceptación y concatenación comentarios
                        if (txt[i].Equals('\n'))
                        {
                            Lexeme aux = new Lexeme(concat, 1);
                            tokens.Add(aux);
                            concat = "";
                            currentState = 0;
                        }
                        else
                        {
                            concat += txt[i];
                        }
                        break;
                    case 3: //faults
                        Lexeme fault = new Lexeme(concat, 0);
                        faults.Add(fault);
                        concat = "";
                        currentState = 0;
                        break;
                    case 4:
                        if (txt[i].Equals('!'))
                        {
                            concat += txt[i];
                            currentState = 5;
                        }
                        else
                        {
                            concat += txt[i];
                            currentState = 3;
                        }
                        break;
                    case 5:
                        if (txt[i].Equals('!'))
                        {
                            currentState = 6;
                        }
                        else
                        {
                            concat += txt[i];
                        }
                        break;
                    case 6:
                        if (txt[i].Equals('>')) //accepting comment multi line
                        {
                            Lexeme aux = new Lexeme(concat, 2);
                            tokens.Add(aux);
                            concat = "";
                            currentState = 0;
                        }
                        else //back to 
                        {
                            concat += '1' + txt[i];
                            currentState = 5;
                        }
                        break;
                    case 7:
                        if (txt[i].Equals('_') || //going to id
                            char.IsNumber(txt[i]) ||
                            char.IsLetter(txt[i]))
                        {
                            concat += txt[i];
                            currentState = 8;
                        }
                        else //accepting chars (from macros)
                        {
                            Lexeme aux = new Lexeme(concat, 14);
                            tokens.Add(aux);
                            concat = "";
                            currentState = 0;
                            i--;
                        }
                        break;
                    case 8:
                        if (txt[i].Equals('_') || //going to id and reserved 
                           char.IsNumber(txt[i]) ||
                           char.IsLetter(txt[i]))
                        {
                            concat += txt[i];
                        }
                        else
                        {
                            if (concat.Equals("CONJ"))
                            {
                                Lexeme aux = new Lexeme(concat, 12);
                                tokens.Add(aux);
                                concat = "";
                                currentState = 0;
                            }
                            else
                            {
                                Lexeme aux = new Lexeme(concat, 16);
                                tokens.Add(aux);
                                concat = "";
                                currentState = 0;
                            }
                            i--;
                        }
                        break;
                    case 9:
                        if (txt[i].Equals('"')) //acepting strings
                        {
                            Lexeme aux = new Lexeme(concat, 13);
                            tokens.Add(aux);
                            concat = "";
                            currentState = 0;
                        }
                        else
                        {
                            concat += txt[i];
                        }
                        break;

                }
            }

        }

        private Lexeme TypeOfSymbol(char symbol)
        {
            /*
            Lexeme token = new Lexeme();
            token.Token = symbol.ToString(); */
            int tokenCode;
            switch (symbol)
            {
                case '.':
                    tokenCode = 3;
                    break;
                case '|':
                    tokenCode = 4;
                    break;
                case '?':
                    tokenCode = 5;
                    break;
                case '*':
                    tokenCode = 6;
                    break;
                case '+':
                    tokenCode = 7;
                    break;
                case '~':
                    tokenCode = 8;
                    break;
                case ',':
                    tokenCode = 9;
                    break;
                case ';':
                    tokenCode = 10;
                    break;
                case ':':
                    tokenCode = 11;
                    break;
                default:
                    tokenCode = 0; //fault
                    break;
            }
            return new Lexeme(symbol.ToString(), tokenCode);
        }



        private void CreateXML()
        {
            XmlWriter xmlWriter = XmlWriter.Create("Reporte.xml");
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("ListaTokens");
            foreach (Lexeme t in tokens)
            {
                xmlWriter.WriteStartElement("Token");
                xmlWriter.WriteStartElement("Nombre");
                xmlWriter.WriteString(t.Type);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Valor");
                xmlWriter.WriteString(t.Token);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Codigo");
                xmlWriter.WriteString(t.Code.ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteStartElement("ListaErrores");
            foreach(Lexeme f in faults)
            {
                xmlWriter.WriteStartElement("Error");
                xmlWriter.WriteStartElement("Valor");
                xmlWriter.WriteString(f.Token);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("Codigo");
                xmlWriter.WriteString(f.Code.ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Insanciar objeto OpenFileDIalog para abrir archivos *.er
            OpenFileDialog dialogo = new OpenFileDialog();
            if (dialogo.ShowDialog() == DialogResult.OK)
            {
                string url = dialogo.FileName;
                richTextBox1.Text = System.IO.File.ReadAllText(url);
            }
        }
    }
}
