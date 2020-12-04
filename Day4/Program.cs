﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day4
{
    public class MinMaxRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public bool Valid(int number)
        {
            return number >= Min && number <= Max;
        }
    }
    public class DigitRule: MinMaxRule
    {
        public int NumDigits { get; set; }

        public bool Valid(string number)
        {
            if (int.TryParse(number, out var parsed))
            {
                return number.Length == NumDigits && base.Valid(parsed);
            }

            return false;
        }
    }

    public class HeightRule
    {
        public bool Valid(string height)
        {
            var heights = new Dictionary<string, MinMaxRule>
            {
                {"cm", new MinMaxRule() {Min = 150, Max = 193}}, {"in", new MinMaxRule() {Min = 59, Max = 76}}
            };

            if (height.Contains("cm"))
            {
                var totalHeight = height.Replace("cm", "");
                var rule = heights["cm"];
                return rule.Valid(int.Parse(totalHeight));
            }
            if (height.Contains("in"))
            {
                var totalHeight = height.Replace("in", "");
                var rule = heights["in"];
                return rule.Valid(int.Parse(totalHeight));
            }

            return false;
        }
    }

    public class HairRule {
        public bool Valid(string hair)
        {
            string pattern = @"#[0-9a-f]{6}";
            return Regex.Matches(hair, pattern).Count == 1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //SimplePassportChecker();
            ComplexPassportChecker();
        }

        private static void ComplexPassportChecker()
        {
            var file = File.ReadAllLines("input.txt");
            var mandatoryFields = new[] { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" };
            var fieldRules = new Dictionary<string, object>
            {
                {"byr", new DigitRule {NumDigits = 4, Min = 1920, Max = 2002}},
                {"iyr", new DigitRule {NumDigits = 4, Min = 2010, Max = 2020}},
                {"eyr", new DigitRule {NumDigits = 4, Min = 2020, Max = 2030}},
                {"hgt", new HeightRule() },
                {"hcl", new HairRule() }
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

        private static bool ExtraValidation(string passport, Dictionary<string, object> rules)
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
                    if (rule is DigitRule digitRule)
                    {
                        if (!digitRule.Valid(keys[1]))
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (rule is HeightRule heightRule)
                    {
                        if (!heightRule.Valid(keys[1]))
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (rule is HairRule hairRule)
                    {
                        if (!hairRule.Valid(keys[1]))
                        {
                            valid = false;
                            break;
                        }
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
