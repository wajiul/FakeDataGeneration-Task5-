using System.Text;

namespace FakeDataGeneration.Services
{
    public class ErrorGeneratorService
    {
        private Random rng = new Random(1234);
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private string DeleteCharacter(string data)
        {
            if (data.Length == 0)
                return data;
            int pos = rng.Next(0, data.Length);
            return data.Remove(pos, 1);
        }

        private string AddRandomCharacter(string data)
        {
            int pos = rng.Next(0, data.Length + 1);
            char randomChar = GetRandomCharacter();
            return data.Insert(pos, randomChar.ToString());
        }

        private string SwapCharacters(string data)
        {
            if (data.Length < 2)
                return data;
            int pos = rng.Next(0, data.Length - 1);
            var sb = new StringBuilder(data);
            char temp = sb[pos];
            sb[pos] = sb[pos + 1];
            sb[pos + 1] = temp;
            return sb.ToString();
        }

        private char GetRandomCharacter()
        {
            return chars[rng.Next(chars.Length)];
        }

        private string AddErrorToColumn(string data)
        {
            Func<string, string>[] errorMethods = { DeleteCharacter, AddRandomCharacter, SwapCharacters };
            var errorMethod = errorMethods[rng.Next(errorMethods.Length)];
            return errorMethod(data);
        }
        public List<string> AddErrors(List<string> data, double numErrors)
        {
            numErrors = (rng.NextDouble() < numErrors % 1) ? numErrors + 1 : numErrors;

            for (int i = 0; i < Math.Floor(numErrors); i++)
            {
                int columnIndex = rng.Next(data.Count);
                string column = data[columnIndex];
                data[columnIndex] = AddErrorToColumn(column);
            }
            return data;
        }

    }
}
