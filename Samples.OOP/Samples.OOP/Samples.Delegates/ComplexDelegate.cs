using System;
using System.Collections.Generic;

namespace Solution
{
    public class User
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class Company
    {
        public string? Name { get; set; }
        public string? City { get; set; }
    }

    public class TemplateParser
    {
        public delegate string convertToString<O>(O item);

        // TODO: It should be Refactor
        Dictionary<string, convertToString<User>> _userDictionary = new Dictionary<string, convertToString<User>>();
        Dictionary<string, convertToString<Company>> _companyDictionary = new Dictionary<string, convertToString<Company>>();

        public TemplateParser()
        {

        }

        public void Register<T>(string? nameClassProperty, convertToString<User> itemPath)
        {
            _userDictionary.Add(nameClassProperty, itemPath);
        }

        public void Register<T>(string? nameClassProperty, convertToString<Company> itemPath)
        {
            _companyDictionary.Add(nameClassProperty, itemPath);
        }

        public string Parse(string template, object[] objects)
        {
            string result = template;
            foreach (var shape in objects)
            {
                var type = shape.GetType();

                // TODO: It should be Refactor
                foreach (var register in _userDictionary)
                {
                    // TODO: It should be Refactor
                    if (type == typeof(User))
                    {
                        string value = register.Value.Invoke((User)shape);
                        result = result.Replace(register.Key, value);
                    }
                }

                // TODO: It should be Refactor
                foreach (var register in _companyDictionary)
                {
                    // TODO: It should be Refactor
                    if (type == typeof(Company))
                    {
                        string value = register.Value.Invoke((Company)shape);
                        result = result.Replace(register.Key, value);
                    }
                }
            }
            return result;
        }

    }

    public class ComplexDelegate
    {
        public static void PrintResult()
        {
            var templateParser = new TemplateParser();
            templateParser.Register<User>("{{UserFirstName}}", (user) => user.FirstName);
            templateParser.Register<Company>("{{CompanyName}}", (comp) => comp.Name);

            var objects = new object[] {
                new User() { FirstName = "John", LastName = "Doe" },
                new Company() { Name = "ABC Company", City = "Dallas" }
            };


            var formatText = templateParser.Parse("Dear {{UserFirstName}} from  {{CompanyName}}", objects);
            Console.WriteLine(formatText);
            Console.ReadLine();
        }
    }
}