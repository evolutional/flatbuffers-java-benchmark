// automatically generated, do not modify

package fbbench;

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
  public int size() { return bb.getShort(bb_pos + 24) & 0xFFFF; }
  public void mutateSize(int size) { bb.putShort(bb_pos + 24, (short)size); }

  public static int createBar(FlatBufferBuilder builder, long parent_id, short parent_count, byte parent_prefix, long parent_length, int time, float ratio, int size) {
    builder.prep(8, 32);
    builder.pad(6);
    builder.putShort((short)(size & 0xFFFF));
    builder.putFloat(ratio);
    builder.putInt(time);
    builder.prep(8, 16);
    builder.putInt((int)(parent_length & 0xFFFFFFFFL));
    builder.pad(1);
    builder.putByte(parent_prefix);
    builder.putShort(parent_count);
    builder.putLong(parent_id);
    return builder.offset();
  }
};

