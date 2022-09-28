using System.Text;

[XLua.Hotfix]
public class MD5Util
{
    /// <summary>
    /// 获取字符串MD5
    /// </summary>
    /// <param name="input"></param>
    /// <param name="isLowerCase"></param>
    /// <returns></returns>
    public static string FromString(string input, bool isLowerCase = true)
    {
        byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
        return FromBytes(inputBytes, isLowerCase);
    }

    /// <summary>
    /// 获取字节流MD5
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="isLowerCase"></param>
    /// <returns></returns>
    public static string FromBytes(byte[] bytes, bool isLowerCase = true)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(bytes);
            StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString(isLowerCase ? "x2" : "X2"));
            }

            return sb.ToString();
        }
    }
}