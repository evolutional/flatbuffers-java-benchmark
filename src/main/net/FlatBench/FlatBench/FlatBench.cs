using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlatBuffers;
using Enum = System.Enum;


namespace FlatBench
{
    public class FlatBench
    {
        private FooBar _fooBar = new FooBar();
        private Bar _bar = new Bar();
        private Foo _foo = new Foo();

        private const int _vecLen = 3;
        private const string _location = "http://google.com/flatbuffers/";
        private const bool _initialized = true;
        private const short _anEnum = (short) AnEnum.Bananas;


        public int Encode(ByteBuffer buffer)
        {
            var builder = new FlatBufferBuilder(buffer);

            var fooBars = new Offset<FooBar>[_vecLen];
            for (var i = 0; i < _vecLen; i++)
            {

                var name = builder.CreateString("Hello, World!");

                FooBar.StartFooBar(builder);
                FooBar.AddName(builder, name);
                FooBar.AddRating(builder, 3.1415432432445543543 + i);
                FooBar.AddPostfix(builder, (byte)('!' + i));

                var bar = Bar.CreateBar(builder,
                    // Foo fields (nested struct)
                        0xABADCAFEABADCAFEL + (ulong)i,
                        (short)(10000 + i),
                        (byte)('@' + i),
                        1000000 + i,
                    // Bar fields
                        123456 + i,
                        3.14159f + i,
                        (short)(10000 + i));

                FooBar.AddSibling(builder, bar);
                var fooBar = FooBar.EndFooBar(builder);
                fooBars[i] = fooBar;
            }

            var list = FooBarContainer.CreateListVector(builder, fooBars);
            var loc = builder.CreateString(_location);

            FooBarContainer.StartFooBarContainer(builder);
            FooBarContainer.AddLocation(builder, loc);
            FooBarContainer.AddInitialized(builder, _initialized);
            FooBarContainer.AddFruit(builder, _anEnum);
            FooBarContainer.AddLocation(builder, loc);
            FooBarContainer.AddList(builder, list);
            var fooBarContainer = FooBarContainer.EndFooBarContainer(builder);
            builder.Finish(fooBarContainer.Value);

            return buffer.Position;
        }

        public FooBarContainer Decode(ByteBuffer buffer)
        {
            return FooBarContainer.GetRootAsFooBarContainer(buffer);
        }

        public long Use(ByteBuffer buffer)
        {
            // The root object really should be reusable
            var fooBarContainer = FooBarContainer.GetRootAsFooBarContainer(buffer);

            long sum = 0;
            sum += fooBarContainer.Initialized ? 1 : 0;
            sum += fooBarContainer.Location.Length;
            sum += fooBarContainer.Fruit;

            var length = fooBarContainer.ListLength;
            for (var i = 0; i < length; i++)
            {

                fooBarContainer.GetList(_fooBar, i);
                sum += _fooBar.Name.Length;
                sum += _fooBar.Postfix;
                sum += (long)_fooBar.Rating;

                _fooBar.GetSibling(_bar);
                sum += _bar.Size;
                sum += _bar.Time;

                _bar.GetParent(_foo);
                sum += _foo.Count;
                sum += (long)_foo.Id;
                sum += _foo.Length;
                sum += _foo.Prefix;
            }

            return sum;

        }
    }
}
