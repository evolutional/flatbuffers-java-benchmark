// automatically generated, do not modify

package fbbench;

import java.nio.*;
import java.lang.*;
import java.util.*;
import com.google.flatbuffers.*;

@SuppressWarnings("unused")
public final class Foo extends Struct {
  public Foo __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public long id() { return bb.getLong(bb_pos + 0); }
  public void mutateId(long id) { bb.putLong(bb_pos + 0, id); }
  public short count() { return bb.getShort(bb_pos + 8); }
  public void mutateCount(short count) { bb.putShort(bb_pos + 8, count); }
  public byte prefix() { return bb.get(bb_pos + 10); }
  public void mutatePrefix(byte prefix) { bb.put(bb_pos + 10, prefix); }
  public long length() { return (long)bb.getInt(bb_pos + 12) & 0xFFFFFFFFL; }
  public void mutateLength(long length) { bb.putInt(bb_pos + 12, (int)length); }

  public static int createFoo(FlatBufferBuilder builder, long id, short count, byte prefix, long length) {
    builder.prep(8, 16);
    builder.putInt((int)(length & 0xFFFFFFFFL));
    builder.pad(1);
    builder.putByte(prefix);
    builder.putShort(count);
    builder.putLong(id);
    return builder.offset();
  }
};

