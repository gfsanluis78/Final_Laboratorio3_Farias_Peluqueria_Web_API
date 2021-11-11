using System;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    internal class LoginRetrofit
    {
        private string token;

        public LoginRetrofit(string Token)
        {
            this.Token = Token;
        }

        public String Token { get; set; }

}
}