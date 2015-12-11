﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FlatBuffers;

namespace FlatBench
{
    public struct BenchmarkResult
    {
        public string Name { get; set; }
        public int Iterations { get; set; }
        public double[] Measurements { get; set; }

        public double Sum
        {
            get { return Iterations <= 0 ? Double.NaN : Measurements.Sum(); }
        }

        public double Mean
        {
            get { return Iterations <= 0 ? Double.NaN  : Sum / Iterations; }
        }

        public double Variance
        {
            get
            {
                if (Iterations <= 0)
                {
                    return Double.NaN;
                }
                var mean = Mean;
                var variance = Measurements.Sum(i => Math.Pow(i - mean, 2));
                return variance/(Iterations - 1);
            }
        }

        public double StandardDeviation
        {
            get { return Math.Sqrt(Variance); }
        }
    }

    public class BenchmarkRunner
    {
        private FlatBench _flatBench = new FlatBench();
        private ByteBuffer _buffer = new ByteBuffer(new byte[1024]);
        public int Position { get; set; }

        public BenchmarkRunner()
        {
            _flatBench.Encode(_buffer);
            Position = _buffer.Position;
        }

        private BenchmarkResult Measure(string name, Action action, int warmupIterations, int measurements)
        {
            const double TicksPerMicrosecond = 10.0;

            var result = new BenchmarkResult
            {
                Name = name,
                Iterations = measurements,
                Measurements = new double[measurements]
            };

            // Pre collect and finalize
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Do warmup for test
            for (var i = 0; i < warmupIterations; ++i)
            {
                action();
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var sw = new Stopwatch();
            for (var i = 0; i < measurements; ++i)
            {
                sw.Reset();
                sw.Start();

                action();

                sw.Stop();

                result.Measurements[i] = sw.ElapsedTicks / TicksPerMicrosecond; // http://stackoverflow.com/questions/1206367/c-sharp-time-in-microseconds
            }

            return result;
        }

        public void Run(int warmupIterations, int measurements)
        {
            var encode = Measure("Encode", () => _flatBench.Encode(_buffer), warmupIterations, measurements);
            var decode = Measure("Decode", () => _flatBench.Decode(_buffer), warmupIterations, measurements);
            var use = Measure("Traverse", () =>
            {
                _buffer.Position = Position;
                _flatBench.Use(_buffer);
            }, warmupIterations, measurements);

            Console.WriteLine("{0,-25}\t{1, 10} {2, 6}  Unit", "Name", "Mean", "StdD");
            Console.WriteLine("{0, -25}\t{1, 10:0.000} {2,6:0.000} us/op", encode.Name, encode.Mean, encode.StandardDeviation);
            Console.WriteLine("{0, -25}\t{1, 10:0.000} {2,6:0.000} us/op", decode.Name, decode.Mean, decode.StandardDeviation);
            Console.WriteLine("{0, -25}\t{1, 10:0.000} {2,6:0.000} us/op", use.Name, use.Mean, use.StandardDeviation);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var runner = new BenchmarkRunner();
            runner.Run(10, 50);

            Console.WriteLine("\nPress a key...");
            Console.ReadKey();
        }
    }
}
