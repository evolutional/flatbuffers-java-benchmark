using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FlatBuffers;

namespace FlatBench
{
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

        public void Run()
        {
            var sw = new Stopwatch();
            var encodeMeasurements = new TimeSpan[20];
            var decodeMeasurements = new TimeSpan[20];
            var useMeasurements = new TimeSpan[20];

            for (var i = 0; i < encodeMeasurements.Length; ++i)
            {
                sw.Reset();
                sw.Start();

                _flatBench.Encode(_buffer);
                sw.Stop();
                encodeMeasurements[i] = sw.Elapsed;
            }

            for (var i = 0; i < decodeMeasurements.Length; ++i)
            {
                sw.Reset();
                sw.Start();

                _flatBench.Decode(_buffer);
                sw.Stop();
                decodeMeasurements[i] = sw.Elapsed;
            }

            for (var i = 0; i < useMeasurements.Length; ++i)
            {
                sw.Reset();
                sw.Start();

                _buffer.Position = Position;
                _flatBench.Use(_buffer);
                sw.Stop();
                useMeasurements[i] = sw.Elapsed;
            }
        }

    }

    class Program
    {

        static void Main(string[] args)
        {
            var runner = new BenchmarkRunner();
            runner.Run();
        }
    }
}
