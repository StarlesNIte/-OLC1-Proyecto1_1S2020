using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_Compi1_1S2020
{
    class Lexeme
    {
        private String token;
        private int code;

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

        public Lexeme(string token, int code)
        {
            this.Token = token;
            this.Code = code;
        }
        public Lexeme()
        {

        }
    }
}
