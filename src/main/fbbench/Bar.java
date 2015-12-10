// automatically generated, do not modify

import java.nio.*;
import java.lang.*;
import java.util.*;
import com.google.flatbuffers.*;

@SuppressWarnings("unused")
public final class Bar extends Struct {
  public Bar __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Foo parent() { return parent(new Foo()); }
  public Foo parent(Foo obj) { return obj.__init(bb_pos + 0, bb); }
  public int time() { return bb.getInt(bb_pos + 16); }
  public void mutateTime(int time) { bb.putInt(bb_pos + 16, time); }
  public float ratio() { return bb.getFloat(bb_pos + 20); }
  public void mutateRatio(float ratio) { bb.putFloat(bb_pos + 20, ratio); }
  public short size() { return bb.getShort(bb_pos + 24); }
  public void mutateSize(short size) { bb.putShort(bb_pos + 24, size); }

  public static int createBar(FlatBufferBuilder builder, long parent_id, short parent_count, int parent_prefix, int parent_length, int time, float ratio, short size) {
    builder.prep(8, 32);
    builder.pad(6);
    builder.putShort(size);
    builder.putFloat(ratio);
    builder.putInt(time);
    builder.prep(8, 16);
    builder.putInt(parent_length);
    builder.pad(1);
    builder.putByte((byte)parent_prefix);
    builder.putShort(parent_count);
    builder.putLong(parent_id);
    return builder.offset();
  }
};

