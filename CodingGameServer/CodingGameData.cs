using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

namespace CodingGameBase;


public class CodingGameData
{
    public byte[] data;

    public DataWriter ToDataWriter()
    {
        DataWriter writer = new DataWriter();
        writer.Put(data);
        return writer;
    }

    public void Decode(DataReader reader)
    {
        data = reader.GetBytes();
    }
}