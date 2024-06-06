using System;
using System.Text.RegularExpressions;
using Notebook.Models;
using Notebook.Enums;

class App
{
    private readonly BirthdayMessageCreator birthdayMessageCreator = new();
    bool listen = true;
    StorageService service;

    public App(StorageService service)
    {
        this.service = service;
    }

    public void Start()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        StartListening();
    }

    public void Stop()
    {
        listen = false;
    }

    private void StartListening()
    {
        Console.CancelKeyPress += (_, _) => Stop();

        while(listen)
        {
            Console.WriteLine("Перелік команд:\n 1)Додати\n 2)День Народження\n 3)Знайти\n 4)Показати список");
            Console.WriteLine("Введіть команду: ");
            var command = Console.ReadLine();
            try
            {
                switch(command)
                {
                    case "1":
                    {
                        QuestionnaireData questionnaireData;
                        {
                            string name, last_name;
                            string? second_name = null;
                            DateOnly dob;

                            name = ConsoleHelper.ReadString("Введіть ім'я: ");
                            last_name = ConsoleHelper.ReadString("Введіть прізвище: ");

                            if(ConsoleHelper.ReadString("Додати по батькові (y/n): ", "Введіть 'y' або 'n'", (s) => "yn".Contains(s.ToLower())).ToLower() == "y")
                            {
                                second_name = ConsoleHelper.ReadString("Введіть ім'я по батькові: ");
                            }

                            dob = ConsoleHelper.ReadDateOnly("Введіть дату народження", "Введіть дату (формат: dd.MM.yyyy)", "dd.MM.yyyy");

                            questionnaireData = new QuestionnaireData(name, last_name, second_name, dob);
                        }

                        ConsoleHelper.PrintSuccess("Введені дані: " + questionnaireData);
                        
                        List<Adress> adresses = new ();
                        {
                            while(ConsoleHelper.ReadString("Додати адресу (y/n): ", "Введіть 'y' або 'n'", (s) => "yn".Contains(s.ToLower())).ToLower() == "y")
                            {
                                var code = ConsoleHelper.ReadString("Введіть поштовий код: ", "Не відповідає формату поштовго коду (XXXXX)", (s) => Regex.IsMatch(s, "^[0-9]{5}$"));
                                var adress = ConsoleHelper.ReadString("Введіть адресу: ");
                                var home = ConsoleHelper.ReadString("Введіть будівлю/квартиру: ");

                                adresses.Add(new Adress(code, adress, home));
                            }
                        }

                        ConsoleHelper.PrintSuccess("Введені дані: " + string.Join(", ", adresses));
                            
                        PhoneNumberCollection collection;
                        {
                            List<CollectionEntry<PhoneNumberType, string>> phones = new ();

                            ConsoleHelper.PrintInfo("Тільки один номер телефону доступний на кожен тип номеру");

                            while(ConsoleHelper.ReadString("Додати номер телефону (y/n): ", "Введіть 'y' або 'n'", (s) => "yn".Contains(s.ToLower())).ToLower() == "y")
                            {
                                var phone_number = ConsoleHelper.ReadString("Введіть номер теллефону: ");
                                var type = ConsoleHelper.ReadEnum<PhoneNumberType>("Введіть тип номеру телефону: ");

                                phones.Add(new (type, phone_number));
                            }

                            collection = new (phones);
                        }

                        ConsoleHelper.PrintSuccess("Введені дані: " + collection);

                        OccupationPlace occupationPlace;
                        {
                            var code = ConsoleHelper.ReadString("Введіть поштовий код: ", "Не відповідає формату поштовго коду (XXXXX)", (s) => Regex.IsMatch(s, "^[0-9]{5}$"));
                            var adress = ConsoleHelper.ReadString("Введіть адресу: ");
                            var home = ConsoleHelper.ReadString("Введіть будівлю/квартиру: ");
                            var occupation = ConsoleHelper.ReadString("Введіть посаду: ");

                            occupationPlace = new (code, adress, home, occupation);
                        }

                        ConsoleHelper.PrintSuccess("Введені дані: " + occupationPlace);


                        Dictionary<RelationshipType, List<PersonalData>> data = new();
                        {
                            while(ConsoleHelper.ReadString("Додати відносини (y/n): ", "Введіть 'y' або 'n'", (s) => "yn".Contains(s.ToLower())).ToLower() == "y")
                            {
                                var type = ConsoleHelper.ReadEnum<RelationshipType>("Введіть тип номеру відносин: ");

                                string name, last_name;
                                string? second_name = null;
                                DateOnly dob;

                                name = ConsoleHelper.ReadString("Введіть ім'я: ");
                                last_name = ConsoleHelper.ReadString("Введіть прізвище: ");

                                if(ConsoleHelper.ReadString("Додати по батькові (y/n): ", "Введіть 'y' або 'n'", (s) => "yn".Contains(s.ToLower())).ToLower() == "y")
                                {
                                    second_name = ConsoleHelper.ReadString("Введіть по батькові: ");
                                }

                                dob = ConsoleHelper.ReadDateOnly("Введіть дату народження", "Введіть дату (формат: dd.MM.yyyy)", "dd.MM.yyyy");

                                OccupationPlace occupation;
                                {
                                    var code = ConsoleHelper.ReadString("Введіть поштовий код: ", "Не відповідає формату поштовго коду (XXXXX)", (s) => Regex.IsMatch(s, "^[0-9]{5}$"));
                                    var adress = ConsoleHelper.ReadString("Введіть адресу: ");
                                    var home = ConsoleHelper.ReadString("Введіть будівлю/квартиру: ");
                                    var occup = ConsoleHelper.ReadString("Введіть посаду: ");

                                    occupation = new (code, adress, home, occup);
                                }

                                var _qualities = ConsoleHelper.ReadString("Перелік якостей: ");

                                var unregisteredPersonalData = new UnregisteredPersonalData(name, last_name, second_name, dob, occupation, _qualities);

                                if(data.ContainsKey(type))
                                    data[type].Add(unregisteredPersonalData);
                                else
                                    data.Add(type, new () { unregisteredPersonalData });
                            }
                        }

                        var qualities = ConsoleHelper.ReadString("Перелік якостей: ");

                        var notebook_entry = new NotebookEntry(qualities, questionnaireData, adresses, collection, data, occupationPlace);
                        
                        ConsoleHelper.PrintSuccess("Введені дані:\n" + notebook_entry);

                        service.Add(notebook_entry);
                    }
                    break;
                    case "3":
                    {
                        var selectors = ConsoleHelper.ReadString("Перелік шаблон ([Поле/Поле.Поле]: [Значення], через кому, без квадратних дужок): ");
                        var all = service.GetAll();

                        foreach(var entry in all)
                        {
                            bool valid = true;
                            foreach(var selector in SplitAndTrim(selectors, ','))
                            {
                                var values = SplitAndTrim(selector, ':');

                                if(values.Length != 2)
                                {
                                    throw new Exception("Incorrect selector");
                                }

                                if(!MatchesSelector(values[0], values[1], entry))
                                {
                                    valid = false;
                                    break;
                                }
                            }

                            if(valid)
                            {
                                Console.WriteLine(entry);
                            }
                        }
                        

                        bool MatchesSelector(string propname, string value, object obj)
                        {
                            if(propname.Contains('.'))
                                return MatchesSelector(
                                    propname.Substring(propname.IndexOf('.') + 1),
                                    value,
                                    obj.GetType().GetProperty(propname.Substring(0, propname.IndexOf('.')))!.GetValue(obj)!
                                );

                            return obj.GetType().GetProperty(propname)!.GetValue(obj)!.ToString() == value;
                        }

                        string[] SplitAndTrim(string s, char separator)
                        {
                            return s.Split(separator).Select(s => s.Trim()).ToArray();
                        }
                    }
                    break;
                    case "4":
                    {
                        if(ConsoleHelper.ReadString("Сортувати за алфавітом (y/n): ", "Введіть 'y' або 'n'", (s) => "yn".Contains(s.ToLower())).ToLower() == "y")
                        {
                            System.Console.WriteLine(string.Join("\n", service.GetAll().OrderBy(t => t.Name + t.LastName)));

                        }
                        System.Console.WriteLine(string.Join("\n", service.GetAll()));
                    }
                    break;
                    case "2":
                    {
                        var entries = service.GetAll().Where(t => t.DateOfBirth.Day == DateTime.Now.Day && t.DateOfBirth.Month == DateTime.Now.Month);

                        if(entries.Count() == 0)
                        {
                            ConsoleHelper.PrintError("Дня народження сьогодні ні в кого не має");
                        }
                        else
                        {
                            foreach(var entry in entries)
                            {
                                Console.WriteLine(birthdayMessageCreator.CreateFor(entry));
                            }
                        }
                    }
                    break;
                    default:
                        Console.WriteLine("Невідома команда");
                        break;
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.PrintError("Трапилася помилка");
                ConsoleHelper.PrintError(e.Message);
            }
        }
    }
}