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

namespace Fractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Хранение выбранного пользователем фрактала.
        /// </summary>
        private string fractalType;
        /// <summary>
        /// Хранение выбранной пользователем глубины.
        /// </summary>
        private int? depth;
        /// <summary>
        /// Хранение коэффициента пользователя.
        /// </summary>
        private int? coefficient;
        /// <summary>
        /// Хранение первого угла пользователя.
        /// </summary>
        private int? angleFirst;
        /// <summary>
        /// Хранение второго угла пользователя.
        /// </summary>
        private int? angleSecond;
        /// <summary>
        /// Хранение дистанции пользователя.
        /// </summary>
        private int? distance;

        /// <summary>
        /// Конструктор, где задаются некоторые значения
        /// и происходит инициализация.
        /// </summary>
        public MainWindow()
        {
            fractalType = null;
            depth = null;
            InitializeComponent();
        }

        /// <summary>
        /// Получение типа фрактала.
        /// </summary>
        /// <param name="sender">ссылка на объект.</param>
        /// <param name="e">объект, относящийся к обрабатываемому событию.</param>
        private void RadioButtonType_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            fractalType = pressed.Content.ToString();
        }

        /// <summary>
        /// Получение значения глубины и запуск фрактала.
        /// </summary>
        /// <param name="sender">ссылка на объект.</param>
        /// <param name="e">объект, относящийся к обрабатываемому событию.</param>
        private void ComboBoxDepth_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            TextBlock selectedNumber = (TextBlock)comboBox.SelectedItem;
            depth = int.Parse(selectedNumber.Text);
            Draw_Click(sender, e);
        }

        /// <summary>
        /// Получение значения коэффициента.
        /// </summary>
        /// <param name="sender">ссылка на объект.</param>
        /// <param name="e">объект, относящийся к обрабатываемому событию.</param>
        private void TextBoxCoefficient_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int coefficientNow;
            if (int.TryParse(textBox.Text, out coefficientNow))
            {
                coefficient = coefficientNow;
                return;
            }
            coefficient = null;
        }

        /// <summary>
        /// Получение значения первого угла.
        /// </summary>
        /// <param name="sender">ссылка на объект.</param>
        /// <param name="e">объект, относящийся к обрабатываемому событию.</param>
        private void TextBoxAngleFirst_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int angleFirstNow;
            if (int.TryParse(textBox.Text, out angleFirstNow))
            {
                angleFirst = angleFirstNow;
                return;
            }
            angleFirst = null;
        }

        /// <summary>
        /// Получение значения второго угла.
        /// </summary>
        /// <param name="sender">ссылка на объект.</param>
        /// <param name="e">объект, относящийся к обрабатываемому событию.</param>
        private void TextBoxAngleSecond_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int angleSecondNow;
            if (int.TryParse(textBox.Text, out angleSecondNow))
            {
                angleSecond = angleSecondNow;
                return;
            }
            angleSecond = null;
        }

        /// <summary>
        /// Получение значения дистанции.
        /// </summary>
        /// <param name="sender">ссылка на объект.</param>
        /// <param name="e">объект, относящийся к обрабатываемому событию.</param>
        private void TextBoxDistance_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int distanceNow;
            if (int.TryParse(textBox.Text, out distanceNow))
            {
                distance = distanceNow;
                return;
            }
            distance = null;
        }

        /// <summary>
        /// Метод для создания дерева и вызова метода рисования.
        /// </summary>
        private void DrawTree()
        {
            if (coefficient == null || coefficient > 75 || coefficient < 50)
            {
                MessageBox.Show("Некорректное значение коэффициента.\n" +
                    "Оно должно быть целым числом\n" +
                    "и лежать в отрезке [50, 75],\n" +
                    "то есть Вы вводите длина/пред. длина в %.\n" +
                    "Например: ввод 53 = 53% = 0.5");
                return;
            }
            if (angleFirst == null || angleFirst > 90 || angleFirst < 20)
            {
                MessageBox.Show("Некорректное значение первого угла.\n" +
                    "Оно должно быть целым числом\n" +
                    "и лежать в отрезке [20, 90].");
                return;
            }
            if (angleSecond == null || angleSecond > 90 || angleSecond < 20)
            {
                MessageBox.Show("Некорректное значение второго угла.\n" +
                    "Оно должно быть целым числом\n" +
                    "и лежать в отрезке [20, 90].");
                return;
            }
            Tree tree = new Tree(this, (int)depth, (int)coefficient, (int)angleFirst, (int)angleSecond);
            if (tree.DepthCheck())
            {
                MessageBox.Show("Некорректное значение глубины.\n" +
                    "Оно должно быть <= 10.");
                return;
            }
            canvas.Children.Clear();
            tree.Draw();
        }

        /// <summary>
        /// Метод для создания Коха и вызова метода рисования.
        /// </summary>
        private void DrawKoch()
        {
            Koch koch = new Koch(this, (int)depth);
            if (koch.DepthCheck())
            {
                MessageBox.Show("Некорректное значение глубины.\n" +
                    "Оно должно быть <= 6.");
                return;
            }
            canvas.Children.Clear();
            koch.Draw();
        }

        /// <summary>
        /// Метод для создания ковра и вызова метода рисования.
        /// </summary>
        private void DrawSierpinskiСarpet()
        {
            SierpinskiСarpet sierpinskiСarpet = new SierpinskiСarpet(this, (int)depth);
            if (sierpinskiСarpet.DepthCheck())
            {
                MessageBox.Show("Некорректное значение глубины.\n" +
                    "Оно должно быть <= 4.");
                return;
            }
            canvas.Children.Clear();
            sierpinskiСarpet.Draw();
        }


        /// <summary>
        /// Метод для создания треугольника и вызова метода рисования.
        /// </summary>
        private void DrawSierpinskiTriangle()
        {
            SierpinskiTriangle sierpinskiTriangle = new SierpinskiTriangle(this, (int)depth);
            if (sierpinskiTriangle.DepthCheck())
            {
                MessageBox.Show("Некорректное значение глубины.\n" +
                    "Оно должно быть <= 7.");
                return;
            }
            canvas.Children.Clear();
            sierpinskiTriangle.Draw();
        }

        /// <summary>
        /// Метод для создания множества Кантора и вызова метода рисования.
        /// </summary>
        private void DrawCantorSet()
        {
            if (distance == null || distance > 25 || distance < 1)
            {
                MessageBox.Show("Некорректное значение расстояния.\n" +
                    "Оно должно быть целым числом\n" +
                    "и лежать в отрезке [1, 25].");
                return;
            }
            CantorSet cantorSet = new CantorSet(this, (int)depth, (int)distance);
            if (cantorSet.DepthCheck())
            {
                MessageBox.Show("Некорректное значение глубины.\n" +
                    "Оно должно быть <= 7.");
                return;
            }
            canvas.Children.Clear();
            cantorSet.Draw();
        }

        /// <summary>
        /// Метод для вызова нужного фрактала по нажатию кнопки.
        /// </summary>
        /// <param name="sender">ссылка на объект.</param>
        /// <param name="e">объект, относящийся к обрабатываемому событию.</param>
        private void Draw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (fractalType == null)
                {
                    MessageBox.Show("Не выбран фрактал.");
                    return;
                }
                if (depth == null)
                {
                    MessageBox.Show("Не выбрана глубина.");
                    return;
                }
                switch (fractalType)
                {
                    case "Фрактальное дерево":
                        DrawTree();
                        break;
                    case "Кривая Коха":
                        DrawKoch();
                        break;
                    case "Ковёр Серпинского":
                        DrawSierpinskiСarpet();
                        break;
                    case "Треугольник Серпинского":
                        DrawSierpinskiTriangle();
                        break;
                    default:
                        DrawCantorSet();
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Некорректные данные.");
            }
        }
    }
}
