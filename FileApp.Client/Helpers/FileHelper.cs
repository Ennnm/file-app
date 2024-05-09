using System.Text.RegularExpressions;

namespace FileApp.Client.Helpers
{
    public class StringHelper
    {
        public enum Operation
        {
            changeFileExt,
            addPrefix,
            addSuffix,
            addNumbering,
            removeNCharFromStart,
            removeNCharFromEnd,
            // removeDoubleSpace,
            // removeCharsFromList,
            // replaceText,
            // transformWithRegex,
            // covertToLowercase,
            // covertToUppercase,
            // appendDateModified,
            // appendDateAccessed,
        }

        public static string ChangeFileExt(string input, string replacement, string from, int i)
        {
            var pattern = @"(\.[^.]+)$";
            return Regex.Replace(input, pattern, $".{replacement}");
        }

        public static string AddPrefix(string input, string prefix, string from, int i)
        {
            return $"{prefix}_{input}";
        }

        public static string AddSuffix(string input, string suffix, string from, int i)
        {
            string pattern = @"^(.*?)(\.[^.]+)?$";
            string replacement = $"$1_{suffix}$2";

            return Regex.Replace(input, pattern, replacement);
        }

        public static string AddNumbering(string input, string to, string from, int i)
        {
            var success = int.TryParse(from, out int result);
            return AddSuffix(input, (result + i).ToString(), "", i);
        }

        public static string RemoveNCharFromStart(string input, string to, string from, int i)
        {
            var success = int.TryParse(from, out int result);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(input);
            string fileExt = Path.GetExtension(input);
            int newLength = Math.Min(result, fileNameWithoutExt.Length);
            return $"{input[newLength..]}{fileExt}";
        }

        public static string RemoveNCharFromEnd(string input, string to, string from, int i)
        {
            var success = int.TryParse(from, out int result);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(input);
            string fileExt = Path.GetExtension(input);

            int newLength = Math.Max(fileNameWithoutExt.Length - result, 0);

            string newFileNameWithoutExtension = fileNameWithoutExt[..newLength];

            return $"{newFileNameWithoutExtension}{fileExt}";
        }

        public static bool RequiresFrom(Operation operation)
        {
            switch (operation)
            {
                case Operation.addNumbering:
                case Operation.removeNCharFromStart:
                case Operation.removeNCharFromEnd:
                    return true;
                default:
                    return false;
            }
        }
        public static bool RequiresTo(Operation operation)
        {
            switch (operation)
            {
                case Operation.changeFileExt:
                case Operation.addPrefix:
                case Operation.addSuffix:
                    return true;
                default:
                    return false;
            }
        }
        public static string GetReadableOperation(Operation operation)
        {
            string spacedString = Regex.Replace(operation.ToString(), "(\\B[A-Z])", " $1");
            return char.ToUpper(spacedString[0]) + spacedString[1..];
        }

        public static Func<string, string, string, int, string> GetOperationFn(Operation operation)
        {
            return operation switch
            {
                Operation.changeFileExt => ChangeFileExt,
                Operation.addPrefix => AddPrefix,
                Operation.addSuffix => AddSuffix,
                Operation.addNumbering => AddNumbering,
                Operation.removeNCharFromStart => RemoveNCharFromStart,
                Operation.removeNCharFromEnd => RemoveNCharFromEnd,
                _ => (_, _, _, _) => "",
            };
        }
    }
}