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
    /// Класс для треугольник Серпинского.
    /// </summary>
    class SierpinskiTriangle : Fractal
    {
        /// <summary>
        /// Конструктор для присваивания значений.
        /// </summary>
        /// <param name="placeToDraw">форма, где надо рисовать.</param>
        /// <param name="depth">глубина фрактала.</param>
        public SierpinskiTriangle(MainWindow placeToDraw, int depth) : base(placeToDraw, depth) { }

        /// <summary>
        /// Метод для проверки корректности глубины.
        /// </summary>
        /// <returns>некорректна ли глубина.</returns>
        public override bool DepthCheck()
        {
            return depth > 7;
        }

        /// <summary>
        /// Нарисовать один треугольник.
        /// </summary>
        /// <param name="pointFirst">первая вершина.</param>
        /// <param name="pointSecond">вторая вершина.</param>
        /// <param name="pointThird">третья вершина.</param>
        private void DrawTriangle(Point pointFirst, Point pointSecond, Point pointThird)
        {
            Polygon polygonDraw = new Polygon();
            polygonDraw.Points.Add(pointFirst);
            polygonDraw.Points.Add(pointSecond);
            polygonDraw.Points.Add(pointThird);
            polygonDraw.Stroke = Brushes.DeepPink;
            placeToDraw.canvas.Children.Add(polygonDraw);
        }

        /// <summary>
        /// Рекурсивный метод, который рисует 
        /// для треугольника одну итерацию и вызывает следующие.
        /// </summary>
        /// <param name="pointFirst">первая вершина.</param>
        /// <param name="pointSecond">вторая вершина.</param>
        /// <param name="pointThird">третья вершина.</param>
        /// <param name="depthNow">глубина данной итерации.</param>
        private void DrawTriangleIter(Point pointFirst, Point pointSecond, Point pointThird, int depthNow)
        {
            DrawTriangle(pointFirst, pointSecond, pointThird);
            if (depth != depthNow)
            {
                Point pointNewFirst = new Point();
                pointNewFirst.X = (pointFirst.X + pointSecond.X) / 2;
                pointNewFirst.Y = (pointFirst.Y + pointSecond.Y) / 2;
                Point pointNewSecond = new Point();
                pointNewSecond.X = (pointFirst.X + pointThird.X) / 2;
                pointNewSecond.Y = (pointFirst.Y + pointThird.Y) / 2;
                Point pointNewThird = new Point();
                pointNewThird.X = (pointThird.X + pointSecond.X) / 2;
                pointNewThird.Y = (pointThird.Y + pointSecond.Y) / 2;
                DrawTriangleIter(pointNewFirst, pointNewSecond, pointFirst, depthNow + 1);
                DrawTriangleIter(pointNewFirst, pointNewThird, pointSecond, depthNow + 1);
                DrawTriangleIter(pointNewThird, pointNewSecond, pointThird, depthNow + 1);
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
            Line triangleBase = new Line();
            triangleBase.X1 = leftX;
            triangleBase.X2 = rightX;
            triangleBase.Y1 = midY + 150;
            triangleBase.Y2 = midY + 150;
            Line triangleLeftSide = Rotation(triangleBase, -60);
            Point pointThird = new Point(triangleLeftSide.X2, triangleLeftSide.Y2);
            DrawTriangleIter(new Point(leftX, midY + 150), new Point(rightX, midY + 150), pointThird, 0);
        }
    }
}