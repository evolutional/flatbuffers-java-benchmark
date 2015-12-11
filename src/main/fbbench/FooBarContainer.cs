// automatically generated, do not modify

namespace fbbench
{

using FlatBuffers;

public sealed class FooBarContainer : Table {
  public static FooBarContainer GetRootAsFooBarContainer(ByteBuffer _bb) { return GetRootAsFooBarContainer(_bb, new FooBarContainer()); }
  public static FooBarContainer GetRootAsFooBarContainer(ByteBuffer _bb, FooBarContainer obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public FooBarContainer __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public FooBar GetList(int j) { return GetList(new FooBar(), j); }
  public FooBar GetList(FooBar obj, int j) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int ListLength { get { int o = __offset(4); return o != 0 ? __vector_len(o) : 0; } }
  public bool Initialized { get { int o = __offset(6); return o != 0 ? 0!=bb.Get(o + bb_pos) : (bool)false; } }
  public bool MutateInitialized(bool initialized) { int o = __offset(6); if (o != 0) { bb.Put(o + bb_pos, (byte)(initialized ? 1 : 0)); return true; } else { return false; } }
  public AnEnum Fruit { get { int o = __offset(8); return o != 0 ? (AnEnum)bb.GetShort(o + bb_pos) : (AnEnum)0; } }
  public bool MutateFruit(AnEnum fruit) { int o = __offset(8); if (o != 0) { bb.PutShort(o + bb_pos, (short)fruit); return true; } else { return false; } }
  public string Location { get { int o = __offset(10); return o != 0 ? __string(o + bb_pos) : null; } }

  public static Offset<FooBarContainer> CreateFooBarContainer(FlatBufferBuilder builder,
      VectorOffset list = default(VectorOffset),
      bool initialized = false,
      AnEnum fruit = AnEnum.Apples,
      StringOffset location = default(StringOffset)) {
    builder.StartObject(4);
    FooBarContainer.AddLocation(builder, location);
    FooBarContainer.AddList(builder, list);
    FooBarContainer.AddFruit(builder, fruit);
    FooBarContainer.AddInitialized(builder, initialized);
    return FooBarContainer.EndFooBarContainer(builder);
  }

  public static void StartFooBarContainer(FlatBufferBuilder builder) { builder.StartObject(4); }
  public static void AddList(FlatBufferBuilder builder, VectorOffset listOffset) { builder.AddOffset(0, listOffset.Value, 0); }
  public static VectorOffset CreateListVector(FlatBufferBuilder builder, Offset<FooBar>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static void StartListVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddInitialized(FlatBufferBuilder builder, bool initialized) { builder.AddBool(1, initialized, false); }
  public static void AddFruit(FlatBufferBuilder builder, AnEnum fruit) { builder.AddShort(2, (short)(fruit), 0); }
  public static void AddLocation(FlatBufferBuilder builder, StringOffset locationOffset) { builder.AddOffset(3, locationOffset.Value, 0); }
  public static Offset<FooBarContainer> EndFooBarContainer(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<FooBarContainer>(o);
  }
  public static void FinishFooBarContainerBuffer(FlatBufferBuilder builder, Offset<FooBarContainer> offset) { builder.Finish(offset.Value); }
};


}
