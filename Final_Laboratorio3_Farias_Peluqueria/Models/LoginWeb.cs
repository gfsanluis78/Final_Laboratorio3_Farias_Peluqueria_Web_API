using System;

namespace Final_Laboratorio3_Farias_Peluqueria.Models
{
    internal class LoginWeb
    {
        private string accessToken;
        private int roles;

        public LoginWeb(int Roles, string AccessToken)
        {
            this.AccessToken = AccessToken;
            this.Roles = Roles;
        }

        public String AccessToken { get; set; }

        public int Roles { get; set; }

}
}