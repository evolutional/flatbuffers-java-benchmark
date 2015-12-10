// automatically generated, do not modify

using FlatBuffers;

public sealed class FooBar : Table {
  public static FooBar GetRootAsFooBar(ByteBuffer _bb) { return GetRootAsFooBar(_bb, new FooBar()); }
  public static FooBar GetRootAsFooBar(ByteBuffer _bb, FooBar obj) { return (obj.__init(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public FooBar __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Bar Sibling { get { return GetSibling(new Bar()); } }
  public Bar GetSibling(Bar obj) { int o = __offset(4); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public string Name { get { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; } }
  public double Rating { get { int o = __offset(8); return o != 0 ? bb.GetDouble(o + bb_pos) : (double)0; } }
  public bool MutateRating(double rating) { int o = __offset(8); if (o != 0) { bb.PutDouble(o + bb_pos, rating); return true; } else { return false; } }
  public byte Postfix { get { int o = __offset(10); return o != 0 ? bb.Get(o + bb_pos) : (byte)0; } }
  public bool MutatePostfix(byte postfix) { int o = __offset(10); if (o != 0) { bb.Put(o + bb_pos, postfix); return true; } else { return false; } }

  public static void StartFooBar(FlatBufferBuilder builder) { builder.StartObject(4); }
  public static void AddSibling(FlatBufferBuilder builder, Offset<Bar> siblingOffset) { builder.AddStruct(0, siblingOffset.Value, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(1, nameOffset.Value, 0); }
  public static void AddRating(FlatBufferBuilder builder, double rating) { builder.AddDouble(2, rating, 0); }
  public static void AddPostfix(FlatBufferBuilder builder, byte postfix) { builder.AddByte(3, postfix, 0); }
  public static Offset<FooBar> EndFooBar(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<FooBar>(o);
  }
};

