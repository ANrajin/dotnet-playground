using System.Text;

var path = new DirectoryInfo(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent;
var file = Path.Combine(path.FullName, "Test.txt");

using (BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open, FileAccess.ReadWrite)))
{
    var len = br.BaseStream.Length - 1;

    for(var i = 0; i <= len/2; i++)
    {
        br.BaseStream.Position = i;
        var left = br.ReadByte();

        br.BaseStream.Position = len - i;
        var right = br.ReadByte();

        br.BaseStream.Position = 1;
        br.BaseStream.WriteByte(right);

        br.BaseStream.Position = len - i;
        br.BaseStream.WriteByte(left);
    }
}
