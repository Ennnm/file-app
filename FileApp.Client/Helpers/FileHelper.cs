using System;
using System.Text.RegularExpressions;

namespace FileApp.Client.Helpers
{
    public static class StringHelper
    {
        public enum Operation
        {
            changeFileExt,
            addPrefix,
            addSuffix,
            addNumbering,
            removeNCharFromStart,
            removeNCharFromEnd,
            removeDoubleSpace,
            removeCharsFromList,
            replaceText,
            transformWithRegex,
            covertToLowercase,
            covertToUppercase,
            appendDateModified,
            appendDateAccessed,
        }

        public static string ChangeFileExt(string input, string replacement)
        {
            var pattern = @"(\.[^.]+)$";
            return Regex.Replace(input, pattern, $".{replacement}");
        }

        public static bool RequiresFrom(Operation operation)
        {
            switch (operation)
            {
                case Operation.changeFileExt:
                case Operation.addPrefix:
                    return false;
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
                    return true;
                default:
                    return false;
            }
        }
        public static string GetReadableOperation(Operation operation)
        {
            switch (operation)
            {
                case Operation.changeFileExt:
                    return "Change file extension";
                case Operation.addPrefix:
                    return "Add prefix";
                default:
                    return "Whatever";
            }
        }
        public static Func<string, string, string> GetOperationFn(Operation operation)
        {
            switch (operation)
            {
                case Operation.changeFileExt:
                    return ChangeFileExt;
                case Operation.addPrefix:
                    return ChangeFileExt;
                default:
                    return (_, _) => "";
            }
        }
    }
}