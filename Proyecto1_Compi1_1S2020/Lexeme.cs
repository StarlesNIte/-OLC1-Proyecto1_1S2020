using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Compi1_1S2020
{
    class Lexeme
    {
        private string token;
        private int code;
        private string type;

        public string Token
        {
            get
            {
                return token;
            }

            set
            {
                token = value;
            }
        }

        public int Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = TypeOfToken(this.Code);
            }
        }

        public Lexeme()
        {

        }

        public Lexeme(string token, int code)
        {
            this.token = token;
            this.code = code;
            this.Type = TypeOfToken(code);
        }

        private string TypeOfToken(int code)
        {
            string t = "";
            switch (code)
            {
                case 0:
                    t = "Error";
                    break;
                case 1:
                    t = "Comentario de una línea";
                    break;
                case 2:
                    t = "Comentario multilínea ";
                    break;
                case 3:
                    t = "Punto";
                    break;
                case 4:
                    t = "Barra";
                    break;
                case 5:
                    t = "Interrogación";
                    break;
                case 6:
                    t = "Asterisco";
                    break;
                case 7:
                    t = "Suma";
                    break;
                case 8:
                    t = "Acento";
                    break;
                case 9:
                    t = "Coma";
                    break;
                case 10:
                    t = "Punto y coma";
                    break;
                case 11:
                    t = "Dos puntos";
                    break;
                case 12:
                    t = "Palabra reservada \"CONJ\"";
                    break;
                case 13:
                    t = "String";
                    break;
                case 14:
                    t = "Char";
                    break;
                case 15:
                    t = " Int";
                    break;
                case 16:
                    t = "Identificador";
                    break;
            }
            return t;
        }
    }
}
