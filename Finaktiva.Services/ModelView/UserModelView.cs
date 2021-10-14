using System;
using System.Collections.Generic;
using System.Text;

namespace Finaktiva.Services.ModelView
{
    public class UserModelView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public RoleModelView Role { get; set; }
    }
}
