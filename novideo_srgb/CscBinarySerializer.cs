using System.IO;
using static novideo_srgb.Novideo;

internal static class CscBinarySerializer
{
    public static void Save(string path, CscBinaryModel model)
    {
        var fs = File.Create(path);
        var bw = new BinaryWriter(fs);

        bw.Write(model.ContentColorSpace);
        bw.Write(model.MonitorColorSpace);

        bw.Write(model.UseMatrix1);
        if (model.UseMatrix1)
        {
            for (int i = 0; i < 12; i++)
                bw.Write(model.Matrix1[i]);
        }

        bw.Write(model.UseMatrix2);
        if (model.UseMatrix2)
        {
            for (int i = 0; i < 12; i++)
                bw.Write(model.Matrix2[i]);
        }
    }

    public static CscBinaryModel Load(string path)
    {
        var fs = File.OpenRead(path);
        var br = new BinaryReader(fs);

        var model = new CscBinaryModel
        {
            ContentColorSpace = br.ReadUInt32(),
            MonitorColorSpace = br.ReadUInt32(),
            UseMatrix1 = br.ReadBoolean()
        };

        if (model.UseMatrix1)
        {
            model.Matrix1 = new float[12];
            for (int i = 0; i < 12; i++)
                model.Matrix1[i] = br.ReadSingle();
        }

        model.UseMatrix2 = br.ReadBoolean();
        if (model.UseMatrix2)
        {
            model.Matrix2 = new float[12];
            for (int i = 0; i < 12; i++)
                model.Matrix2[i] = br.ReadSingle();
        }

        return model;
    }
}
