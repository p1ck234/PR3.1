using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Text.RegularExpressions;
using System.IO;

namespace PR3._1
{
    /// <summary>
    /// Interaction logic for CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Page
    {
        public CreateWindow()
        {
            InitializeComponent();
            // длина для текстбоксов
            tbPassport1.MaxLength = 4;
            tbPassport2.MaxLength = 6;
            tbNumber.MaxLength = 12;
        }
        // используемые регулярные выражения для ограничения ввода
        Regex inputNumber = new Regex(@"[0-9]");
        Regex inputPhone = new Regex(@"[0-9\+]");
        Regex inputChar = new Regex(@"^[а-яА-Я]+$");
        Regex inputEmail = new Regex(@"[a-zA-Z0-9_\-\.]");

        private void TbID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match match = inputNumber.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void TbNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match match = inputPhone.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void TbSurName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match match = inputChar.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void TbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match match = inputChar.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void TbPatro_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match match = inputChar.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void TbPassport1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match match = inputNumber.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void TbPassport2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match match = inputNumber.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void TbEmail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Match match = inputEmail.Match(e.Text);
            if (!match.Success)
            {
                e.Handled = true;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //Экземпляр который собирает ошибки
            StringBuilder errors = new StringBuilder();
            //Путь к папке
            string path = @"C:\Users\danil\Desktop\employee.txt";
            // лист для сбора данных из программы
            List<string> employeeList = new List<string>();
            // лист для сбора данных из файла
            List<string> employeeTXT = new List<string>();

            try
            {
                // поток считывания файла
                using (StreamReader sr = new StreamReader(path))
                {
                    // заполнение листа из файла
                    while (!sr.EndOfStream)
                    {
                        employeeTXT.Add(sr.ReadLine());
                    }

                    // if для проверки идентификатора на занятость
                    string temp = $"Идентификатор: {tbID.Text}\t";
                    if (tbID.Text != "")
                    {
                        if (employeeTXT.Exists(s => s.Contains(temp) == true))
                        {
                            errors.AppendLine("Идентификатор уже занят");
                        }
                        else
                        {
                            employeeList.Add(temp);
                        }
                    }
                    else
                    {
                        errors.AppendLine("Идентификатор не заполнен");
                    }

                    // if для проверки ФИО
                    if (tbSurName.Text != "" && tbName.Text != "")
                    {
                        // if для проверки первой буквы Фамилии
                        string surname = tbSurName.Text;
                        if (surname[0] == surname.ToUpper()[0])
                        {
                            // if для проверки первой буквы Имени
                            string name = tbName.Text;
                            if (name[0] == name.ToUpper()[0])
                            {
                                // if для проверки первой буквы Отчества
                                string patro = tbPatro.Text;
                                if (patro != "")
                                {
                                    if (patro[0] == patro.ToUpper()[0])
                                    {
                                        employeeList.Add($"Фамилия: {tbSurName.Text}\tИмя: {tbName.Text}\tОтчество: {tbPatro.Text}\t");
                                    }
                                    else
                                    {
                                        errors.AppendLine("Отчество должно быть с заглавной буквы");
                                    }
                                }
                                else
                                {
                                    employeeList.Add($"Фамилия: {tbSurName.Text}\tИмя: {tbName.Text}\tОтчество: \t");
                                }
                            }
                            else
                            {
                                errors.AppendLine("Имя должно быть с заглавной буквы");
                            }
                        }
                        else
                        {
                            errors.AppendLine("Фамилия должна быть с заглавной буквы");
                        }

                    }
                    else
                    {
                        errors.AppendLine("Некорректно заполненые поля Имя и Фамилия");
                    }

                    // if для проверки заполненности паспортных данных
                    if (tbPassport1.Text.Length != 4 && tbPassport2.Text.Length != 6)
                    {
                        errors.AppendLine("Паспортные данные заполнены не корректно");
                    }
                    else
                    {
                        employeeList.Add($"Паспорт: {tbPassport1.Text} {tbPassport2.Text}\t");
                    }

                    // if для проверки номера телефона
                    string number = tbNumber.Text;
                    if (number != "")
                    {
                        if ((number[0] == '+' && number.Length == 12) || (number[0] == '8' && number.Length == 11))
                        {
                            bool k = false;
                            for (int i = 1; i < number.Length; i++)
                            {
                                if (number[i] == '+')
                                {
                                    errors.AppendLine("Номер телефона указан неверно");
                                }
                                else
                                {
                                    k = true;
                                }
                            }
                            if (k == true)
                            {
                                employeeList.Add($"Мобильный телефон: {tbNumber.Text}\t");
                            }
                        }
                    }
                    else
                    {
                        employeeList.Add($"Мобильный телефон: \t");
                    }

                    // if для проверки email адреса
                    string Email = tbEmail.Text;
                    if (Email != "" && char.IsLetter(Email, 0) == true)
                    {
                        employeeList.Add($"Email: {tbEmail.Text}@firma.ru\t");
                    }
                    else
                    {
                        errors.AppendLine("Некорректно указан email адрес");
                    }

                    // закрытие потока чтения
                    sr.Close();
                }

                // проверка на ошибки 
                if (errors.Length > 0)
                {
                    // выводит ошибки в месседжбокс
                    errors.AppendLine("Данные не сохранены");
                    MessageBox.Show(errors.ToString(),
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    // поток записи в файл
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        foreach (var akk in employeeList)
                        {
                            sw.Write($"{akk.ToString()}");
                        }
                        sw.WriteLine();
                        sw.Close();
                    }
                    MessageBox.Show("Данные сохранены в файл",
                        "Информация",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }
    }
}
