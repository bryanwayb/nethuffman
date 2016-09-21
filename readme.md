#NetHuffman Compressor

**Provides methods for fast encoding and decoding huffman based data.**

I wrote the compression and decompression logic with speed in mind. Instead of doing traditional bit by bit reading and writing this is using hash lookups with performance optimizations littered throughout.

I originally wrote this for use in the HPACK format specified for use in HTTP/2, but it's capable of uses elsewhere.

##Examples
For an example on usage, see `Testing/Program.cs`.

##TODO
- Add more use case tests, current status is lacking...
- `NetHuffman.Tree` is only stable for use when manually providing a table lookup (assuming that it's valid as well). The `BuildDictionary` methods exist, but they aren't complete for actual use beyond a small lookup table size (<12 unique bytes). This is a known problem, and should be fixed.