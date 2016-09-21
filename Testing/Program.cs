using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            NetHuffman.Tree tree = new NetHuffman.Tree();

            tree.BuildDictionary("this is just a test");

            NetHuffman.Coder coder = new NetHuffman.Coder(tree);

            byte[] decoded = Encoding.UTF8.GetBytes("this is just a test");

            Console.WriteLine(decoded.Length);

            byte[] encoded;
            uint size = coder.Encode(decoded, out encoded);

            Console.WriteLine(encoded.Length);

            coder.Decode(encoded, out decoded, size);

            Console.WriteLine(Encoding.UTF8.GetString(decoded));
        }
    }
}
