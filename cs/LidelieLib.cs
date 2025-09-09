using System;
using System.Collections;
using System.Collections.Generic;
using static System.Math;

namespace LidelieLib{
    public static class Std{
        public static class IO{
            static Queue<string> input;
            static IO(){
                input = new Queue<string>();
            }
            
            public static T Assign<T>(){
                object re = null;
                if(typeof(T) == typeof(string[]))re = Console.ReadLine().Split(' ');
                //if(typeof(T) == typeof(char[]))re = Array.ConvertAll(Console.ReadLine().Split(' '),char.Parse);
                if(typeof(T) == typeof(int[]))re = Array.ConvertAll(Console.ReadLine().Split(' '),int.Parse);
                if(typeof(T) == typeof(long[]))re = Array.ConvertAll(Console.ReadLine().Split(' '),long.Parse);
                if(typeof(T) == typeof(float[]))re = Array.ConvertAll(Console.ReadLine().Split(' '),float.Parse);
                if(typeof(T) == typeof(double[]))re = Array.ConvertAll(Console.ReadLine().Split(' '),double.Parse);
                if(re!=null){
                    return (T)re;
                }
                if(input.Count == 0){
                    foreach(string item in Console.ReadLine().Split(' ')){
                        input.Enqueue(item);
                    }
                }
                if(typeof(T) == typeof(string))re = input.Dequeue();
                if(typeof(T) == typeof(char))re = char.Parse(input.Dequeue());
                if(typeof(T) == typeof(char[]))re = input.Dequeue().ToCharArray();
                if(typeof(T) == typeof(int))re = int.Parse(input.Dequeue());
                if(typeof(T) == typeof(long))re = long.Parse(input.Dequeue());
                if(typeof(T) == typeof(ulong))re = ulong.Parse(input.Dequeue());
                if(typeof(T) == typeof(float))re = float.Parse(input.Dequeue());
                if(typeof(T) == typeof(double))re = double.Parse(input.Dequeue());
                
                return (T)re;
            }
            
            public static void Out(List<int> arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(List<long> arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(List<string> arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(List<char> arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(List<float> arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(List<double> arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(int[] arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(long[] arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(string[] arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(char[] arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(float[] arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(double[] arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Out(params object[] arg){Console.Write(string.Join(' ',arg) + "\n");}
            public static void Yes(){Console.Write("Yes\n");}
            public static void No(){Console.Write("No\n");}
        }
        public static int LowerBound<T>(this IReadOnlyList<T> list, T value, IComparer<T> comparer = null){
            comparer ??= Comparer<T>.Default;
            int left = 0;
            int right = list.Count;
            while (left < right){
                int mid = left + (right - left)/2;
                if (comparer.Compare(list[mid], value) < 0)left = mid + 1;
                else right = mid;
            }
            return left;
        }
        public static int UpperBound<T>(this IReadOnlyList<T> list, T value, IComparer<T> comparer = null){
            comparer ??= Comparer<T>.Default;
            int left = 0;
            int right = list.Count;
            while (left < right){
                int mid = left + (right - left)/2;
                if (comparer.Compare(list[mid], value) <= 0)left = mid + 1;
                else right = mid;
            }
            return left;
        }
        public static T[] Accumulate<T>(this IList<T> list) where T : System.Numerics.INumber<T> {
            for(int i = 0;i < list.Count - 1; i++){
                list[i+1] += list[i];
            }
            return list.ToArray();
        }
        public static T[][] Accumulate<T>(this T[][] arr) where T : System.Numerics.INumber<T> {
            int a = arr.Length;
            int b = arr[0].Length;
            for(int i = 0;i<a-1;i++){
                for(int j = 0;j<b;j++){
                    arr[i+1][j] += arr[i][j];
                }
            }
            for(int i = 0;i<a;i++){
                for(int j = 0;j<b-1;j++){
                    arr[i][j+1] += arr[i][j];
                }
            }
            return arr;
        }

        public static T MakeArray<T>(object defaultVal, params int[] dims){
            if (dims == null || dims.Length < 1)
                throw new ArgumentException("Too few arguments", nameof(dims));
            Type arrayType = typeof(T);
            Type elementType = GetInnermostElementType(arrayType);

            return (T)MakeArrayRecursive(arrayType, dims, 0, elementType, defaultVal);
        }
        private static object MakeArrayRecursive(Type currentType, int[] dims, int index, Type finalElementType, object defaultVal){
            int size = dims[index];

            Array array = Array.CreateInstance(currentType.GetElementType(), size);

            if (index < dims.Length - 1){
                for (int i = 0; i < size; i++)
                    array.SetValue(MakeArrayRecursive(currentType.GetElementType(), dims, index + 1, finalElementType, defaultVal), i);
            }
            else{
                for (int i = 0; i < size; i++)
                    array.SetValue(defaultVal, i);
            }
            return array;
        }
        private static Type GetInnermostElementType(Type type){
            while (type.IsArray){
                type = type.GetElementType();
            }
            return type;
        }
    }
}
