using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //Analyze
        }

        private void Analyze()
        {
            String concat = "";
            int currentState = 0;
            char[] txt = richTextBox1.Text.ToCharArray();

            for (int i = 0; i<txt.Length;i++)
            {
                switch (currentState)
                {
                    case 0: //estado inicial

                        if (txt[i].Equals('/'))
                        {
                            currentState = 1;
                        }

                        break;
                    case 1: //transición de comentarios
                        if (txt[i].Equals('/'))
                        {
                            currentState = 2;
                        }else
                        {
                            concat += txt[i];
                            currentState = 3;
                        }
                        break;
                    case 2: //aceptación y concatenación comentarios
                        if (txt[i].Equals('\n'))
                        {
                            Lexeme aux = new Lexeme();
                            tokens.Add(aux);
                            concat = "";
                            currentState = 0;
                        }else
                        {
                            concat += txt[i];
                        }
                        break;
                    case 3: //errores
                        Lexeme fault = new Lexeme();
                        break;
                    case 4:
                        break;
                    case 5:
                        break;

                }
            }

        }
    }
}
