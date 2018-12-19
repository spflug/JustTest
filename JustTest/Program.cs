using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

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
    public delegate bool TryParse<T>(string text, out T t);

    public abstract class Maybe<T>
    {
        public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> f)
        {
            switch (this)
            {
                case Just<T> just: return f(just.Value);
                default: return new Nothing<TResult>();
            }
        }



        public static implicit operator Maybe<T>(T value)
            => value == null
                ? (Maybe<T>) new Nothing<T>()
                : new Just<T>(value);
    }

    public sealed class Just<T> : Maybe<T>
    {
        public Just(T value)
        {
            Value = value;
        }

        public T Value { get; }

        public static implicit operator T(Just<T> maybe) => maybe.Value;
    }

    public sealed class Nothing<T> : Maybe<T>
    {
    }

    static class Program
    {
        private const double Epsilon = 0.00000001D;
        private static readonly Dictionary<string, string> config = new Dictionary<string, string>
        {
            ["1"] = "1",
            ["2"] = "two"
        };

        private static Func<string, Maybe<T>> TryParse<T>(TryParse<T> f)
            => text
                => f(text, out var t)
                    ? (Maybe<T>) new Just<T>(t)
                    : new Nothing<T>();

        static Maybe<string> ReadConfig(string key) => TryParse(new TryParse<string>(config.TryGetValue))(key);
            //=> config.TryGetValue(key, out var value)
            //    ? (Maybe<string>)new Just<string>(value)
            //    : new Nothing<string>();

        private static Maybe<int> ToInt(string text) => TryParse(new TryParse<int>(int.TryParse))(text);

        static void Main(string[] args)
        {
            var just = ReadConfig("1").Bind(ToInt);
            var nothing1 = ReadConfig("2").Bind(ToInt);
            var nothing2 = ReadConfig("---").Bind(ToInt);

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

            const string text = "Point";

            int a = 10;
            int b = 0;

            Console.WriteLine($"a = {a} und b = {b}");
            Swap(ref a, ref b);
            Console.WriteLine($"a = {a} und b = {b}");

            var text1 = text;
            Print(ref text1);
            Console.WriteLine(text1 == text);

            WithExpression(s => s.Length, "foo");
            WithLambda(s => s.Length, "foo");

            Console.ReadKey();
        }

        static void CleanUp(Kacke.Kacke kacke)
        {

        }

        static void MongolianVowel()
        {
            const string emptyOrNot = "᠎";
            var format = "\"" + emptyOrNot + "\" => .Length = " + Length(emptyOrNot) + " or is it " +
                         ULength(emptyOrNot) + "?";
            Console.WriteLine(format);
            Console.ReadKey();

            int Length(string t) => t.Length;
            int ULength(string t) => Encoding.Default.GetByteCount(t);
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
