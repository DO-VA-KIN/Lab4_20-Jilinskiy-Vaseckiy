using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace lab4_20
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //регулярные выражения(+1 балл)
        public static readonly Regex[] MyRegex = new Regex[1]
        {        
        new Regex(@"^(\D)+$", RegexOptions.Compiled | RegexOptions.IgnoreCase),//соответсвует строке без цифр
        //new Regex(@"(\)$", RegexOptions.Compiled | RegexOptions.IgnoreCase)
        };


        public MainWindow()
        {
            InitializeComponent();
        }
        //при загрузке окна добавит строки для демонстрации
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LBInputWays.Items.Add(@"B:\sys!2\SystemThemes\Sys!");
            LBInputWays.Items.Add(@"A:\Sys\SystemThemes\Sys!.html");
            LBInputWays.Items.Add(@"A:\visual\studio\DesignTools\SystemThemes\Wpf!");
            LBInputWays.Items.Add(@"A:\we\23\DesignTools\Syst4mThemes\12wfs2.exe");
            LBInputWays.Items.Add(@"C:\Program Files\desctop.ini");
        }

        private void BtnNewWay_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog//создаст диалоговое окно для выбора файлов
            {
                Title = "Выберите файл",
                Multiselect = false
            };
            if(ofd.ShowDialog() == true)//отбразит его и дождется ответа
                TBNewWay.Text = ofd.FileName;//введет в строку путь к выбранному файлу
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LBInputWays.Items.Add(TBNewWay.Text);
            TBNewWay.Text = "";
        }




        private void BtnSort_Click(object sender, RoutedEventArgs e)
        {
            //это использование linq
            string[] ways = (from object item in LBInputWays.Items select item.ToString()).ToArray();
            List<string> list1 = new List<string>();//без цифр
            List<string> list2 = new List<string>();//с цифрами

            int i = 1;//для генерации пути
            foreach (string way in ways)
            {

                string[] ss = way.Split('\\');
                string s1 = ss[ss.Length - 1];
                for (int j = 0; j < ss.Length - 1; j++)
                {
                    string s2 = ss[j];
                    if (s1[0] == s2[0] && s1[1] == s2[1] && s1[2] == s2[2])
                        ss[j] = "way" + i;// если первые 3 символа в 
                }

                ss[ss.Length - 1] = ss[ss.Length - 1].Replace("!.", ".");//замена ! у файлов с расширением
                if (ss[ss.Length - 1][ss[ss.Length - 1].Length-1] == '!')//замена ! у файлов без расширения
                    ss[ss.Length - 1] = ss[ss.Length - 1].Remove(ss[ss.Length - 1].Length-1);


                string newWay = String.Join("\\", ss);
                if (MyRegex[0].IsMatch(newWay))//с цифрами и без - сортировка
                    list1.Add(newWay);
                else list2.Add(newWay);
            }

            //сперва выведет без цифр
            foreach (string s in list1)
                LBResultWays.Items.Add(s);
            foreach (string s in list2)
                LBResultWays.Items.Add(s);
        }






        private void BtnClearInput_Click(object sender, RoutedEventArgs e)
        {
            LBInputWays.Items.Clear();
        }

        private void BtnClearResult_Click(object sender, RoutedEventArgs e)
        {
            LBResultWays.Items.Clear(); 
        }


    }
}
