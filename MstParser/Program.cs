using System.Text;
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
var regexIds = new Regex(@"\D{2}\d*-\d*");
var regexSpecialChars = new Regex(@"[^A-Za-z0-9-]");
var results = regexIds.Matches(parsedString);

foreach (Match match in results)
{
    var id = match.Value;
    if (id is { Length: > 3 })
    {
        var specialCharsInId = regexSpecialChars.Match(id).Value;

        var idParsed = specialCharsInId.Length > 0 ? id.Replace(specialCharsInId, "") : id;
        var indexInString = parsedString?.IndexOf(idParsed, StringComparison.Ordinal);

        if (indexInString > -1)
        {
            var product = parsedString?.Substring(indexInString.Value, 60);
            if (product is not null)
            {
                var productByes = Encoding.ASCII.GetBytes(product);
                productByes = productByes.Where(x => x != 1 && x != 95).ToArray();

                product = Encoding.ASCII.GetString(productByes);
                var ser = product.Split("  ");
                foreach (var s in product.Split("  "))
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
