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

            tree.BuildDictionary("this is a test");

            NetHuffman.Coder coder = new NetHuffman.Coder(tree);

            Console.WriteLine(Encoding.UTF8.GetString(coder.Decode(coder.Encode(Encoding.UTF8.GetBytes("this is a test")))));
        }
    }
}
