using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Basic_abcd
    {
        [TestMethod]
        [TestCategory("Basic_abcd")]
        public void Encoding()
        {
            NetHuffman.Coder coder = new NetHuffman.Coder(HuffmanTableDefinition.Basic_abcd);

            byte[] buffer = coder.Encode(System.Text.Encoding.UTF8.GetBytes("abcd"));

            Assert.AreEqual(buffer.Length, 1);
            Assert.AreEqual(buffer[0], 27); // Index 0 should be 27 if encoded correctly
        }

        [TestMethod]
        [TestCategory("Basic_abcd")]
        public void Decoding()
        {
            NetHuffman.Coder coder = new NetHuffman.Coder(HuffmanTableDefinition.Basic_abcd);

            byte[] decoded = new byte[] { 97, 98, 99, 100 };
            byte[] buffer = coder.Decode(new byte[] { 27 });

            Assert.AreEqual(decoded.Length, buffer.Length);
            for (int i = 0; i < buffer.Length; i++)
            {
                Assert.AreEqual(buffer[i], decoded[i]);
            }
        }

        [TestMethod]
        [TestCategory("Basic_abcd")]
        public void EncodingAndDecoding()
        {
            NetHuffman.Coder coder = new NetHuffman.Coder(HuffmanTableDefinition.Basic_abcd);

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("abcd");
            byte[] decoded = coder.Decode(coder.Encode(buffer));

            Assert.AreEqual(buffer.Length, decoded.Length);
            for (int i = 0; i < buffer.Length; i++)
            {
                Assert.AreEqual(buffer[i], decoded[i]);
            }
        }
    }
}