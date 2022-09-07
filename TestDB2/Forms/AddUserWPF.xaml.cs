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
using System.Windows.Shapes;
using TestDB2.DB;

namespace TestDB2.Forms
{
    /// <summary>
    /// Логика взаимодействия для AddUserWPF.xaml
    /// </summary>
    public partial class AddUserWPF : Window
    {

        private List<string> genderContentComboboxGender = 
            new List<string>() { "Мужской", "Женский" }; // статичные данные  для комбобокса

        public AddUserWPF()
        {
            InitializeComponent();
            this.Loaded += AddUserWPF_Loaded; // загрузка формы
            this.Closing += AddUserWPF_Closed; // закрытие формы /// переписал  тут 
        }

        private void AddUserWPF_Closed(object? sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow(); 
            mainWindow.Show();                             // и тут 
        }

        private void AddUserWPF_Loaded(object sender, RoutedEventArgs e)
        {
           cbGender.ItemsSource = genderContentComboboxGender;
           cbGender.SelectedIndex = 0; // Установим  по умолчанию при  загрузке первый элемент 
        }

        private void btAddUser_Click(object sender, RoutedEventArgs e)
        {
            #region  валидация 

            if (string.IsNullOrEmpty(tbName.Text))
            {
                Supporting.MessageService.MessageError("Укажите имя пользователя");
                return;
            }
            int age = 0;

            try
            {
                age = Convert.ToInt32(tbAge.Text);
                if (age < 0)
                {
                    Supporting.MessageService.MessageError("Некорректный возраст пользователя");
                    return;
                }
            }
            catch
            {
                Supporting.MessageService.MessageError("Вы указали не число  в  графе возраст");
                return;
            }
            #endregion

            DB.User newUser = new DB.User // созадли объет 
            {
                UserName = tbName.Text, // 
                UserAge = age,
                Gender = cbGender.SelectedItem.ToString()
            };

            DB.MyContext myContext = new DB.MyContext(); // создали соединение  с бд 
            try
            {
                myContext.Users.Add(newUser); // добавили в  таблицу  юзек в бд 
                myContext.SaveChanges(); // сохранили  изменения  при  добавлении !!!! ОБАЯЗАТЕЛЬНО ДЕЛАТЬ 
                Supporting.MessageService.MessageInform($"Пользователь {newUser.UserName} добавлен в базу данных"); // Вывели пользователю  результат 
            }
            catch (Exception ex) // если ошибка 
            {
                Supporting.MessageService.MessageError($"Ошибка добавления пользователя  в  базу данных " + ex.Message); // Оповестить
                // пользователя 
            }
        }

        private void btDown_Click(object sender, RoutedEventArgs e)
        {
            Close(); // переписал тут 
        }


      
        private void btAddRange_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            try
            {
                count = Convert.ToInt32(tbCount.Text);
                if(count < 0)
                {
                    Supporting.MessageService.MessageError("Некорректный ввод");
                    return; 
                }
            }
            catch (Exception)
            {
                Supporting.MessageService.MessageError("Ошибка при  конвертации  числа ");
                return;
            }

            try
            {
                AddUserAvto(count);
            }
            catch (Exception)
            {
                Supporting.MessageService.MessageError("Error");
            }
        }

        /// <summary>
        /// Случайные  пользователи  в бд
        /// </summary>
        /// <param name="count"></param>
        public void AddUserAvto(int count) // Кол-во  юзеров 
        {
            try
            {
                for (int i = 0; i < count; i++)
                {
                    AddUserRandon(); 
                }
                Supporting.MessageService.MessageInform("Все !!!");
            }
            catch (Exception)
            {
                Supporting.MessageService.MessageError("Ошибка при сохранении  в  бд");
            }
        }

        private void AddUserRandon( ) // случайный  пользователь  в  бд 
        {
            Random random = new Random();
            string name = string.Empty;
            name += Convert.ToChar(random.Next(1040, 1071)); // заглавная буква 

            for (int i = 0; i < random.Next(3 , 10); i++)
            {
                name += Convert.ToChar(random.Next(1072, 1103)); // прописыне  буква 
            }

            User user = new User
            {
                UserName = name,
                UserAge = random.Next(1, 100), // случайный  возраст 
                Gender = genderContentComboboxGender[random.Next(0, genderContentComboboxGender.Count)] // случайный гендер 
            };
            DB.MyContext myContext = new DB.MyContext();
            myContext.Users.Add(user);
            myContext.SaveChanges();
        }

        private void btClear_Click(object sender, RoutedEventArgs e)
        {
                var rez = Supporting.MessageService.MessageQuestion("вы уверены  что  хотите  удалить  все данные  из таблицы  Юзеры?" +
                "\n если нажмете ДА -  процесс  будет  не  обратим "  
                );

            if (rez == MessageBoxResult.Yes)
            {
                try
                {
                    DB.MyContext myContext = new DB.MyContext();
                    myContext.Users.RemoveRange(myContext.Users);
                    myContext.SaveChanges();
                    Supporting.MessageService.MessageInform("БД  чиста");
                }
                catch (Exception)
                {
                    Supporting.MessageService.MessageError("Ошибка");
                }
            }
        }
    }
}
