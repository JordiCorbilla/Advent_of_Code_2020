using System;
using System.Collections.Generic;
using System.IO;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            SimplePassportChecker();
            ComplexPassportChecker();
        }

        private static void ComplexPassportChecker()
        {
            var file = File.ReadAllLines("input.txt");
            var mandatoryFields = new[] { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" };
            var fieldRules = new Dictionary<string, IRule>
            {
                {"byr", new DigitRule {NumDigits = 4, Min = 1920, Max = 2002}},
                {"iyr", new DigitRule {NumDigits = 4, Min = 2010, Max = 2020}},
                {"eyr", new DigitRule {NumDigits = 4, Min = 2020, Max = 2030}},
                {"hgt", new HeightRule() },
                {"hcl", new HairRule() },
                {"ecl", new EyeRule() },
                {"pid", new PassportRule() }
            };

            int countValid = 0;
            string passport = "";
            foreach (var line in file)
            {
                if (line != "")
                {
                    passport += line + Environment.NewLine;
                }
                else
                {
                    bool valid = ProcessPassport(passport, mandatoryFields);
                    valid = valid && ExtraValidation(passport, fieldRules);
                    if (valid)
                    {
                        countValid++;
                    }

                    passport = "";
                }
            }

            if (passport != "")
            {
                bool valid = ProcessPassport(passport, mandatoryFields);
                valid = valid && ExtraValidation(passport, fieldRules);
                if (valid)
                {
                    countValid++;
                }
            }
            Console.WriteLine(countValid);
        }

        private static bool ExtraValidation(string passport, Dictionary<string, IRule> rules)
        {
            var cleanup = passport.Replace(Environment.NewLine, " ");
            var fields = cleanup.Split(" ");
            bool valid = true;
            foreach (var field in fields)
            {
                var keys = field.Split(":");
                if (rules.ContainsKey(keys[0]))
                {
                    var rule = rules[keys[0]];
                    if (!rule.Valid(keys[1]))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            return valid;
        }

        private static void SimplePassportChecker()
        {
            var file = File.ReadAllLines("input.txt");
            var mandatoryFields = new[] { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" };

            int countValid = 0;
            string passport = "";
            foreach (var line in file)
            {
                if (line != "")
                {
                    passport += line;
                }
                else
                {
                    bool valid = ProcessPassport(passport, mandatoryFields);
                    if (valid)
                    {
                        countValid++;
                    }

                    passport = "";
                }
            }

            if (passport != "")
            {
                bool valid = ProcessPassport(passport, mandatoryFields);
                if (valid)
                {
                    countValid++;
                }
            }

            Console.WriteLine(countValid);
        }

        private static bool ProcessPassport(string passport, string[] mandatoryFields)
        {
            bool valid = true;
            foreach (var field in mandatoryFields)
            {
                if (!passport.Contains(field))
                {
                    valid = false;
                    break;
                }
            }
            return valid;
        }
    }

    
}
