using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

namespace CodingGameBase;


public class CodingGameData
{
    public string Name;
    public byte[] data;

    public DataWriter ToDataWriter()
    {
        DataWriter writer = new DataWriter();
        writer.Put(Name);
        writer.Put(data);
        return writer;
    }

    public void Decode(DataReader reader)
    {
        Name = reader.GetString();
        data = reader.GetBytes();
    }
}