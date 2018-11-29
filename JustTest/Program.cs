using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

#region don't touch it, don't ásk, don't look at it.... it just works

using ContractKacke = Kacke.Kacke;
using AuchKacke = AuchKacke.Kacke;

#endregion

namespace AuchKacke
{
    public class Kacke
    {
        public bool Stinky { get; set; }
    }
}

namespace Kacke
{
    public class Kacke
    {
        public bool Stinky { get; set; }
    }
}


namespace JustTest
{
    public class Int
    {
        public Int()
        {
            var col1 = Enumerable.Range(0, 100);
            var col2 = Enumerable.Range(0, 100);

            var t =new List<(int, int)>();

            var tl = col1.Join(col2, i => i, i => i * i, (i1, i2) => (i1, i2)).ToList();

            foreach (var i1 in col1)
            {
                foreach (var i2 in col2)
                {
                    if (i2 == i1 * i1) t.Add((i1, i2));
                }
            }

            t.ForEach(tx => Console.WriteLine($"{tx}"));
        }
    }

    public class Bool
    {

    }

    #region MyRegion

    interface IFeature
    {

    }

    abstract class Shape
    {
        protected Shape(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public abstract double Area();
    }

    class Rectangle : Shape
    {
        public Rectangle(string name, double a, double b) : base(name)
        {
            A = a;
            B = b;
        }

        public Rectangle(double a, double b) : this(nameof(Rectangle), a, b)
        {
        }

        public double A { get; }
        public double B { get; }
        public override double Area()
        {
            return A * B;
        }
    }

    class Square : Rectangle
    {
        public Square(double a) : base(nameof(Square), a, a)
        {
        }
    }

    class Triangle : Shape
    {
        public Triangle(double @base, double heigth) : base(nameof(Triangle))
        {
            Base = @base;
            Heigth = heigth;
        }

        public double Base { get; }
        public double Heigth { get; }

        public override double Area()
        {
            return .5 * Base * Heigth;
        }
    }

    class KrasseKlasse
    {
        private KrasseKlasse()
        {
        }

        public IEnumerable<IFeature> Features { get; set; }

        public class Builder
        {
            private List<IFeature> features = new List<IFeature>();

            public Builder AddFeeture(IFeature feature)
            {
                this.features.Add(feature);
                return this;
            }

            public KrasseKlasse Build()
            {
                return new KrasseKlasse { Features = this.features };
            }
        }
    }

    class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public double DistanceFromOrigin()
        {
            var dist = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            return dist;
        }

        public override string ToString() => $"({X}|{Y})";
    }

    #endregion

    static class Program
    {
        private const double Epsilon = 0.00000001D;

        static void CleanUp(Kacke.Kacke kacke)
        {

        }

        static void Main(string[] args)
        {
            var k1 = new Kacke.Kacke();
            var k2 = new AuchKacke.Kacke();

            int big = int.MaxValue;
            int bigger = big + 1;
            Console.WriteLine(bigger == int.MinValue);

            var first = Enumerable.Range(1, 11000);
            var snd = SomeInts().Take(153223);

            var strings = first.Concat(snd).Select(id => id.ToString("X2")).ToArray();

            IEnumerable<int> SomeInts()
            {
                var rnd = new Random();
                while (true)
                {
                    yield return rnd.Next();
                }
            }

            var point = new Point(0, 0);

            var dist1 = point.DistanceFromOrigin();
            var dist2 = point.DistanceFromOrigin();

            const string text = "Point";

            int a = 10;
            int b = 0;

            Console.WriteLine($"a = {a} und b = {b}");
            Swap(ref a, ref b);
            Console.WriteLine($"a = {a} und b = {b}");

            var text1 = text;
            Print(ref text1);
            Console.WriteLine(text1 == text);

            Console.WriteLine(Math.Abs(dist1 - dist2) < Epsilon);
            Console.ReadKey();
            var builder = new KrasseKlasse.Builder()
                .AddFeeture(default(IFeature))
                .AddFeeture(default(IFeature))
                .AddFeeture(default(IFeature))
                .AddFeeture(default(IFeature))
                .AddFeeture(default(IFeature))
                .AddFeeture(default(IFeature))
                .AddFeeture(default(IFeature));

            var kk = builder.Build();

            WithExpression(s => s.Length, "foo");
            WithLambda(s => s.Length, "foo");

            Console.ReadKey();
        }

        static Shape Scale(this Shape shape, double factor)
        {
            var c = new ReadOnlyObservableCollection<int>(new ObservableCollection<int>(new[] {0, 1}));

            switch (shape)
            {
                case Square sqr: return new Square(sqr.A * factor);
                case Rectangle rct: return new Rectangle(rct.A * factor, rct.B * factor);
                case Triangle tr: return new Triangle(tr.Base * factor, tr.Heigth * factor);
                default: throw new NotSupportedException("unsupported shape!");
            }
        }

        static Point TranslateByX(Point p, double dist)
        {
            return new Point(p.X + dist, p.Y);
        }

        static void Swap(ref int a, ref int b)
        {
            a ^= b;
            b ^= a;
            a ^= b;
        }

        static void Print<T>(ref T p)
        {
            Console.WriteLine(p);
        }

        static void WithExpression<TSource, TProperty>(Expression<Func<TSource, TProperty>> lambda, TSource source)
        {
            Console.WriteLine(lambda);
            var compile = lambda.Compile();
            var property = compile(source);
            switch (lambda.Body)
            {
                case MemberExpression me when me.Member is PropertyInfo pi:
                    Console.WriteLine($"SELECT {typeof(TSource).Name}.{pi.Name} FROM {source}");
                    break;
            }

            Console.WriteLine($"with expression {lambda} = {property}");
            Console.WriteLine($"with lambda     {compile} = {property}");
        }

        static void WithLambda<TSource, TProperty>(Func<TSource, TProperty> lambda, TSource source)
        {
            Console.WriteLine($"with lambda     {lambda} = {lambda(source)}");
        }

    }
}
