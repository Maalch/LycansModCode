using System;
using System.Security.Cryptography;
using System.Text;

namespace LycansNewRoles;

internal static class StringExtensions
{
	public static Guid ToGuid(this string value)
	{
		return new Guid(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
	}
}
