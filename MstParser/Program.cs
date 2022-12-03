using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

var bytes = File.ReadAllBytes("E:\\DATA\\S-CORTRN.MST");

// Set null bytes to dash
for (var index = 0; index < bytes.Length; index++)
{
    bytes[index] = bytes[index] switch
    {
        > 127 or 0 => 95,
        _ => bytes[index]
    };
}

var parsedString = System.Text.Encoding.UTF8.GetString(bytes);
// Console.WriteLine(parsedString);
var regexIds = new Regex(@"\D{2}\d*-\d*");
var regexSpecialChars = new Regex(@"[^A-Za-z0-9-]");
var results = regexIds.Matches(parsedString);

var ids = new List<string>();
foreach (Match match in results)
{
    var id = match.Value;
    if (id is { Length: > 3 })
    {
        var specialCharsInId = regexSpecialChars.Match(id).Value;

        ids.Add(specialCharsInId.Length > 0 ? id.Replace(specialCharsInId, "") : id);
        // Console.WriteLine(specialCharsInId.Length > 0 ? id.Replace(specialCharsInId, "") : id);
    }
}
Console.Write(parsedString?.Substring(803, 60));

// foreach (var id in ids)
// {
//     var indexInString = parsedString?.IndexOf(id, StringComparison.Ordinal);
//     
//     // Console.WriteLine(indexInString);
//     Console.Write(parsedString?.Substring(803));
// }