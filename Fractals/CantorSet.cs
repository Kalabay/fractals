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
    /// Класс для множества Кантора.
    /// </summary>
    class CantorSet : Fractal
    {
        /// <summary>
        /// Расстояние между двумя итерациями.
        /// </summary>
        private int distance;

        /// <summary>
        /// Конструктор для присваивания значений.
        /// </summary>
        /// <param name="placeToDraw">форма, где надо рисовать.</param>
        /// <param name="depth">глубина фрактала.</param>
        /// <param name="distanceNew">расстояние между двумя итерациями.</param>
        public CantorSet(MainWindow placeToDraw, int depth, int distanceNew) : base(placeToDraw, depth)
        {
            distance = distanceNew;
        }

        /// <summary>
        /// Метод для проверки корректности глубины.
        /// </summary>
        /// <returns>некорректна ли глубина.</returns>
        public override bool DepthCheck()
        {
            return depth > 7;
        }

        /// <summary>
        /// Рекурсивный метод, который рисует 
        /// для Кантора одну итерацию и вызывает следующие.
        /// </summary>
        /// <param name="lineNow">линия для данной итерации.</param>
        /// <param name="depthNow">глубина данной итерации.</param>
        private void DrawCantorIter(Line lineNow, int depthNow)
        {
            lineNow.Stroke = Brushes.DeepPink;
            lineNow.StrokeThickness = 20;
            placeToDraw.canvas.Children.Add(lineNow);
            if (depthNow != depth)
            {
                Line lineLeft = new Line();
                lineLeft.X1 = lineNow.X1;
                lineLeft.X2 = (2 * lineNow.X1 + lineNow.X2) / 3;
                lineLeft.Y1 = lineNow.Y1 + distance + 20;
                lineLeft.Y2 = lineNow.Y2 + distance + 20;
                DrawCantorIter(lineLeft, depthNow + 1);
                Line lineRight = new Line();
                lineRight.X1 = (lineNow.X1 + 2 * lineNow.X2) / 3;
                lineRight.X2 = lineNow.X2;
                lineRight.Y1 = lineNow.Y1 + distance + 20;
                lineRight.Y2 = lineNow.Y2 + distance + 20;
                DrawCantorIter(lineRight, depthNow + 1);
            }
        }

        /// <summary>
        /// Метод для отображения фрактала.
        /// </summary>
        public override void Draw()
        {
            double midX = placeToDraw.canvas.ActualWidth / 2;
            double midY = placeToDraw.canvas.ActualHeight / 2;
            double leftX = midX - 180;
            double rightX = midX + 180;
            Line cantorBase = new Line();
            cantorBase.X1 = leftX;
            cantorBase.X2 = rightX;
            cantorBase.Y1 = midY - 150;
            cantorBase.Y2 = midY - 150;
            DrawCantorIter(cantorBase, 0);
        }
    }
}