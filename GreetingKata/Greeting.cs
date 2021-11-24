using System.Text;
using System.Text.RegularExpressions;

namespace GreetingKata;

public class Greeting
{
    public Greeting()
    {
    }

    private bool IsAllUpperCase(string name)
    {
        foreach (var charInName in name.ToArray<char>())
        {
            if (!Char.IsUpper(charInName))
            {
                return false;
            }
        }
        return true;
    }

    public string greet(string[] names)
    {
        if (names.Length == 0)
        {
            return returnNullResult();
        }

        names = splitNamesArrayIfCommaInString(names);

        if (isTwoNames(names))
        {
            return $"Hello, {names[0]} and {names[1]}.";
        }

        return moreThenTwoNames(names);


    }

    private string[] splitNamesArrayIfCommaInString(string[] names)
    {
        var nameList = new List<string>();
        foreach (var name in names)
        {
            if (name.Contains(",") && !name.Contains("\""))
            {
                var splitNames = name.Split(", ");
                nameList.AddRange(splitNames);
            }
            else
            {
                nameList.Add(Regex.Replace(name, @"(\[|""|\])", "").Trim());
            }
        }
        return nameList.ToArray<string>();

    }

    private string moreThenTwoNames(string[] names)
    {
        var upperNames = new List<string>();
        var lowerNames = new List<string>();

        seperateUpperAndLowerNames(names, upperNames, lowerNames);

        StringBuilder sb = new StringBuilder();

        sb.Append(constructGreetString(lowerNames, false));

        sb.Append(constructGreetString(upperNames, true));


        return sb.ToString();
    }

    private string constructGreetString(List<string> nameList, bool isUpperCase)
    {
        StringBuilder sb = new StringBuilder();
        if (nameList.Count > 0)
        {

            if (nameList.Count == 1)
            {
                sb.Append($" and hello { nameList[0]}!");
            }
            else
            {
                sb.Append("Hello,");
                foreach (var item in nameList.Select((name, i) => new { i, name }))
                {
                    var name = item.name;
                    sb.Append(nameList.Count == (item.i + 1) ? $" and {name}." : $" {name},");
                }
            }
        }
        return isUpperCase ? sb.ToString().ToUpper() : sb.ToString();
    }

    private void seperateUpperAndLowerNames(string[] names, List<string> upperNames, List<string> lowerNames)
    {
        foreach (var name in names)
        {
            if (IsAllUpperCase(name))
            {
                upperNames.Add(name);
            }
            else
            {
                lowerNames.Add(name);
            }
        }
    }

    private bool isTwoNames(string[] names)
    {
        return names.Length < 3;
    }

    public string greet(string name)
    {
        if (name == "")
        {
            return returnNullResult();
        }

        if (IsAllUpperCase(name))
        {
            return ($"HELLO {name}!");
        }

        return ($"Hello, {name}.");
    }

    private string returnNullResult()
    {
        return ($"Hello, my friend.");
    }
}
