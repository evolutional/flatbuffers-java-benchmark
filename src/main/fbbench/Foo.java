// automatically generated, do not modify

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
  public int prefix() { return bb.get(bb_pos + 10) & 0xFF; }
  public void mutatePrefix(int prefix) { bb.put(bb_pos + 10, (byte)prefix); }
  public int length() { return bb.getInt(bb_pos + 12); }
  public void mutateLength(int length) { bb.putInt(bb_pos + 12, length); }

  public static int createFoo(FlatBufferBuilder builder, long id, short count, int prefix, int length) {
    builder.prep(8, 16);
    builder.putInt(length);
    builder.pad(1);
    builder.putByte((byte)prefix);
    builder.putShort(count);
    builder.putLong(id);
    return builder.offset();
  }
};

