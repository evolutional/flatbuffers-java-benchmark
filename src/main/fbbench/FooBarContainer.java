// automatically generated, do not modify

package fbbench;

import java.nio.*;
import java.lang.*;
import java.util.*;
import com.google.flatbuffers.*;

@SuppressWarnings("unused")
public final class FooBarContainer extends Table {
  public static FooBarContainer getRootAsFooBarContainer(ByteBuffer _bb) { return getRootAsFooBarContainer(_bb, new FooBarContainer()); }
  public static FooBarContainer getRootAsFooBarContainer(ByteBuffer _bb, FooBarContainer obj) { _bb.order(ByteOrder.LITTLE_ENDIAN); return (obj.__init(_bb.getInt(_bb.position()) + _bb.position(), _bb)); }
  public FooBarContainer __init(int _i, ByteBuffer _bb) { bb_pos = _i; bb = _bb; return this; }

  public FooBar list(int j) { return list(new FooBar(), j); }
  public FooBar list(FooBar obj, int j) { int o = __offset(4); return o != 0 ? obj.__init(__indirect(__vector(o) + j * 4), bb) : null; }
  public int listLength() { int o = __offset(4); return o != 0 ? __vector_len(o) : 0; }
  public boolean initialized() { int o = __offset(6); return o != 0 ? 0!=bb.get(o + bb_pos) : false; }
  public boolean mutateInitialized(boolean initialized) { int o = __offset(6); if (o != 0) { bb.put(o + bb_pos, (byte)(initialized ? 1 : 0)); return true; } else { return false; } }
  public short fruit() { int o = __offset(8); return o != 0 ? bb.getShort(o + bb_pos) : 0; }
  public boolean mutateFruit(short fruit) { int o = __offset(8); if (o != 0) { bb.putShort(o + bb_pos, fruit); return true; } else { return false; } }
  public String location() { int o = __offset(10); return o != 0 ? __string(o + bb_pos) : null; }
  public ByteBuffer locationAsByteBuffer() { return __vector_as_bytebuffer(10, 1); }

  public static int createFooBarContainer(FlatBufferBuilder builder,
      int list,
      boolean initialized,
      short fruit,
      int location) {
    builder.startObject(4);
    FooBarContainer.addLocation(builder, location);
    FooBarContainer.addList(builder, list);
    FooBarContainer.addFruit(builder, fruit);
    FooBarContainer.addInitialized(builder, initialized);
    return FooBarContainer.endFooBarContainer(builder);
  }

  public static void startFooBarContainer(FlatBufferBuilder builder) { builder.startObject(4); }
  public static void addList(FlatBufferBuilder builder, int listOffset) { builder.addOffset(0, listOffset, 0); }
  public static int createListVector(FlatBufferBuilder builder, int[] data) { builder.startVector(4, data.length, 4); for (int i = data.length - 1; i >= 0; i--) builder.addOffset(data[i]); return builder.endVector(); }
  public static void startListVector(FlatBufferBuilder builder, int numElems) { builder.startVector(4, numElems, 4); }
  public static void addInitialized(FlatBufferBuilder builder, boolean initialized) { builder.addBoolean(1, initialized, false); }
  public static void addFruit(FlatBufferBuilder builder, short fruit) { builder.addShort(2, fruit, 0); }
  public static void addLocation(FlatBufferBuilder builder, int locationOffset) { builder.addOffset(3, locationOffset, 0); }
  public static int endFooBarContainer(FlatBufferBuilder builder) {
    int o = builder.endObject();
    return o;
  }
  public static void finishFooBarContainerBuffer(FlatBufferBuilder builder, int offset) { builder.finish(offset); }
};

