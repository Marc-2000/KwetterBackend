using System;
using System.Collections.Generic;

namespace UserService.BLL.Models
{
    public class Role
    {
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public List<UserRole>? UserRoles { get; set; }
    }
}
