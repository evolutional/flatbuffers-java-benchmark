// automatically generated, do not modify

using FlatBuffers;

public sealed class Foo : Struct {
  public Foo __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public ulong Id { get { return bb.GetUlong(bb_pos + 0); } }
  public void MutateId(ulong id) { bb.PutUlong(bb_pos + 0, id); }
  public short Count { get { return bb.GetShort(bb_pos + 8); } }
  public void MutateCount(short count) { bb.PutShort(bb_pos + 8, count); }
  public byte Prefix { get { return bb.Get(bb_pos + 10); } }
  public void MutatePrefix(byte prefix) { bb.Put(bb_pos + 10, prefix); }
  public int Length { get { return bb.GetInt(bb_pos + 12); } }
  public void MutateLength(int length) { bb.PutInt(bb_pos + 12, length); }

  public static Offset<Foo> CreateFoo(FlatBufferBuilder builder, ulong Id, short Count, byte Prefix, int Length) {
    builder.Prep(8, 16);
    builder.PutInt(Length);
    builder.Pad(1);
    builder.PutByte(Prefix);
    builder.PutShort(Count);
    builder.PutUlong(Id);
    return new Offset<Foo>(builder.Offset);
  }
};

