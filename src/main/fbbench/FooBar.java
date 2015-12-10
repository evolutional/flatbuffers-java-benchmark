// automatically generated, do not modify

import java.nio.*;
import java.lang.*;
import java.util.*;
import com.google.flatbuffers.*;

@SuppressWarnings("unused")
public final class FooBar extends Table {
  public static FooBar getRootAsFooBar(ByteBuffer _bb) { return getRootAsFooBar(_bb, new FooBar()); }
  public static FooBar getRootAsFooBar(ByteBuffer _bb, FooBar obj) { _bb.order(ByteOrder.LITTLE_ENDIAN); return (obj.__init(_bb.getInt(_bb.position()) + _bb.position(), _bb)); }
  public FooBar __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public Bar sibling() { return sibling(new Bar()); }
  public Bar sibling(Bar obj) { int o = __offset(4); return o != 0 ? obj.__init(o + bb_pos, bb) : null; }
  public String name() { int o = __offset(6); return o != 0 ? __string(o + bb_pos) : null; }
  public ByteBuffer nameAsByteBuffer() { return __vector_as_bytebuffer(6, 1); }
  public double rating() { int o = __offset(8); return o != 0 ? bb.getDouble(o + bb_pos) : 0; }
  public boolean mutateRating(double rating) { int o = __offset(8); if (o != 0) { bb.putDouble(o + bb_pos, rating); return true; } else { return false; } }
  public int postfix() { int o = __offset(10); return o != 0 ? bb.get(o + bb_pos) & 0xFF : 0; }
  public boolean mutatePostfix(int postfix) { int o = __offset(10); if (o != 0) { bb.put(o + bb_pos, (byte)postfix); return true; } else { return false; } }

  public static void startFooBar(FlatBufferBuilder builder) { builder.startObject(4); }
  public static void addSibling(FlatBufferBuilder builder, int siblingOffset) { builder.addStruct(0, siblingOffset, 0); }
  public static void addName(FlatBufferBuilder builder, int nameOffset) { builder.addOffset(1, nameOffset, 0); }
  public static void addRating(FlatBufferBuilder builder, double rating) { builder.addDouble(2, rating, 0); }
  public static void addPostfix(FlatBufferBuilder builder, int postfix) { builder.addByte(3, (byte)postfix, 0); }
  public static int endFooBar(FlatBufferBuilder builder) {
    int o = builder.endObject();
    return o;
  }
};

