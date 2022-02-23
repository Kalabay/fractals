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
    /// Класс для обдуваемого ветром дерева.
    /// </summary>
    class Tree : Fractal
    {
        /// <summary>
        /// Отношение отрезков на двух итерациях.
        /// </summary>
        private double coefficient;
        /// <summary>
        /// Первый угол.
        /// </summary>
        private int angleFirst;
        /// <summary>
        /// Второй угол.
        /// </summary>
        private int angleSecond;

        /// <summary>
        /// Конструктор для присваивания значений.
        /// </summary>
        /// <param name="placeToDraw">форма, где надо рисовать.</param>
        /// <param name="depth">глубина фрактала.</param>
        /// <param name="parameters">коэффициент и оба угла.</param>
        public Tree(MainWindow placeToDraw, int depth, params int[] parameters) : base(placeToDraw, depth)
        {
            coefficient = (double)parameters[0] / 100;
            angleFirst = parameters[1];
            angleSecond = parameters[2];
        }

        /// <summary>
        /// Метод для проверки корректности глубины.
        /// </summary>
        /// <returns>некорректна ли глубина.</returns>
        public override bool DepthCheck()
        {
            return depth > 10;
        }

        /// <summary>
        /// Рекурсивный метод, который рисует 
        /// для дерева одну итерацию и вызывает следующие.
        /// </summary>
        /// <param name="lineNow">линия для данной итерации.</param>
        /// <param name="depthNow">глубина данной итерации.</param>
        private void DrawTreeIter(Line lineNow, int depthNow)
        {
            double xNow = lineNow.X2;
            double yNow = lineNow.Y2;
            double xNew = xNow + (lineNow.X2 - lineNow.X1);
            double yNew = yNow + (lineNow.Y2 - lineNow.Y1);
            Line lineNew = new Line();
            lineNew.X1 = xNow;
            lineNew.X2 = xNew;
            lineNew.Y1 = yNow;
            lineNew.Y2 = yNew;
            Line lineFirst = Rotation(lineNew, -angleFirst);
            Line lineSecond = Rotation(lineNew, angleSecond);
            xNew = lineFirst.X1
                + (lineFirst.X2 - lineFirst.X1) * coefficient;
            yNew = lineFirst.Y1
                + (lineFirst.Y2 - lineFirst.Y1) * coefficient;
            lineFirst.X2 = xNew;
            lineFirst.Y2 = yNew;
            xNew = lineSecond.X1
                + (lineSecond.X2 - lineSecond.X1) * coefficient;
            yNew = lineSecond.Y1
                + (lineSecond.Y2 - lineSecond.Y1) * coefficient;
            lineSecond.X2 = xNew;
            lineSecond.Y2 = yNew;
            lineFirst.Stroke = Brushes.DeepPink;
            lineSecond.Stroke = Brushes.DeepPink;
            lineFirst.StrokeThickness = 2;
            lineSecond.StrokeThickness = 2;
            placeToDraw.canvas.Children.Add(lineFirst);
            placeToDraw.canvas.Children.Add(lineSecond);
            if (depth != depthNow)
            {
                DrawTreeIter(lineFirst, depthNow + 1);
                DrawTreeIter(lineSecond, depthNow + 1);
            }
        }

        /// <summary>
        /// Метод для отображения фрактала.
        /// </summary>
        public override void Draw()
        {
            double midX = placeToDraw.canvas.ActualWidth / 2;
            double midY = placeToDraw.canvas.ActualHeight / 2;
            Line lineStart = new Line();
            lineStart.X1 = midX;
            lineStart.X2 = midX;
            lineStart.Y1 = midY + 120;
            lineStart.Y2 = midY + 60;
            lineStart.Stroke = Brushes.DeepPink;
            lineStart.StrokeThickness = 2;
            placeToDraw.canvas.Children.Add(lineStart);
            if (depth > 0)
            {
                DrawTreeIter(lineStart, 1);
            }
        }
    }
}