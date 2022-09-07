using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestDB2.Supporting
{
    public class MessageService
    {
        public static void MessageInform(string messsage)
        {
            MessageBox.Show(messsage, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void MessageError(string messsage)
        {
            MessageBox.Show(messsage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static MessageBoxResult MessageQuestion(string messsage)
        {
           return MessageBox.Show(messsage, "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

    }
}
