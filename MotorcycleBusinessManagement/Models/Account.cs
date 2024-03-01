using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleBusinessManagement.Models;

public partial class Account
{
    public int Idacc { get; set; }
    //[Range(3, 50, ErrorMessage = "Tài khoản phải có độ dài bằng hoặc hơn 3 ký tự")]
    public string Username { get; set; } = null!;
    //[Range(8, 50, ErrorMessage = "Mật khẩu phải có độ dài bằng hoặc hơn 8 ký tự")]
    public string Password { get; set; } = null!;

    public string? Phone { get; set; }
    //[Range(8, 500, ErrorMessage = "Email phải có độ dài bằng hoặc hơn 8 ký tự")]
    public string? Email { get; set; }

    public string? Role { get; set; }

    public bool? Active { get; set; }
}
