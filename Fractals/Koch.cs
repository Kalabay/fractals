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
    /// Класс для кривой Коха.
    /// </summary>
    class Koch : Fractal
    {
        /// <summary>
        /// Конструктор для присваивания значений.
        /// </summary>
        /// <param name="placeToDraw">форма, где надо рисовать.</param>
        /// <param name="depth">глубина фрактала.</param>
        public Koch(MainWindow placeToDraw, int depth) : base(placeToDraw, depth) { }

        /// <summary>
        /// Метод для проверки корректности глубины.
        /// </summary>
        /// <returns>некорректна ли глубина.</returns>
        public override bool DepthCheck()
        {
            return depth > 6;
        }

        /// <summary>
        /// Метод для смены направления линии.
        /// </summary>
        /// <param name="line">линия для смены.</param>
        private void Swap(Line line)
        {
            double swap = line.X1;
            line.X1 = line.X2;
            line.X2 = swap;
            swap = line.Y1;
            line.Y1 = line.Y2;
            line.Y2 = swap;
        }

        /// <summary>
        /// Рекурсивный метод, который рисует 
        /// для Коха одну итерацию и вызывает следующие.
        /// </summary>
        /// <param name="lineNow">линия для данной итерации.</param>
        /// <param name="depthNow">глубина данной итерации.</param>
        private void DrawKochIter(Line lineNow, int depthNow)
        {
            if (depth == depthNow)
            {
                lineNow.Stroke = Brushes.DeepPink;
                lineNow.StrokeThickness = 1.25;
                placeToDraw.canvas.Children.Add(lineNow);
                return;
            }
            double xNowFirst = (lineNow.X2 + 2 * lineNow.X1) / 3;
            double xNowSecond = (2 * lineNow.X2 + lineNow.X1) / 3;
            double yNowFirst = (lineNow.Y2 + 2 * lineNow.Y1) / 3;
            double yNowSecond = (2 * lineNow.Y2 + lineNow.Y1) / 3;
            Line lineFirst = new Line();
            lineFirst.X1 = lineNow.X1;
            lineFirst.X2 = xNowFirst;
            lineFirst.Y1 = lineNow.Y1;
            lineFirst.Y2 = yNowFirst;
            Line lineSecond = new Line();
            lineSecond.X1 = xNowFirst;
            lineSecond.X2 = xNowSecond;
            lineSecond.Y1 = yNowFirst;
            lineSecond.Y2 = yNowSecond;
            Line lineThird = new Line();
            lineThird.X1 = xNowSecond;
            lineThird.X2 = lineNow.X2;
            lineThird.Y1 = yNowSecond;
            lineThird.Y2 = lineNow.Y2;
            Line lineNextFirst = Rotation(lineSecond, -60);
            Swap(lineSecond);
            Line lineNextSecond = Rotation(lineSecond, 60);
            Swap(lineNextSecond);
            DrawKochIter(lineFirst, depthNow + 1);
            DrawKochIter(lineNextFirst, depthNow + 1);
            DrawKochIter(lineNextSecond, depthNow + 1);
            DrawKochIter(lineThird, depthNow + 1);
        }

        /// <summary>
        /// Метод для отображения фрактала.
        /// </summary>
        public override void Draw()
        {
            double midX = placeToDraw.canvas.ActualWidth / 2;
            double midY = placeToDraw.canvas.ActualHeight / 2;
            Line lineStart = new Line();
            lineStart.X1 = midX - 250;
            lineStart.X2 = midX + 250;
            lineStart.Y1 = midY + 50;
            lineStart.Y2 = midY + 50;
            DrawKochIter(lineStart, 0);
        }
    }
}