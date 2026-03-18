using System.Text;
using System.Globalization;

Console.OutputEncoding = Encoding.UTF8;

TriviaGame.PlayGame();

public static class TriviaGame
{
    public static string RemoveDiacritics(string text) 
    // Source - https://stackoverflow.com/a/249126
    // Posted by Blair Conrad, modified by community. See post 'Timeline' for change history
    // Retrieved 2026-03-17, License - CC BY-SA 4.0
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

        for (int i = 0; i < normalizedString.Length; i++)
        {
            char c = normalizedString[i];
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder
            .ToString()
            .Normalize(NormalizationForm.FormC);
    }
    public static string PlayGame()
    {
        Dictionary<string, List<string>> countryCapitals = new Dictionary<string, List<string>>()
        {
            ["Algeria"] = new List<string>{"Algiers"},["Angola"] = new List<string>{"Luanda"},
            ["Benin"] = new List<string>{"Porto-Novo"},["Botswana"] = new List<string>{"Gaborone"},
            ["Burkina Faso"] = new List<string>{"Ouagadougou"},["Burundi"] = new List<string>{"Gitega"},
            ["Cabo Verde"] = new List<string>{"Praia"},["Cameroon"] = new List<string>{"Yaoundé"},
            ["Central African Republic"] = new List<string>{"Bangui"},["Chad"] = new List<string>{"N'Djamena"},
            ["Comoros"] = new List<string>{"Moroni"},["Democratic Republic of the Congo"] = new List<string>{"Kinshasa"},
            ["Republic of the Congo"] = new List<string>{"Brazzaville"},["Djibouti"] = new List<string>{"Djibouti City"},
            ["Egypt"] = new List<string>{"Cairo"},["Equatorial Guinea"] = new List<string>{"Malabo"},
            ["Eritrea"] = new List<string>{"Asmara"},["Eswatini"] = new List<string>{"Mbabane", "Lobamba"},
            ["Ethiopia"] = new List<string>{"Adis Ababa"},["Gabon"] = new List<string>{"Libreville"},
            ["Gambia"] = new List<string>{"Banjul"},["Ghana"] = new List<string>{"Accra"},
            ["Guinea"] = new List<string>{"Conakry"},["Guinea-Bissau"] = new List<string>{"Bissau"},
            ["Cote d'Ivoire"] = new List<string>{"Yamoussoukro"},["Kenya"] = new List<string>{"Nairobi"},
            ["Lesotho"] = new List<string>{"Maseru"},["Liberia"] = new List<string>{"Monrovia"},
            ["Libya"] = new List<string>{"Tripoli"},["Madagascar"] = new List<string>{"Antananarivo"},
            ["Malawi"] = new List<string>{"Lilongwe"},["Mali"] = new List<string>{"Bamako"},
            ["Mauritania"] = new List<string>{"Nouakchott"},["Mauritius"] = new List<string>{"Port Louis"},
            ["Morocco"] = new List<string>{"Rabat"},["Mozambique"] = new List<string>{"Maputo"},
            ["Namibia"] = new List<string>{"Windhoek"},["Niger"] = new List<string>{"Niamey"},
            ["Nigeria"] = new List<string>{"Abuja"},["Rwanda"] = new List<string>{"Kigali"},
            ["São Tomé and Principe"] = new List<string>{"São Tomé"},["Senegal"] = new List<string>{"Dakar"},
            ["Seychelles"] = new List<string>{"Victoria"},["Sierra Leone"] = new List<string>{"Freetown"},
            ["Somalia"] = new List<string>{"Mogadishu"},["South Africa"] = new List<string>{"Pretoria", "Cape Town", "Bloemfontein"},
            ["South Sudan"] = new List<string>{"Juba"},["Sudan"] = new List<string>{"Khartoum"},
            ["Tanzania"] = new List<string>{"Dodoma"},["Togo"] = new List<string>{"Lomé"}, 
            ["Tunisia"] = new List<string>{"Tunis"},["Uganda"] = new List<string>{"Kampala"},
            ["Zambia"] = new List<string>{"Lusaka"},["Zimbabwe"] = new List<string>{"Harare"},
        };
        
        int score = 0;
        int question_num = 1;
        Console.WriteLine("Welcome to the African capitals quiz!");

        foreach (KeyValuePair<string, List<string>> countryCapitalPair in countryCapitals)
        {
            Console.WriteLine($"Question {question_num++}: What's the capital of {countryCapitalPair.Key}?");

            string input = Console.ReadLine();
            string cleaned_input = RemoveDiacritics(input).Replace('\u002D', ' ');
            foreach (string capital in countryCapitalPair.Value)
            {
                string cleaned_answer = RemoveDiacritics(capital).Replace('\u002D', ' ');

                if (string.Equals(cleaned_input, cleaned_answer, StringComparison.CurrentCultureIgnoreCase))
                    {
                        Console.WriteLine("That's correct!");
                        score++;
                        continue;
                    }
                else
                {
                    if (countryCapitalPair.Value.Count == 1)
                    {
                        Console.WriteLine($"That's incorrect. The capital of {countryCapitalPair.Key} is {capital}.");
                    }
                    else
                    {
                        string allCapitals = string.Join(", ", countryCapitalPair.Value);
                        Console.WriteLine($"That's incorrect. The capitals of {countryCapitalPair.Key} are: {allCapitals}.");
                        break;
                    }
                    ;
                }
            }
        }

        Console.WriteLine($"That's the end of the quiz. Your final score is {score} out of {countryCapitals.Count}");
        return null;
    }
}