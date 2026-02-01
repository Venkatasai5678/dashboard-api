using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEntity.MODEL
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; }      // actual refresh token
        public string Username { get; set; }   // user it belongs to

        public DateTime ExpiresAt { get; set; } // validity (ex: 7 days)
        public bool IsRevoked { get; set; }     // logout / security revoke


    }
}
