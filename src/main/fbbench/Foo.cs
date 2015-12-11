// automatically generated, do not modify

namespace fbbench
{

using FlatBuffers;

public sealed class Foo : Struct {
  public Foo __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public ulong Id { get { return bb.GetUlong(bb_pos + 0); } }
  public void MutateId(ulong id) { bb.PutUlong(bb_pos + 0, id); }
  public short Count { get { return bb.GetShort(bb_pos + 8); } }
  public void MutateCount(short count) { bb.PutShort(bb_pos + 8, count); }
  public sbyte Prefix { get { return bb.GetSbyte(bb_pos + 10); } }
  public void MutatePrefix(sbyte prefix) { bb.PutSbyte(bb_pos + 10, prefix); }
  public uint Length { get { return bb.GetUint(bb_pos + 12); } }
  public void MutateLength(uint length) { bb.PutUint(bb_pos + 12, length); }

  public static Offset<Foo> CreateFoo(FlatBufferBuilder builder, ulong Id, short Count, sbyte Prefix, uint Length) {
    builder.Prep(8, 16);
    builder.PutUint(Length);
    builder.Pad(1);
    builder.PutSbyte(Prefix);
    builder.PutShort(Count);
    builder.PutUlong(Id);
    return new Offset<Foo>(builder.Offset);
  }
};


}
