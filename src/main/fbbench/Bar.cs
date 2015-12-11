// automatically generated, do not modify

namespace fbbench
{

using FlatBuffers;

public sealed class Bar : Struct {
  public Bar __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Foo Parent { get { return GetParent(new Foo()); } }
  public Foo GetParent(Foo obj) { return obj.__init(bb_pos + 0, bb); }
  public int Time { get { return bb.GetInt(bb_pos + 16); } }
  public void MutateTime(int time) { bb.PutInt(bb_pos + 16, time); }
  public float Ratio { get { return bb.GetFloat(bb_pos + 20); } }
  public void MutateRatio(float ratio) { bb.PutFloat(bb_pos + 20, ratio); }
  public ushort Size { get { return bb.GetUshort(bb_pos + 24); } }
  public void MutateSize(ushort size) { bb.PutUshort(bb_pos + 24, size); }

  public static Offset<Bar> CreateBar(FlatBufferBuilder builder, ulong parent_Id, short parent_Count, sbyte parent_Prefix, uint parent_Length, int Time, float Ratio, ushort Size) {
    builder.Prep(8, 32);
    builder.Pad(6);
    builder.PutUshort(Size);
    builder.PutFloat(Ratio);
    builder.PutInt(Time);
    builder.Prep(8, 16);
    builder.PutUint(parent_Length);
    builder.Pad(1);
    builder.PutSbyte(parent_Prefix);
    builder.PutShort(parent_Count);
    builder.PutUlong(parent_Id);
    return new Offset<Bar>(builder.Offset);
  }
};


}
